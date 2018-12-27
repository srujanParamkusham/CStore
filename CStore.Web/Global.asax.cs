using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Domain.Providers.Authentication;
using Catalyst.MVC.Domain.Providers.Profiling;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Services.Log;
using Catalyst.MVC.Infrastructure.Services.State;
using CStore.Domain.IOC;
using CStore.Domain.Services.State;
using CStore.Web.App_Start;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Microsoft.Owin.Security;

namespace CStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Application Start

        /// <summary>
        /// Check if this is a Web API request
        /// </summary>
        /// <returns></returns>
        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }

        /// <summary>
        /// Application Start
        /// </summary>
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            LogService.Instance.SetApplicationVariables(DomainApplicationService.Instance.MachineName, DomainApplicationService.Instance.ApplicationCode);

            LogService.Instance.Log.InfoFormat("Global.asax Application_Start() Called.");

            DomainApplicationService.Instance.IOCContainer = new ClientBootstrapper().Run();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            EntityConfig.Register();
            CacheConfig.PreloadItems();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ModelBinderConfig.Register();

            //
            //If you want to ignore the certificate check when requesting an https resource, uncomment the following code.
            //This is useful if your app connects uses https/ssl resources which dont have a cert issued by a valid 
            //signing authority.
            //This should probably never be uncommented in a production app though. Its here for reference in case its needed.
            //
            //ServicePointManager.ServerCertificateValidationCallback = delegate
            //{
            //    return true;
            //};
        }

        #endregion

        #region Application End

        /// <summary>
        /// Application End
        /// </summary>
        protected void Application_End()
        {
            LogService.Instance.Log.InfoFormat("Global.asax Application_End() Called.");
            //Flush the profiler queue if needed
            EntityFrameworkSQLProfilingProvider.FlushQueue();
        }
        #endregion

        #region Request event pipeline handlers
        /// <summary>
        /// Application PostAuthorizeRequest
        /// Occurs when the user for the current request has been authorized.
        /// </summary>
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        /// <summary>
        /// Session Start
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        /// <remarks>
        /// Session Start
        /// </remarks>
        protected virtual void Session_Start(Object sender, EventArgs e)
        {
            LogService.Instance.SetSessionId(DomainSessionService.Instance.SessionId);
        }

        /// <summary>
        /// Application PostAcquireRequestState
        /// Occurs when the request state (for example, session state) that is associated with the current request has been obtained.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            //
            //Put start time and transaction ID in the context so we can use it to determine 
            //how long the page takes to render
            //
            String transactionId = Guid.NewGuid().ToString();
            DomainHTTPContextService.Instance.RequestHandlerExecuteStartTime = DateTime.Now;
            DomainHTTPContextService.Instance.RequestTransactionId = transactionId;

            //
            //Set the session ID and transaction ID as an MDC variable in Log4Net so that they can be added
            //to log entries
            //
            HttpContext context = HttpContext.Current;
            if (context != null && (context.Session != null || context.Handler is IRequiresSessionState))
            {
                LogService.Instance.SetSessionId(Session.SessionID);
            }
            LogService.Instance.SetTransactionId(transactionId);

            //
            //Sign the user back in and hydrate their user session objects
            //when returning to an existing session
            //
            if (Request.IsAuthenticated && HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                if (DomainSessionService.Instance.CurrentUser == null || DomainSessionService.Instance.CurrentUser.Anonymous)
                {
                    var membershipService =
                        DomainApplicationService.Instance.IOCContainer.GetInstance<IApplicationMembershipProvider>();
                    membershipService.RenewCurrentUserSessionState();
                }
            }

            //
            //Store the current username as an MDC variable in Log4Net
            //
            if (DomainSessionService.Instance != null && DomainSessionService.Instance.CurrentUser != null)
            {
                LogService.Instance.SetUserName(DomainSessionService.Instance.CurrentUser.UserName);
            }

            //
            //Store the date and time of the last request for this session
            //
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                DomainSessionService.Instance.LastRequestDate = DateTime.Now;
                //Record when this session will timeout
                if (Session != null && Session.Timeout > 0)
                {
                    DomainSessionService.Instance.SessionTimeoutDate =
                        DomainSessionService.Instance.LastRequestDate.Value.AddMinutes(Session.Timeout);
                }
            }
        }

        /// <summary>
        /// Occurs when the ASP.NET event handler (for example, a page or an XML Web service) begins execution.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Occurs when the ASP.NET event handler (for example, a page or an XML Web service) finishes execution.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            //
            //Record the profiling entry if enabled
            //
            RecordHttpProfilingEntry();
        }

        #endregion

        #region Logging
        /// <summary>
        /// Log Exception
        /// Occurs when an unhandled exception is thrown.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        void Application_Error(Object sender, EventArgs e)
        {
            string information = "";
            try
            {
                if (Request != null)
                {
                    information = "Url: " + this.Request.RawUrl + " | ";
                    information += "Referrer: " + this.Request.UrlReferrer + " | ";
                }
            }
            catch { }

            Exception ex = Server.GetLastError().GetBaseException();
            DomainHTTPContextService.Instance.LastException = ex;

            if (ex != null)
            {
                information += "Exception: " + ex.ToString() + " | ";
            }

            //
            //Record the profiling entry if enabled
            //
            RecordHttpProfilingEntry(true, information);

            //
            //Only log controller not found errors as warnings, not errors, since the 404 page will end up
            //being displayed to the user
            //The exception looks like the following:
            //System.Web.HttpException (0x80004005): The controller for path '/4r34r/34r34r/34r3' was not found or does not implement IController.
            //
            if (ex.Message.Contains("The controller for path") &&
                ex.Message.Contains("was not found or does not implement IController"))
            {
                LogService.Instance.Log.Warn(information, ex);
            }
            else
            {
                LogService.Instance.LogException(information, ex);
            }
        }
        #endregion

        #region Profiling Logging/Recording
        /// <summary>
        /// Record the SecurityUserActivityHistory record if profiling is enabled
        /// </summary>
        /// <param name="isException"></param>
        /// <param name="additionalInfo"></param>
        private void RecordHttpProfilingEntry(bool isException = false, String additionalInfo = null)
        {
            //
            //Persist profiling data to the database
            //Also, dont profile glimpse requests, this is counterproductive and causes a big performance hit
            //
            var currentHandlerName = HttpContext.Current.Handler.GetType().ToString();
            if (!ApplicationService.Instance.HTTPRequestProfilingEnabled
                || Request.RawUrl.Contains("/Glimpse.axd")
                //Visual studio debug browser link, dont log these items
                || Request.RawUrl.Contains("/__browserLink/")
                || (currentHandlerName.Equals("System.Web.Optimization.BundleHandler"))
                )
            {
                //Dont profile in this case
                return;
            }

            //
            //Add in additional profiler info if it was set in other modules.
            //
            var additionalProfilerInfo = DomainHTTPContextService.Instance.ProfilerAdditionalInfo;
            if (!String.IsNullOrWhiteSpace(additionalProfilerInfo))
            {
                //Init the string if necessary
                if (String.IsNullOrWhiteSpace(additionalInfo))
                {
                    additionalInfo = "";
                }
                additionalInfo += additionalProfilerInfo;
            }

            //We already put start time in the context so we can use it to determine how long the 
            //page takes to render, so retrieve it here
            var requestHandlerExecuteStartTime = DomainHTTPContextService.Instance.RequestHandlerExecuteStartTime.Value;
            var requestHandlerExecuteEndTime = DateTime.Now;
            TimeSpan span = requestHandlerExecuteEndTime - requestHandlerExecuteStartTime;
            int loadTimeMS = (int)span.TotalMilliseconds;
            var transactionId = DomainHTTPContextService.Instance.RequestTransactionId;
            var httpMethod = Request.HttpMethod;
            var url = Request.RawUrl;
            var referrer = Request.UrlReferrer;
            var statusCode = Response.StatusCode;
            var user = DomainSessionService.Instance.CurrentUser;
            int? securityUserId = DomainSessionService.Instance.CurrentUser.UserIdAsNullableInt;

            //
            //Since we dont have an HTTP Context at this point, we cannot use the Domain Application Service to get the IOC Container,
            //So instead we need to initialize a new one
            //
            var iocContainer = new ClientBootstrapper().Run();
            using (var repository = iocContainer.GetInstance<IRepository>())
            {
                //
                //Update the login history record with the last request date, and record the activity that just occurred
                //
                if (user != null && user.SecurityUserLoginHistoryId != null)
                {
                    var loginHistory = repository.GetAll<SecurityUserLoginHistory>()
                                                    .FirstOrDefault(
                                                        p =>
                                                        p.SecurityUserLoginHistoryId ==
                                                        user.SecurityUserLoginHistoryId);
                    if (loginHistory != null)
                    {
                        loginHistory.LastRequestDate = DateTime.Now;
                        loginHistory.SessionTimeoutDate = DomainSessionService.Instance.SessionTimeoutDate;
                        loginHistory.LastRequestUrl = url;
                    }
                }

                //
                //Record the SecurityUserActivityHistory record
                //
                var securityUserActivityHistory = new SecurityUserActivityHistory()
                {
                    SecurityUserId = securityUserId,
                    SecurityUserLoginHistoryId =
                        DomainSessionService.Instance.CurrentUser.SecurityUserLoginHistoryId,
                    UserName = DomainSessionService.Instance.CurrentUser.UserName,
                    MachineName = ApplicationService.Instance.MachineName,
                    ApplicationCode = ApplicationService.Instance.ApplicationCode,
                    IPAddress = Request.UserHostAddress,
                    SessionId = DomainSessionService.Instance.SessionId,
                    TransactionId = transactionId,
                    RequestUrl = url,
                    Method = httpMethod,
                    StatusCode = statusCode,
                    LoadTimeMS = loadTimeMS,
                    Referrer = (referrer != null ? referrer.ToString() : null),
                    SecuritySecurableActionId = null,
                    AddtlInfo = additionalInfo
                };
                repository.Add(securityUserActivityHistory);
                repository.Commit();
            }
        }
        #endregion
    }
}
