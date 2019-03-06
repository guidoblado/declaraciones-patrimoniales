using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.Models
{
    public class VehiculoModel
    {
        public string Marca { get; set; }

        [Display(Name = "Tipo Vehículo")]
        public string TipoVehiculo { get; set; }

        [Display(Name = "Año")]
        public int Anio { get; set; }

        [Display(Name = "Valor Aproximado")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorAproximado { get; set; }

        [Display(Name = "Saldo Deudor")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SaldoDeudor { get; set; }
    }
}
