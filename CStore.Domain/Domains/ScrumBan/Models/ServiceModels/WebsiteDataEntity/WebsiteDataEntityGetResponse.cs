using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Catalyst.MVC.Infrastructure.Attributes.Validation;
using CStore.Domain.Entities;
using CStore.Domain.Domains.ScrumBan.Models.ServiceModels;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.ScrumBan.Models.ServiceModels.WebsiteDataEntity
{
  /// <summary>
  /// Service request object for GetResponse 
  /// </summary>
  public class WebsiteDataEntityGetResponse : DomainServiceGetResponse<CStore.Domain.Entities.WebsiteData>
  {
    
  }
}