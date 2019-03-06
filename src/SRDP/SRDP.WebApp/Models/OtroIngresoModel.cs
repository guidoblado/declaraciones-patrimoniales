using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.Models
{
    public class OtroIngresoModel
    {
        public string Concepto { get; set; }

        [Display(Name = "Ingreso Menusal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal IngresoMensual { get; set; }
    }
}
