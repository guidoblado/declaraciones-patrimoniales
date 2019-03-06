using SRDP.Domain.Depositos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SRDP.Domain.Declaraciones
{
    public sealed class DepositoCollection : IGuidCollection
    {
        private readonly IList<Guid> _depositoIDs;
        private readonly IList<DepositoMayor10K> _depositos;

        public IReadOnlyCollection<DepositoMayor10K> Items
        {
            get { return GetItems(); }
        }
        public decimal ValorNeto
        {
            get { return GetValorNeto(); }
        }

        public DepositoCollection()
        {
            _depositoIDs = new List<Guid>();
            _depositos = new List<DepositoMayor10K>();
        }

        public void Add(Guid ID)
        {
            _depositoIDs.Add(ID);
        }

        public void AddItem(DepositoMayor10K depositoMayor10K)
        {
            _depositos.Add(depositoMayor10K);
        }

        public IReadOnlyCollection<Guid> GetIds()
        {
            var depositoIds = new ReadOnlyCollection<Guid>(_depositoIDs);
            return depositoIds;
        }

        public IReadOnlyCollection<DepositoMayor10K> GetItems()
        {
            return new ReadOnlyCollection<DepositoMayor10K>(_depositos);
        }

        public decimal GetValorNeto()
        {
            decimal result = 0;
            foreach (var item in Items)
            {
                result += item.Saldo;
            }
            return result;
        }
    }
}