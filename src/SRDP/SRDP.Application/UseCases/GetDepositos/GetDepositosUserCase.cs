using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetDepositos
{
    public class GetDepositosUserCase : IGetDepositosUserCase
    {
        private IDepositoReadOnlyRepository _depositoReadOnlyRepository;

        public GetDepositosUserCase(IDepositoReadOnlyRepository depositoReadOnlyRepository)
        {
            _depositoReadOnlyRepository = depositoReadOnlyRepository;
        }

        public async Task<DepositoOutput> Execute(Guid depositoID)
        {
            var deposito = await _depositoReadOnlyRepository.Get(depositoID);
            if (deposito == null) return null;

            return new DepositoOutput(deposito.ID, deposito.DeclaracionID, deposito.InstitucionFinanciera, deposito.TipoDeCuenta, deposito.Saldo);
        }

        public async Task<ICollection<DepositoOutput>> ExecuteList(Guid declaracionID)
        {
            var depositos = await _depositoReadOnlyRepository.GetByDeclaracion(declaracionID);

            var outputList = new List<DepositoOutput>();

            if (depositos == null) return outputList;

            foreach (var deposito in depositos)
            {
                outputList.Add(new DepositoOutput(deposito.ID, deposito.DeclaracionID, deposito.InstitucionFinanciera, deposito.TipoDeCuenta, deposito.Saldo));
            }
            return outputList;
        }
    }
}
