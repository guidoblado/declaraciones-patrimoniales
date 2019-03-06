using SRDP.Domain.DeudasBancarias;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SRDP.Domain.Declaraciones
{
    public sealed class DeudaBancariaCollection : IGuidCollection
    {
        private readonly IList<Guid> _deudaBancariaIDs;
        private readonly IList<DeudaBancariaMayor10K> _deudasBancarias;

        public IReadOnlyCollection<DeudaBancariaMayor10K> Items
        {
            get { return GetItems(); }
        }
        public decimal ValorNeto
        {
            get { return GetValorNeto(); }
        }

        public DeudaBancariaCollection()
        {
            _deudaBancariaIDs = new List<Guid>();
            _deudasBancarias = new List<DeudaBancariaMayor10K>();
        }

        public void Add(Guid deudaBancariaID)
        {
            _deudaBancariaIDs.Add(deudaBancariaID);
        }

        public void AddItem(DeudaBancariaMayor10K item)
        {
            _deudasBancarias.Add(item);
        }

        public IReadOnlyCollection<Guid> GetIds()
        {
            var deudaBancariaIDs = new ReadOnlyCollection<Guid>(_deudaBancariaIDs);
            return deudaBancariaIDs;
        }

        public IReadOnlyCollection<DeudaBancariaMayor10K> GetItems()
        {
            return new ReadOnlyCollection<DeudaBancariaMayor10K>(_deudasBancarias);
        }
        public decimal GetValorNeto()
        {
            decimal result = 0;
            foreach (var item in Items)
            {
                result += item.Monto;
            }
            return result;
        }
    }
}