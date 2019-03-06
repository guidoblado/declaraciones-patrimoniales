using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebUI.Models
{
    public class AlertaGeneralModel
    {
        [Key]
        public Guid DeclaracionID { get; set; }
        public int FuncionarioID { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        public string CodCargo { get; set; }

        public string Cargo { get; set; }

        public string CodArea { get; set; }

        [Display(Name = "Área")]
        public string Area { get; set; }

        public string CodUbicacionGeografica { get; set; }

        [Display(Name = "Ubicación Geográfica")]
        public string UbicacionGeografica { get; set; }

        public string CodCentroDeCosto { get; set; }

        [Display(Name = "Centro de Costo")]
        public string CentroDeCosto { get; set; }

        public int TipoRol { get; set; }

        public string Rol { get; set; }

        [Display(Name = "Estado")]
        public int EstadoDeclaracion { get; set; }

        [Display(Name = "Patrimonio Actual")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal PatrimonioActual { get; set; }

        [Display(Name = "Patrimonio Anterior")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal PatrimonioGestionAnterior { get; set; }

        [Display(Name = "Dif. Patrimonio")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DiferenciaPatrimonio { get; set; }

        [Display(Name = "Variacion Porcentual")]
        public decimal VariacionPorcentual { get; set; }

        public Guid DeclaracionAnteriorID { get; set; }
    }
}
