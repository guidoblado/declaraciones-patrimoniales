using SRDP.Application.SearchParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SRDP.Application;
using SRDP.Domain.Reportes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SRDP.WebApp.Models
{
    public class HistoricoIndividualModel
    {
        public int FuncionarioID { get; private set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; private set; }

        public string Cargo { get; private set; }

        public string Area { get; private set; }

        [Display(Name = "Ubicación Geográfica")]
        public string UbicacionGeografica { get; private set; }

        [Display(Name = "Centro de Costo")]
        public string CentroCosto { get; private set; }
        //Deposito
        public Guid DepositoDeclaracionIDAnterior { get; private set; }
        public Guid DepositoDeclaracionIDActual { get; private set; }
        [Display(Name = "Gestion Anterior", GroupName = "Deposito")]
        public int DepositoGestionAnterior { get; private set; }
        [Display(Name = "Monto Gestion Anterior", GroupName = "Deposito")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DepositoMontoGestionAnterior { get; private set; }
        [Display(Name = "Gestion Actual", GroupName = "Deposito")]
        public int DepositoGestionActual { get; private set; }
        [Display(Name = "Monto Gestion Actual", GroupName = "Deposito")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DepositoMontoGestionActual { get; private set; }
        //Inmueble
        public Guid InmuebleDeclaracionIDAnterior { get; private set; }
        public Guid InmuebleDeclaracionIDActual { get; private set; }
        public int InmuebleGestionAnterior { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal InmuebleMontoGestionAnterior { get; private set; }
        public int InmuebleGestionActual { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal InmuebleMontoGestionActual { get; private set; }
        //Vehiculo
        public Guid VehiculoDeclaracionIDAnterior { get; private set; }
        public Guid VehiculoDeclaracionIDActual { get; private set; }
        public int VehiculoGestionAnterior { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal VehiculoMontoGestionAnterior { get; private set; }
        public int VehiculoGestionActual { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal VehiculoMontoGestionActual { get; private set; }
        //OtroIngreso
        public Guid OtroIngresoDeclaracionIDAnterior { get; private set; }
        public Guid OtroIngresoDeclaracionIDActual { get; private set; }
        public int OtroIngresoGestionAnterior { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OtroIngresoMontoGestionAnterior { get; private set; }
        public int OtroIngresoGestionActual { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OtroIngresoMontoGestionActual { get; private set; }
        //DeudaBancaria
        public Guid DeudaBancariaDeclaracionIDAnterior { get; private set; }
        public Guid DeudaBancariaDeclaracionIDActual { get; private set; }
        public int DeudaBancariaGestionAnterior { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DeudaBancariaMontoGestionAnterior { get; private set; }
        public int DeudaBancariaGestionActual { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DeudaBancariaMontoGestionActual { get; private set; }
        
    }
}
