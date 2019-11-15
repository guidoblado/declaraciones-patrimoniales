using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.Models
{
    public class AlertaIndividualModel
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

        [Display(Name = "Estado Declaracion")]
        public int EstadoDeclaracion { get; set; }

        [Display(Name = "Inmuebles Actual")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal InmueblesPatrimonioActual { get; set; }

        [Display(Name = "Inmuebles Anterior")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal InmueblesPatrimonioGestionAnterior { get; set; }

        [Display(Name = "Inmuebles Dif. Patrimonio")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal InmueblesDiferenciaPatrimonio { get; set; }

        [Display(Name = "Inmuebles Variacion Porcentual")]
        public decimal InmueblesVariacionPorcentual { get; set; }

        [Display(Name = "OtrosIngresos Actual")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OtrosIngresosPatrimonioActual { get; set; }

        [Display(Name = "OtrosIngresos Anterior")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OtrosIngresosPatrimonioGestionAnterior { get; set; }

        [Display(Name = "OtrosIngresos Dif. Patrimonio")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OtrosIngresosDiferenciaPatrimonio { get; set; }

        [Display(Name = "OtrosIngresos Variacion Porcentual")]
        public decimal OtrosIngresosVariacionPorcentual { get; set; }

        [Display(Name = "Depositos Actual")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DepositosPatrimonioActual { get; set; }

        [Display(Name = "Depositos Anterior")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DepositosPatrimonioGestionAnterior { get; set; }

        [Display(Name = "Depositos Dif. Patrimonio")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DepositosDiferenciaPatrimonio { get; set; }

        [Display(Name = "Depositos Variacion Porcentual")]
        public decimal DepositosVariacionPorcentual { get; set; }

        [Display(Name = "Deuda Bancaria Actual")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DeudaBancariaPatrimonioActual { get; set; }

        [Display(Name = "Deuda Bancaria Anterior")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DeudaBancariaPatrimonioGestionAnterior { get; set; }

        [Display(Name = "Deuda Bancaria Dif. Patrimonio")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DeudaBancariaDiferenciaPatrimonio { get; set; }

        [Display(Name = "Deuda Bancaria Variacion Porcentual")]
        public decimal DeudaBancariaVariacionPorcentual { get; set; }

        [Display(Name = "Vehiculos Actual")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal VehiculosPatrimonioActual { get; set; }

        [Display(Name = "Vehiculos Anterior")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal VehiculosPatrimonioGestionAnterior { get; set; }

        [Display(Name = "Vehiculos Dif. Patrimonio")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal VehiculosDiferenciaPatrimonio { get; set; }

        [Display(Name = "Vehiculos Variacion Porcentual")]
        public decimal VehiculosVariacionPorcentual { get; set; }

        [Display(Name = "Valores Negociables Actual")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValoresNegociablesPatrimonioActual { get; set; }

        [Display(Name = "Valores Negociables Anterior")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValoresNegociablesPatrimonioGestionAnterior { get; set; }

        [Display(Name = "ValoresValoresNegociablesNegociables Dif. Patrimonio")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValoresNegociablesDiferenciaPatrimonio { get; set; }

        [Display(Name = "Valores Negociables Variacion Porcentual")]
        public decimal ValoresNegociablesVariacionPorcentual { get; set; }

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