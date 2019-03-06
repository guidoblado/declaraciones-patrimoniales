using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    internal class Vehiculo
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string Marca { get; set; }
        public string TipoVehiculo { get; set; }
        public string Anio { get; set; }
        public decimal ValorAproximado { get; set; }
        public decimal SaldoDeudor { get; set; }
        public string Banco { get; set; }
    }
}
