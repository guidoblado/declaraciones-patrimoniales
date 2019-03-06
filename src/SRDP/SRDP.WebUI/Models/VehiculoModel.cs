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
        public string TipoVehiculo { get; set; }

        [Display(Name = "Año")]
        public int Anio { get; set; }

        [Display(Name = "Valor Aproximado $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorAproximado { get; set; }

        [Display(Name = "Saldo Deudor $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SaldoDeudor { get; set; }

        public string Banco { get; set; }
    }
}
