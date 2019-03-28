using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetNotificacion
{
    public interface IGetNotificacionUserCase
    {
        Task<NotificacionOutput> Execute(Guid id);
        Task<ICollection<NotificacionOutput>> ExecuteList(int funcionarioID);
    }
}
