using SRDP.Domain.ValoresNegociables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SRDP.Domain.Declaraciones
{
    public sealed class ValorNegociableCollection : IGuidCollection
    {
        private readonly IList<Guid> _valorNegociableIDs;
        private readonly IList<ValorNegociableMayor10K> _valoresNegociables;

        public IReadOnlyCollection<ValorNegociableMayor10K> Items
        {
            get { return GetItems(); }
        }
        public decimal ValorNeto
        {
            get { return GetValorNeto(); }
        }
        
        public ValorNegociableCollection()
        {
            _valorNegociableIDs = new List<Guid>();
            _valoresNegociables = new List<ValorNegociableMayor10K>();
        }

        public void Add(Guid valorNegociableID)
        {
            _valorNegociableIDs.Add(valorNegociableID);
        }

        public void AddItem(ValorNegociableMayor10K item)
        {
            _valoresNegociables.Add(item);
        }

        public IReadOnlyCollection<Guid> GetIds()
        {
            var valorNegociableIDs = new ReadOnlyCollection<Guid>(_valorNegociableIDs);
            return valorNegociableIDs;
        }

        public IReadOnlyCollection<ValorNegociableMayor10K> GetItems()
        {
            return new ReadOnlyCollection<ValorNegociableMayor10K>(_valoresNegociables);
        }

        public decimal GetValorNeto()
        {
            decimal result = 0;
            foreach (var item in Items)
            {
                result += item.ValorAproximado;
            }
            return result;
        }
    }
}