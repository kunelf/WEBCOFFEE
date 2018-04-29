using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteCafe.Models;

namespace WebSiteCafe.Controllers
{
    public class ChiTietDonHangController : Controller
    {
        // GET: ChiTietDonHang
        QuanLyCafeEntities db = new QuanLyCafeEntities();
        public ActionResult ChiTietDonHang()
        {
            return View(db.ChiTietDonHangs.ToList());
        }
    }
}