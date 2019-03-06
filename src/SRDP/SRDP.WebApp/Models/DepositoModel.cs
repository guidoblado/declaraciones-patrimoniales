using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.Models
{
    [Serializable]
    public class DepositoModel
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }

        [Display(Name = "Institución")]
        public string Institucion { get; set; }

        [Display(Name = "Tipo de Cuenta")]
        [Required(ErrorMessage = "El tipo de cuenta es requerido")]
        public string TipoDeCuenta { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "El Saldo es requerido")]
        [Range(10000, 10000000, ErrorMessage = "El saldo debe ser de minimo $ 10,000.00")]
        public decimal Saldo { get; set; }
    }
}
