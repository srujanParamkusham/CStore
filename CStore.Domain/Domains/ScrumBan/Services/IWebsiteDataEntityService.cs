using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Catalyst.MVC.Infrastructure.Attributes.Validation;
using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using CStore.Domain.Entities;
using CStore.Domain.Domains.ScrumBan.Models.ServiceModels.WebsiteDataEntity;
using CStore.Domain.Models.ServiceModels;


namespace CStore.Domain.Domains.ScrumBan.Services
{
    public interface IWebsiteDataEntityService
    {
        WebsiteDataEntityDeleteResponse Delete(WebsiteDataEntityDeleteRequest request);
        WebsiteDataEntityGetResponse Get(WebsiteDataEntityGetRequest request);
        WebsiteDataEntityPagedListResponse GetWebsiteDataList(WebsiteDataEntityPagedListRequest request);
        WebsiteDataEntityListResponse GetWebsiteDataList(WebsiteDataEntityListRequest request);
        WebsiteDataEntitySaveResponse Save(WebsiteDataEntitySaveRequest request);
    }
}