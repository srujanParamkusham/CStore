using Catalyst.MVC.Domain.Entities;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Model of a single effective permissions for a role and a securable.
    /// </summary>
    public class EffectivePermissionModel
    {
        /// <summary>
        /// The security securable action on the securable that the permissions are granted to.
        /// </summary>
        public SecuritySecurableAction SecuritySecurableAction { get; set; }

        /// <summary>
        /// Whether or not that action is allowed.
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        /// True if the permission on the current securable was inherited from a parent securable
        /// </summary>
        public bool Inherited { get; set; }

        /// <summary>
        /// If Inherited is true, then this will contain the securable that the 
        /// permission was inherited from.
        /// </summary>
        public SecuritySecurable SecuritySecurableInheritedFrom { get; set; }

        /// <summary>
        /// A message about how the security was applied to the securable/role/action.
        /// </summary>
        public string Message { get; set; }
    }
}
