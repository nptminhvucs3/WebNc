using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.MVC.Models;

namespace Web.MVC.Areas.User.Controllers
{
    public class HomeUController : Controller
    {
        Models.NhaSachEntities1 db= new Models.NhaSachEntities1();  
        // GET: User/HomeU
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("/Login", "Login", new { area = "User" });
            }
            else
            {
            List<Models.SanPham> data = db.SanPhams.Take(16).ToList();
            ViewBag.HotProducts = data;
            return View();
            }

        }
 
        public ActionResult Details(int id)
        {
            SanPham sanpham = db.SanPhams.Where(row => row.Id == id).FirstOrDefault();
            return View(sanpham);
        }

    }
}