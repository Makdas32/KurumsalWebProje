using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Kategori
    {
        public int Id { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 karakterden fazla girilemez")]
        public string KategoriAdi { get; set; }
        public string Aciklama { get; set; }

        public List<Blog> Bloglar { get; set; }
    }
}