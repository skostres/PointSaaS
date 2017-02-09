using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using PCMC.DTO;
using PCMC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PCMC.Client.Hubs
{
    public class ProjectsHub : Hub
    {
        private static DashboardHub dashHub = new DashboardHub();
        private static IHubContext hubContextDash = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>();
        protected ModelADO db = new ModelADO();
        
        /*
         * This hub action will serve to identify a connection and assign it to groups.
         * Groups are used to inform authorized users of certain data changes, such as project submissions and grade assignments.
         */
        public void Subscribe(UserDTO usr)
        {
            IQueryable<User> userDB = db.User.Where(c => c.Username == usr.Username && c.Password == usr.Password);

            if (userDB != null && userDB.Count() == 1)
            {
                User userFromDB = userDB.First();
                switch (userFromDB.Role)
                {
                    case UserRole.Admin:
                        // Admin's hear all
                        Groups.Add(Context.ConnectionId, "Admins");
                        break;
                    case UserRole.Judge:

                        IQueryable<JudgeTeamMap> teamDB = db.JudgeTeamMap.Where(c => c.Judge.ID == userFromDB.ID);
                        ICollection<Team> teamAssignments = teamDB.Select(c => c.Team).Cast<Team>().ToList();
                        Groups.Add(Context.ConnectionId, "Judges");

                        // Map this Judge to their respective team assignments
                        foreach (var team in teamAssignments)
                            Groups.Add(Context.ConnectionId, "JudgeGroupTeam"+ team.ID);

                        break;
                    case UserRole.Participant:
                        IQueryable<Student> stuDB = db.Students.Where(c => c.User.ID == usr.ID);

                        Team teamAssigned = stuDB.Select(c => c.TeamAssigned).Cast<Team>().First();
                        Groups.Add(Context.ConnectionId, "Participants");
                        Groups.Add(Context.ConnectionId, "Participants-" + teamAssigned.lvl.ToString());
                        Groups.Add(Context.ConnectionId, "Team" + teamAssigned.ID);

                        break;
                } 
                
            }
            // Do nothing otherwise
        }


        // Used to request the current project list
        public void PollProjectList(UserDTO usr)
        {
            IQueryable<User> userDB = db.User.Where(c => c.Username == usr.Username && c.Password == usr.Password);

            if (userDB != null && userDB.Count() == 1)
            {
                User userFromDB = userDB.First();
                IQueryable<Project> projectDB = db.Projects;
                List<Project> list = projectDB.ToList();
                List<ProjectDTO> projList = new List<ProjectDTO>();
                foreach (Project proj in list)
                    projList.Add(new ProjectDTO(proj));

                switch (userFromDB.Role)
                {
                    // Retrieve project list with Judge Instructions
                    case UserRole.Admin:
                    case UserRole.Judge:
                        Clients.Caller.projectListPoll(projList);
                        break;

                    case UserRole.Participant:
                        Team assigned = db.Students.Select(c => c.TeamAssigned).First();
                        Clients.Caller.projectListPoll(filterProjectListByVisibility(projList, assigned));
                        break;
                }
            }
        }

        /*
         * Excludes all projects in provided list that are in a hidden state.
         * Also hides RawZipFileJudges data
         * Method will be used when communicating with Participants
         */
        private List<ProjectDTO> filterProjectListByVisibility(List<ProjectDTO> projList, Team assigned)
        {
            
            var filteredProjList = new List<ProjectDTO>();
            foreach (ProjectDTO p in projList)
            {
                if (!p.Hidden && assigned.lvl == p.Level)
                {
                    p.RawZipFileJudges = ""; // Hide RawZipFileJudges from Participants
                    filteredProjList.Add(p);
                }
            }
            return filteredProjList;
        }

        private void BroadcastProjectListUpdate()
        {
            IQueryable<Project> projectDB = db.Projects;
            List<Project> list = projectDB.ToList();
            List<ProjectDTO> projList = new List<ProjectDTO>();
            foreach (Project proj in list)
                projList.Add(new ProjectDTO(proj));

            Clients.Groups(new string[] { "Admins", "Judges" }).projectListPoll(projList);
            Clients.Group("Participants-"+Level.ADVANCED.ToString()).projectListPoll(filterProjectListByVisibility(projList, new Team { lvl=Level.ADVANCED}));
            Clients.Group("Participants-" + Level.INTRODUCTION.ToString()).projectListPoll(filterProjectListByVisibility(projList, new Team { lvl = Level.INTRODUCTION }));
            hubContextDash.Clients.All.triggerUpdate();
        }

        private bool IsAdmin(UserDTO usr)
        {
            IQueryable<User> userDB = db.User.Where(c => c.Username == usr.Username && c.Password == usr.Password);

            if (userDB != null && userDB.Count() == 1)
            {
                User userFromDB = userDB.First();
                if (userFromDB.Role == UserRole.Admin)
                {
                    return true;
                }
            }
            return false;
        }

        private void BroadcastNotification (ProjectDTO proj, string msg, MsgTypeDTO type)
        {
            Clients.All.notifyChange(proj, msg, type);
        }

        public void DeleteProject(UserDTO usr, ProjectDTO proj)
        {
            if (IsAdmin(usr))
            { 
                // User is authorized to perform change
                var original = db.Projects.Remove(db.Projects.Find(proj.ID));
                db.SaveChanges();
                PollProjectList(usr);
                Clients.Caller.notifyChange(proj, "The project has been deleted!", MsgTypeDTO.SUCCESS);
                BroadcastNotification(proj, "The project \"" + proj.Name + "\" was deleted", MsgTypeDTO.WARNING);
                BroadcastProjectListUpdate();

                dashHub.BroadcastNumberOfProjects(usr);
            }
        }

        public void AddProject(UserDTO usr, ProjectDTO proj)
        {
            if (IsAdmin(usr))
            {
                Project newProject = proj.toProjectType();
                newProject.ID = 0;
                // User is authorized to perform change
                var original = db.Projects.Add(newProject);
                db.SaveChanges();
                Clients.Caller.notifyChange(proj, "The project has been added!", MsgTypeDTO.SUCCESS);
                BroadcastNotification(proj, "A project was added with name: "+proj.Name, MsgTypeDTO.INFORMATION);
                BroadcastProjectListUpdate();

                dashHub.BroadcastNumberOfProjects(usr);
            }
        }
        public void UpdateProject(UserDTO usr, ProjectDTO proj)
        {
            if (IsAdmin(usr))
            {
                // User is authorized to perform change
                var original = db.Projects.Find(proj.ID);
                if (original != null)
                {
                    db.Entry(original).CurrentValues.SetValues(proj.toProjectType());
                    db.SaveChanges();
                    BroadcastNotification(proj, "The project : \"" + proj.Name + "\" was modified.", MsgTypeDTO.INFORMATION);
                    BroadcastProjectListUpdate();
                }
            }
        }

        public override Task OnConnected()
        {
            // Process Credential information to determine role
            //Clients.All.numberOfUsers(numberOfUsers);

            return (base.OnConnected());
        }

        public override Task OnDisconnected(bool flag)
        {
            //Clients.All.numberOfUsers(numberOfUsers);
            return (base.OnDisconnected(flag));
        }

    }
}