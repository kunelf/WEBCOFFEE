using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteCafe.Models;

namespace WebSiteCafe.Controllers
{
    public class DonHangController : Controller
    {
        // GET: DonHang
        QuanLyCafeEntities db = new QuanLyCafeEntities();
        public ActionResult DonHang()
        {
            if (Session["Taikhoanadmin"] != null)
            {
                return View(db.DonHangs.ToList());
            }
            else
            {
                return RedirectToAction("Login", "QuanLySanPham");
            }
        }
         
    }
}