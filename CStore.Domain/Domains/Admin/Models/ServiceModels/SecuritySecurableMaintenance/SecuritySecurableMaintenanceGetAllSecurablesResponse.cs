using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecuritySecurableMaintenance
{
    /// <summary>
    /// Service response object to get all of the security securables, used to create select lists with
    /// </summary>
    public class SecuritySecurableMaintenanceGetAllSecurablesResponse : DomainServiceResponse
    {
        public List<SecuritySecurable> SecuritySecurables { get; set; }
    }
}
