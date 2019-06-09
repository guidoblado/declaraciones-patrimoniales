using SRDP.WebUI.DataValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebUI.Models
{
    public class VehiculoModel
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        public string Marca { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "El Modelo es requerido")]
        public string TipoVehiculo { get; set; }

        [Display(Name = "Año")]
        public int Anio { get; set; }

        [Display(Name = "Valor Comercial Actual $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un valor mayor a cero")]
        public decimal ValorAproximado { get; set; }

        [Display(Name = "Monto actual del saldo del préstamo vehicular")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SaldoDeudor { get; set; }

        [RequiredOnDecimalPropertyValue("SaldoDeudor", ErrorMessage = "El Banco es requerido cuando el Monto de préstamo tiene valor")]
        public string Banco { get; set; }
    }
}
