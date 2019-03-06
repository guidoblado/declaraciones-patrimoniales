using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Application.SearchParameters;
using SRDP.Domain.Reportes;

namespace SRDP.Application.UseCases.GetHistoricoIndividual
{
    public class GetHistoricoIndividualUserCase : IGetHistoricoIndividualUserCase
    {
        private readonly IHistoricoIndividualReadOnlyRepository _HistoricoIndividualReadOnlyRepository;

        public GetHistoricoIndividualUserCase(IHistoricoIndividualReadOnlyRepository historicoIndividualReadOnlyRepository)
        {
            _HistoricoIndividualReadOnlyRepository = historicoIndividualReadOnlyRepository;
        }

        public async Task<ICollection<HistoricoIndividualOutput>> ExecuteList(int gestion, FiltroFuncionario filtro)
        {
            var historicoIndividualList = await _HistoricoIndividualReadOnlyRepository.GetTwoGestiones(gestion, 2016);
            var result = new List<HistoricoIndividualOutput>();
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.CodArea))
                    historicoIndividualList = historicoIndividualList.Where(x => x.Funcionario.CodArea == filtro.CodArea).ToList();
                if (!String.IsNullOrEmpty(filtro.CodCargo))
                    historicoIndividualList = historicoIndividualList.Where(x => x.Funcionario.CodCargo == filtro.CodCargo).ToList();
                if (!String.IsNullOrEmpty(filtro.CodCentroCosto))
                    historicoIndividualList = historicoIndividualList.Where(x => x.Funcionario.CodCentroDeCosto == filtro.CodCentroCosto).ToList();
                if (!String.IsNullOrEmpty(filtro.CodGeografico))
                    historicoIndividualList = historicoIndividualList.Where(x => x.Funcionario.CodUbicacionGeografica == filtro.CodGeografico).ToList();
                if (filtro.Rol.HasValue)
                    historicoIndividualList = historicoIndividualList.Where(x => x.Funcionario.TipoRol == Convert.ToInt32(filtro.Rol)).ToList();
            }

            foreach (var historicoIndividual in historicoIndividualList)
            {
                result.Add(new HistoricoIndividualOutput(historicoIndividual));
            }

            return result;
        }

       
    }
}
