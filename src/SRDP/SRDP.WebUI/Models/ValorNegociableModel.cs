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
        [Required]
        public string Descripcion { get; set; }

        [Display(Name = "Tipo Valor")]
        [Required]
        public string TipoValor { get; set; }

        [Display(Name = "Valor Aproximado $us")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorAproximado { get; set; }
    }
}
