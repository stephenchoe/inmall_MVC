using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Configuration;


namespace AdminApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Autofac
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            string sendGridKey = ConfigurationManager.AppSettings["sendGridKey"];
            string gRecaptchaSecret = ConfigurationManager.AppSettings["Recaptcha2Secret"];
            BLL.AutofacConfig.RegisterWebAppTypes(builder, sendGridKey, gRecaptchaSecret);


            builder.RegisterModule(new AutofacWebTypesModule());

            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
