using Catalyst.MVC.Infrastructure.Models.User;
using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;
using CStore.Domain.Services.State;

namespace CStore.Web.Glimpse
{
    /// <summary>
    /// Custom runtime security policy for Glimpse
    /// </summary>
    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        /// <summary>
        /// Execute and apply the runtime policy.
        /// </summary>
        /// <param name="policyContext"></param>
        /// <returns></returns>
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            //Check web.config to see if we globally have glimpse turned on or off
            if (!DomainApplicationService.Instance.GlimpseEnabled)
            {
                return RuntimePolicy.Off;
            }

            //Running locally, always turn glimpse on
            if (policyContext.GetHttpContext().Request.IsLocal)
            {
                return RuntimePolicy.On;
            }

            //Otherwise make sure user is a system admin for it to be on and to get to
            //the widgets to view glimpse data
            if (DomainSessionService.Instance != null && DomainSessionService.Instance.CurrentUser != null &&
                DomainSessionService.Instance.CurrentUser.SystemAdmin)
            {
                return RuntimePolicy.On;
            }

            //Otherwise, only collect data for glimpse from the users but dont allow them to see it.
            return RuntimePolicy.PersistResults;
        }

        /// <summary>
        /// Executes the above custom security policy on these events
        /// </summary>
        public RuntimeEvent ExecuteOn
        {
            // The RuntimeEvent.ExecuteResource is only needed in case you create a security policy
            // Have a look at http://blog.getglimpse.com/2013/12/09/protect-glimpse-axd-with-your-custom-runtime-policy/ for more details
            //TEA Be sure to use EndSessionAccess instead of EndRequest so that the users session is available in the above check
            get { return RuntimeEvent.EndSessionAccess | RuntimeEvent.ExecuteResource; }
        }
    }
}
