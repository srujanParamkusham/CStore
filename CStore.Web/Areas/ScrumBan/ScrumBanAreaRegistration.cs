using System.Web.Mvc;

namespace CStore.Web.Areas.ScrumBan
{
    public class ScrumBanAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ScrumBan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ScrumBan_default",
                "ScrumBan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}