using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.Models
{
    public class HistoricoIndividualPivotModel
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
        public decimal Depositos2016 { get; set; }
        public decimal Depositos2017 { get; set; }
        public decimal Depositos2018 { get; set; }
        public decimal Depositos2019 { get; set; }
        public decimal Depositos2020 { get; set; }
        public decimal Inmuebles2016 { get; set; }
        public decimal Inmuebles2017 { get; set; }
        public decimal Inmuebles2018 { get; set; }
        public decimal Inmuebles2019 { get; set; }
        public decimal Inmuebles2020 { get; set; }
        public decimal Vehiculos2016 { get; set; }
        public decimal Vehiculos2017 { get; set; }
        public decimal Vehiculos2018 { get; set; }
        public decimal Vehiculos2019 { get; set; }
        public decimal Vehiculos2020 { get; set; }
        public decimal OtrosIngresos2016 { get; set; }
        public decimal OtrosIngresos2017 { get; set; }
        public decimal OtrosIngresos2018 { get; set; }
        public decimal OtrosIngresos2019 { get; set; }
        public decimal OtrosIngresos2020 { get; set; }
        public decimal DeudasBancarias2016 { get; set; }
        public decimal DeudasBancarias2017 { get; set; }
        public decimal DeudasBancarias2018 { get; set; }
        public decimal DeudasBancarias2019 { get; set; }
        public decimal DeudasBancarias2020 { get; set; }
    }
}