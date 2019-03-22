using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.UpdateGestiones
{
    public interface IUpdateGestionesUserCase
    {
        Task<GestionOutput> Update(int gestion, DateTime fechaInicio, DateTime fechaFinal, bool vigente);
        Task SetAsActive(int gestion);
        Task Delete(int gestion);
    }
}
