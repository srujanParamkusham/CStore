using CStore.Domain.Models.ViewModels;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityAccessMaintenance
{

    public class EditPermissionsForSecurableViewModel : DomainViewModel
    {
        public int? SecuritySecurableId { get; set; }
    }
}