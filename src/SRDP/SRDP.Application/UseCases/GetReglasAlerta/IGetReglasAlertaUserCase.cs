using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetReglasAlerta
{
    public interface IGetReglasAlertaUserCase
    {
        Task<ReglaAlertaOutput> Execute(Guid id);
        Task<ICollection<ReglaAlertaOutput>> ExecuteList(int gestion);
    }
}
