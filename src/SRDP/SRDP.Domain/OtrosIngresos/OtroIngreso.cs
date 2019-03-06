using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.OtrosIngresos
{
    public sealed class OtroIngreso : IEntity
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string Concepto { get; private set; }
        public decimal IngresoMensual { get; private set; }

        #region Constructor
        public OtroIngreso(Guid declaracionID, string concepto, decimal ingresoMensual)
        {
            ID = Guid.NewGuid();
            DeclaracionID = declaracionID;
            Concepto = concepto;
            IngresoMensual = ingresoMensual;
        }

        private OtroIngreso() { }
        #endregion

        public static OtroIngreso Load(Guid id, Guid declaracionID, string concepto, decimal ingresoMensual)
        {
            var otroIngreso = new OtroIngreso
            {
                ID = id,
                DeclaracionID = declaracionID,
                Concepto = concepto,
                IngresoMensual = ingresoMensual
            };
            return otroIngreso;
        }

    }
}
