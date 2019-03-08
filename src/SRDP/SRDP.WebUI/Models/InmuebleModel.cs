using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRDP.WebUI.Models
{
    public class InmuebleModel
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Tipo de Inmueble")]
        public string TipoDeInmueble { get; set; }
        public IEnumerable<SelectListItem> TiposDeInmuebles { get; set; }

        [Display(Name = "% Participación")]
        [DisplayFormat(DataFormatString = "{0:P1}")]
        [RegularExpression(@"^\d+[.]?\d*%?$", ErrorMessage = "El valor de porcentaje introducido ne es válido")]
        public decimal PorcentajeParticipacion { get; set; }

        [Display(Name ="Valor Comercial $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorComercial { get; set; }

        [Display(Name ="Saldo Hipoteca $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SaldoHipoteca { get; set; }

        [Display(Name = "Banco")]
        public string Banco { get; set; }

        public static IEnumerable<SelectListItem> GetTiposDeInmuebles()
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "Casa", Value = "Casa" });
            result.Add(new SelectListItem { Text = "Departamento", Value = "Departamento" });
            result.Add(new SelectListItem { Text = "Terreno", Value = "Terreno" });
            result.Add(new SelectListItem { Text = "Local Comercial - Deposito", Value = "Local Comercial - Deposito" });
            result.Add(new SelectListItem { Text = "Casa de Campo - Hacienda", Value = "Cas de Campo - Hacienda" });
            return result;
        }
    }
}
