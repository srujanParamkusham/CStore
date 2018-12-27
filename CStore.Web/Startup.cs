using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SWCPTOTrack.Web.Startup))]
namespace SWCPTOTrack.Web
{
    /// <summary>
    /// Main Owin Startup class, used to register OWIN Authentication when the application starts.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Execute Configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
