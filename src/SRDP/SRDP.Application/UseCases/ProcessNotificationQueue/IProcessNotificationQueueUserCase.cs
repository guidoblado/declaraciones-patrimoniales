using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.ProcessNotificationQueue
{
    public interface IProcessNotificationQueueUserCase
    {
        Task<bool> Execute(string serverPath, string fromAddress);
    }
}
