using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuItemMaintenance;
using CStore.Domain.Services.Entity;
using System;
using System.Linq;

namespace CStore.Domain.Domains.Admin.Services
{
    /// <summary>
    /// Service for the app announcement maintenance screens
    /// </summary>
    public class AppMenuItemMaintenanceService : DomainEntityService, IAppMenuItemMaintenanceService
    {
        #region Internals
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public AppMenuItemMaintenanceService(IRepository repository, ISendMailProvider sendMail)
            : base(repository, sendMail)
        {
        }
        #endregion

        #region List Method
        /// <summary>
        /// Lists records matching the search criteria
        /// </summary>
        /// <param name="request"></param>
        public AppMenuItemMaintenanceListResponse List(AppMenuItemMaintenanceListRequest request)
        {
            return List<AppMenuItem, AppMenuItemMaintenanceListResponse, AppMenuItemMaintenanceListRequest>(request);
        }

        /// <summary>
        /// Create the IQueryable object for the list so we can include additional navigation properties.
        /// </summary>
        /// <param name="request"></param>
        public override IQueryable<TEntity> CreateListIQueryable<TEntity>(BaseServiceListRequest request, IRepository repository)
        {
            return _repository.GetAll<AppMenuItem>(p => p.AppMenu) as IQueryable<TEntity>;
        }

        /// <summary>
        /// Apply List Filter - Applies the Filters from the Request to the Query
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="request">Request Type</param>
        /// <param name="query">Query</param>
        /// <returns>Updated Query</returns>
        public override IQueryable<T> ApplyListFilter<T>(BaseServiceListRequest request, IQueryable<T> query)
        {
            var filterRequest = (AppMenuItemMaintenanceListRequest)request;
            var filterQuery = (IQueryable<AppMenuItem>)query;

            // apply filters to the IQueryable

            if (!String.IsNullOrEmpty(filterRequest.ParentAppMenuItemId))
            {
                filterQuery = filterQuery.Where(p => p.AppMenuItemId.ToString() == filterRequest.ParentAppMenuItemId);
            }
            if (!String.IsNullOrEmpty(filterRequest.AppMenuId))
            {
                filterQuery = filterQuery.Where(p => p.AppMenuId.ToString().Contains(filterRequest.AppMenuId));
            }
            if (!String.IsNullOrEmpty(filterRequest.Name))
            {
                filterQuery = filterQuery.Where(p => p.Name.Contains(filterRequest.Name));
            }
            if (!String.IsNullOrEmpty(filterRequest.Handler))
            {
                filterQuery = filterQuery.Where(p => p.Handler.Contains(filterRequest.Handler));
            }
            if (!String.IsNullOrEmpty(filterRequest.Text))
            {
                filterQuery = filterQuery.Where(p => p.Text.Contains(filterRequest.Text));
            }
            if (!String.IsNullOrEmpty(filterRequest.Style))
            {
                filterQuery = filterQuery.Where(p => p.Style.Contains(filterRequest.Style));
            }
            if (!String.IsNullOrEmpty(filterRequest.ToolTip))
            {
                filterQuery = filterQuery.Where(p => p.ToolTip.Contains(filterRequest.ToolTip));
            }

            return (IQueryable<T>)filterQuery;
        }

        /// <summary>
        /// On Validate List Request / Used for Custom Validation or Tweaking of Parameters
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <typeparam name="R">Request Type</typeparam>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>
        public override R OnValidateListRequest<T, R>(BaseServiceListRequest request)
        {
            var listRequest = request as AppMenuItemMaintenanceListRequest;

            if (string.IsNullOrEmpty(listRequest.OrderBy) == true)
            {
                listRequest.OrderBy = "Name ASC";
            }

            return null;
        }

        #endregion

        #region Get Menu Item Method
        /// <summary>
        /// Get the record at the specified id.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppMenuItemMaintenanceGetResponse Get(AppMenuItemMaintenanceGetRequest request)
        {
            return Get<AppMenuItem, AppMenuItemMaintenanceGetResponse, int>(request);
        }
        #endregion

        #region Save Method
        /// <summary>
        /// Save the records, either adding a new record or updating the appropriate record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual AppMenuItemMaintenanceSaveResponse Save(AppMenuItemMaintenanceSaveRequest request)
        {
            return Save<AppMenuItem, AppMenuItemMaintenanceSaveResponse, AppMenuItemMaintenanceSaveRequest, int>(request);
        }
        #endregion

        #region Save Validation
        /// <summary>
        /// Custom Save Validation Logic. If successful, then return null.
        /// </summary>
        /// <typeparam name="T">Type of Entity</typeparam>
        /// <typeparam name="R">Type of Response</typeparam>
        /// <typeparam name="I">Type of ID Column</typeparam>
        /// <param name="request">Request</param>
        /// <returns>Validation Response</returns>
        public override R OnValidateRecordToSave<T, R, I>(BaseServiceSaveRequest request)
        {
            var saveRequest = request as AppMenuItemMaintenanceSaveRequest;

            //
            //Ensure the menu item id is unique
            //
            var duplicateRecords = _repository.GetAll<AppMenuItem>()
                       .Where(p => p.Name == saveRequest.Name && p.AppMenuItemId != saveRequest.AppMenuItemId)
                       .ToList();
            if (duplicateRecords.Any())
            {
                var response = Activator.CreateInstance<R>();
                response.IsSuccessful = false;
                response.Message = String.Format("Unable to save the record. The name of {0} already exists on another record.", saveRequest.Name);
                return response;
            }

            //Simply return null to indicate success
            return null;
        }
        #endregion

        #region On Save
        /// <summary>
        /// On Save - Happens before the record is saved
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <typeparam name="S">Request Type</typeparam>
        /// <typeparam name="R">Response Type</typeparam>
        /// <param name="request">Request</param>
        /// <param name="record">That that has been Mapped</param>
        public override R OnSave<T, R, S>(S request, T originalRecord, T record)
        {
            var saveRequest = request as AppMenuItemMaintenanceSaveRequest;
            var AppMenuItem = record as AppMenuItem;

            //Simply return null to indicate success
            return null;
        }
        #endregion

        #region Post Save
        /// <summary>
        /// After the Record has been saved
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="request"></param>
        /// <param name="originalRecord"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public override bool PostSave<T, R, S>(S request, T originalRecord, T record, R response)
        {
            var saveRequest = request as AppMenuItemMaintenanceSaveRequest;
            var AppMenuItem = record as AppMenuItem;

            //Dont need to commit after this processing is done
            return false;
        }

        #endregion

        #region Delete Method
        /// <summary>
        /// Delete the records
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppMenuItemMaintenanceDeleteResponse Delete(AppMenuItemMaintenanceDeleteRequest request)
        {
            return Delete<AppMenuItem, AppMenuItemMaintenanceDeleteResponse, AppMenuItemMaintenanceDeleteRequest, int>(request);
        }

        #endregion

        #region Method to get all AppMenus, used for a select list
        /// <summary>
        /// Method to get all AppMenus used for a select list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppMenuItemMaintenanceGetAllAppMenusResponse GetAllAppMenus(
            AppMenuItemMaintenanceGetAllAppMenusRequest request)
        {
            var appMenusQueryable = _repository.GetAll<AppMenu>();
            if (request != null && request.AppMenuIdExcludeList != null &&
                request.AppMenuIdExcludeList.Any())
            {
                appMenusQueryable = appMenusQueryable.Where(p => !request.AppMenuIdExcludeList.Contains(p.AppMenuId));
            }
            var appMenus = appMenusQueryable.OrderBy(p => p.Name).ToList();

            return new AppMenuItemMaintenanceGetAllAppMenusResponse()
            {
                AppMenus = appMenus,
                IsSuccessful = true
            };
        }
        #endregion

        #region Method to get all AppMenuItems, used for a select list
        /// <summary>
        /// Method to get all AppMenuItems used for a select list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppMenuItemMaintenanceGetAllAppMenuItemsResponse GetAllAppMenuItems(
            AppMenuItemMaintenanceGetAllAppMenuItemsRequest request)
        {
            var appMenuItemsQueryable = _repository.GetAll<AppMenuItem>();
            if (request != null && request.AppMenuItemIdExcludeList != null &&
                request.AppMenuItemIdExcludeList.Any())
            {
                appMenuItemsQueryable = appMenuItemsQueryable.Where(p => !request.AppMenuItemIdExcludeList.Contains(p.AppMenuItemId));
            }
            var appMenuItems = appMenuItemsQueryable.OrderBy(p => p.Name).ToList();

            return new AppMenuItemMaintenanceGetAllAppMenuItemsResponse()
            {
                AppMenuItems = appMenuItems,
                IsSuccessful = true
            };
        }
        #endregion

    }

}
