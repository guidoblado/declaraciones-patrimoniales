using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Domain.Enumerations;
using SRDP.Domain.Notificaciones;

namespace SRDP.Application.UseCases.UpdateNotificationQ
{
    public class UpdateNotificationQUserCase : IUpdateNotificationQUserCase
    {
        public readonly INotificacionQueueWriteOnlyRepository _notificacionQueueWriteOnlyRepository;

        public UpdateNotificationQUserCase(INotificacionQueueWriteOnlyRepository notificacionQueueWriteOnlyRepository)
        {
            _notificacionQueueWriteOnlyRepository = notificacionQueueWriteOnlyRepository;
        }

        public async Task<NotificationQOutput> Execute(NotificacionQueueItem queueItem)
        {
            await _notificacionQueueWriteOnlyRepository.Update(queueItem);

            return new NotificationQOutput(queueItem.ID, queueItem.FuncionarioID, queueItem.UserName, queueItem.ActionType.ToString(), queueItem.Status.ToString(),
                queueItem.CreateDate, queueItem.ModifyDate, queueItem.ErrorMessage);
        }

        public async Task<NotificationQOutput> Execute(Guid id, int funcionarioID, string userName, string actionType, string queueStatus, DateTime createDate, DateTime modifyDate, string errorMessage)
        {
            var actionTypeEnum = (ActionType)Enum.Parse(typeof(ActionType), actionType);
            var queueStatusEnum = (QueueStatus)Enum.Parse(typeof(QueueStatus), queueStatus);
            var queueItem = NotificacionQueueItem.Load(id, funcionarioID, userName, actionTypeEnum, queueStatusEnum, createDate, modifyDate, errorMessage);
            return new NotificationQOutput(queueItem.ID, queueItem.FuncionarioID, queueItem.UserName, queueItem.ActionType.ToString(), queueItem.Status.ToString(),
                queueItem.CreateDate, queueItem.ModifyDate, queueItem.ErrorMessage);
        }
    }
}
