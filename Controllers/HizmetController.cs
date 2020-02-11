using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HizmetController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();
        // GET: Hizmet
        public ActionResult Index()
        {
            return View(db.Hizmet.ToList());
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var hizmet = db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return RedirectToAction("Index");
            }
            return View(hizmet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle([Bind(Include = "Id,Baslik,Aciklama,ResimURL")] Hizmet gelenhizmet,HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(gelenhizmet.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(gelenhizmet.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);
                    string imgname = ResimURL.FileName;
                    img.Resize(800,600);
                    img.Save("~/Upload/Hizmet/"+imgname);
                    gelenhizmet.ResimURL = "/Upload/Hizmet/" + imgname;
                }
                db.Entry(gelenhizmet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gelenhizmet);
        }

        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Hizmet hizmet,HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);
                var imgname = ResimURL.FileName;
                img.Resize(800,600);
                img.Save("~/Upload/Hizmet/"+imgname);
                hizmet.ResimURL = "/Upload/Hizmet/" + imgname;
                db.Hizmet.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Hizmet Eklenemedi");
            return View(hizmet);
        }

        public ActionResult Sil(int? id)
        {

            var hizmet = db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return RedirectToAction("Index");
            }
            return View(hizmet);
        }
        [HttpPost,ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public ActionResult SilOnay(int? id)
        {
            var hizmet = db.Hizmet.Find(id);
            if (System.IO.File.Exists(Server.MapPath(hizmet.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(hizmet.ResimURL));
            }
            db.Hizmet.Remove(hizmet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}