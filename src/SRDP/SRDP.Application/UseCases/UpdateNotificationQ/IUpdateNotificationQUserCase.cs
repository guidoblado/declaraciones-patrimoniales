using SRDP.Domain.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.UpdateNotificationQ
{
    public interface IUpdateNotificationQUserCase
    {
        Task<NotificationQOutput> Execute(NotificacionQueueItem queueItem);
        Task<NotificationQOutput> Execute(Guid id, int funcionarioID, string userName, string actionType, string queueStatus,
            DateTime createDate, DateTime modifyDate, string errorMessage);
    }
}
