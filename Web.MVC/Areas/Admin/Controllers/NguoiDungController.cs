﻿using System;
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
    public class NguoiDungController : Controller
    {
        private NhaSachEntities1 db = new NhaSachEntities1();
        public ActionResult Index()
        {
            var NguoiDung = db.NguoiDungs.ToList();

            return View(NguoiDung);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ChucVuSelectList = new SelectList(db.ChucVus, "ID", "TenChucVu");
            return View();
        }

        [HttpPost]
        public ActionResult Create(NguoiDung obj)
        {
           
            NhaSachEntities1 db = new NhaSachEntities1();
            try
            {
                var f = Request.Files["FileAnh"];
                if (f != null && f.ContentLength > 0)
                {
                    string fName = f.FileName;
                    string folder = Server.MapPath("/Assets/Upload/") + fName;
                    f.SaveAs(folder);
                    obj.Anh = "/Assets/Upload/" + fName;
                }
                db.NguoiDungs.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }

        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var chucVu = db.ChucVus.ToList();
            SelectList ChucVuSelectList = new SelectList(chucVu, "Id", "TenChucVu");
            ViewBag.ChucVuSelectList = ChucVuSelectList;
            var nguoiDung = db.NguoiDungs.Where(item => item.Id == id).FirstOrDefault();
            return View(nguoiDung);
        }
        [HttpPost]
        public ActionResult Update(NguoiDung nguoiDung, HttpPostedFileBase Anh)
        {
            var result = db.NguoiDungs.Find(nguoiDung.Id);
            try
            {
                {
                    if (Anh != null && Anh.ContentLength > 0)
                    {
                        string fName = Anh.FileName;
                        string folder = Server.MapPath("/Assets/Upload/") + fName;
                        Anh.SaveAs(folder);
                        nguoiDung.Anh = "/Assets/Upload/" + fName;
                        result.Anh = nguoiDung.Anh;
                    }
                    result.Ten = nguoiDung.Ten;
                    result.NgaySinh = nguoiDung.NgaySinh;
                    result.DiaChi = nguoiDung.DiaChi;
                    result.GioiTinh = nguoiDung.GioiTinh;
                    result.SDT = nguoiDung.SDT;
                    result.ChucVu = nguoiDung.ChucVu;
                    result.TaiKhoan = nguoiDung.TaiKhoan;
                    result.MatKhau = nguoiDung.MatKhau;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Khong the thay doi thong tin nguoi dung, vui long thu lai");
                return View();
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var nguoiDung = db.NguoiDungs.Where(item => item.Id == id).FirstOrDefault();
            return View(nguoiDung);
        }
        [HttpPost]        
        
        public ActionResult Delete(int id,NguoiDung n)
        {
            try
            {
                var nguoiDung = db.NguoiDungs.Where(item => item.Id == id).FirstOrDefault();
                /*var result = db.NguoiDung.Remove(nguoiDung);*/
                db.NguoiDungs.Remove(nguoiDung);
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
