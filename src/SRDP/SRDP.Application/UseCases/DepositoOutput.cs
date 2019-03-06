using System;

namespace SRDP.Application.UseCases
{
    public class DepositoOutput
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string Institucion { get; private set; }
        public string TipoDeCuenta { get; private set; }
        public decimal Saldo { get; private set; }

        public DepositoOutput(Guid depositoID, Guid declaracionID, string institucion, string tipoDeCuenta, decimal saldo)
        {
            ID = depositoID;
            DeclaracionID = declaracionID;
            Institucion = institucion;
            TipoDeCuenta = tipoDeCuenta;
            Saldo = saldo;
        }
    }
}