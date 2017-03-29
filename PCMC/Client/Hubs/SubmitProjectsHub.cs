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
    public class SubmitProjectsHub : Hub
    {
        private static DashboardHub dashHub = new DashboardHub();
        private static ProjectsHub projHub = new ProjectsHub();
        private static IHubContext hubContextDash = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>();
        private static IHubContext hubContextProj = GlobalHost.ConnectionManager.GetHubContext<ProjectsHub>();
        private static IHubContext hubContextGrades = GlobalHost.ConnectionManager.GetHubContext<GradeSubmissionsHub>();
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
                    case UserRole.Judge:
                        //
                        break;
                    case UserRole.Participant:
                        IQueryable<Student> stuDB = db.Students.Where(c => c.User.ID == usr.ID);

                        Team teamAssigned = stuDB.Select(c => c.TeamAssigned).Cast<Team>().First();
                        Groups.Add(Context.ConnectionId, "Team" + teamAssigned.ID);
                        //Clients.Caller.teamSubmissions(getTeamSubmissions(usr));
                        break;
                }

            }
            // Do nothing otherwise
        }

        public void pollTeamSubmissions(UserDTO usr)
        {
            Clients.Caller.submissionTeamPoll(getTeamSubmissions(usr));
        }

        private List<TeamSubmissionDTO> getTeamSubmissions(UserDTO usr)
        {
            List<TeamSubmissionDTO> teamSubmissions = new List<TeamSubmissionDTO>();
            User usrDB = GetUser(usr);
            if (usrDB != null)
            {
                // Determine team assignment, do not trust TeamSubmissionDTO
                Team teamAssigned = db.Students.Where(c => c.User.ID == usrDB.ID).Select(c => c.TeamAssigned).First();
                // Determine if no project exists, if so add it otherwise update it.
                List<TeamSubmission> tSub = db.TeamSubmission.Include("Project").Where(c => c.Team.ID == teamAssigned.ID).ToList();
                foreach(TeamSubmission sub in tSub)
                {
                    teamSubmissions.Add(new TeamSubmissionDTO(sub));
                }
                return teamSubmissions;
            }
            return teamSubmissions;
        }

        public void addSubmission(UserDTO usr, TeamSubmissionDTO submission)
        {
            User usrDB = GetUser(usr);
            if (usrDB != null)
            {
                // Determine team assignment, do not trust TeamSubmissionDTO
                Team teamAssigned = db.Students.Where(c=>c.User.ID == usrDB.ID).Select(c=>c.TeamAssigned).First();
                // Determine if no project exists, if so add it otherwise update it.
                IQueryable<TeamSubmission> tSub = db.TeamSubmission.Where(c => c.Team.ID == teamAssigned.ID && c.Project.ID == submission.Project.ID);
                TeamSubmission sub = null;

                if (tSub.Count() == 0)
                {
                    sub = new TeamSubmission() { Project = db.Projects.Where(c => c.ID == submission.Project.ID).First(), RawZipSolution = Convert.FromBase64String(submission.RawZipSolution), Score = -1, Team = teamAssigned, GraderComment="" };
                    db.TeamSubmission.Add(sub);
                    db.SaveChanges();
                } else
                {
                    int id = tSub.First().ID;
                    sub = db.TeamSubmission.Include("Team").Include("Project").Where(c=>c.ID == id).First();
                    sub.RawZipSolution = Convert.FromBase64String(submission.RawZipSolution);
                    sub.Score = -1;
                    sub.GraderComment = "";
                    db.SaveChanges();
                    //Alert Team members of submission..
                }
                BroadcastTeamSubmissions(usr);
                hubContextProj.Clients.Group("Team"+teamAssigned.ID).notifyChange(submission.Project, "A submission was made by: \""+ usrDB.FirstName+"\"", MsgTypeDTO.INFORMATION);
                hubContextProj.Clients.Group("JudgeGroupTeam" + teamAssigned.ID).notifyChange(submission.Project, "A student made a submission!", MsgTypeDTO.INFORMATION);
                hubContextProj.Clients.Group("Admins").notifyChange(submission.Project, "A student made a submission!", MsgTypeDTO.INFORMATION);
                BroadcastGraders("JudgeGroupTeam" + teamAssigned.ID, teamAssigned, new TeamSubmissionDTO(sub));

                //
                dashHub.BroadcastSubmissionsAwaitingGrade(usr);
                dashHub.BroadcastProjectsAwaitingSubmissions(usr);
                
                
                // Does not exist so we will replace it.
            }
        }

        /**
         * Broadcasts a complete update of all submissions for Admins and the specified judge mapping
         */
        private void BroadcastGraders(string judgeGroup, Team team, TeamSubmissionDTO submission)
        {
            hubContextGrades.Clients.Group("Admins").updatedSubmission(submission);    //Admins see all..
            hubContextGrades.Clients.Group(judgeGroup).updatedSubmission(submission);
        }

        public void BroadcastTeamSubmissions(UserDTO usr)
        {
            User usrDB = GetUser(usr);
            if (usrDB != null && usrDB.Role == UserRole.Participant)
            {
                IQueryable<Student> stuDB = db.Students.Where(c => c.User.ID == usrDB.ID);
                Team teamAssigned = stuDB.Select(c => c.TeamAssigned).First();
                Clients.Group("Team" + teamAssigned.ID).submissionTeamPoll(getTeamSubmissions(usr));
            }
        }

        /*
         * Used to retrieve the User object from the database or null if invalid credentials
         */
        private User GetUser(UserDTO usr)
        {
            IQueryable<User> userDB = db.User.Where(c => c.Username == usr.Username && c.Password == usr.Password);

            if (userDB != null && userDB.Count() == 1)
            {
                return userDB.First();
            }
            return null;
        }

        private bool IsAdmin(UserDTO usr)
        {
            User usrDB = GetUser(usr); 
            if (usrDB != null && usrDB.Role == UserRole.Admin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Task OnConnected()
        {
            // Process Credential information to determine role
            // Clients.All.numberOfUsers(numberOfUsers);
            return (base.OnConnected());
        }

        public override Task OnDisconnected(bool flag)
        {
            //Clients.All.numberOfUsers(numberOfUsers);
            return (base.OnDisconnected(flag));
        }

    }
}