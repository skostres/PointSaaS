namespace PCMC.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PCMC.ModelADO>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PCMC.ModelADO context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            Instructor ins = new Instructor { ID = 1, Name = "Alla Webb", Email = "email@domain.com", Phone = "555-555-5555" };
            context.Instructors.AddOrUpdate(
                p => p.Name, ins
            );

            context.User.AddOrUpdate(
                p => p.ID, 
                new User {ID=1, FirstName = "Stephan", LastName = "K", Password = "Admin123", Role = UserRole.Admin, Username = "Admin" }
                //new User {ID=2, FirstName = "Edward", LastName = "K", Password = "student", Role = UserRole.Participant, Username = "student" },
               // new User {ID=3, FirstName = "Judge", LastName = "K=J", Password = "judge", Role = UserRole.Judge, Username = "judge" }
            );

            context.Projects.AddOrUpdate(
                p=> p.Description,
                new Project {Description="Demo Project", MaxScore=20, Name = "Project 1", Hidden = false, RawZipFileJudges = new byte[] {
                        0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x8E, 0xB1, 0x24, 0x4A, 0xDF, 0x77,
                        0x18, 0xAD, 0x0A, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x12, 0x00, 0x00, 0x00, 0x74, 0x65,
                        0x73, 0x74, 0x7A, 0x69, 0x70, 0x2F, 0x72, 0x65, 0x61, 0x64, 0x6D, 0x65, 0x2E, 0x74, 0x78, 0x74,
                        0x54, 0x65, 0x73, 0x74, 0x20, 0x66, 0x69, 0x6C, 0x65, 0x2E, 0x50, 0x4B, 0x01, 0x02, 0x14, 0x00,
                        0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x8E, 0xB1, 0x24, 0x4A, 0xDF, 0x77, 0x18, 0xAD, 0x0A, 0x00,
                        0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x12, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00,
                        0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 0x65, 0x73, 0x74, 0x7A, 0x69, 0x70, 0x2F,
                        0x72, 0x65, 0x61, 0x64, 0x6D, 0x65, 0x2E, 0x74, 0x78, 0x74, 0x50, 0x4B, 0x05, 0x06, 0x00, 0x00,
                        0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x40, 0x00, 0x00, 0x00, 0x3A, 0x00, 0x00, 0x00, 0x00, 0x00,

                },
                    RawZipFileParticipants = new byte[] {
                    0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x45, 0x01, 0x2A, 0x4A, 0xD5, 0xD8,
                    0x21, 0x35, 0x10, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x74, 0x65,
                    0x73, 0x74, 0x7A, 0x69, 0x70, 0x2F, 0x72, 0x65, 0x61, 0x64, 0x6D, 0x65, 0x2D, 0x70, 0x61, 0x72,
                    0x74, 0x69, 0x63, 0x69, 0x70, 0x61, 0x6E, 0x74, 0x73, 0x2E, 0x74, 0x78, 0x74, 0x46, 0x6F, 0x72,
                    0x20, 0x70, 0x61, 0x72, 0x74, 0x69, 0x63, 0x69, 0x70, 0x61, 0x6E, 0x74, 0x73, 0x50, 0x4B, 0x01,
                    0x02, 0x14, 0x00, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x45, 0x01, 0x2A, 0x4A, 0xD5, 0xD8, 0x21,
                    0x35, 0x10, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x01, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 0x65, 0x73, 0x74, 0x7A,
                    0x69, 0x70, 0x2F, 0x72, 0x65, 0x61, 0x64, 0x6D, 0x65, 0x2D, 0x70, 0x61, 0x72, 0x74, 0x69, 0x63,
                    0x69, 0x70, 0x61, 0x6E, 0x74, 0x73, 0x2E, 0x74, 0x78, 0x74, 0x50, 0x4B, 0x05, 0x06, 0x00, 0x00,
                    0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x4D, 0x00, 0x00, 0x00, 0x4D, 0x00, 0x00, 0x00, 0x00, 0x00
                },Level=Level.INTRODUCTION
                });

            List<Team> teams = new List<Team>();
            teams.Add(new Team { ID=1,lvl=Level.INTRODUCTION, Name="Team1"});

            context.Teams.AddOrUpdate(teams.First());
            School school = new School { ID = 1, Instructor = ins, Name = "Montgomery College", Teams = teams };
            context.Schools.AddOrUpdate(school);
            context.JudgeTeamMap.AddOrUpdate(new JudgeTeamMap {ID=1,Judge= new User { ID = 3, FirstName = "Judge", LastName = "K=J", Password = "judge", Role = UserRole.Judge, Username = "judge" },Team= teams.First() });
            context.Students.AddOrUpdate(new Student { ID = 1, SchoolEnrolled = school, TeamAssigned = teams.First(), User = new User { ID = 2, FirstName = "Edward", LastName = "K", Password = "student", Role = UserRole.Participant, Username = "student" }});
            
            //new School {Instructor = new Instructor {Name="Alla",Phone="",Email="@" }, Name="Montgomery College", Teams = new List<Team> { new Team {ID=0,Name="Team2" } } }

        }
    }
}
