using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class OrganizationController : Controller
    {
        public ActionResult CreateOrganization(CreateOrganization organization, HttpPostedFileBase LogoImage)
        {
            //Models.Organization newOrganization;
            try
            {
                string logoName = WsConstant.randomString() + Path.GetExtension(LogoImage.FileName).ToLower();
                string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Upload"), logoName);
                LogoImage.SaveAs(path);
                organization.LogoUrl = "/Content/Upload/" + logoName;

                using (var db = new OrganizationDAL())
                {
                    db.AddNewOrganization(organization, User.Identity.Name);
                }
            }
            catch (Exception)
            {
                return Redirect("/#/Error");
            }

            return Redirect("/#/Home");
        }

        public ActionResult EditOrganization(CreateOrganization organization, HttpPostedFileBase LogoImage)
        {
            try
            {
                Organization organi = new Organization();
                if (LogoImage != null)
                {
                    string logoName = WsConstant.randomString() + Path.GetExtension(LogoImage.FileName).ToLower();
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Upload"), logoName);
                    LogoImage.SaveAs(path);
                    organization.LogoUrl = "/Content/Upload/" + logoName;
                }
                
                using (var db = new OrganizationDAL())
                {
                    organi = db.EditOrganization(organization, User.Identity.Name);
                }
                return Redirect("/#/OrganizationDetail/" + organi.OrganizationId);
            }
            catch (Exception)
            {
                return Redirect("/#/Error");
            }
        }
        public ActionResult UpdateUserAvatar(HttpPostedFileBase Image)
        {
            try
            {
               
                string ImageName = WsConstant.randomString() + Path.GetExtension(Image.FileName).ToLower();
                string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Upload"), ImageName);
                Image.SaveAs(path);
                string newName = "/Content/Upload/" + ImageName;
                //Delete Current Image in Upload folder.

                //Update Current Image to UserInformation
                using (var db = new UserDAL())
                {
                    var userId = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
                    var userInfo = db.GetUserInformation(userId);
                    userInfo.ProfileImage = newName;
                    using (var context = new Ws_DataContext())
                    {
                        context.User_Information.AddOrUpdate(userInfo);
                        context.SaveChanges();
                    }
                    return Redirect("/#/Profile/" + User.Identity.Name);
                }
            }
            catch (Exception)
            {
                return Redirect("/#/Error");
            }
           
        }
	}
}