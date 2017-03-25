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
    public class GradeSubmissionsHub : Hub
    {
        private static DashboardHub dashHub = new DashboardHub();
        private static ProjectsHub projHub = new ProjectsHub();
        private static IHubContext hubContextDash = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>();
        private static IHubContext hubContextProj = GlobalHost.ConnectionManager.GetHubContext<ProjectsHub>();
        protected ModelADO db = new ModelADO();

        public void Subscribe(UserDTO usr)
        {
            User userDB = GetUser(usr);
            if (userDB != null)
            {
                switch (userDB.Role)
                {
                    case UserRole.Admin:
                        // Admin's hear all
                        Groups.Add(Context.ConnectionId, "Admins");
                        break;
                    case UserRole.Judge:

                        IQueryable<JudgeTeamMap> teamDB = db.JudgeTeamMap.Where(c => c.Judge.ID == userDB.ID);
                        ICollection<Team> teamAssignments = teamDB.Select(c => c.Team).Cast<Team>().ToList();
                        Groups.Add(Context.ConnectionId, "Judges");

                        // Map this Judge to their respective team assignments
                        foreach (var team in teamAssignments)
                            Groups.Add(Context.ConnectionId, "JudgeGroupTeam" + team.ID);
                        break;
                    case UserRole.Participant:
                        // Students should not access this area.
                        break;
                }
            }
            // Do nothing otherwise
        }

        public void submissionTeamPoll(UserDTO usr)
        {
            User userDB = GetUser(usr);
            if (userDB != null)
            {
                List<TeamSubmissionDTO> listToBeReturned = new List<TeamSubmissionDTO>();
                switch (userDB.Role)
                {
                    case UserRole.Admin:
                        List<TeamSubmission> tSub = db.TeamSubmission.Include("Project").Include("Team").ToList();
                        
                        foreach (var sub in tSub)
                        {
                            listToBeReturned.Add(new TeamSubmissionDTO(sub));
                        }
                        Clients.Caller.submissionTeamPoll(listToBeReturned);    //Admins see all..
                        break;
                    case UserRole.Judge:
                        IQueryable<JudgeTeamMap> teamDB = db.JudgeTeamMap.Where(c => c.Judge.ID == userDB.ID);
                        Team[] teamAssignments = teamDB.Select(c => c.Team).Cast<Team>().ToList().ToArray();
                        foreach(Team team in teamAssignments)
                        {
                            List<TeamSubmission> listDB = db.TeamSubmission.Include("Project").Include("Team").Where(c => c.Team.ID == team.ID).ToList(); //List of submissions for this team
                            foreach (var sub in listDB)
                            {
                                listToBeReturned.Add(new TeamSubmissionDTO(sub));
                            }
                        }
                        
                        Clients.Caller.submissionTeamPoll(listToBeReturned);
                        break;
                }

                //Clients.Caller.submissionTeamPoll(listToBeReturned);
            }
        }

        public void GradeSubmission (UserDTO usr, TeamSubmissionDTO item)
        {
            User userDB = GetUser(usr);

            if(userDB != null)
            {
                TeamSubmission record = db.TeamSubmission.Include("Team").Include("Project").Where(c=>c.ID == item.ID).First();
                Team teamAssigned = record.Team;
                switch (userDB.Role)
                {
                    case UserRole.Admin:
                        if (record != null)
                        {
                            
                            record.Score = item.Score;
                            record.GraderComment = item.GraderComment; //Vulnerable to XSS...
                            db.SaveChanges();
                            BroadcastGraders("JudgeGroupTeam" + teamAssigned.ID, teamAssigned, new TeamSubmissionDTO(record));
                        }
                        break;
                    case UserRole.Judge:
                        // Verify judge can edit this submission, update it, notify all parties
                        JudgeTeamMap[] mapping = db.JudgeTeamMap.Where(c=>c.Judge.ID == userDB.ID && c.Team.ID == record.Team.ID).ToArray();

                        if (mapping.ToArray().Length >= 1)
                        {
                            // Judge is authorized to grade this
                            if (record != null)
                            {
                                record.Score = item.Score;
                                record.GraderComment = item.GraderComment; //Vulnerable to XSS...
                                db.SaveChanges();
                                BroadcastGraders("JudgeGroupTeam" + teamAssigned.ID, teamAssigned, new TeamSubmissionDTO(record));
                            }
                        }
                        break;
                }
            }
        }

        private void BroadcastGraders(string judgeGroup, Team team, TeamSubmissionDTO submission)
        {
            Clients.Group("Admins").updatedSubmission(submission);    //Admins see all..
            Clients.Group(judgeGroup).updatedSubmission(submission);
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