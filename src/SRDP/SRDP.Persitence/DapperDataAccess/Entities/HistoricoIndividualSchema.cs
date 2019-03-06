using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    public class HistoricoIndividualSchema
    {
        public int Codigo { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
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
        public int Gestion_1 { get; set; }
        public int FuncionarioID_1 { get; set; }
        public Guid DeclaracionID_1 { get; set; }
        public decimal Depositos_1 { get; set; }
        public decimal DeudaBanco_1 { get; set; }
        public decimal Inmueble_1 { get; set; }
        public decimal OtrosIngresos_1 { get; set; }
        public decimal ValoresNegociables_1 { get; set; }
        public decimal Vehiculos_1 { get; set; }
        public int Gestion_2 { get; set; }
        public int FuncionarioID_2 { get; set; }
        public Guid DeclaracionID_2 { get; set; }
        public decimal Depositos_2 { get; set; }
        public decimal DeudaBanco_2 { get; set; }
        public decimal Inmueble_2 { get; set; }
        public decimal OtrosIngresos_2 { get; set; }
        public decimal ValoresNegociables_2 { get; set; }
        public decimal Vehiculos_2 { get; set; }
    }
}
