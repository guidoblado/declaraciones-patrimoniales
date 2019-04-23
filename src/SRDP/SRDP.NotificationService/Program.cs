using SRDP.NotificationService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using Topshelf.Autofac;
using Autofac;
using Autofac.Extras.Quartz;
using System.Configuration;
using Topshelf;
using System.Collections.Specialized;
using log4net;

namespace SRDP.NotificationService
{
    class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<EmailNotificationService>()
                            .AsSelf()
                            .InstancePerLifetimeScope();
            containerBuilder.RegisterType<NotificationJob>()
                .AsSelf()
                .InstancePerLifetimeScope();
            containerBuilder.RegisterAssemblyTypes(typeof(SRDP.Persitence.ApplicationModule).Assembly);

            #region Modules
            containerBuilder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = context =>
                    (NameValueCollection)ConfigurationManager.GetSection("quartz")
            });
            containerBuilder.RegisterType<NotificationJob>().AsImplementedInterfaces();
            containerBuilder.RegisterModule(new QuartzAutofacJobsModule(typeof(NotificationJob).Assembly));
            containerBuilder.RegisterModule(new Persitence.DapperDataAccess.Module() { ConnectionString = ConfigurationManager.ConnectionStrings["SRDPConnection"].ConnectionString });
            containerBuilder.RegisterModule(new Persitence.ApplicationModule());
            containerBuilder.RegisterModule(new UseCases.Module());
            containerBuilder.Register(c => LogManager.GetLogger(typeof(Object))).As<ILog>();
            //containerBuilder.RegisterModule(new Jobs.Module());
            #endregion


            IContainer container = containerBuilder.Build();

            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.SetServiceName("Email Notification");
                hostConfigurator.SetDisplayName("Email Notification");
                hostConfigurator.SetDescription("Reads Notification Queue and Send Email Notifications and created an empty Employee SRDP Form");

                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.UseLog4Net();
                hostConfigurator.UseAutofacContainer(container);

                hostConfigurator.Service<EmailNotificationService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(hostSettings => container.Resolve<EmailNotificationService>());
                    
                    serviceConfigurator.WhenStarted(service => service.OnStart());
                    serviceConfigurator.WhenStopped(service => service.OnStop());
                });
            });

        }
    }
}
