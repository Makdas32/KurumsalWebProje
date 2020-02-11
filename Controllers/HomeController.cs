using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _SliderPartial()
        {
            return PartialView(db.Slider.ToList());
        }

        public PartialViewResult _HizmetPartial()
        {
            return PartialView(db.Hizmet.ToList().OrderByDescending(x => x.Id));
        }

        public PartialViewResult _FooterPartial()
        {
            ViewBag.İletisim = db.İletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList();
            ViewBag.Hizmet = db.Hizmet.ToList();
            return PartialView();
        }

        public ActionResult Hakkimizda()
        {
            return View(db.Hakkimizda.FirstOrDefault());
        }

        public ActionResult Hizmetlerimiz()
        {
            return View();
        }
        public ActionResult İletisim()
        {
            return View(db.İletisim.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult İletisim(string ad=null,string email= null,string baslik = null,string mesaj = null)
        {
            if (ad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "mailgelecek@gmail.com";
                WebMail.Password = "mailinsifresigelecek";
                WebMail.SmtpPort = 587;
                WebMail.Send("mailgelecek@gmail.com",baslik,email+"Mesaj İçeriği: "+mesaj);
                ViewBag.Basarili = "Mail Gönderildi";
            }
            else
            {
                ViewBag.Basarisiz = "Mail Gönderilemedi";
            }
            return View();
        }

        public ActionResult Blog(int sayfa=1)
        {
            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.Id).ToPagedList(sayfa,2));
        }

        public PartialViewResult _SagKisimPartial()
        {
            ViewBag.Kategori = db.Kategori.Include("Bloglar").ToList();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.Id);
            return PartialView();
        }

        public ActionResult BlogDetay(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var blog = db.Blog.Include("Kategori").Include("Yorumlar").Where(x => x.Id == id).FirstOrDefault();
            return View(blog);
        }

        //public JsonResult YorumYap(string adsoyad,string email,string icerik,int blogid)
        //{
        //    if (icerik == null)
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //    db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Email = email, İcerik = icerik, BlogId = blogid, Onay = false });
        //    db.SaveChanges();
        //    return Json(false, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int blogid)
        {
            if (icerik == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Email = eposta, İcerik = icerik, BlogId = blogid, Onay = false });
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}