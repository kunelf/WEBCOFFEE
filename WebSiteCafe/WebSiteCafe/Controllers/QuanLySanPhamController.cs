using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteCafe.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace WebSiteCafe.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        QuanLyCafeEntities db = new QuanLyCafeEntities();
        public ActionResult Index(int? page)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                //return View();
                int pageNumber = (page ?? 1);
                int pageSize = 10;

                return View(db.SanPhams.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["Taikhoanadmin"] = null;
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection.Get("Username");
            var matkhau = collection.Get("Password");
            Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
            if (ad != null)
            {

                Session["Taikhoanadmin"] = ad;
                Session["Tenadmin"] = tendn;
                return RedirectToAction("Index", "QuanLySanPham");
            }
            else
                return View();
        }
        //thêm mới
        [HttpGet]
        public ActionResult ThemMoiSanPham()
        {
            ViewBag.MaLoai = new SelectList(db.PhanLoais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSanPham(SanPham sanpham, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaLoai = new SelectList(db.PhanLoais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            //luu tên file
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn hình ảnh!!!";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Lưu đường dẫn
                    var path = Path.Combine(Server.MapPath("~/hinhanhsp"), fileName);
                    //kiểm tra hình ảnh có tồn tại?
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại!!!";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sanpham.AnhBia = fileUpload.FileName;
                    db.SanPhams.Add(sanpham);
                    db.SaveChanges();
                }
                return View();
            }

        }
        //hiển thị sản phẩm
        public ActionResult ChiTietSanPham(int id)
        {
            //lấy sản phẩm theo mã  
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpGet]
        public ActionResult XoaSanPham(int id)
        {
            //lấy sản phẩm theo mã  
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost, ActionName("XoaSanPham")]
        public ActionResult XacNhanXoa(int id)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SuaSanPham(int id)
        {
            //lấy sản phẩm theo mã  
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(db.PhanLoais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            return View(sanpham);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(int id, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            var sp = db.SanPhams.First(k => k.MaSP == id);
            ViewBag.MaLoai = new SelectList(db.PhanLoais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");


            if (ModelState.IsValid)
            {
                try
                {
                    if (fileUpload != null)
                    {
                        //Luu ten file
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        //Luu duong dan File
                        var path = Path.Combine(Server.MapPath("~/hinhanhsp"), fileName);
                        //Kiem tra hinh da ton tai chua\
                        if (System.IO.File.Exists(path))
                            ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                        else
                        {
                            fileUpload.SaveAs(path);//Luu file vao duong dan
                        }
                        sp.AnhBia = fileUpload.FileName;
                    }

                }
                catch (Exception)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh cho sản phẩm";
                }

            }
            string tenSP = collection["TenSP"];
            decimal giaBan = decimal.Parse(collection["GiaBan"]);
            string moTa = collection["MoTa"];
            DateTime ngayNhap = DateTime.Parse(collection["NgayCapNhat"]);
            int soLuongTon = int.Parse(collection["SoLuongTon"]);
            int maLoai = int.Parse(collection["MaLoai"]);
            int maNCC = int.Parse(collection["MaNCC"]);
            int moi = int.Parse(collection["Moi"]);

            sp.TenSP = tenSP;
            sp.GiaBan = giaBan;
            sp.MoTa = moTa;
            sp.NgayCapNhat = ngayNhap;
            sp.SoLuongTon = soLuongTon;
            sp.MaLoai = maLoai;
            sp.MaNCC = maNCC;
            sp.Moi = moi;

            UpdateModel(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult SuaSanPham(SanPham sanpham, HttpPostedFileBase fileUpload)
        //{
        //    ViewBag.MaLoai = new SelectList(db.PhanLoais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
        //    ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");

        //    if (fileUpload == null)
        //    {
        //        ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
        //        return View();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        //Luu ten file
        //        var fileName = Path.GetFileName(fileUpload.FileName);
        //        //Luu duong dan File
        //        var path = Path.Combine(Server.MapPath("~/hinhanhsp"), fileName);
        //        //Kiem tra hinh da ton tai chua\
        //        if (System.IO.File.Exists(path))
        //            ViewBag.ThongBao = "Hình ảnh đã tồn tại";
        //        else
        //        { 
        //            fileUpload.SaveAs(path);//Luu file vao duong dan
        //        }
        //        sanpham.AnhBia = fileUpload.FileName;
        //        UpdateModel(sanpham);
        //        db.SaveChanges();
        //    }      
        //        return RedirectToAction("Index");
        //    }




        //public ActionResult Logout()
        //{
        //    Session.Clear();
        //    return RedirectToAction("")
        //}
    }
}