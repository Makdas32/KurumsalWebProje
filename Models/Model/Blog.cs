using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Blog
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string İcerik { get; set; }
        public string ResimURL { get; set; }


        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }

        public List<Yorum> Yorumlar { get; set; }
    }
}