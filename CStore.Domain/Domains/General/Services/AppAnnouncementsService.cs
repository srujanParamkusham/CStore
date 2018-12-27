using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Services.Log;
using StructureMap;
using CStore.Domain.Domains.General.Models.ServiceModels.AppAnnouncements;
using CStore.Domain.Services;
using System;
using System.Linq;

namespace CStore.Domain.Domains.General.Services
{
    /// <summary>
    /// Service used to get application announcements to display to the user
    /// </summary>
    public class AppAnnouncementsService : DomainService, IAppAnnouncementsService
    {
        #region Internals
        protected readonly IRepository _repository;
        protected readonly IContainer _container;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container"></param>
        /// <param name="repository"></param>
        [DefaultConstructor]
        public AppAnnouncementsService(IContainer container, IRepository repository)
        {
            this._container = container;
            this._repository = repository;
        }
        #endregion

        #region Get all of the active announcements 
        /// <summary>
        /// Get all of the active announcements to be displayed on the screen.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetActiveAnnouncementsResponse GetActiveAnnouncements(GetActiveAnnouncementsRequest request)
        {
            LogService.Instance.Log.Info("GetActiveAnnouncements Service Method Called");

            var announcements = _repository.GetAll<AppAnnouncement>()
                                           .Where(
                                               p =>
                                               (p.EffectiveDate == null || p.EffectiveDate <= DateTime.Now) &&
                                               (p.ExpirationDate == null || p.ExpirationDate >= DateTime.Now) &&
                                               p.Active == true)
                                           .OrderByDescending(p => p.ForceToTopOfList)
                                           .ThenByDescending(p => p.EffectiveDate)
                                           .ThenByDescending(p => p.CreateDate)
                                           .ToList();

            return new GetActiveAnnouncementsResponse()
            {
                IsSuccessful = true,
                Message = "Service successfully completed",
                AppAnnouncements = announcements
            };
        }
        #endregion

    }
}
