using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using log4net;
using Quartz;
using SRDP.NotificationService.Jobs;

namespace SRDP.NotificationService.Services
{
    public class EmailNotificationService
    {
        private IScheduler Scheduler { get; }
        private int IntervaloMinutos { get; }

        public EmailNotificationService(IScheduler scheduler)
        {
            Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
            IntervaloMinutos = Convert.ToInt32(ConfigurationManager.AppSettings["IntervaloEnMinutos"]);
        }

        public void OnStart()
        {
            Scheduler.Start();
            IJobDetail job = JobBuilder
                    .Create<NotificationJob>()
                    .WithIdentity(typeof(NotificationJob).Name, SchedulerConstants.DefaultGroup)
                    .Build();

            ITrigger trigger = TriggerBuilder
                                .Create()
                                .WithIdentity("simpletrigger", SchedulerConstants.DefaultGroup)
                                .ForJob(job)
                                .StartNow()
                                .WithSimpleSchedule( x=> x
                                    .WithIntervalInMinutes(IntervaloMinutos)
                                    .RepeatForever())
                                .Build();

            Scheduler.ScheduleJob(job, trigger);

        }

        public void OnPaused()
        {
            Scheduler.PauseAll();
        }

        public void OnContinue()
        {
            Scheduler.ResumeAll();
        }

        public void OnStop()
        {
            Scheduler.Shutdown();
        }
    }
}
