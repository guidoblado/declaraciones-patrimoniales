using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Application.SearchParameters;
using SRDP.Domain.Enumerations;
using SRDP.Domain.Reportes;

namespace SRDP.Application.UseCases.GetEstadoGeneral
{
    public class GetEstadoGeneralUserCase : IGetEstadoGeneralUserCase
    {
        private IEstadoGeneralReadOnlyRepository _estadoGeneralReadOnlyRepository;

        public GetEstadoGeneralUserCase(IEstadoGeneralReadOnlyRepository estadoGeneralReadOnlyRepository)
        {
            _estadoGeneralReadOnlyRepository = estadoGeneralReadOnlyRepository;
        }
        public async Task<ICollection<EstadoGeneralOutput>> ExecuteList(int gestion, int estadoDeclaracion, FiltroFuncionario filtro)
        {
            var estadoGeneralList = await _estadoGeneralReadOnlyRepository.GetFromGestion(gestion, estadoDeclaracion);
            var outputResult = new List<EstadoGeneralOutput>();

            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.CodArea))
                    estadoGeneralList = estadoGeneralList.Where(x => x.Funcionario.CodArea == filtro.CodArea).ToList();
                if (!String.IsNullOrEmpty(filtro.CodCargo))
                    estadoGeneralList = estadoGeneralList.Where(x => x.Funcionario.CodCargo == filtro.CodCargo).ToList();
                if (!String.IsNullOrEmpty(filtro.CodCentroCosto))
                    estadoGeneralList = estadoGeneralList.Where(x => x.Funcionario.CodCentroDeCosto == filtro.CodCentroCosto).ToList();
                if (!String.IsNullOrEmpty(filtro.CodGeografico))
                    estadoGeneralList = estadoGeneralList.Where(x => x.Funcionario.CodUbicacionGeografica == filtro.CodGeografico).ToList();
                if (filtro.Rol.HasValue)
                    estadoGeneralList = estadoGeneralList.Where(x => x.Funcionario.TipoRol == Convert.ToInt32(filtro.Rol)).ToList();
            }

            foreach (var item in estadoGeneralList)
            {
                outputResult.Add(new EstadoGeneralOutput(item));
            }
            return outputResult;
        }

        
    }
}
