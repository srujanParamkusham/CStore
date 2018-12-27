using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Service response object to get the active permissions for all of the roles for a securable.
    /// </summary>
    public class SecurityAccessMaintenanceGetEffectivePermissionsForSecurableResponse : DomainServiceResponse
    {
        /// <summary>
        /// The securable that the permissions are effective for.
        /// </summary>
        public SecuritySecurable SecuritySecurable { get; set; }

        /// <summary>
        /// The list of roles and their permissions on this securable.
        /// </summary>
        public List<EffectivePermissionsForRoleModel> EffectivePermissionsForRoles { get; set; }
    }
}
