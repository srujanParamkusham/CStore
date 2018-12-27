using CStore.Domain.Models.ServiceModels;
using System;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityUserMaintenance
{
    /// <summary>
    /// Service request object to search for a list of users
    /// </summary>
    public class SecurityUserMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The username to filter on
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// The users last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The role assigned to the user
        /// </summary>
        public List<int> SecurityRoleIds { get; set; }

        /// <summary>
        /// If the user is active or not
        /// </summary>
        public bool? Active { get; set; }

        /// <summary>
        /// Filter as to if the user is a system admin
        /// </summary>
        public bool? SystemAdmin { get; set; }

        /// <summary>
        /// The starting last login date to filter on
        /// </summary>
        public DateTime? LastLoginDateStart { get; set; }

        /// <summary>
        /// The ending last login date to filter on
        /// </summary>
        public DateTime? LastLoginDateEnd { get; set; }


    }
}
