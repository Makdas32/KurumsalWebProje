using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Kimlik
    {
        public int Id { get; set; }
        [Required,StringLength(100,ErrorMessage ="100 karakterden fazla girilemez")]
        [DisplayName("Site Başlık")]
        public string Title { get; set; }
        [Required, StringLength(200, ErrorMessage = "200 karakterden fazla girilemez")]
        [DisplayName("Anahtar Kelimeler")]
        public string Keywords { get; set; }
        [Required, StringLength(300, ErrorMessage = "300 karakterden fazla girilemez")]
        [DisplayName("Site Açıklaması")]
        public string Description { get; set; }
        [DisplayName("Site Logosu")]
        public string LogoUrl { get; set; }
        [DisplayName("Site Ünvan")]
        public string Unvan { get; set; }
    }
}