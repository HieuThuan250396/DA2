using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models.Db;
namespace WebShop.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index()
        {
            string username = Session["UserInfo"] as string;
            dbWebShop db = new dbWebShop();
            STAFF staff = db.STAFFs.SingleOrDefault(a => a.USERID.Equals(username));
            var temp = staff.STAFFID;
            TempData["TempID"] = temp;
            return View(staff);
        }
    }
}