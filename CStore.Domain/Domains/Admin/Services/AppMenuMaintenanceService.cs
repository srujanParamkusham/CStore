using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuMaintenance;
using CStore.Domain.Services.Entity;
using System;
using System.Linq;

namespace CStore.Domain.Domains.Admin.Services
{
    /// <summary>
    /// Service for the app announcement maintenance screens
    /// </summary>
    public class AppMenuMaintenanceService : DomainEntityService, IAppMenuMaintenanceService
    {
        #region Internals
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public AppMenuMaintenanceService(IRepository repository, ISendMailProvider sendMail)
            : base(repository, sendMail)
        {
        }
        #endregion

        #region List Method
        /// <summary>
        /// Lists records matching the search criteria
        /// </summary>
        /// <param name="request"></param>
        public AppMenuMaintenanceListResponse List(AppMenuMaintenanceListRequest request)
        {
            return List<AppMenu, AppMenuMaintenanceListResponse, AppMenuMaintenanceListRequest>(request);
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
            var filterRequest = (AppMenuMaintenanceListRequest)request;
            var filterQuery = (IQueryable<AppMenu>)query;

            // apply filters to the IQueryable
            if (!String.IsNullOrEmpty(filterRequest.MenuCode))
            {
                filterQuery = filterQuery.Where(p => p.MenuCode.Contains(filterRequest.MenuCode));
            }
            if (!String.IsNullOrEmpty(filterRequest.Name))
            {
                filterQuery = filterQuery.Where(p => p.Name.Contains(filterRequest.Name));
            }
            if (!String.IsNullOrEmpty(filterRequest.Description))
            {
                filterQuery = filterQuery.Where(p => p.Description.Contains(filterRequest.Description));
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
            var listRequest = request as AppMenuMaintenanceListRequest;

            if (string.IsNullOrEmpty(listRequest.OrderBy) == true)
            {
                listRequest.OrderBy = "MenuCode ASC";
            }

            return null;
        }

        #endregion

        #region Get Record Method
        /// <summary>
        /// Get the record at the specified id.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppMenuMaintenanceGetResponse Get(AppMenuMaintenanceGetRequest request)
        {
            return Get<AppMenu, AppMenuMaintenanceGetResponse, int>(request);
        }
        #endregion

        #region Save Method
        /// <summary>
        /// Save the records, either adding a new record or updating the appropriate record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual AppMenuMaintenanceSaveResponse Save(AppMenuMaintenanceSaveRequest request)
        {
            return Save<AppMenu, AppMenuMaintenanceSaveResponse, AppMenuMaintenanceSaveRequest, int>(request);
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
            var saveRequest = request as AppMenuMaintenanceSaveRequest;

            //
            //Ensure the menu code is unique
            //
            var duplicateRecords = _repository.GetAll<AppMenu>()
                       .Where(p => p.MenuCode == saveRequest.MenuCode && p.AppMenuId != saveRequest.AppMenuId)
                       .ToList();
            if (duplicateRecords.Any())
            {
                var response = Activator.CreateInstance<R>();
                response.IsSuccessful = false;
                response.Message = String.Format("Unable to save the record. The menu code of {0} already exists on another record.", saveRequest.MenuCode);
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
            var saveRequest = request as AppMenuMaintenanceSaveRequest;
            var appMenu = record as AppMenu;

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
            var saveRequest = request as AppMenuMaintenanceSaveRequest;
            var appMenu = record as AppMenu;

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
        public AppMenuMaintenanceDeleteResponse Delete(AppMenuMaintenanceDeleteRequest request)
        {
            return Delete<AppMenu, AppMenuMaintenanceDeleteResponse, AppMenuMaintenanceDeleteRequest, int>(request);
        }
        #endregion

        #region Validate if the record can be deleted
        /// <summary>
        /// Method to check if the record being deleted can be
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="request"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public override R OnValidateRecordToDelete<T, R, I>(BaseServiceDeleteRequest<I> request, T record)
        {
            var deleteRequest = request as AppMenuMaintenanceDeleteRequest;
            var appMenu = record as AppMenu;

            //Returning null means it is OK to be deleted.
            return null;
        }
        #endregion

        #region On Delete (Happens pre-delete)
        /// <summary>
        /// Before the record is deleted, do this functionality.
        /// Mainly used to delete other child objects to the one being deleted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="request"></param>
        /// <param name="record"></param>
        public override void OnRecordDelete<T, R, I>(BaseServiceDeleteRequest<I> request, T record)
        {
            var deleteRequest = request as AppMenuMaintenanceDeleteRequest;
            var appMenu = record as AppMenu;

            //App Menu Items
            var appMenuItems =
                _repository.GetAll<AppMenuItem>()
                           .Where(p => p.AppMenuId == appMenu.AppMenuId).ToList();
            _repository.DeleteAll(appMenuItems);
        }
        #endregion
    }

}
