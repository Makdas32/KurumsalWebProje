using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Slider Başlık"),Required,StringLength(30,ErrorMessage = "30 Karakterden Fazla Olamaz")]
        public string Baslik { get; set; }
        [DisplayName("Slider Açıklama"), Required, StringLength(150, ErrorMessage = "150 Karakterden Fazla Olamaz")]
        public string Aciklama { get; set; }
        [DisplayName("Slider Resim"), Required, StringLength(250)]
        public string Resim { get; set; }
    }
}