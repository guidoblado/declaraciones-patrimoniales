using System;

namespace SRDP.Application.UseCases
{
    public class ValorNegociableOutput
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }

        public string Descripcion { get; private set; }
        public string TipoValor { get; private set; }
        public decimal ValorAproximado { get; private set; }

        public ValorNegociableOutput(Guid id, Guid declaracionID, string descrpcion, string tipoValor, decimal valorAproximado)
        {
            ID = id;
            DeclaracionID = declaracionID;
            Descripcion = descrpcion;
            TipoValor = tipoValor;
            ValorAproximado = valorAproximado;
        }
    }
}