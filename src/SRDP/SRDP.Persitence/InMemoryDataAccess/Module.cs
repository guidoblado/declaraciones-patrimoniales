using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.InMemoryDataAccess
{
    using Autofac;

    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>()
                .As<Context>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(PersistenceException).Assembly)
                .Where(type => type.Namespace.Contains("InMemoryDataAccess"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        }
    }
}
