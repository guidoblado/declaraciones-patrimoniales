using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.Models
{
    public class ReglaAlertaModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "La gestion es requerida")]
        public int Gestion { get;  set; }
        [Required(ErrorMessage = "La descripcion de la alerta es requerida")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El Monto es requerido")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Monto Inválido")]
        public decimal Monto { get; set; }

        public string Operador { get; set; }

        [Required(ErrorMessage = "El porcentaje es requerido")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Monto Inválido")]
        public decimal Porcentaje { get; set; }
    }
}