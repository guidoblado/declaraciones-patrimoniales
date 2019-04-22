using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Domain.Enumerations;

namespace SRDP.Application.UseCases.GetNotificationQ
{
    public class GetNotificationQUserCase : IGetNotificationQUserCase
    {
        private readonly INotificacionQueueReadOnlyRepository _notificacionQueueReadOnlyRepository;

        public GetNotificationQUserCase(INotificacionQueueReadOnlyRepository notificacionQueueReadOnlyRepository)
        {
            _notificacionQueueReadOnlyRepository = notificacionQueueReadOnlyRepository;
        }

        public async Task<NotificationQOutput> Execute(Guid id)
        {
            var notificacionQItem = await _notificacionQueueReadOnlyRepository.GetAsync(id);
            return new NotificationQOutput(notificacionQItem.ID, notificacionQItem.FuncionarioID, notificacionQItem.UserName, notificacionQItem.ActionType.ToString(),
                notificacionQItem.Status.ToString(), notificacionQItem.CreateDate, notificacionQItem.ModifyDate, notificacionQItem.ErrorMessage);
        }

        public async Task<ICollection<NotificationQOutput>> ExecuteList(QueueStatus queueStatus)
        {
            var notificacionesQueueList = await _notificacionQueueReadOnlyRepository.GetByStatusAsync(queueStatus);

            var outputList = new List<NotificationQOutput>();

            if (notificacionesQueueList == null) return outputList;

            foreach (var notificacionQItem in notificacionesQueueList)
            {
                outputList.Add(new NotificationQOutput(notificacionQItem.ID, notificacionQItem.FuncionarioID, notificacionQItem.UserName, notificacionQItem.ActionType.ToString(),
                notificacionQItem.Status.ToString(), notificacionQItem.CreateDate, notificacionQItem.ModifyDate, notificacionQItem.ErrorMessage));
            }
            return outputList;
        }
    }
}
