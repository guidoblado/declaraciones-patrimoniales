using System;

namespace SRDP.Persitence.Entities
{
    internal class ValorNegociable
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string TipoValor { get; private set; }
        public string Descripcion { get; set; }
        public decimal ValorAproximado { get; set; }
    }
}
