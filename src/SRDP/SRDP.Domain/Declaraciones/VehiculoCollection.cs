using SRDP.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SRDP.Domain.Declaraciones
{
    public sealed class VehiculoCollection : IGuidCollection
    {
        private readonly IList<Guid> _vehiculoIDs;
        private readonly IList<Vehiculo> _vehiculos;

        public IReadOnlyCollection<Vehiculo> Items
        {
            get { return GetItems(); }
        }
        public decimal ValorNeto
        {
            get { return GetValorNeto(); }
        }

        public VehiculoCollection()
        {
            _vehiculoIDs = new List<Guid>();
            _vehiculos = new List<Vehiculo>();
        }

        public void Add(Guid vehiculoID)
        {
            _vehiculoIDs.Add(vehiculoID);
        }

        public void AddItem(Vehiculo item)
        {
            _vehiculos.Add(item);
        }

        public IReadOnlyCollection<Guid> GetIds()
        {
            var vehiculoIDs =  new ReadOnlyCollection<Guid>(_vehiculoIDs);
            return vehiculoIDs;
        }

        public IReadOnlyCollection<Vehiculo> GetItems()
        {
            return new ReadOnlyCollection<Vehiculo>(_vehiculos);
        }

        public decimal GetValorNeto()
        {
            decimal result = 0;
            foreach (var item in Items)
            {
                result += item.ValorAproximado - item.SaldoDeudor;
            }
            return result;
        }
    }
}