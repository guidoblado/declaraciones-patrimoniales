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

        [Required(ErrorMessage = "Debe ingresar un concepto")]
        public string Concepto { get; set; }

        [Display(Name = "Ingreso Menusal $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un monto mayor a cero")]
        public decimal IngresoMensual { get; set; }
    }
}
