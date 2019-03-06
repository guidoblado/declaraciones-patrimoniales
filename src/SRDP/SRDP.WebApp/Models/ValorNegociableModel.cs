using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.Models
{
    public class ValorNegociableModel
    {
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Tipo Valor")]
        public string TipoValor { get; set; }

        [Display(Name = "Valor Aproximado")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorAproximado { get; private set; }
    }
}
