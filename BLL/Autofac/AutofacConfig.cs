using System.Reflection;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;
using DAL;
using DAL.Infrastructure;
using BLL;
using Web.Core.ErrorHandler;
using Web.Core;
using Web.Core.Recaptcha;

namespace BLL
{
    public class AutofacConfig
    {
        public static void RegisterWebAppTypes(ContainerBuilder builder, string emailKey, string gRecaptchaSecret)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerDependency();
            builder.RegisterType<ElmahLogger>().As<IErrorLogger>().InstancePerDependency();

            builder.RegisterType<SendGridEmailService>().As<ISendGridEmailService>().InstancePerDependency()
               .WithParameter("key", emailKey);
            builder.RegisterType<Recaptcha2Verifier>().As<IRecaptcha2Verifier>().InstancePerDependency()
              .WithParameter("gRecaptchaSecret", gRecaptchaSecret);

            builder.RegisterAssemblyTypes(typeof(CityRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(AddressService).Assembly)
             .Where(t => t.Name.EndsWith("Service"))
             .AsImplementedInterfaces().InstancePerDependency();

        }
    }
}
