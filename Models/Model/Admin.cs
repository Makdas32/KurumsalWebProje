using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 karakterden fazla olamaz")]
        [DisplayName("Email")]
        public string Eposta { get; set; }
        [Required, StringLength(20, ErrorMessage = "20 karakterden fazla olamaz")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }
        public string Yetki { get; set; }
    }
}