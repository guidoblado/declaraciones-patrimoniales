using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SRDP.WebUI.DataValidations;

namespace SRDP.WebUI.Models
{
    public class InmuebleModel
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe introducir la dirección del Inmueble")]
        public string Direccion { get; set; }

        [Display(Name = "Tipo de Inmueble")]
        [Required(ErrorMessage = "Seleccione un tipo de inmueble")]
        public string TipoDeInmueble { get; set; }
        public IEnumerable<SelectListItem> TiposDeInmuebles { get; set; }

        [Display(Name = "% Participación")]
        [Required(ErrorMessage = "el porcentaje de participación es requerido")]
        [DisplayFormat(DataFormatString = "{0:P1}")]
        [Range(1, 100, ErrorMessage = "Debe ingresar un valor entre 1 y 100")]
        public decimal PorcentajeParticipacion { get; set; }

        [Display(Name ="Valor Comercial $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un valor mayor a cero")]
        public decimal ValorComercial { get; set; }

        [Display(Name ="Saldo Hipoteca $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SaldoHipoteca { get; set; }

        [Display(Name = "Banco")]
        [RequiredOnDecimalPropertyValue("SaldoHipoteca", ErrorMessage = "El Banco es requerido cuando Saldo Hipoteca tiene valor")]
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
