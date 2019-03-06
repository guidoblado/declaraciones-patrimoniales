using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebUI.Models
{
    public class OtroIngresoModel
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }

        [Required]
        public string Concepto { get; set; }

        [Display(Name = "Ingreso Menusal $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal IngresoMensual { get; set; }
    }
}
