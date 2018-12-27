using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using Catalyst.MVC.Domain.Providers.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using CStore.Domain.Services.State;
using CStore.Web.App_Start;

namespace SWCPTOTrack.Web
{
    /// <summary>
    /// Configure the authentication for the application.
    /// This is used instead of the <authentication mode="Forms"> in web.config.
    /// You must have <authentication mode="None"> in web.config, and remove the FormAuthentication module in system.webserver.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configure the OWIN Authentication
        /// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ApplicationCookie);

            //
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // This is for forms based cookie authentication.
            //
            // Important! When using only windows authentication, and anonymous authentication is disabled, then you MUST comment out the 
            // Forms Based Authentication setup below. Otherwise, you will end up in an endless loop redirecting to the login page. This is only needed to be done when 
            // Windows auth is enabled, and anonymous auth is disabled. Comment out this entire section:
            //
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Authentication/Login"),
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(2880),
                SlidingExpiration = true,
                CookieSecure = CookieSecureOption.SameAsRequest,
                Provider = new CookieAuthenticationProvider
                {
                    OnResponseSignIn = ctx =>
                    {
                        //This is the last chance before the ClaimsIdentity get serialized into a cookie. You can modify the ClaimsIdentity here and create the mapping here. This event is invoked one time on sign in. 
                        var ident = ctx.Identity;
                    },
                    OnValidateIdentity = async ctx =>
                    {
                        //This method gets invoked for every request after the cookie is converted into a ClaimsIdentity. Here you can look up your claims from the mapping table. 
                        var userId = ctx.Identity.GetUserId(); //Just a simple extension method to get the ID using identity.FindFirst(x => x.Type == ClaimTypes.NameIdentifier) and account for possible NULLs
                    },
                    OnApplyRedirect = ctx =>
                    {
                        //
                        //Do not redirect WebApi requests that are unauthorized to the login page.
                        //For WebApi, you dont want the user to be returned an html page when they are expecting json.
                        //
                        var owinContext = ctx.OwinContext;
                        if (!IsApiRequest(ctx.Request))
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //
            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            //
            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            //
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            //
            //ADFS Authentication
            //
            /*Uncomment the below to enable ADFS Authentication
            app.UseWsFederationAuthentication(
                new WsFederationAuthenticationOptions
                {
                    Wtrealm = DomainApplicationService.Instance.ADFSRealm,
                    MetadataAddress = DomainApplicationService.Instance.ADFSMetadataURL,
                    SignInAsAuthenticationType = WsFederationAuthenticationDefaults.AuthenticationType,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = DomainApplicationService.Instance.ADFSValidAudience
                    },
                    Caption = "ADFS",
                    //Change to Active if you want ADFS auth to be the primary auth, otherwise set to Passive if
                    //the user must select it off the login screen.
                    AuthenticationMode = AuthenticationMode.Active,
                    Notifications = new WsFederationAuthenticationNotifications()
                    {
                        RedirectToIdentityProvider = (ctx) =>
                        {
                            //To avoid a redirect loop to the federation server send 403 when user is authenticated but does not have access
                            if (ctx.OwinContext.Response.StatusCode == 401 && ctx.OwinContext.Authentication.User.Identity.IsAuthenticated)
                            {
                                ctx.OwinContext.Response.StatusCode = 403;
                                ctx.HandleResponse();
                            }
                            //Set the address that ADFS will redirect back to after an authentication
                            if (ctx.Request != null && ctx.Request.Uri != null)
                            {
                                ctx.ProtocolMessage.Wreply = ctx.Request.Uri.ToString();
                            }
                            return Task.FromResult(0);                            
                        },
                        SecurityTokenValidated = (ctx) =>
                        {
                            //
                            //If the token has been validated, then renew the users session state. If we dont do this, the user will
                            //not be signed in, and will get into an infinite redirect loop with the ADFS server, until the ADFS server 
                            //throws an error after about 6 loops.
                            //
                            var membershipService =
                                DomainApplicationService.Instance.IOCContainer
                                    .GetInstance<IApplicationMembershipProvider>();
                            membershipService.RenewCurrentUserSessionState();

                            //
                            //Ignore scheme/host name in redirect Uri to make sure a redirect to HTTPS does not redirect back to HTTP
                            //
                            if (ctx.AuthenticationTicket != null && ctx.AuthenticationTicket.Properties != null &&
                                !String.IsNullOrWhiteSpace(ctx.AuthenticationTicket.Properties.RedirectUri))
                            {
                                var redirectUri = new Uri(ctx.AuthenticationTicket.Properties.RedirectUri, UriKind.RelativeOrAbsolute);
                                if (redirectUri.IsAbsoluteUri)
                                {
                                    ctx.AuthenticationTicket.Properties.RedirectUri = redirectUri.PathAndQuery;
                                }
                            }

                            return Task.FromResult(0);
                        }
                        //TODO Need to be able to specify the encryption certs. Private key stays with app, public key gets loaded on adfs server
                    }
                });
            */
        }

        /// <summary>
        /// Returns whether or not this is a Web Api request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static bool IsApiRequest(IOwinRequest request)
        {
            string apiPathPrefix = WebApiConfig.UrlPrefix;
            if (String.IsNullOrWhiteSpace(apiPathPrefix))
            {
                return false;
            }
            if (!apiPathPrefix.StartsWith("/"))
            {
                apiPathPrefix = "/" + apiPathPrefix;
            }
            if (!apiPathPrefix.EndsWith("/"))
            {
                apiPathPrefix += "/";
            }
            //See if the request path starts with /api/
            return request.Uri.AbsolutePath.StartsWith(apiPathPrefix);
        }

    }
}
