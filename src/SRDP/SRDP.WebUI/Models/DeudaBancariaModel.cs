using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRDP.WebUI.Models
{
    public class DeudaBancariaModel
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }

        [Required(ErrorMessage = "Debe ingresar la Institución Financiera")]
        [Display(Name = "Institución Financiera")]
        public string InstitucionFinanciera { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Monto $us (Monto actual de saldo adeudado)")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un monto mayor a cero")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de deuda")]
        public string Tipo { get; set; }
        public IEnumerable<SelectListItem> TiposDeDeuda { get; set; }

        public static IEnumerable<SelectListItem> GetTiposDeuda()
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "Tarjeta de Crédito", Value = "Tarjeta de Crédito" });
            result.Add(new SelectListItem { Text = "Crédito de Consumo", Value = "Crédito de Consumo" });
            return result;
        }
    }
}
