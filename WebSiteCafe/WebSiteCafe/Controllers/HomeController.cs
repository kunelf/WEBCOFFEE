using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteCafe.Models;

namespace WebSiteCafe.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyCafeEntities db = new QuanLyCafeEntities();
        public ActionResult Index()
        {
            var spmoi = db.SanPhams.Where(n=>n.Moi==1).Take(4).ToList();
            return View(spmoi);
        }
        //xem chi tiết
        public ViewResult XemChiTiet(int MaSP=0)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if(sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.TenLoai = db.PhanLoais.Single(n => n.MaLoai == sanpham.MaLoai).TenLoai;
            ViewBag.TenNCC = db.NhaCungCaps.Single(n => n.MaNCC == sanpham.MaNCC).TenNCC;
            return View(sanpham);
        }
    }
}