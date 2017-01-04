using WebApp.DI;
using WebApp.DI.Autofac;
using WebApp.DI.Autofac.Modules;
using Autofac;
using Autofac.Core;
using System.Configuration;
using Autofac.Integration.Mvc;
using System.Reflection;
using WebApp.Services;

public class CompositionRoot
{
    public static IDependencyInjectionContainer Compose()
    {
         
        var builder = new ContainerBuilder();

        builder.RegisterAssemblyTypes(typeof(ShoppingService).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces().InstancePerDependency();
      

        string sendGridKey = ConfigurationManager.AppSettings["sendGridKey"];
        string gRecaptchaSecret = ConfigurationManager.AppSettings["Recaptcha2Secret"];
        BLL.AutofacConfig.RegisterWebAppTypes(builder, sendGridKey, gRecaptchaSecret);


        builder.RegisterModule(new AutofacWebTypesModule());


        builder.RegisterModule(new MvcSiteMapProviderModule());
        builder.RegisterModule(new MvcModule());

        var container = builder.Build();

        // Return our DI container wrapper instance
        return new AutofacDependencyInjectionContainer(container);
    }
}

