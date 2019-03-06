using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using SRDP.Persitence.DapperDataAccess;
using System.Configuration;

namespace SRDP.WebUI.App_Start
{
    public class IoCConfig
    {
        public static void RegisterDependencies()
        {
            #region Create the builder
            var builder = new ContainerBuilder();
            #endregion
            // MVC - Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(SRDP.Persitence.ApplicationModule).Assembly);

            #region Register modules
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new Persitence.DapperDataAccess.Module() { ConnectionString = ConfigurationManager.ConnectionStrings["SRDPConnection"].ConnectionString });
            builder.RegisterModule(new Persitence.ApplicationModule());
            #endregion
            #region Model binder providers - excluded - not sure if need
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterModelBinderProvider();
            #endregion

            #region Inject HTTP Abstractions
            /*
             The MVC Integration includes an Autofac module that will add HTTP request 
             lifetime scoped registrations for the HTTP abstraction classes. The 
             following abstract classes are included: 
            -- HttpContextBase 
            -- HttpRequestBase 
            -- HttpResponseBase 
            -- HttpServerUtilityBase 
            -- HttpSessionStateBase 
            -- HttpApplicationStateBase 
            -- HttpBrowserCapabilitiesBase 
            -- HttpCachePolicyBase 
            -- VirtualPathProvider 

            To use these abstractions add the AutofacWebTypesModule to the container 
            using the standard RegisterModule method. 
            */
            builder.RegisterModule<AutofacWebTypesModule>();

            #endregion

            #region Set the MVC dependency resolver to use Autofac
            // MVC - Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion
        }
    }
}