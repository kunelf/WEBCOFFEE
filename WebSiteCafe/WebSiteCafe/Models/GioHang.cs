using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteCafe.Models
{
    public class GioHang
    {
        QuanLyCafeEntities db = new QuanLyCafeEntities();
        public int iMasp { set; get;}
        public string sTensp { set; get; }
        public string sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong*dDongia; }
        }
        public GioHang(int Masp)
        {
            iMasp = Masp;
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == iMasp);
            sTensp = sanpham.TenSP;
            sAnhbia = sanpham.AnhBia;
            dDongia = double.Parse(sanpham.GiaBan.ToString());
            iSoluong = 1;
        }
    }
}