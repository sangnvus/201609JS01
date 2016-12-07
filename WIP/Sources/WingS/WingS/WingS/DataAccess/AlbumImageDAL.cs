using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class AlbumImageDAL : IDisposable
    {
        /// <summary>
        /// Add New Album
        /// </summary>
        /// <param name="album"></param>
        /// <returns>ThreadAlbumImage</returns>
        public ThreadAlbumImage AddNewAlbum(ThreadAlbumImageDTO album)
        {
            var emptyAlbum = CreateEmptyThreadAlbum();
            emptyAlbum.ThreadId = album.ThreadId;
            emptyAlbum.ImageUrl = album.ImageUrl;
            using (var db = new Ws_DataContext())
            {
                db.ThreadAlbum.Add(emptyAlbum);
                db.SaveChanges();
                return emptyAlbum;
            }

        }
        /// <summary>
        /// Create Empty Thread Album
        /// </summary>
        /// <returns>ThreadAlbumImage</returns>
        public ThreadAlbumImage CreateEmptyThreadAlbum()
        {
            using (var db = new Ws_DataContext()){
                var album = db.ThreadAlbum.Create();
                album.ThreadId = 0;
                album.ImageUrl = "";
                album.status = true;
                return album;
                }
        }
        /// <summary>
        /// Add Event Album
        /// </summary>
        /// <param name="album"></param>
        /// <returns>eventAlbum</returns>
        public EventAlbumImage AddEventAlbum(EventAlbumImageDTO album)
        {
            var eventAlbum = CreateEmptyEventAlbum();
            eventAlbum.EventId = album.EventId;
            eventAlbum.ImageUrl = album.ImageUrl;
            using (var db = new Ws_DataContext())
            {
                db.EventAlbum.Add(eventAlbum);
                db.SaveChanges();
                return eventAlbum;
            }

        }
        /// <summary>
        /// Create Empty Event Album
        /// </summary>
        /// <returns>album</returns>
        public EventAlbumImage CreateEmptyEventAlbum()
        {
            using (var db = new Ws_DataContext())
            {
                var album = db.EventAlbum.Create();
                album.EventId = 0;
                album.ImageUrl = "";
                album.status = true;
                return album;
            }
        }
        public void Dispose()
        {
         
        }
    }
}