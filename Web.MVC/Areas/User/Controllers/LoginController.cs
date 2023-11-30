using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.MVC.Models;

namespace Web.MVC.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        // GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

            [HttpPost]
            public ActionResult Login(string TaiKhoan, string MatKhau/*, int ChucVu*/)
            {
                Map map = new Map();
                var user = map.Timkiem(TaiKhoan, MatKhau/*, ChucVu*/);

                if (user != null)
                {
                    Session["TaiKhoan"] = TaiKhoan;
                    return RedirectToAction("/Index", "HomeU", new { area = "User" });
                }
                ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
            }


/*            public ActionResult Logout()
            {
                Session["TENDANGNHAP"] = null;
                return ...
            }*/
        }
    }
