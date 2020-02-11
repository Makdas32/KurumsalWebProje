using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Yorum
    {
        public int Id { get; set; }
        [Required,StringLength(30,ErrorMessage ="30 karakterden fazla olamaz")]
        public string AdSoyad { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 karakterden fazla olamaz"),EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(200, ErrorMessage = "200 karakterden fazla olamaz")]
        [DisplayName("Yorumunuz")]
        public string İcerik { get; set; }
        public bool Onay { get; set; }

        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}