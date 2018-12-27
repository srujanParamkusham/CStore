using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using CStore.Domain.Domains.Admin.Models.ServiceModels.AppContentMaintenance;
using CStore.Domain.Services.Entity;
using System;
using System.Linq;

namespace CStore.Domain.Domains.Admin.Services
{
    /// <summary>
    /// Service for the app content maintenance screens
    /// </summary>
    public class AppContentMaintenanceService : DomainEntityService, IAppContentMaintenanceService
    {
        #region Internals
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public AppContentMaintenanceService(IRepository repository, ISendMailProvider sendMail)
            : base(repository, sendMail)
        {
        }
        #endregion

        #region List Method
        /// <summary>
        /// Lists records matching the search criteria
        /// </summary>
        /// <param name="request"></param>
        public AppContentMaintenanceListResponse List(AppContentMaintenanceListRequest request)
        {
            return List<AppContent, AppContentMaintenanceListResponse, AppContentMaintenanceListRequest>(request);
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
            var filterRequest = (AppContentMaintenanceListRequest)request;
            var filterQuery = (IQueryable<AppContent>)query;

            // apply filters to the IQueryable
            if (!String.IsNullOrEmpty(filterRequest.ContentGroup))
            {
                filterQuery = filterQuery.Where(p => p.ContentGroup.Contains(filterRequest.ContentGroup));
            }
            if (!String.IsNullOrEmpty(filterRequest.ContentName))
            {
                filterQuery = filterQuery.Where(p => p.ContentName.Contains(filterRequest.ContentName));
            }
            if (!String.IsNullOrEmpty(filterRequest.ContentValue))
            {
                filterQuery = filterQuery.Where(p => p.ContentValue.Contains(filterRequest.ContentValue));
            }
            if (filterRequest.Active)
            {
                filterQuery = filterQuery.Where(p => p.Active == true);
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
            var listRequest = request as AppContentMaintenanceListRequest;

            if (string.IsNullOrEmpty(listRequest.OrderBy) == true)
            {
                listRequest.OrderBy = "ContentGroup ASC, ContentName ASC, ContentValue ASC";
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
        public AppContentMaintenanceGetResponse Get(AppContentMaintenanceGetRequest request)
        {
            return Get<AppContent, AppContentMaintenanceGetResponse, int>(request);
        }
        #endregion

        #region Save Method
        /// <summary>
        /// Save the records, either adding a new record or updating the appropriate record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual AppContentMaintenanceSaveResponse Save(AppContentMaintenanceSaveRequest request)
        {
            return Save<AppContent, AppContentMaintenanceSaveResponse, AppContentMaintenanceSaveRequest, int>(request);
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
            var saveRequest = request as AppContentMaintenanceSaveRequest;

            //Simply return null to indicate success
            //Ensure the content name is unique.
            //
            var duplicateRecords = _repository.GetAll<AppContent>()
                        .Where(p => p.ContentName == saveRequest.ContentName && p.AppContentId != saveRequest.AppContentId)
                        .ToList();
            if (duplicateRecords.Any())
            {
                var response = Activator.CreateInstance<R>();
                response.IsSuccessful = false;
                response.Message = String.Format("Unable to save the record. The Content Name of {0} already exists on another record.", saveRequest.ContentName);
                return response;
            }
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
            var saveRequest = request as AppContentMaintenanceSaveRequest;
            var AppContent = record as AppContent;

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
            var saveRequest = request as AppContentMaintenanceSaveRequest;
            var AppContent = record as AppContent;

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
        public AppContentMaintenanceDeleteResponse Delete(AppContentMaintenanceDeleteRequest request)
        {
            return Delete<AppContent, AppContentMaintenanceDeleteResponse, AppContentMaintenanceDeleteRequest, int>(request);
        }

        #endregion
    }
}
