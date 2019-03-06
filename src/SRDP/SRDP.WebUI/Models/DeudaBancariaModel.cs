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

        [Required]
        [Display(Name = "Institución Financiera")]
        public string InstitucionFinanciera { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Monto { get; set; }

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
