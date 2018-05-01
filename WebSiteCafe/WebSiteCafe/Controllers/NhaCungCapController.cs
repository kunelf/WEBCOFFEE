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
            if (Session["Taikhoanadmin"] != null)
            {
                return View(db.NhaCungCaps.ToList());
            }
            else
            {
                return RedirectToAction("Login","QuanLySanPham");
            }
        }
        [HttpGet]
        public ActionResult ThemMoiNCC()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiNCC( NhaCungCap NCC,HttpPostedFileBase fileUpload)
        {

            db.NhaCungCaps.Add(NCC);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult XoaNCC(int id)
        {
            NhaCungCap NCC = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == id);
            ViewBag.MaNCC = NCC.MaNCC;
            if(NCC== null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(NCC);
        }
        [HttpPost, ActionName("XoaNCC")]
        public ActionResult XacNhanXoa(int id)
        {
            NhaCungCap NCC = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == id);
            ViewBag.MaNCC = NCC.MaNCC;
            if (NCC == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhaCungCaps.Remove(NCC);
            db.SaveChanges();
            return RedirectToAction("NhaCungCap");
        }

    }
}