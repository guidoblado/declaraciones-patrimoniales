using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveValoresNegociables
{
    public interface ISaveValoresNegociablesUserCase
    {
        Task<ValorNegociableOutput> Execute(Guid valorNegociableID, Guid declaracionID, string descripcion, string tipoValor, decimal valorAproximado);
        Task DeleteValorNegociable(Guid id);
    }
}
