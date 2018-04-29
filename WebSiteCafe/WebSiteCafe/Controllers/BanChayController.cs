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
    public class BanChayController : Controller
    {
        // GET: BanChay
        QuanLyCafeEntities db = new QuanLyCafeEntities();
        public ActionResult BanChay(int? page)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            List<SanPham> lstsp = db.SanPhams.Where(n => n.Moi == 3).ToList();
            {
                if (lstsp.Count == 0)
                {
                    ViewBag.SanPham = "Không có sản phẩm nào thuộc loại này";
                }
            }
            return View(/*db.SanPhams.Where(n=>n.MaLoai==1)*/lstsp.ToPagedList(pageNumber,pageSize ));
        }
    }
}