using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.Models
{
    public class InmuebleModel
    {
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Tipo de Inmueble")]
        public string TipoDeInmueble { get; set; }

        [Display(Name = "% Participación")]
        [DisplayFormat(DataFormatString = "{0:P1}")]
        public decimal PorcentajeParticipacion { get; set; }

        [Display(Name ="Valor Comercial")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorComercial { get; set; }

        [Display(Name ="Saldo Hipoteca")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SaldoHipoteca { get; set; }
    }
}
