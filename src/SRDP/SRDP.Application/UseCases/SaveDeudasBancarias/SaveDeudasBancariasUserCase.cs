using SRDP.Application.Repositories;
using SRDP.Domain.DeudasBancarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveDeudasBancarias
{
    public class SaveDeudasBancariasUserCase : ISaveDeudasBancariasUserCase
    {
        private IDeudaBancariaWriteOnlyRepository _deudaBancariaWriteOnlyRepository;

        public SaveDeudasBancariasUserCase(IDeudaBancariaWriteOnlyRepository deudaBancariaWriteOnlyRepository)
        {
            _deudaBancariaWriteOnlyRepository = deudaBancariaWriteOnlyRepository;
        }
        public async Task DeleteDeuda(Guid id)
        {
            await _deudaBancariaWriteOnlyRepository.Delete(id);
        }

        public async Task<DeudaBancariaOutput> Execute(Guid deudaID, Guid declaracionID, string institucionFinanciera, decimal monto, string tipo)
        {
            if (deudaID == null || deudaID == Guid.Empty)
                await _deudaBancariaWriteOnlyRepository.Add(new DeudaBancariaMayor10K(declaracionID, institucionFinanciera, monto, tipo));
            else
                await _deudaBancariaWriteOnlyRepository.Update(DeudaBancariaMayor10K.Load(deudaID, declaracionID, institucionFinanciera, monto, tipo));
            return new DeudaBancariaOutput(deudaID, declaracionID, institucionFinanciera, monto, tipo);
        }
    }
}
