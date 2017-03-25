using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using PCMC.DTO;
using PCMC.Entities;
using PCMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PCMC.Client.Hubs
{
    public class ReportHub : Hub
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
                        // Judge's are not permitted.
                        break;
                    case UserRole.Participant:
                        // Participants are not permitted.
                        break;
                }

            }
            // Do nothing otherwise
        }

        /**
         * Retrieves all Teams associated by level.
         */
        private List<TeamDTO> GetTeamList(Level lvl)
        {
            List<Team> teamList = db.Teams.Where(c=>c.lvl == lvl).ToList();// retrieve team list by lvl
            List<TeamDTO> teamListDTO = new List<TeamDTO>(); // List to be sent.

            foreach(Team t in teamList)
            {
                teamListDTO.Add(new TeamDTO(t));
            }

            return teamListDTO;
        }

        /**
         * Retrieves the list of all submissions given a level.
         */ 
        private List<TeamSubmissionDTO> GetSubmissionList(Level lvl)
        {
            List<TeamSubmission> subList = db.TeamSubmission.Where(c => c.Team.lvl == lvl).ToList();
            List<TeamSubmissionDTO> subListDTO = new List<TeamSubmissionDTO>(); // List to be sent.

            foreach (TeamSubmission t in subList)
            {
                subListDTO.Add(new TeamSubmissionDTO(t));
            }


            return subListDTO;
        }

        private List<School> GetSchoolList()
        {
            List<School> schoolList = db.Schools.Include("Instructor").Include("Teams").ToList();
            return schoolList;
        }

        private List<Student> GetStudentList()
        {
            List<Student> studentList = db.Students.Include("TeamAssigned").Include("SchoolEnrolled").Include("User").ToList();
            return studentList;
        }

        /*
         * Admin requests a report of who won by specified level.
         */ 
        public void PollReportByLevel(UserDTO usr, Level lvl)
        {
            // Is authorized?
            if (IsAdmin(usr))
            {
                //TODO: Need to get list of teams, and their correspoding: school, members, instructor, submission score totals.
                // List
                ReportModel report = new ReportModel(lvl);
                List<TeamDTO> teamList = GetTeamList(lvl);
                //List<Instructor> insList = GetInstructorList();
                List<TeamSubmissionDTO> subList = GetSubmissionList(lvl);
                List<School> schoolList = GetSchoolList();
                List<Student> studentList = GetStudentList();

                foreach (TeamDTO t in teamList)
                {
                    report.teamList.Add(t);
                }

                foreach (TeamSubmissionDTO sub in subList)
                {
                    // If the team id key does not exist, add a new list
                    if (!report.submissionList.ContainsKey(sub.Team.ID))
                    {
                        report.submissionList.Add(sub.Team.ID, new List<TeamSubmissionDTO>());
                    }
                    report.submissionList[sub.Team.ID].Add(sub);
                }

                foreach(School school in schoolList)
                {
                    // Map school to each team
                    foreach (Team t in school.Teams)
                    {
                        report.schoolList.Add(t.ID, new SchoolDTO(school));
                    }
                }

                foreach(Student stu in studentList)
                {
                    // If the team id key does not exist, add a new list
                    if (!report.studentList.ContainsKey(stu.TeamAssigned.ID))
                    {
                        report.studentList.Add(stu.TeamAssigned.ID, new List<StudentDTO>());
                    }
                    // Add this student to the running list of students for this team.
                    report.studentList[stu.TeamAssigned.ID].Add(new StudentDTO(stu));
                }

                Clients.Caller.reportPush(report);

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