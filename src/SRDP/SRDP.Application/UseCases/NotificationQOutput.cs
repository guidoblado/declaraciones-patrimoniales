using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class NotificationQOutput
    {
        public Guid ID { get; private set; }
        public int FuncionarioID { get; private set; }
        public string UserName { get; private set; }
        public string ActionType { get; private set; }
        public string Status { get; private set; }
        public string ErrorMessage { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ModifyDate { get; private set; }

        public NotificationQOutput(Guid id, int funcionarioID, string userName, string actionType, string queueStatus,
            DateTime createDate, DateTime modifyDate, string errorMessage)
        {
            ID = id;
            FuncionarioID = funcionarioID;
            UserName = userName;
            ActionType = actionType;
            Status = queueStatus;
            CreateDate = createDate;
            ModifyDate = modifyDate;
        }
    }
}
