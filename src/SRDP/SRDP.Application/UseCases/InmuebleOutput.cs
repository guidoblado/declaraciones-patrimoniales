using System;

namespace SRDP.Application.UseCases
{
    public class InmuebleOutput
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string Direccion { get; private set; }
        public string TipoDeInmueble { get; private set; }
        public decimal PorcentajeParticipacion { get; private set; }
        public decimal ValorComercial { get; private set; }
        public decimal SaldoHipoteca { get; private set; }
        public string Banco { get; private set; }


        public InmuebleOutput(Guid inmuebleID, Guid declaracionID, string direccion, string tipoDeInmueble, decimal porcentajeParticipacion, decimal valorComercial, decimal saldoHipoteca, string banco)
        {
            ID = inmuebleID;
            DeclaracionID = declaracionID;
            Direccion = direccion;
            TipoDeInmueble = tipoDeInmueble;
            PorcentajeParticipacion = porcentajeParticipacion;
            ValorComercial = valorComercial;
            SaldoHipoteca = saldoHipoteca;
            Banco = banco;
        }
    }
}