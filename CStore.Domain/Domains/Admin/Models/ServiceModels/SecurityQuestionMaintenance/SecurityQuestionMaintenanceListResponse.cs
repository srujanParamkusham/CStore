﻿using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityQuestionMaintenance
{
    /// <summary>
    /// Service response object to search for a list of security questions
    /// </summary>
    public class SecurityQuestionMaintenanceListResponse : DomainServicePagedListResponse<SecurityQuestion>
    {

    }
}
