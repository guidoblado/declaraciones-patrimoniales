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
        [Required(ErrorMessage = "Debe ingresar la gestión")]
        public int Gestion { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "Debe ingresar la fecha inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha Final")]
        [Required(ErrorMessage = "Debe ingresar la fecha final")]
        public DateTime FechaFinal { get; set; }

        public bool Vigente { get; set; }

    }
}