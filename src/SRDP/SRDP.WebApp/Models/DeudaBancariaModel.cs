using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.Models
{
    public class DeudaBancariaModel
    {
        [Display(Name = "Institución Financiera")]
        public string InstitucionFinanciera { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Monto { get; set; }
    }
}
