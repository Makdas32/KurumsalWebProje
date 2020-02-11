using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class İletisim
    {
        public int Id { get; set; }
        [StringLength(250,ErrorMessage ="250 karakterden fazla olamaz")]
        public string Adres { get; set; }
        [StringLength(50, ErrorMessage = "50 karakterden fazla olamaz")]
        public string Telefon { get; set; }
        [StringLength(50, ErrorMessage = "50 karakterden fazla olamaz")]
        public string Fax { get; set; }
        [StringLength(50, ErrorMessage = "50 karakterden fazla olamaz")]
        public string Whatsapp { get; set; }
        [StringLength(50, ErrorMessage = "50 karakterden fazla olamaz")]
        public string Facebook { get; set; }
        [StringLength(50, ErrorMessage = "50 karakterden fazla olamaz")]
        public string Twitter { get; set; }
        [StringLength(50, ErrorMessage = "50 karakterden fazla olamaz")]
        public string Instagram { get; set; }
    }
}