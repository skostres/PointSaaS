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

            
            context.User.AddOrUpdate(
                p => p.ID, 
                new User { ID = 1, FirstName = "Stephan", LastName = "K", Password = "Admin123", Email="Stephan@Kostreski.com", Role = UserRole.Admin, Username = "Admin" },
                new User { ID = 2, FirstName = "Benjamin", LastName = "Aronson", Password = "sn00pd0g23", Email = "Ben@Kostreski.com", Role = UserRole.User, Username = "Ben" }
            );


        }
    }
}
