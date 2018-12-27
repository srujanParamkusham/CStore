using Catalyst.MVC.Domain.Entities;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Model of a single effective permissions for a role and a securable.
    /// </summary>
    public class SecurityResultModel
    {
        /// <summary>
        /// The security securable that the permission was granted to.
        /// </summary>
        public SecuritySecurable SecuritySecurable { get; set; }

        /// <summary>
        /// Whether or not that action is allowed.
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        /// Message about how security is setup on the securable.
        /// </summary>
        public string Message { get; set; }
    }
}
