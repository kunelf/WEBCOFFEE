using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteCafe.Models;
using PagedList;
using PagedList.Mvc;

namespace WebSiteCafe.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QuanLyCafeEntities db = new QuanLyCafeEntities();  
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            String sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<SanPham> lstKQTK = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.SanPhams.ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kết quả!";
            return View(lstKQTK.ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(string sTuKhoa, int? page)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<SanPham> lstKQTK = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.SanPhams.ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy" + lstKQTK.Count + " kết quả!";
            return View(lstKQTK.ToPagedList(pageNumber, pageSize));
        }
    }
}