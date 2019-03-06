using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SRDP.Application.Repositories;
using SRDP.Persitence.DapperDataAccess.Repositories;

namespace SRDP.Persitence.DapperDataAccess
{
    public class Module : Autofac.Module
    {
        public string ConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(PersistenceException).Assembly)
                .Where(type => type.Namespace.Contains("DapperDataAccess"))
                .WithParameter("connectionString", ConnectionString)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            //builder.RegisterType<DeclaracionRepository>().As<IDeclaracionReadOnlyRepository>().InstancePerRequest();
        }
    }
}
