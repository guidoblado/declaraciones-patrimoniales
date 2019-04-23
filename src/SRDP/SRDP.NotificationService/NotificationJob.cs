using log4net;
using Quartz;
using SRDP.Application.UseCases.ProcessNotificationQueue;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.NotificationService
{
    public class NotificationJob : IJob
    {
        private ILog Log { get;}
        private IProcessNotificationQueueUserCase ProcessNotificationQueueUserCase { get; }

        public NotificationJob(ILog log, IProcessNotificationQueueUserCase processNotificationQueueUserCase )
        {
            Log = log ?? throw new ArgumentNullException(nameof(log));
            ProcessNotificationQueueUserCase = processNotificationQueueUserCase ?? throw new ArgumentNullException(nameof(processNotificationQueueUserCase));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => Log.Info("Procesando NotificationQueue..."));
            var webServerURL = ConfigurationManager.AppSettings["WebServerURL"];
            var fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            await ProcessNotificationQueueUserCase.Execute(webServerURL, fromAddress);
        }
    }
}
