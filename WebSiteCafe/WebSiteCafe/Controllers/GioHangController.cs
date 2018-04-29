using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteCafe.Models;

namespace WebSiteCafe.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyCafeEntities db = new QuanLyCafeEntities();
        // GET: GioHang
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGioHang(int iMasp, string strURL)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMasp);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = Laygiohang();
            GioHang sanpham = lstGioHang.Find(n => n.iMasp == iMasp);
            if(sanpham==null)
            {
                sanpham = new GioHang(iMasp);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        public ActionResult CapNhatGioHang(int iMasp,FormCollection f)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMasp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = Laygiohang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMasp);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int iMasp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMasp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = Laygiohang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMasp);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMasp == sanpham.iMasp);
                
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = Laygiohang();
            return View(lstGioHang);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = Laygiohang();
            return View(lstGioHang);
        }
        //Đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //kiểm tra đăng nhập
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            //kiemtragiohang
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //themdonhang
            DonHang ddh = new DonHang();
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            List<GioHang> gh = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.DonHangs.Add(ddh);
            db.SaveChanges();
            //thêm chi tiết đơn hàng
            foreach(var item in gh)
            {
                ChiTietDonHang ctDH = new ChiTietDonHang();
                ctDH.MaDonHang = ddh.MaDonHang;
                ctDH.MaSP = item.iMasp;
                ctDH.SoLuong = item.iSoluong;
                ctDH.DonGia = (decimal)item.dDongia;
                db.ChiTietDonHangs.Add(ctDH);
            }
            db.SaveChanges();
            return RedirectToAction("Xacnhandonhang","GioHang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}