namespace PCMC
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ModelADO : DbContext
    {
        // Your context has been configured to use a 'ModelADO' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PCMC.ModelADO' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelADO' 
        // connection string in the application configuration file.
        public ModelADO()
            : base("name=ModelADO")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<GlobalSettings> GlobalSettings { get; set; }
        public virtual DbSet<Instances> Instances { get; set; }

        public virtual DbSet<ServerLocations> ServerLocations { get; set; }

        public virtual DbSet<EmailQueue> EmailQueue { get; set; }
        public virtual DbSet<EmailTemplates> EmailTemplates { get; set; }

        public virtual DbSet<InstanceTypes> InstanceTypes { get; set; }

        public virtual DbSet<User> User { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}