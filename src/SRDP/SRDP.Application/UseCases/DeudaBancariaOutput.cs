using System;

namespace SRDP.Application.UseCases
{
    public class DeudaBancariaOutput
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string InstitucionFinanciera { get; private set; }
        public decimal Monto { get; private set; }
        public string Tipo { get; private set; }

        public DeudaBancariaOutput(Guid deudaID, Guid declaracionID, string institucionFinanciera, decimal monto, string tipo)
        {
            ID = deudaID;
            DeclaracionID = declaracionID;
            InstitucionFinanciera = institucionFinanciera;
            Monto = monto;
            Tipo = tipo;
        }
    }
}