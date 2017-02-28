[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Challenge.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Challenge.WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace Challenge.WebApi.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Challenge.Service;
    using Challenge.Model;
    using Challenge.Data;
    using Ninject;
    using Ninject.Web.Common;
    using Challenge.Data.Interfaces;
    using Challenge.Service.Interfaces;
    using System.Web.Http;
    using Ninject.Web.Mvc.FilterBindingSyntax;
    using Util;
    using Ninject.Web.WebApi.FilterBindingSyntax;
    using System.Web.Http.Filters;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                // Install our Ninject-based IDependencyResolver into the Web API config
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.BindHttpFilter<AppServiceFilter>(FilterScope.Action)
              .WhenActionMethodHas<AppFilterAttribute>().InRequestScope();
            kernel.Bind<IContext>().To<DBContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind<IService<Application>>().To<ApplicationService>().InRequestScope();
            kernel.Bind<IService<Log>>().To<LogService>().InRequestScope();
            kernel.Bind<IService<SessionTime>>().To<SessionTimeService>().InRequestScope();
          
        }
    }
}
