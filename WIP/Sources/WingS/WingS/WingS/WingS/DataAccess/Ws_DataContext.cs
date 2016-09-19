using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WingS.Models;

namespace WingS.DataAccess
{
    public class Ws_DataContext:DbContext
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
        public Ws_DataContext() : base("Ws_DataContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }

}