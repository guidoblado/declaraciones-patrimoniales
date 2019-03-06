using SRDP.Application.Repositories;
using SRDP.Domain.ValoresNegociables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveValoresNegociables
{
    public class SaveValoresNegociablesUserCase : ISaveValoresNegociablesUserCase
    {
        private IValorNegociableWriteOnlyRepository _valorNegociableWriteOnlyRepository;

        public SaveValoresNegociablesUserCase(IValorNegociableWriteOnlyRepository valorNegociableWriteOnlyRepository)
        {
            _valorNegociableWriteOnlyRepository = valorNegociableWriteOnlyRepository;
        }

        public async Task DeleteValorNegociable(Guid id)
        {
            await _valorNegociableWriteOnlyRepository.Delete(id);
        }

        public async Task<ValorNegociableOutput> Execute(Guid valorNegociableID, Guid declaracionID, string descripcion, string tipoValor, decimal valorAproximado)
        {
            if(valorNegociableID == null || valorNegociableID == Guid.Empty)
                await _valorNegociableWriteOnlyRepository.Add(new ValorNegociableMayor10K(declaracionID, descripcion, tipoValor, valorAproximado));
            else
                await _valorNegociableWriteOnlyRepository.Update(ValorNegociableMayor10K.Load(valorNegociableID, declaracionID, descripcion, tipoValor, valorAproximado));
            return new ValorNegociableOutput(valorNegociableID, declaracionID, descripcion, tipoValor, valorAproximado);
        }
    }
}
