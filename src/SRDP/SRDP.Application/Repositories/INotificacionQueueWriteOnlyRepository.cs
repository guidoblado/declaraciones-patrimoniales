using SRDP.Domain.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface INotificacionQueueWriteOnlyRepository
    {
        Task Update(NotificacionQueueItem notificacionQueueItem);
    }
}
