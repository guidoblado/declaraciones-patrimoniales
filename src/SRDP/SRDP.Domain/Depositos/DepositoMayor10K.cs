using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Depositos
{
    public sealed class DepositoMayor10K : IEntity
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string InstitucionFinanciera { get; private set; }
        public string TipoDeCuenta { get; private set; }
        public decimal Saldo { get; private set; }

        #region Constructors
        public DepositoMayor10K(Guid  declaracionID, string institucionFinanciera, string tipoDeCuenta, decimal saldo)
        {
            ID = Guid.NewGuid();
            DeclaracionID = declaracionID;
            InstitucionFinanciera = institucionFinanciera;
            TipoDeCuenta = tipoDeCuenta;
            Saldo = saldo;
        }

        private DepositoMayor10K() { }
        #endregion

        public static DepositoMayor10K Load(Guid id, Guid declaracionID, string institucionFinanciera, string tipoDeCuenta, decimal saldo)
        {
            var depositoMayor10K = new DepositoMayor10K
            {
                ID = id,
                DeclaracionID = declaracionID,
                TipoDeCuenta = tipoDeCuenta, 
                InstitucionFinanciera = institucionFinanciera,
                Saldo = saldo
            };
            return depositoMayor10K;
        }


    }
}
