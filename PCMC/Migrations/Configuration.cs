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

            User ben = new User { ID = 2, FirstName = "Benjamin", LastName = "Aronson", Password = "sn00pd0g23", Email = "Ben@Kostreski.com", Role = UserRole.User, Username = "Ben" };
            context.User.AddOrUpdate(
                p => p.ID,
                new User { ID = 1, FirstName = "Stephan", LastName = "K", Password = "Admin123", Email="Stephan@Kostreski.com", Role = UserRole.Admin, Username = "Admin" },
                ben
            );
            context.SaveChanges();

            InstanceTypes drupal = new InstanceTypes { ID = 1, Name = "Drupal v8.3.0", SysUser = "admin", SysPass = "maintenance!" };
            context.InstanceTypes.AddOrUpdate(
                p => p.ID,
                drupal
            );
            context.SaveChanges();

            ServerLocations silicon_valley = new ServerLocations { ID = 1, LocationName = "Silicon Valley", ServerIP = "104.207.149.59", ServerPort = "2983", ServerURL = "http://104.207.149.59/" };
            context.ServerLocations.AddOrUpdate(
                p => p.ID,
                silicon_valley
            );
            context.SaveChanges();

            context.Instances.AddOrUpdate(
                p => p.ID,
                new Instances
                {
                    ID = 1,
                    DeleteDate = new DateTime(2016, 8, 19),
                    InstanceType = context.InstanceTypes.Single(p=>p.ID==drupal.ID),
                    LocationInstalled = context.ServerLocations.Single(p => p.ID == silicon_valley.ID),
                    Owner = context.User.Single(p => p.ID == ben.ID),
                    URL= "drupal"
                }
            );
            context.SaveChanges();

            EmailTemplates Drupal_Welcome = new EmailTemplates { ID = 1, EmailSubject = "Welcome to Drupal on PointSaaS", InstanceType = context.InstanceTypes.Single(p => p.ID == drupal.ID), TemplateFileName = "Welcome.tpl" };
            context.EmailTemplates.AddOrUpdate(
                p => p.ID,
                Drupal_Welcome
            );
            context.SaveChanges();

            EmailQueue queue = new EmailQueue { ID = 1, FutureTime= new DateTime(2016, 8, 19),Instance= context.Instances.Single(p => p.ID == 1),IsReady=true,
                Owner = context.User.Single(p => p.ID == ben.ID),
                Template = context.EmailTemplates.Single(p => p.ID == Drupal_Welcome.ID)
            };
            context.EmailQueue.AddOrUpdate(
                p => p.ID,
                queue
            );
            context.SaveChanges();
        }
    }
}
