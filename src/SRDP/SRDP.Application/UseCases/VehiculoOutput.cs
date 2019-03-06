using System;

namespace SRDP.Application.UseCases
{
    public class VehiculoOutput
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string Marca { get; private set; }
        public string TipoVehiculo { get; private set; }
        public string Anio { get; private set; }
        public decimal ValorAproximado { get; private set; }
        public decimal SaldoDeudor { get; private set; }
        public string Banco { get; private set; }

        public VehiculoOutput(Guid vehiculoID, Guid declaracionID, string marca, string tipoVehiculo, string anio, decimal valorAproximado, decimal saldoDeudor, string banco)
        {
            ID = vehiculoID;
            DeclaracionID = declaracionID;
            Marca = marca;
            TipoVehiculo = tipoVehiculo;
            Anio = anio;
            ValorAproximado = valorAproximado;
            SaldoDeudor = saldoDeudor;
            Banco = banco;
        }
    }
}