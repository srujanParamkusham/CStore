using Catalyst.MVC.Infrastructure.Models.ViewModels;
using System;

namespace CStore.Domain.Models.ViewModels
{
    /// <summary>
    /// Base select list view model that all other select list view models should extend.
    /// </summary>
    [Serializable]
    public class DomainSelectListViewModel : BaseSelectListViewModel
    {
    }
}
