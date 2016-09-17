namespace WingS.DataAccess
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class Model1 : DbContext
    {

        public virtual DbSet<CommentEvent> CommentEvents { get; set; }
        public virtual DbSet<CommentThread> CommentThreads { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Event_Statistic> Event_Statistic { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Organazation> Organazations { get; set; }
        public virtual DbSet<ReportEvent> ReportEvents { get; set; }
        public virtual DbSet<Thread> Threads { get; set; }
        public virtual DbSet<User_Information> User_Information { get; set; }
        public virtual DbSet<Ws_User> Ws_User { get; set; }
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WingS.DataAccess.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public Model1()
            : base("name=Model1")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}