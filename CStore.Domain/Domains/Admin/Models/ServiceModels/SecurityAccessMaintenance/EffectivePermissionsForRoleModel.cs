using Catalyst.MVC.Domain.Entities;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Model of all of the effective permissions for a role for a given securable.
    /// </summary>
    public class EffectivePermissionsForRoleModel
    {
        /// <summary>
        /// The role the permissions are effective for.
        /// </summary>
        public SecurityRole SecurityRole { get; set; }

        /// <summary>
        /// The effective permissions on the securable for this role
        /// </summary>
        public List<EffectivePermissionModel> EffectivePermissions { get; set; }
    }
}
