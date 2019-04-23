using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetNotificationQ
{
    public interface IGetNotificationQUserCase
    {
        Task<NotificationQOutput> Execute(Guid id);
        Task<ICollection<NotificationQOutput>> ExecuteList(QueueStatus queueStatus);
    }
}
