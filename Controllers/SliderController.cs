using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class SliderController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();

        // GET: Slider
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Ekle(Slider model, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(Resim.InputStream);
                FileInfo imginfo = new FileInfo(Resim.FileName);
                string imgname = Resim.FileName;
                img.Resize(1024, 360);
                img.Save("~/Upload/Slider/" + imgname);
                model.Resim = "/Upload/Slider/" + imgname;
                db.Slider.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Slider Eklenemedi");
            return View(model);
        }

        public ActionResult Duzenle(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var slider = db.Slider.Find(id);
            if (slider == null)
            {
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Duzenle(int id, Slider model, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                var slider = db.Slider.Find(id);
                if (slider == null)
                {
                    return RedirectToAction("Index");
                }
                if (Resim != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(model.Resim)))
                    {
                        System.IO.File.Delete(Server.MapPath(model.Resim));
                    }
                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo imginfo = new FileInfo(Resim.FileName);
                    string imgname = Resim.FileName;
                    img.Resize(1024, 360);
                    img.Save("~/Upload/Slider/" + imgname);
                    slider.Resim = "/Upload/Slider/" + imgname;
                }
                slider.Baslik = model.Baslik;
                slider.Aciklama = model.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Slider Güncellenemedi");
            return View(model);
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var slider = db.Slider.Find(id);
            if (slider == null)
            {
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        [HttpPost, ActionName("Sil")]
        public ActionResult SilOnay(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var slider = db.Slider.Find(id);
            if (slider == null)
            {
                return RedirectToAction("Index");
            }
            if (System.IO.File.Exists(Server.MapPath(slider.Resim)))
            {
                System.IO.File.Delete(Server.MapPath(slider.Resim));
            }
            db.Slider.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}