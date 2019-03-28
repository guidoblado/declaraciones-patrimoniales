using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    public class NotificationQ
    {
        public Guid ID { get; set; }
        public int FuncionarioID { get; set; }
        public string UserName { get; set; }
        public string ActionType { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
