using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveReglasAlerta
{
    public interface ISaveReglasAlertaUserCase
    {
        Task<ReglaAlertaOutput> Execute(Guid id, int gestion, string descripcion, decimal porcentaje, string operador, decimal monto);
        Task DeleteRegla(Guid id);
    }
}
