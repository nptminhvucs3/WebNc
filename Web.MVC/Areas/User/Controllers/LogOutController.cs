using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.MVC.Areas.User.Controllers
{
    public class LogOutController : Controller
    {
        // GET: User/LogOut
        public ActionResult LogOut()
        {

        Session["TaiKhoan"] = null;
        return Redirect("https://localhost:44368/User/Login/Login");
            

        }
    }
}