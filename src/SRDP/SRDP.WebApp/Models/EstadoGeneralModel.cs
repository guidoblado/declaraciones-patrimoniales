using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.Models
{
    public class EstadoGeneralModel
    {
        [Key]
        public Guid DeclaracionID { get; set; }

        [Display(Name = "Codigo")]
        public int FuncionarioID { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        public string Cargo { get; set; }

        [Display(Name = "Área")]
        public string Area { get; set; }

        [Display(Name = "Ubicación Geográfica")]
        public string UbicacionGeografica { get; set; }

        [Display(Name = "Centro de Costo")]
        public string CentroCosto { get; set; }

        [Display(Name = "Rol")]
        public string Rol { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDeclaracion { get; set; }
    }
}
