using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;


namespace KurumsalWeb.Controllers
{
    public class BlogController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();
        // GET: Blog
        public ActionResult Index()
        {
            var blog = db.Blog.Include(b => b.Kategori);
            return View(blog.ToList());
        }

        public ActionResult Ekle()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "KategoriAdi");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Ekle(Blog gelenblog, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(ResimURL.InputStream);
                //FileInfo imginfo = new FileInfo(ResimURL.FileName);
                string imgname = ResimURL.FileName;
                img.Resize(800, 450);
                img.Save("~/Upload/Blog/" + imgname);
                gelenblog.ResimURL = "/Upload/Blog/" + imgname;
                db.Blog.Add(gelenblog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Yazı Eklenemedi");
            return View(gelenblog);
        }
        public ActionResult Duzenle(int? id)
        {
            
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var blog = db.Blog.Find(id);
            if (blog == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "KategoriAdi",blog.KategoriId);
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Duzenle(int id,Blog gelenblog,HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var blog = db.Blog.Find(id);
                if (blog == null)
                {
                    return RedirectToAction("Index");
                }
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(gelenblog.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(gelenblog.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);
                    string imgname = ResimURL.FileName;
                    img.Resize(800,450);
                    img.Save("~/Upload/Blog/"+imgname);
                    blog.ResimURL = "/Upload/Blog/" + imgname;
                }
                blog.Baslik = gelenblog.Baslik;
                blog.İcerik = gelenblog.İcerik;
                blog.KategoriId = gelenblog.KategoriId;
                db.SaveChanges();
               return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "KategoriAdi",gelenblog.KategoriId);
            ModelState.AddModelError("", "Yazı Güncellenemedi");
            return View(gelenblog);
        }
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var blog = db.Blog.Find(id);
            if (blog == null)
            {
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        [HttpPost,ActionName("Sil")]
        public ActionResult SilOnay(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var blog = db.Blog.Find(id);
            if (blog == null)
            {
                return RedirectToAction("Index");
            }
            if (System.IO.File.Exists(Server.MapPath(blog.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(blog.ResimURL));
            }
            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}