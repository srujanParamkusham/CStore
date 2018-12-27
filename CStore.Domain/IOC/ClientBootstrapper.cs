using Catalyst.MVC.Domain.Providers.Authentication;
using Catalyst.MVC.Domain.Providers.Security;
using Catalyst.MVC.Domain.Providers.Token;
using Catalyst.MVC.Infrastructure.IOC;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using Catalyst.MVC.Infrastructure.Providers.Security;
using Catalyst.MVC.Infrastructure.Providers.Token;
using Catalyst.MVC.Infrastructure.Providers.User;
using Catalyst.MVC.Infrastructure.Util.Token;
using CStore.Domain.Domains.Authentication.Services;

namespace CStore.Domain.IOC
{
    /// <summary>
    /// This class is used to setup initialize the StructureMap IOC Container. 
    /// You should call a method such as this to perform the initialization:
    /// var iocContainer = new ClientBootstrapper().Run();
    /// </summary>
    public class ClientBootstrapper : Bootstrapper
    {
        /// <summary>
        /// Custom initialization logic where you would tell StructureMap which 
        /// implementations to use for each interface class.
        /// </summary>
        /// <param name="container"></param>
        protected override void ClientInit(StructureMap.Container container)
        {
            container.Configure(x =>
            {
                x.For<ISecurityProvider>().Use<SecurityProvider>();
                x.For<IUserProvider>().Use<SessionUserProvider>();
                x.For<ISendMailProvider>().Use(() => new SendMailProvider());
                x.For<IApplicationMembershipProvider>().Use<FormsApplicationMembershipProvider>();
                x.For<ITokenResolver>().Use<TokenResolver>();
                x.For<ITokenResolutionProvider>().Use<TokenResolutionProvider>();
                x.For<IDomainLoginService>().Use<DomainLoginService>();
                x.For<IDomainLogoutService>().Use<DomainLogoutService>();

                /*
                TODO:  Choose the appropriate membership provider if you need a custom one, and then remove the Forms 
                 * membership provider above.
                x.For<IApplicationMembershipProvider>().Use<ApplicationMembershipProvider>();
                */

            });
        }
    }
}