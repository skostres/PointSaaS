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

            InstanceTypes wordpress = new InstanceTypes { ID = 1, Name = "Wordpress Version 2.43", SysUser = "Admin", SysPass = "TheGreatPassword" };
            context.InstanceTypes.AddOrUpdate(
                p => p.ID,
                wordpress

            );
            context.SaveChanges();

            ServerLocations miami = new ServerLocations { ID = 1, LocationName = "Miami", ServerIP = "45.63.105.19", ServerPort = "2983", ServerURL = "Miami.kostreski.com" };
            context.ServerLocations.AddOrUpdate(
                p => p.ID,
                miami
            );
            context.SaveChanges();

            context.Instances.AddOrUpdate(
                p => p.ID,
                new Instances
                {
                    ID = 1,
                    DeleteDate = new DateTime(2016, 8, 19),
                    InstanceType = context.InstanceTypes.Single(p=>p.ID==wordpress.ID),
                    LocationInstalled = context.ServerLocations.Single(p => p.ID == miami.ID),
                    Owner = context.User.Single(p => p.ID == ben.ID),
                    URL= "miami.pointsaas.kostreski.com/benswordpress"
                }
            );
            context.SaveChanges();

            EmailTemplates wordpress_Welcome = new EmailTemplates { ID = 1, EmailSubject = "Welcome to wordpress on PointSaaS", InstanceType = context.InstanceTypes.Single(p => p.ID == wordpress.ID), TemplateFileName = "Welcome.tpl" };
            context.EmailTemplates.AddOrUpdate(
                p => p.ID,
                wordpress_Welcome
            );
            context.SaveChanges();

            EmailQueue queue = new EmailQueue { ID = 1, FutureTime= new DateTime(2016, 8, 19),Instance= context.Instances.Single(p => p.ID == 1),IsReady=true,
                Owner = context.User.Single(p => p.ID == ben.ID),
                Template = context.EmailTemplates.Single(p => p.ID == wordpress_Welcome.ID)
            };
            context.EmailQueue.AddOrUpdate(
                p => p.ID,
                queue
            );
            context.SaveChanges();
        }
    }
}
