namespace WingS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<CommentEvent> CommentEvents { get; set; }
        public virtual DbSet<CommentThread> CommentThreads { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Event_Statistic> Event_Statistic { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Organazation> Organazations { get; set; }
        public virtual DbSet<ReportEvent> ReportEvents { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Thread> Threads { get; set; }
        public virtual DbSet<User_Information> User_Information { get; set; }
        public virtual DbSet<Ws_User> Ws_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.CommentEvents)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Donations)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasOptional(e => e.Event_Statistic)
                .WithRequired(e => e.Event);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.ReportEvents)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.EventType1)
                .HasForeignKey(e => e.EventType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organazation>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Organazation)
                .HasForeignKey(e => e.CreatorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Thread>()
                .HasMany(e => e.CommentThreads)
                .WithRequired(e => e.Thread)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ws_User>()
                .HasMany(e => e.CommentEvents)
                .WithRequired(e => e.Ws_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ws_User>()
                .HasMany(e => e.CommentThreads)
                .WithRequired(e => e.Ws_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ws_User>()
                .HasMany(e => e.Donations)
                .WithRequired(e => e.Ws_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ws_User>()
                .HasOptional(e => e.Organazation)
                .WithRequired(e => e.Ws_User);

            modelBuilder.Entity<Ws_User>()
                .HasMany(e => e.ReportEvents)
                .WithRequired(e => e.Ws_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ws_User>()
                .HasMany(e => e.Threads)
                .WithRequired(e => e.Ws_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ws_User>()
                .HasOptional(e => e.User_Information)
                .WithRequired(e => e.Ws_User);
        }
    }
}
