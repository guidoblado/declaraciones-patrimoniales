using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SendNotificacion
{
    public interface ISendNotificacionUserCase
    {
        Task<bool> Execute(NotificacionOutput notificacion);
    }
}
