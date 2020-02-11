using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class İletisimController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();

        // GET: İletisim
        public ActionResult Index()
        {
            return View(db.İletisim.ToList());
        }
    
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return Redirect("Index");
            }
            İletisim İletisim = db.İletisim.Find(id);
            if (İletisim == null)
            {
                return Redirect("Index");
            }
            return View(İletisim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle([Bind(Include = "Id,Adres,Telefon,Fax,Whatsapp,Facebook,Twitter,Instagram")] İletisim İletisim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(İletisim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(İletisim);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
