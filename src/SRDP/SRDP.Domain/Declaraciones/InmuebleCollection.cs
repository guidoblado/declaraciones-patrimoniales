using SRDP.Domain.Inmuebles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SRDP.Domain.Declaraciones
{
    public sealed class InmuebleCollection : IGuidCollection
    {
        private readonly IList<Guid> _inmueblesIDs;
        private readonly IList<Inmueble> _inmuebles;

        public IReadOnlyCollection<Inmueble> Items
        {
            get { return GetItems(); }
        }
        public decimal ValorNeto
        {
            get { return GetValorNeto(); }
        }

        public InmuebleCollection()
        {
            _inmueblesIDs = new List<Guid>();
            _inmuebles = new List<Inmueble>();
        }

        public void Add(Guid inmuebleID)
        {
            _inmueblesIDs.Add(inmuebleID);
        }

        public void AddItem(Inmueble item)
        {
            _inmuebles.Add(item);
        }

        public IReadOnlyCollection<Guid> GetIds()
        {
            var inmueblesIDs = new ReadOnlyCollection<Guid>(_inmueblesIDs);
            return inmueblesIDs;
        }

        public IReadOnlyCollection<Inmueble> GetItems()
        {
            return new ReadOnlyCollection<Inmueble>(_inmuebles);
        }
        public decimal GetValorNeto()
        {
            decimal result = 0;
            foreach (var item in Items)
            {
                result += (item.ValorComercial - item.SaldoHipoteca) * item.PorcentajeParticipacion;
            }
            return result;
        }
    }
}