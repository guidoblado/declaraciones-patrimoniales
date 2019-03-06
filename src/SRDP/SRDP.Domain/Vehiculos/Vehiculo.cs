using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Vehiculos
{
    public sealed class Vehiculo : IEntity
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string Marca { get; private set; }
        public string TipoVehiculo { get; private set; }
        public string Anio { get; private set; }
        public decimal ValorAproximado { get; private set; }
        public decimal SaldoDeudor { get; private set; }
        public string Banco { get; private set; }

        #region Constructors
        public Vehiculo(Guid declaracionID, string marca, string tipoVehiculo, string anio, decimal valorAproximado, decimal saldoDeudor, string banco)
        {
            ID = Guid.NewGuid();
            DeclaracionID = declaracionID;
            Marca = marca;
            TipoVehiculo = tipoVehiculo;
            Anio = anio;
            ValorAproximado = valorAproximado;
            SaldoDeudor = saldoDeudor;
            Banco = banco;
        }
        private Vehiculo() { }

        #endregion

        public static Vehiculo Load(Guid id, Guid declaracionID, string marca, string tipoVehiculo, string anio, decimal valorAproximado, decimal saldoDeudor, string banco)
        {
            var vehiculo = new Vehiculo
            {
                ID = id,
                DeclaracionID = declaracionID,
                Marca = marca,
                TipoVehiculo = tipoVehiculo,
                Anio = anio, 
                ValorAproximado = valorAproximado,
                SaldoDeudor = saldoDeudor,
                Banco = banco
            };
            return vehiculo;
        }

    }
}
