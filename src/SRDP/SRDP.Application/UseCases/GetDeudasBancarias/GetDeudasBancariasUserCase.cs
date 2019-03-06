using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetDeudasBancarias
{
    public class GetDeudasBancariasUserCase : IGetDeudasBancariasUserCase
    {
        private IDeudaBancariaReadOnlyRepository _deudaReadOnlyRepository;

        public GetDeudasBancariasUserCase(IDeudaBancariaReadOnlyRepository deudaReadOnlyRepository)
        {
            _deudaReadOnlyRepository = deudaReadOnlyRepository;
        }

        public async Task<DeudaBancariaOutput> Execute(Guid dudaBancariaID)
        {
            var deuda = await _deudaReadOnlyRepository.Get(dudaBancariaID);
            if (deuda == null) return null;

            return new DeudaBancariaOutput(deuda.ID, deuda.DeclaracionID, deuda.InstitucionFinanciera, deuda.Monto, deuda.Tipo);
        }

        public async Task<ICollection<DeudaBancariaOutput>> ExecuteList(Guid declaracionID)
        {
            var deudasBancarias = await _deudaReadOnlyRepository.GetByDeclaracion(declaracionID);

            var outputList = new List<DeudaBancariaOutput>();

            if (deudasBancarias == null) return outputList;

            foreach (var deuda in deudasBancarias)
            {
                outputList.Add(new DeudaBancariaOutput(deuda.ID, deuda.DeclaracionID, deuda.InstitucionFinanciera, deuda.Monto, deuda.Tipo));
            }
            return outputList;
        }
    }
}
