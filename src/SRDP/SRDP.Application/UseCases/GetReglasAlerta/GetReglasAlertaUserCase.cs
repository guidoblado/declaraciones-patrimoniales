using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetReglasAlerta
{
    public class GetReglasAlertaUserCase : IGetReglasAlertaUserCase
    {
        private IReglaAlertaReadOnlyRepository _reglaAlertaReadOnlyRepository;

        public GetReglasAlertaUserCase(IReglaAlertaReadOnlyRepository reglaAlertaReadOnlyRepository)
        {
            _reglaAlertaReadOnlyRepository = reglaAlertaReadOnlyRepository;
        }
        public async Task<ReglaAlertaOutput> Execute(Guid id)
        {
            var reglaAlerta = await _reglaAlertaReadOnlyRepository.Get(id);

            if (reglaAlerta == null) return null;

            return new ReglaAlertaOutput(reglaAlerta.ID, reglaAlerta.Gestion, reglaAlerta.Descripcion, reglaAlerta.PorcentajeVariacion, reglaAlerta.Condicion.ToString(), reglaAlerta.Monto); 
        }

        public async Task<ICollection<ReglaAlertaOutput>> ExecuteList(int gestion)
        {
            var reglasAlerta = await _reglaAlertaReadOnlyRepository.GetByGestion(gestion);

            var outputResult = new List<ReglaAlertaOutput>();

            if (reglasAlerta == null) return outputResult;

            foreach (var reglaAlerta in reglasAlerta)
            {
                outputResult.Add(new ReglaAlertaOutput(reglaAlerta.ID, reglaAlerta.Gestion, reglaAlerta.Descripcion, reglaAlerta.PorcentajeVariacion, reglaAlerta.Condicion.ToString(), reglaAlerta.Monto));
            }

            return outputResult;
        }
    }
}
