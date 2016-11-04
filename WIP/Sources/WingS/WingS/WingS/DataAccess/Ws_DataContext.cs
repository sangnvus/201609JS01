using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WingS.DataHelper;
using WingS.Models;

namespace WingS.DataAccess
{
    public class Ws_DataContext:DbContext
    {
        public virtual DbSet<CommentEvent> CommentEvents { get; set; }
        public virtual DbSet<CommentThread> CommentThreads { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<ReportEvent> ReportEvents { get; set; }
        public virtual DbSet<Thread> Threads { get; set; }
        public virtual DbSet<User_Information> User_Information { get; set; }
        public virtual DbSet<Ws_User> Ws_User { get; set; }
        public virtual DbSet<ThreadAlbumImage> ThreadAlbum { get; set; }
        public virtual DbSet<EventAlbumImage> EventAlbum { get; set; }
        public virtual DbSet<EventTimeLine> ETimeLine { get; set; }
        public virtual DbSet<SubCommentThread> SubCommentThread { get; set; }
        public virtual DbSet<SubCommentEvent> SubCommentEvent { get; set; }
        public virtual DbSet<LikeThreads> LikeThreads { get; set; }
        public virtual DbSet<LikeEvents> LikeEvents { get; set; }
        public virtual DbSet <Conservation> Conservation { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Connection> Connection { get; set; }
        public Ws_DataContext() : base(WsConstant.ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conservation>()
                   .HasRequired(m => m.Creator)
                   .WithMany(t => t.CreatorConservation)
                   .HasForeignKey(m => m.CreatorId)
                   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Conservation>()
                  .HasRequired(m => m.Receiver)
                  .WithMany(t => t.ReceiverConservation)
                  .HasForeignKey(m => m.ReceiverId)
                  .WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }

}