using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class AlertaIndividualOutput
    {
        public Guid DeclaracionID { get; private set; }
        public int FuncionarioID { get; private set; }
        public string NombreCompleto { get; private set; }
        public string CodCargo { get; private set; }
        public string Cargo { get; private set; }
        public string CodArea { get; private set; }
        public string Area { get; private set; }
        public string CodUbicacionGeografica { get; private set; }
        public string UbicacionGeografica { get; private set; }
        public string CodCentroDeCosto { get; private set; }
        public string CentroDeCosto { get; private set; }
        public int TipoRol { get; private set; }
        public string Rol { get; private set; }
        public int EstadoDeclaracion { get; private set; }

        public decimal InmueblesPatrimonioActual { get; set; }
        public decimal InmueblesPatrimonioGestionAnterior { get; private set; }
        public decimal InmueblesDiferenciaPatrimonio { get; private set; }
        public decimal InmueblesVariacionPorcentual { get; private set; }

        public decimal OtrosIngresosPatrimonioActual { get; private set; }
        public decimal OtrosIngresosPatrimonioGestionAnterior { get; private set; }
        public decimal OtrosIngresosDiferenciaPatrimonio { get; private set; }
        public decimal OtrosIngresosVariacionPorcentual { get; private set; }

        public decimal DepositosPatrimonioActual { get; private set; }
        public decimal DepositosPatrimonioGestionAnterior { get; private set; }
        public decimal DepositosDiferenciaPatrimonio { get; private set; }
        public decimal DepositosVariacionPorcentual { get; private set; }

        public decimal DeudaBancariaPatrimonioActual { get; private set; }
        public decimal DeudaBancariaPatrimonioGestionAnterior { get; private set; }
        public decimal DeudaBancariaDiferenciaPatrimonio { get; private set; }
        public decimal DeudaBancariaVariacionPorcentual { get; private set; }

        public decimal VehiculosPatrimonioActual { get; private set; }
        public decimal VehiculosPatrimonioGestionAnterior { get; private set; }
        public decimal VehiculosDiferenciaPatrimonio { get; private set; }
        public decimal VehiculosVariacionPorcentual { get; private set; }

        public decimal ValoresNegociablesPatrimonioActual { get; private set; }
        public decimal ValoresNegociablesPatrimonioGestionAnterior { get; private set; }
        public decimal ValoresNegociablesDiferenciaPatrimonio { get; private set; }
        public decimal ValoresNegociablesVariacionPorcentual { get; private set; }

        public decimal PatrimonioActual { get; private set; }
        public decimal PatrimonioGestionAnterior { get; private set; }
        public decimal DiferenciaPatrimonio { get; private set; }
        public decimal VariacionPorcentual { get; private set; }

        public Guid DeclaracionAnteriorID { get; private set; }

        private AlertaIndividualOutput() { }

        public AlertaIndividualOutput(Guid declaracionID, int funcionarioID, string nombreCompleto, string codCargo, string cargo, string codArea, string area,
            string codUbicacionGeografica, string ubicacionGeografica, string codCentroCosto, string centroDeCosto, int tipoRol, string rol, int estadoDeclaracion,
            Guid declaracionAnteriorID)
        {
            DeclaracionID = declaracionID;
            FuncionarioID = funcionarioID;
            NombreCompleto = nombreCompleto;
            CodCargo = codCargo;
            Cargo = cargo;
            CodArea = codArea;
            Area = area;
            CodUbicacionGeografica = codUbicacionGeografica;
            UbicacionGeografica = ubicacionGeografica;
            CodCentroDeCosto = codCentroCosto;
            CentroDeCosto = centroDeCosto;
            TipoRol = tipoRol;
            Rol = rol;
            EstadoDeclaracion = estadoDeclaracion;
            DeclaracionAnteriorID = declaracionAnteriorID;
        }

        public AlertaIndividualOutput(Guid declaracionID, Guid declaracionAnteriorID, int estadoDeclaracion, Colaborador funcionario)
        {
            DeclaracionID = declaracionID;
            FuncionarioID = funcionario.FuncionarioID;
            NombreCompleto = funcionario.NombreCompleto.ToString();
            CodCargo = funcionario.CodCargo;
            Cargo = funcionario.Cargo;
            CodArea = funcionario.CodArea;
            Area = funcionario.Area;
            CodUbicacionGeografica = funcionario.CodUbicacionGeografica;
            UbicacionGeografica = funcionario.UbicacionGeografica;
            CodCentroDeCosto = funcionario.CodCentroDeCosto;
            CentroDeCosto = funcionario.CentroDeCosto;
            TipoRol = funcionario.TipoRol;
            Rol = funcionario.Rol;
            EstadoDeclaracion = estadoDeclaracion;
            DeclaracionAnteriorID = declaracionAnteriorID;
        }

        public void SetMontosInmuebles(decimal patrimonioActual, decimal patrimonioGestionAnterior,
            decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            InmueblesPatrimonioActual = patrimonioActual;
            InmueblesPatrimonioGestionAnterior = patrimonioGestionAnterior;
            InmueblesDiferenciaPatrimonio = diferenciaPatrimonio;
            InmueblesVariacionPorcentual = variacionPorcentual;
        }

        public void SetMontosOtrosIngresos(decimal patrimonioActual, decimal patrimonioGestionAnterior,
            decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            OtrosIngresosPatrimonioActual = patrimonioActual;
            OtrosIngresosPatrimonioGestionAnterior = patrimonioGestionAnterior;
            OtrosIngresosDiferenciaPatrimonio = diferenciaPatrimonio;
            OtrosIngresosVariacionPorcentual = variacionPorcentual;
        }

        public void SetMontosDepositos(decimal patrimonioActual, decimal patrimonioGestionAnterior,
            decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            DepositosPatrimonioActual = patrimonioActual;
            DepositosPatrimonioGestionAnterior = patrimonioGestionAnterior;
            DepositosDiferenciaPatrimonio = diferenciaPatrimonio;
            DepositosVariacionPorcentual = variacionPorcentual;
        }

        public void SetMontosDeudaBancaria(decimal patrimonioActual, decimal patrimonioGestionAnterior,
            decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            DeudaBancariaPatrimonioActual = patrimonioActual;
            DeudaBancariaPatrimonioGestionAnterior = patrimonioGestionAnterior;
            DeudaBancariaDiferenciaPatrimonio = diferenciaPatrimonio;
            DeudaBancariaVariacionPorcentual = variacionPorcentual;
        }

        public void SetMontosVehiculos(decimal patrimonioActual, decimal patrimonioGestionAnterior,
            decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            VehiculosPatrimonioActual = patrimonioActual;
            VehiculosPatrimonioGestionAnterior = patrimonioGestionAnterior;
            VehiculosDiferenciaPatrimonio = diferenciaPatrimonio;
            VehiculosVariacionPorcentual = variacionPorcentual;
        }

        public void SetMontosValoresNegociables(decimal patrimonioActual, decimal patrimonioGestionAnterior,
            decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            ValoresNegociablesPatrimonioActual = patrimonioActual;
            ValoresNegociablesPatrimonioGestionAnterior = patrimonioGestionAnterior;
            ValoresNegociablesDiferenciaPatrimonio = diferenciaPatrimonio;
            ValoresNegociablesVariacionPorcentual = variacionPorcentual;
        }

        public void SetMontosPatrimonioTotal(decimal patrimonioActual, decimal patrimonioGestionAnterior,
            decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            PatrimonioActual = patrimonioActual;
            PatrimonioGestionAnterior = patrimonioGestionAnterior;
            DiferenciaPatrimonio = diferenciaPatrimonio;
            VariacionPorcentual = variacionPorcentual;
        }
    }
}
