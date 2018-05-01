using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteCafe.Models;

namespace WebSiteCafe.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        QuanLyCafeEntities db = new QuanLyCafeEntities();
        public ActionResult KhachHang()
        {
            if (Session["Taikhoanadmin"] != null)
            {
                return View(db.KhachHangs.ToList());
            }
            else
            {
                return RedirectToAction("Login", "QuanLySanPham");
            }
        }
    }
}