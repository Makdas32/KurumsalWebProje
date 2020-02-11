using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        KurumsalDbContext db = new KurumsalDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(Admin model)
        {
            var kullanici = db.Admin.Where(x => x.Eposta == model.Eposta && x.Sifre == model.Sifre).FirstOrDefault();
            if (kullanici != null)
            {
                Session["Id"] = model.Id;
                Session["Eposta"] = model.Eposta;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Uyari = "Hatalı Eposta veya Şifre";
            return View(model);
        }

        public ActionResult Cikis()
        {
            Session["Id"] = null;
            Session["Eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Giris","Admin");
        }
    }
}