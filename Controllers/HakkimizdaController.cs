using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HakkimizdaController : Controller
    {
        KurumsalDbContext db = new KurumsalDbContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            return View(db.Hakkimizda.ToList());
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var hakkimizda = db.Hakkimizda.Where(i => i.Id == id).FirstOrDefault();
            if (hakkimizda == null)
            {
                return RedirectToAction("Index");
            }
            return View(hakkimizda);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Duzenle(int id,Hakkimizda model)
        {
            var hakkimizda = db.Hakkimizda.Where(i => i.Id == id).FirstOrDefault();
            if (hakkimizda == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                hakkimizda.Aciklama = model.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}