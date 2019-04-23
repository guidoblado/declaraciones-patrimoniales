﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.NotificationService.UseCases
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(NotificationException).Assembly)
                .Where(type => type.Namespace.Contains("UseCases"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
