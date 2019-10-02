using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application
{
    public class HistoricoIndividualItemOutput
    {
        public int FuncionarioID { get; private set; }
        public string NombreCompleto { get; private set; }
        public string CodCargo { get; set; }
        public string Cargo { get; set; }
        public string CodArea { get; set; }
        public string Area { get; set; }
        public string CodGeografico { get; set; }
        public string UbicacionGeografica { get; set; }
        public string CodCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int TipoRol { get; set; }
        public string Rol { get; set; }
        public decimal Depositos2016 { get; private set; }
        public decimal Depositos2017 { get; private set; }
        public decimal Depositos2018 { get; private set; }
        public decimal Depositos2019 { get; private set; }
        public decimal Depositos2020 { get; private set; }
        public decimal Inmuebles2016 { get; private set; }
        public decimal Inmuebles2017 { get; private set; }
        public decimal Inmuebles2018 { get; private set; }
        public decimal Inmuebles2019 { get; private set; }
        public decimal Inmuebles2020 { get; private set; }
        public decimal Vehiculos2016 { get; private set; }
        public decimal Vehiculos2017 { get; private set; }
        public decimal Vehiculos2018 { get; private set; }
        public decimal Vehiculos2019 { get; private set; }
        public decimal Vehiculos2020 { get; private set; }
        public decimal OtrosIngresos2016 { get; private set; }
        public decimal OtrosIngresos2017 { get; private set; }
        public decimal OtrosIngresos2018 { get; private set; }
        public decimal OtrosIngresos2019 { get; private set; }
        public decimal OtrosIngresos2020 { get; private set; }
        public decimal DeudasBancarias2016 { get; private set; }
        public decimal DeudasBancarias2017 { get; private set; }
        public decimal DeudasBancarias2018 { get; private set; }
        public decimal DeudasBancarias2019 { get; private set; }
        public decimal DeudasBancarias2020 { get; private set; }
        public decimal ValoresNegociables2016 { get; private set; }
        public decimal ValoresNegociables2017 { get; private set; }
        public decimal ValoresNegociables2018 { get; private set; }
        public decimal ValoresNegociables2019 { get; private set; }
        public decimal ValoresNegociables2020 { get; private set; }

        public HistoricoIndividualItemOutput(int funcionarioID, string nombreCompleto, string codCargo, string cargo, string codArea, string area, 
            string codGeografico, string ubicacionGeografica, string codCentroCosto, string centroCosto, int tipoRol, string rol)
        {
            FuncionarioID = funcionarioID;
            NombreCompleto = nombreCompleto;
            CodCargo = codCargo;
            Cargo = cargo;
            CodArea = codArea;
            Area = area;
            CodGeografico = codGeografico;
            UbicacionGeografica = ubicacionGeografica;
            CodCentroCosto = codCentroCosto;
            CentroCosto = centroCosto;
            TipoRol = tipoRol;
            Rol = rol;
        }

        public void SetDepositos(decimal monto2016, decimal monto2017, decimal monto2018, decimal monto2019, decimal monto2020)
        {
            Depositos2016 = monto2016;
            Depositos2017 = monto2017;
            Depositos2018 = monto2018;
            Depositos2019 = monto2019;
            Depositos2020 = monto2020;

        }

        public void SetDeudasBancarias(decimal monto2016, decimal monto2017, decimal monto2018, decimal monto2019, decimal monto2020)
        {
            DeudasBancarias2016 = monto2016;
            DeudasBancarias2017 = monto2017;
            DeudasBancarias2018 = monto2018;
            DeudasBancarias2019 = monto2019;
            DeudasBancarias2020 = monto2020;
        }

        public void SetInmuebles(decimal monto2016, decimal monto2017, decimal monto2018, decimal monto2019, decimal monto2020)
        {
            Inmuebles2016 = monto2016;
            Inmuebles2017 = monto2017;
            Inmuebles2018 = monto2018;
            Inmuebles2019 = monto2019;
            Inmuebles2020 = monto2020;
        }

        public void SetVehiculos(decimal monto2016, decimal monto2017, decimal monto2018, decimal monto2019, decimal monto2020)
        {
            Vehiculos2016 = monto2016;
            Vehiculos2017 = monto2017;
            Vehiculos2018 = monto2018;
            Vehiculos2019 = monto2019;
            Vehiculos2020 = monto2020;
        }

        public void SetOtrosIngresos(decimal monto2016, decimal monto2017, decimal monto2018, decimal monto2019, decimal monto2020)
        {
            OtrosIngresos2016 = monto2016;
            OtrosIngresos2017 = monto2017;
            OtrosIngresos2018 = monto2018;
            OtrosIngresos2019 = monto2019;
            OtrosIngresos2020 = monto2020;
        }

        public void SetValoresNegociables(decimal monto2016, decimal monto2017, decimal monto2018, decimal monto2019, decimal monto2020)
        {
            ValoresNegociables2016 = monto2016;
            ValoresNegociables2017 = monto2017;
            ValoresNegociables2018 = monto2018;
            ValoresNegociables2019 = monto2019;
            ValoresNegociables2020 = monto2020;
        }
    }
}
