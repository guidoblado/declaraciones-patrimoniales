using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.Models
{
    public class GestionModel
    {
        [Display(Name = "Gestión")]
        public int Gestion { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "Debe ingresar la fecha inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha Final")]
        [Required(ErrorMessage = "Debe ingresar la fecha final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }

        public bool Vigente { get; set; }

    }
}