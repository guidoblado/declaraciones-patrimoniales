using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.Models
{
    public class DeclaracionResumenModel
    {

        [Key]
        public Guid DeclaracionID { get; set; }

        public int Gestion { get; set; }

        [Display(Name = "Estado Declaración")]
        public string Estado { get; set; }

        public IList<DeclaracionResumenModel> DeclaracionesAnteriores { get; set; }

    }
}