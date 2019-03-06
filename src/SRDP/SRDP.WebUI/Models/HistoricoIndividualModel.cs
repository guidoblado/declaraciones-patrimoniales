using SRDP.Application.SearchParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SRDP.Application;
using SRDP.Domain.Reportes;
using System.ComponentModel.DataAnnotations;

namespace SRDP.WebUI.Models
{
    public class HistoricoIndividualModel
    {
        public int FuncionarioID { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        public string Cargo { get; set; }

        public string Area { get; set; }

        [Display(Name = "Ubicación Geográfica")]
        public string UbicacionGeografica { get; set; }

        [Display(Name = "Centro de Costo")]
        public string CentroCosto { get; set; }
        //Deposito
        public Guid DepositoDeclaracionIDAnterior { get; set; }
        public Guid DepositoDeclaracionIDActual { get; set; }
        [Display(Name = "Gestion Anterior", GroupName = "Deposito")]
        public int DepositoGestionAnterior { get; set; }
        [Display(Name = "Monto Gestion Anterior", GroupName = "Deposito")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DepositoMontoGestionAnterior { get; set; }
        [Display(Name = "Gestion Actual", GroupName = "Deposito")]
        public int DepositoGestionActual { get; set; }
        [Display(Name = "Monto Gestion Actual", GroupName = "Deposito")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DepositoMontoGestionActual { get; set; }
        //Inmueble
        public Guid InmuebleDeclaracionIDAnterior { get; set; }
        public Guid InmuebleDeclaracionIDActual { get; set; }
        public int InmuebleGestionAnterior { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal InmuebleMontoGestionAnterior { get; set; }
        public int InmuebleGestionActual { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal InmuebleMontoGestionActual { get; set; }
        //Vehiculo
        public Guid VehiculoDeclaracionIDAnterior { get; set; }
        public Guid VehiculoDeclaracionIDActual { get; set; }
        public int VehiculoGestionAnterior { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal VehiculoMontoGestionAnterior { get; set; }
        public int VehiculoGestionActual { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal VehiculoMontoGestionActual { get; set; }
        //OtroIngreso
        public Guid OtroIngresoDeclaracionIDAnterior { get; set; }
        public Guid OtroIngresoDeclaracionIDActual { get; set; }
        public int OtroIngresoGestionAnterior { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OtroIngresoMontoGestionAnterior { get; set; }
        public int OtroIngresoGestionActual { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OtroIngresoMontoGestionActual { get; set; }
        //DeudaBancaria
        public Guid DeudaBancariaDeclaracionIDAnterior { get; set; }
        public Guid DeudaBancariaDeclaracionIDActual { get; set; }
        public int DeudaBancariaGestionAnterior { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DeudaBancariaMontoGestionAnterior { get; set; }
        public int DeudaBancariaGestionActual { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DeudaBancariaMontoGestionActual { get; set; }
        
    }
}
