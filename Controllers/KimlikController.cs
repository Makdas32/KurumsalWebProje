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
    public class KimlikController : Controller
    {
        KurumsalDbContext db = new KurumsalDbContext();
        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
        }

        // GET: Kimlik/Edit/5
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var kimlik = db.Kimlik.Where(i => i.Id == id).FirstOrDefault();
            if (kimlik == null)
            {
                return RedirectToAction("Index");
            }
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Duzenle(int? id, Kimlik gelenkimlik, HttpPostedFileBase LogoUrl)
        {
                if (ModelState.IsValid)
                {
                    var kimlik = db.Kimlik.Where(i => i.Id == id).FirstOrDefault();

                    if (LogoUrl != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(kimlik.LogoUrl))) //Logourl de dosya varmı
                        {
                            System.IO.File.Delete(Server.MapPath(kimlik.LogoUrl));
                        }
                        WebImage img = new WebImage(LogoUrl.InputStream);
                        FileInfo imginfo = new FileInfo(LogoUrl.FileName);
                        string logoname = LogoUrl.FileName;
                        img.Resize(300, 200);
                        img.Save("~/Upload/Kimlik/" + logoname);
                        kimlik.LogoUrl = "/Upload/Kimlik/" + logoname;
                    }
                    kimlik.Title = gelenkimlik.Title;
                    kimlik.Description = gelenkimlik.Description;
                    kimlik.Keywords = gelenkimlik.Keywords;
                    kimlik.Unvan = gelenkimlik.Unvan;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }           

            ModelState.AddModelError("", "İşlem Gerçekleştirilemedi");
            return View(gelenkimlik);
        }
    }
}
