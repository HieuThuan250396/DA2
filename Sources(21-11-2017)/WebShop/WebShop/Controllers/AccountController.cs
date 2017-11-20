using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models.Security;
using System.Web.Security;

namespace WebShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(LogOnUser model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (new CustomMembershipProvider().ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        Session["LoginNameNV"] = "Xin Chao, " + model.UserName;
                        Session["UserInfo"] = model.UserName;
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    model.Password = "";
                    TempData["SaiPass"] = "Wrong password or username";
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult LogOut()
        {
            Session["LoginNameNV"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");
        }

        public ActionResult PermissionDenied()
        {
            return View();
        }
    }
}