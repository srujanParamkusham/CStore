using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.General.Models.ServiceModels.AppAnnouncements
{
    public class GetActiveAnnouncementsResponse : DomainServiceResponse
    {
        public IEnumerable<AppAnnouncement> AppAnnouncements { get; set; }
    }
}
