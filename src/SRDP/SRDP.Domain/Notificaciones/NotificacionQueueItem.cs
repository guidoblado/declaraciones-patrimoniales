using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Notificaciones
{
    public class NotificacionQueueItem : IEntity
    {
        public Guid ID { get; private set; }
        public int FuncionarioID { get; private set; }
        public string UserName { get; private set; }
        public ActionType ActionType { get; private set; }
        public QueueStatus Status { get; private set; }
        public string ErrorMessage { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ModifyDate { get; private set; }

        #region Constructors
        public NotificacionQueueItem(int funcionarioID, string userName, ActionType actionType, QueueStatus queueStatus)
        {
            ID = Guid.NewGuid();
            FuncionarioID = funcionarioID;
            UserName = userName;
            ActionType = actionType;
            Status = queueStatus;
            CreateDate = DateTime.Now;
            ModifyDate = DateTime.Now;
            ErrorMessage = String.Empty;
        }

        private NotificacionQueueItem() { }

        #endregion

        public static NotificacionQueueItem Load(Guid id, int funcionarioID, string userName, ActionType actionType, QueueStatus queueStatus, 
            DateTime createDate, DateTime modifyDate, string errorMessage)
        {
            return new NotificacionQueueItem
            {
                ID = id,
                FuncionarioID = funcionarioID,
                UserName = userName,
                ActionType = actionType,
                Status = queueStatus,
                CreateDate = createDate,
                ModifyDate = modifyDate,
                ErrorMessage = errorMessage,
            };
        }
    }
}
