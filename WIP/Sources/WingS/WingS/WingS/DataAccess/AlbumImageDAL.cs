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
        public void Dispose()
        {
         
        }
    }
}