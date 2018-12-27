using Catalyst.MVC.Domain.Entities;
using System.Collections.Generic;

namespace CStore.Domain.Models.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<AppAnnouncement> AppAnnouncements { get; set; }
    }
}
