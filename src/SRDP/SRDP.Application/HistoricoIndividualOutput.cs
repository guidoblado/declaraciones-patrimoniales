using SRDP.Domain.Enumerations;
using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application
{
    public class HistoricoIndividualOutput
    {
        public int FuncionarioID { get; private set; }
        public string NombreCompleto { get; private set; }
        public string Cargo { get; private set; }
        public string Area { get; private set; }
        public string UbicacionGeografica { get; private set; }
        public string CentroCosto { get; private set; }
        //Deposito
        public Guid DepositoDeclaracionIDAnterior { get; private set; }
        public Guid DepositoDeclaracionIDActual { get; private set; }
        public int DepositoGestionAnterior { get; private set; }
        public decimal DepositoMontoGestionAnterior { get; private set; }
        public int DepositoGestionActual { get; private set; }
        public decimal DepositoMontoGestionActual { get; private set; }
        //Inmueble
        public Guid InmuebleDeclaracionIDAnterior { get; private set; }
        public Guid InmuebleDeclaracionIDActual { get; private set; }
        public int InmuebleGestionAnterior { get; private set; }
        public decimal InmuebleMontoGestionAnterior { get; private set; }
        public int InmuebleGestionActual { get; private set; }
        public decimal InmuebleMontoGestionActual { get; private set; }
        //Vehiculo
        public Guid VehiculoDeclaracionIDAnterior { get; private set; }
        public Guid VehiculoDeclaracionIDActual { get; private set; }
        public int VehiculoGestionAnterior { get; private set; }
        public decimal VehiculoMontoGestionAnterior { get; private set; }
        public int VehiculoGestionActual { get; private set; }
        public decimal VehiculoMontoGestionActual { get; private set; }
        //OtroIngreso
        public Guid OtroIngresoDeclaracionIDAnterior { get; private set; }
        public Guid OtroIngresoDeclaracionIDActual { get; private set; }
        public int OtroIngresoGestionAnterior { get; private set; }
        public decimal OtroIngresoMontoGestionAnterior { get; private set; }
        public int OtroIngresoGestionActual { get; private set; }
        public decimal OtroIngresoMontoGestionActual { get; private set; }
        //DeudaBancaria
        public Guid DeudaBancariaDeclaracionIDAnterior { get; private set; }
        public Guid DeudaBancariaDeclaracionIDActual { get; private set; }
        public int DeudaBancariaGestionAnterior { get; private set; }
        public decimal DeudaBancariaMontoGestionAnterior { get; private set; }
        public int DeudaBancariaGestionActual { get; private set; }
        public decimal DeudaBancariaMontoGestionActual { get; private set; }

        private HistoricoIndividualOutput() { }

        public HistoricoIndividualOutput(HistoricoIndividual historicoIndividual)
        {
            FuncionarioID = historicoIndividual.Funcionario.FuncionarioID;
            NombreCompleto = historicoIndividual.Funcionario.NombreCompleto.ToString();
            Cargo = historicoIndividual.Funcionario.Cargo;
            Area = historicoIndividual.Funcionario.Area;
            UbicacionGeografica = historicoIndividual.Funcionario.UbicacionGeografica;
            CentroCosto = historicoIndividual.Funcionario.CentroDeCosto;
            //Deposito
            if (historicoIndividual.Historico.ContainsKey(RubroDeclaracion.Depositos) 
                && historicoIndividual.Historico[RubroDeclaracion.Depositos].Patrimonios.Count ==2)
            {
                DepositoDeclaracionIDActual = historicoIndividual.Historico[RubroDeclaracion.Depositos].Patrimonios[1].DeclaracionID;
                DepositoDeclaracionIDAnterior = historicoIndividual.Historico[RubroDeclaracion.Depositos].Patrimonios[0].DeclaracionID;
                DepositoGestionActual = historicoIndividual.Historico[RubroDeclaracion.Depositos].Patrimonios[1].Gestion;
                DepositoGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.Depositos].Patrimonios[0].Gestion;
                DepositoMontoGestionActual = historicoIndividual.Historico[RubroDeclaracion.Depositos].Patrimonios[1].Monto;
                DepositoMontoGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.Depositos].Patrimonios[0].Monto;
            }
            //Inmueble
            if (historicoIndividual.Historico.ContainsKey(RubroDeclaracion.Inmuebles)
                && historicoIndividual.Historico[RubroDeclaracion.Inmuebles].Patrimonios.Count == 2)
            {
                InmuebleDeclaracionIDActual = historicoIndividual.Historico[RubroDeclaracion.Inmuebles].Patrimonios[1].DeclaracionID;
                InmuebleDeclaracionIDAnterior = historicoIndividual.Historico[RubroDeclaracion.Inmuebles].Patrimonios[0].DeclaracionID;
                InmuebleGestionActual = historicoIndividual.Historico[RubroDeclaracion.Inmuebles].Patrimonios[1].Gestion;
                InmuebleGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.Inmuebles].Patrimonios[0].Gestion;
                InmuebleMontoGestionActual = historicoIndividual.Historico[RubroDeclaracion.Inmuebles].Patrimonios[1].Monto;
                InmuebleMontoGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.Inmuebles].Patrimonios[0].Monto;
            }
            //Vehiculo
            if (historicoIndividual.Historico.ContainsKey(RubroDeclaracion.Vehiculos)
                && historicoIndividual.Historico[RubroDeclaracion.Vehiculos].Patrimonios.Count == 2)
            {
                VehiculoDeclaracionIDActual = historicoIndividual.Historico[RubroDeclaracion.Vehiculos].Patrimonios[1].DeclaracionID;
                VehiculoDeclaracionIDAnterior = historicoIndividual.Historico[RubroDeclaracion.Vehiculos].Patrimonios[0].DeclaracionID;
                VehiculoGestionActual = historicoIndividual.Historico[RubroDeclaracion.Vehiculos].Patrimonios[1].Gestion;
                VehiculoGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.Vehiculos].Patrimonios[0].Gestion;
                VehiculoMontoGestionActual = historicoIndividual.Historico[RubroDeclaracion.Vehiculos].Patrimonios[1].Monto;
                VehiculoMontoGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.Vehiculos].Patrimonios[0].Monto;
            }
            //OtroIngreso
            if (historicoIndividual.Historico.ContainsKey(RubroDeclaracion.OtrosIngresos)
                && historicoIndividual.Historico[RubroDeclaracion.OtrosIngresos].Patrimonios.Count == 2)
            {
                OtroIngresoDeclaracionIDActual = historicoIndividual.Historico[RubroDeclaracion.OtrosIngresos].Patrimonios[1].DeclaracionID;
                OtroIngresoDeclaracionIDAnterior = historicoIndividual.Historico[RubroDeclaracion.OtrosIngresos].Patrimonios[0].DeclaracionID;
                OtroIngresoGestionActual = historicoIndividual.Historico[RubroDeclaracion.OtrosIngresos].Patrimonios[1].Gestion;
                OtroIngresoGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.OtrosIngresos].Patrimonios[0].Gestion;
                OtroIngresoMontoGestionActual = historicoIndividual.Historico[RubroDeclaracion.OtrosIngresos].Patrimonios[1].Monto;
                OtroIngresoMontoGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.OtrosIngresos].Patrimonios[0].Monto;
            }
            //DeudBancaria
            if (historicoIndividual.Historico.ContainsKey(RubroDeclaracion.DeudasBancarias)
                && historicoIndividual.Historico[RubroDeclaracion.DeudasBancarias].Patrimonios.Count == 2)
            {
                DeudaBancariaDeclaracionIDActual = historicoIndividual.Historico[RubroDeclaracion.DeudasBancarias].Patrimonios[1].DeclaracionID;
                DeudaBancariaDeclaracionIDAnterior = historicoIndividual.Historico[RubroDeclaracion.DeudasBancarias].Patrimonios[0].DeclaracionID;
                DeudaBancariaGestionActual = historicoIndividual.Historico[RubroDeclaracion.DeudasBancarias].Patrimonios[1].Gestion;
                DeudaBancariaGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.DeudasBancarias].Patrimonios[0].Gestion;
                DeudaBancariaMontoGestionActual = historicoIndividual.Historico[RubroDeclaracion.DeudasBancarias].Patrimonios[1].Monto;
                DeudaBancariaMontoGestionAnterior = historicoIndividual.Historico[RubroDeclaracion.DeudasBancarias].Patrimonios[0].Monto;
            }
        }
    }
}
