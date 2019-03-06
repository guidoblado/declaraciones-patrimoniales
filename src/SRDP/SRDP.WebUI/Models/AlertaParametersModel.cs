using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebUI.Models
{
    public class AlertaParametersModel
    {
        [Required(ErrorMessage = "El Monto es requerido")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Monto Inválido")]
        public decimal Monto { get; set; }

        public string Operador { get; set; }

        [Required(ErrorMessage = "El porcentaje es requerido")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Monto Inválido")]
        public decimal Porcentaje { get; set; }
    }
}
