using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.ModelViews
{
    public class NuevaGestionModelView
    {
        [Display(Name = "Año Gestión Vigente")]
        public int AnioVigente { get; set; }

        public GestionModel GestionNueva { get; set; }
    }
}