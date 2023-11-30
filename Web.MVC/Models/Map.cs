using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MVC.Models
{
    public class Map
    {
        NhaSachEntities1 db = new NhaSachEntities1();
        public NguoiDung Timkiem(string TaiKhoan, string MatKhau/*, int Chucvu*/)
        {
            var user = db.NguoiDungs.Where(m => m.TaiKhoan == TaiKhoan & m.MatKhau == MatKhau /*& m.ChucVu==3*/).ToList();
            if (user.Count > 0)
            {
                return user[0];
            }
            else
            {
                return null;
            }
        }

    }
}