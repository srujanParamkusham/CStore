using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
//Need an alias to avoid namespace conflicts
using GlimpseAspNet = Glimpse.AspNet;

namespace CStore.Web.Glimpse
{
    /// <summary>
    /// The GlimpseSecurityPolicy that executes for the Glimpse.axd file does not work out of the box with a session, so
    /// we are unable to check if the user is logged in or not to secure that page using the Catalyst framework.
    /// Instead, we use this class which implements IRequireSessionState, giving the GlimpseSecurityPolicy access to the session.
    /// See this post: http://stackoverflow.com/questions/28611058/retrieve-the-session-in-the-glimpsesecuritypolicy-runtimeevent-executeresource
    /// </summary>
    public class SessionAwareGlimpseHttpHandler : IHttpHandler, IRequiresSessionState
    {
        private readonly GlimpseAspNet.HttpHandler _glimpseHttpHandler =
            new GlimpseAspNet.HttpHandler();

        public void ProcessRequest(HttpContext context)
        {
            _glimpseHttpHandler.ProcessRequest(context);
        }

        public bool IsReusable
        {
            get { return _glimpseHttpHandler.IsReusable; }
        }
    }
}
