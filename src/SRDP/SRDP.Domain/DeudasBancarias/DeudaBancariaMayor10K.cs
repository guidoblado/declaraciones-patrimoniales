using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.DeudasBancarias
{
    public sealed class DeudaBancariaMayor10K : IEntity
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string InstitucionFinanciera { get; private set; }
        public decimal Monto { get; private set; }
        public string Tipo { get; private set; }

        #region Constructors
        private DeudaBancariaMayor10K() { }

        public DeudaBancariaMayor10K(Guid declaracionID, string institucionFinanciera, decimal monto, string tipo)
        {
            ID = Guid.NewGuid();
            DeclaracionID = declaracionID;
            InstitucionFinanciera = institucionFinanciera;
            Monto = monto;
            Tipo = tipo;
        }
        #endregion

        public static DeudaBancariaMayor10K Load(Guid id, Guid declaracionID, string institucionFinanciera, decimal monto, string tipo)
        {
            var deudaBancariaMayor10K = new DeudaBancariaMayor10K
            {
                ID = id,
                DeclaracionID = declaracionID,
                InstitucionFinanciera = institucionFinanciera,
                Monto = monto,
                Tipo = tipo,
            };
            return deudaBancariaMayor10K;
        }
    }
}
