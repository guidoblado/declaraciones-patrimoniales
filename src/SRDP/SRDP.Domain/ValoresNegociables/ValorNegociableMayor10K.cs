using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.ValoresNegociables
{
    public sealed class ValorNegociableMayor10K : IEntity
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string Descripcion { get; private set; }
        public string TipoValor { get; private set; }
        public decimal ValorAproximado { get; private set; }

        #region Constructors
        public ValorNegociableMayor10K(Guid declaracionID, string descrpcion, string tipoValor, decimal valorAproximado)
        {
            ID = Guid.NewGuid();
            DeclaracionID = declaracionID;
            Descripcion = descrpcion;
            TipoValor = tipoValor;
            ValorAproximado = valorAproximado;
        }

        private ValorNegociableMayor10K()
        {
        }
        #endregion

        public static ValorNegociableMayor10K Load(Guid id, Guid declaracionID, string descrpcion, string tipoValor, decimal valorAproximado)
        {
            var valorNegociable = new ValorNegociableMayor10K
            {
                ID = id,
                DeclaracionID = declaracionID,
                Descripcion = descrpcion,
                TipoValor = tipoValor,
                ValorAproximado = valorAproximado
            };
            return valorNegociable;
        }
    }
}
