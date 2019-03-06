using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetValoresNegociables
{
    public class GetValoresNegociablesUserCase : IGetValoresNegociablesUserCase
    {
        private IValorNegociableReadOnlyRepository _valorNegociableReadOnlyRepository;

        public GetValoresNegociablesUserCase(IValorNegociableReadOnlyRepository valorNegociableReadOnlyRepository)
        {
            _valorNegociableReadOnlyRepository = valorNegociableReadOnlyRepository;
        }

        public async Task<ValorNegociableOutput> Execute(Guid valorNegociableID)
        {
            var valorNegociable = await _valorNegociableReadOnlyRepository.Get(valorNegociableID);
            if (valorNegociable == null) return null;

            return new ValorNegociableOutput(valorNegociable.ID, valorNegociable.DeclaracionID, valorNegociable.Descripcion, valorNegociable.TipoValor, valorNegociable.ValorAproximado);
        }

        public async Task<ICollection<ValorNegociableOutput>> ExecuteList(Guid declaracionID)
        {
            var valoresNegociables = await _valorNegociableReadOnlyRepository.GetByDeclaracion(declaracionID);

            var outputList = new List<ValorNegociableOutput>();

            if (valoresNegociables == null) return outputList;

            foreach (var valorNegociable in valoresNegociables)
            {
                outputList.Add(new ValorNegociableOutput(valorNegociable.ID, valorNegociable.DeclaracionID, valorNegociable.Descripcion, valorNegociable.TipoValor, valorNegociable.ValorAproximado));
            }
            return outputList;
        }
    }
}
