using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.MVC.Models;

namespace Web.MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class SanPhamController : Controller
    {
        // GET: SanPham
        private NhaSachEntities1 db = new NhaSachEntities1();
        public ActionResult Index()
        {
            var sanPham = db.SanPhams.ToList();

            return View(sanPham);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.NSXSelectList = new SelectList(db.NhaSanXuats, "ID", "TenNSX");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SanPham obj)
        {
            var f = Request.Files["FileAnh"];
            NhaSachEntities1 db = new NhaSachEntities1();
            try
            {
                if (f != null && f.ContentLength > 0)
                {
                    string rootFolder= Server.MapPath("/Assets/Upload/");
                    string folder = rootFolder+ f.FileName;
                    f.SaveAs(folder);
                    obj.Anh = "/Assets/Upload/" + f.FileName;
                }
                db.SanPhams.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException eve)
            {
                foreach (var item in eve.EntityValidationErrors)
                {
                    var errors = item.ValidationErrors;
                    foreach (var error in errors)
                    {
                        var x = error.PropertyName + " - " + error.ErrorMessage;
                    }
                }

            }
            finally
            {
                ViewBag.NSXSelectList = new SelectList(db.NhaSanXuats, "ID", "TenNSX");

            }
            return View(obj);

        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var dsNSX = db.NhaSanXuats.ToList();

            SelectList NSXSelectList = new SelectList(dsNSX, "Id", "TenNSX");
            ViewBag.NSXSelectList = NSXSelectList;
            var sanPham = db.SanPhams.Where(item => item.Id == id).FirstOrDefault();
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult Update(SanPham sanPham)
        {
            var result = db.SanPhams.Find(sanPham.Id);
            try
            {
                result.TenSP = sanPham.TenSP;
                result.NhaSanXuat = sanPham.NhaSanXuat;
                /*result.N = sanPham.NgayNhaps;*/
                result.SoLuong = sanPham.SoLuong;
                result.DonGiaBan = sanPham.DonGiaBan;
                result.DonGiaNhap = sanPham.DonGiaNhap;
            } catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Khong the them san pham, vui long thu lai");
                return View();
            }
            return RedirectToAction("Index", "Sanpham");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sanPham = db.SanPhams.Where(item => item.Id == id).FirstOrDefault();
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult Delete(SanPham sanPham)
        {
            try
            {
                var result = db.SanPhams.Find(sanPham.Id);
                db.SanPhams.Remove(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                return View();
            }
        }
    }
}