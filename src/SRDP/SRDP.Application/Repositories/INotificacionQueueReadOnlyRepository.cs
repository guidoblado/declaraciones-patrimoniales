using SRDP.Domain.Enumerations;
using SRDP.Domain.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface INotificacionQueueReadOnlyRepository
    {
        Task<NotificacionQueueItem> GetAsync(Guid id);
        Task<ICollection<NotificacionQueueItem>> GetByStatusAsync(QueueStatus queueStatus);
    }
}
