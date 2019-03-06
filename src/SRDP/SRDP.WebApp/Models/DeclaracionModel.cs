using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.Models
{
    public class DeclaracionModel
    {
        [Key]
        public Guid DeclaracionID { get; set; }

        public int Gestion { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Display(Name = "C.I.")]
        public string CedulaIdentidad { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; }

        [Display(Name = "Fecha de llenado")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        public DateTime FechaLlenado { get; set; }

        [Display(Name = "Patrimonio Neto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal PatrimonioNeto { get; set; }

        public IList<DepositoModel> Depositos { get; set; }
        public IList<DeudaBancariaModel> DeudasBancarias { get; set; }
        public IList<InmuebleModel> Inmuebles { get; set; }
        public IList<OtroIngresoModel> OtrosIngresos { get; set; }
        public IList<ValorNegociableModel> ValoresNegociables { get; set; }
        public IList<VehiculoModel> Vehiculos { get; set; }
    }
}
