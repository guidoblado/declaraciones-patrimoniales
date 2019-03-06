using SRDP.Application.Repositories;
using SRDP.Domain.Depositos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveDeposito
{
    public class SaveDepositoUserCase : ISaveDepositoUserCase
    {
        private IDepositoWriteOnlyRepository _depositoWriteOnlyRepository;

        public SaveDepositoUserCase(IDepositoWriteOnlyRepository depositoWriteOnlyRepository)
        {
            _depositoWriteOnlyRepository = depositoWriteOnlyRepository;
        }

        public async Task DeleteDeposito(Guid id)
        {
            await _depositoWriteOnlyRepository.Delete(id);
        }

        public async Task<DepositoOutput> Execute(Guid depositoID, Guid declaracionID, string institucion, string tipoCuenta, decimal saldo)
        {
            if (depositoID == null || depositoID == Guid.Empty)
                await _depositoWriteOnlyRepository.Add(new DepositoMayor10K(declaracionID, institucion, tipoCuenta, saldo));
            else
                await _depositoWriteOnlyRepository.Update(DepositoMayor10K.Load(depositoID, declaracionID, institucion, tipoCuenta, saldo));
            return new DepositoOutput(depositoID, declaracionID, institucion, tipoCuenta, saldo);
        }
    }
}
