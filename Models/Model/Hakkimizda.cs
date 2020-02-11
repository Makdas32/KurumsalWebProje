using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Hakkimizda
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
    }
}