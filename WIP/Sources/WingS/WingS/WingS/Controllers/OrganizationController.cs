using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.DataHelper;
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
                organization.LogoUrl = "Content/Upload/" + logoName;

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
                if (LogoImage != null)
                {
                    string logoName = WsConstant.randomString() + Path.GetExtension(LogoImage.FileName).ToLower();
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Upload"), logoName);
                    LogoImage.SaveAs(path);
                    organization.LogoUrl = "Content/Upload/" + logoName;
                }
                
                using (var db = new OrganizationDAL())
                {
                    db.EditOrganization(organization,User.Identity.Name);
                }
            }
            catch (Exception)
            {
                return Redirect("/#/Error");
            }

            return Redirect("/#/Home");
        }
	}
}