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
    public class DashboardHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>();
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
                            Groups.Add(Context.ConnectionId, "JudgeGroupTeam" + team.ID);

                        break;
                    case UserRole.Participant:
                        IQueryable<Student> stuDB = db.Students.Where(c => c.User.ID == usr.ID);

                        Team teamAssigned = stuDB.Select(c => c.TeamAssigned).First();
                        Groups.Add(Context.ConnectionId, "Participants");
                        Groups.Add(Context.ConnectionId, "Team" + teamAssigned.ID);

                        break;
                }

            }
            // Do nothing otherwise
        }

        // Used to request the current stats list
        public void PollInitialData(UserDTO usr)
        {
            IQueryable<User> userDB = db.User.Where(c => c.Username == usr.Username && c.Password == usr.Password);

            if (userDB != null && userDB.Count() == 1)
            {
                User userFromDB = userDB.First();
                switch (userFromDB.Role)
                {
                    // Retrieve project list with Judge Instructions
                    case UserRole.Admin:
                        Clients.Caller.updateNumberOfProjects(NumberOfProjects());
                        Clients.Caller.updateNumberOfJudges(NumberOfJudges());
                        Clients.Caller.updateNumberOfStudents(NumberOfStudents());
                        Clients.Caller.updateNumberOfTeams(NumberOfTeams());
                        Clients.Caller.updateNumberOfSubmissionsAwaitingGrades(NumberOfSubmissionsAwaitingGrades());
                        break;
                    case UserRole.Judge:
                        Clients.Caller.updateNumberOfSubmissionsAwaitingGrades(NumberOfSubmissionsAwaitingGrades());
                        break;

                    case UserRole.Participant:
                        Clients.Caller.updateNumberOfProjectsAwaitingSubmissions(NumberOfProjectsAwaitingSubmissions(usr));
                        break;
                }
            }
        }

        private int NumberOfProjects()
        {
            IQueryable<Project> projectDB = db.Projects;
            return projectDB.Select(c => c.ID).ToList().Count;
        }

        private int NumberOfProjectsNotHidden()
        {
            IQueryable<Project> projectDB = db.Projects;
            return projectDB.Where(c=>c.Hidden==false).Select(c => c.ID).ToList().Count;
        }

        private int NumberOfProjectsByTeam(Team t)
        {
            IQueryable<Project> projectDB = db.Projects;
            return projectDB.Where(c => c.Hidden == false && c.Level == t.lvl)
                .Select(c => c.ID).ToList().Count;
        }

        private int NumberOfJudges()
        {
            IQueryable<User> userDB = db.User.Where(c=>c.Role == UserRole.Judge);
            return userDB.Select(c => c.ID).ToList().Count;
        }

        private int NumberOfStudents()
        {
            IQueryable<User> userDB = db.User.Where(c => c.Role == UserRole.Participant);
            return userDB.Select(c => c.ID).ToList().Count;
        }

        private int NumberOfTeams()
        {
            IQueryable<Team> teamDB = db.Teams;
            return teamDB.Select(c => c.ID).ToList().Count;
        }

        private int NumberOfSubmissionsAwaitingGrades()
        {
            IQueryable<TeamSubmission> submitDB = db.TeamSubmission.Where(c=>c.Score == -1);
            return submitDB.Select(c => c.ID).Count();
        }

        // TODO: 
        private int NumberOfProjectsAwaitingSubmissions(UserDTO usr)
        {
            IQueryable<Student> stuDB = db.Students.Where(c => c.User.ID == usr.ID);
            Team teamAssigned = stuDB.Select(c=>c.TeamAssigned).First();

            IQueryable<TeamSubmission> submitDB = db.TeamSubmission.Where(c=>c.Team.ID == teamAssigned.ID);
            int numSubmissions = submitDB.Select(c => c.ID).Count();
            return NumberOfProjectsByTeam(teamAssigned) - numSubmissions;
        }

        public void BroadcastNumberOfProjects(UserDTO usr)
        {
            if( IsAdmin(usr) )
            {
                hubContext.Clients.Group("Admins").updateNumberOfProjects(NumberOfProjects());
            }
        }

        public void BroadcastNumberOfJudges(UserDTO usr)
        {
            if (IsAdmin(usr))
            {
                hubContext.Clients.Group("Admins").updateNumberOfJudges(NumberOfJudges());
            }
        }

        public void BroadcastNumberOfStudents(UserDTO usr)
        {
            if (IsAdmin(usr))
            {
                hubContext.Clients.Group("Admins").updateNumberOfStudents(NumberOfStudents());
            }
        }

        public void BroadcastNumberOfTeams(UserDTO usr)
        {
            if (IsAdmin(usr))
            {
                hubContext.Clients.Group("Admins").updateNumberOfTeams(NumberOfTeams());
            }
        }

        public void BroadcastSubmissionsAwaitingGrade(UserDTO usr)
        {
            hubContext.Clients.Group("Admins").updateSubmissionsAwaitingGrade(NumberOfSubmissionsAwaitingGrades());
            // TODO Update Judge's list of outstanding submissions
        }

        public void BroadcastProjectsAwaitingSubmissions(UserDTO usr)
        {
            if (IsParticipant(usr))
            {
                int numSubmissionsLeft = NumberOfProjectsAwaitingSubmissions(usr);
                IQueryable<Student> stuDB = db.Students.Where(c => c.User.ID == usr.ID);
                Team teamAssigned = stuDB.Select(c => c.TeamAssigned).First();
                hubContext.Clients.Group("Team"+teamAssigned.ID).updateProjectsAwaitingSubmissions(numSubmissionsLeft);
            }
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

        private bool IsParticipant(UserDTO usr)
        {
            IQueryable<User> userDB = db.User.Where(c => c.Username == usr.Username && c.Password == usr.Password);

            if (userDB != null && userDB.Count() == 1)
            {
                User userFromDB = userDB.First();
                if (userFromDB.Role == UserRole.Participant)
                {
                    return true;
                }
            }
            return false;
        }

    }
}