using System;

namespace SRDP.Persitence.Entities
{
    internal class Inmueble
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }
        public string Direccion { get; set; }
        public string TipoDeInmueble { get; set; }
        public decimal PorcentajeParticipacion { get; set; }
        public decimal ValorComercial { get; set; }
        public decimal SaldoHipoteca { get; set; }
        public string Banco { get; set; }
    }
}