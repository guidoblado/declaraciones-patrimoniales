using SRDP.Application.SearchParameters;
using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetEstadoGeneral
{
    public interface IGetEstadoGeneralUserCase
    {
        Task<ICollection<EstadoGeneralOutput>> ExecuteList(int gestion, int estadoDeclaracion, FiltroFuncionario filtro);
    }
}
