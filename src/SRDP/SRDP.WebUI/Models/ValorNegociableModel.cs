using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebUI.Models
{
    public class ValorNegociableModel
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debe ingresar la Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Tipo Valor")]
        [Required(ErrorMessage = "Debe ingresar Tipo")]
        public string TipoValor { get; set; }

        [Display(Name = "Valor Aproximado $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un monto mayor a cero")]
        public decimal ValorAproximado { get; set; }
    }
}
