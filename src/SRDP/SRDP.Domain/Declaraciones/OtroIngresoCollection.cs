using SRDP.Domain.OtrosIngresos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SRDP.Domain.Declaraciones
{
    public sealed class OtroIngresoCollection : IGuidCollection
    {
        private readonly IList<Guid> _otroIngresoIDs;
        private readonly IList<OtroIngreso> _otrosIngresos;

        public IReadOnlyCollection<OtroIngreso> Items
        {
            get { return GetItems(); }
        }
        public decimal ValorNeto
        {
            get { return GetValorNeto(); }
        }

        public OtroIngresoCollection()
        {
            _otroIngresoIDs = new List<Guid>();
            _otrosIngresos = new List<OtroIngreso>();
        }

        public void Add(Guid otroIngresoID)
        {
            _otroIngresoIDs.Add(otroIngresoID);
        }

        public void AddItem(OtroIngreso item)
        {
            _otrosIngresos.Add(item);
        }

        public IReadOnlyCollection<Guid> GetIds()
        {
            var otroIngresoIDs = new ReadOnlyCollection<Guid>(_otroIngresoIDs);
            return otroIngresoIDs;
        }

        public IReadOnlyCollection<OtroIngreso> GetItems()
        {
            return new ReadOnlyCollection<OtroIngreso>(_otrosIngresos);
        }
        public decimal GetValorNeto()
        {
            decimal result = 0;
            foreach (var item in Items)
            {
                result += item.IngresoMensual;
            }
            return result;
        }
    }
}