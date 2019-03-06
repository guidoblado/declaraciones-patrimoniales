using System;

namespace SRDP.Application.UseCases
{
    public class OtroIngresoOutput
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string Concepto { get; private set; }
        public decimal IngresoMensual { get; private set; }

        public OtroIngresoOutput(Guid id, Guid declaracionID, string concepto, decimal ingresoMensual)
        {
            ID = id;
            DeclaracionID = declaracionID;
            Concepto = concepto;
            IngresoMensual = ingresoMensual;
        }
    }
}