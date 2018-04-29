using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteCafe.Models;
    
namespace WebSiteCafe.Controllers
{
    public class NhaCungCapController : Controller
    {
        // GET: NhaCungCap
        QuanLyCafeEntities db = new QuanLyCafeEntities();
        public ActionResult NhaCungCap()
        {
            return View(db.NhaCungCaps.ToList());
        }
        [HttpGet]
        public ActionResult ThemMoiNCC()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSanPham(SanPham sanpham, HttpPostedFileBase fileUpload)
        {
            return View();
           

        }
    }
}