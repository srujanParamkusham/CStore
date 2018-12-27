using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using CStore.Domain.Domains.Admin.Models.ServiceModels.AppCodeDetailMaintenance;
using CStore.Domain.Services.Entity;
using System;
using System.Linq;

namespace CStore.Domain.Domains.Admin.Services
{
    /// <summary>
    /// Service for the app code detail maintenance screens
    /// </summary>
    public class AppCodeDetailMaintenanceService : DomainEntityService, IAppCodeDetailMaintenanceService
    {
        #region Internals
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public AppCodeDetailMaintenanceService(IRepository repository, ISendMailProvider sendMail)
            : base(repository, sendMail)
        {
        }
        #endregion

        #region List Method
        /// <summary>
        /// Lists records matching the search criteria
        /// </summary>
        /// <param name="request"></param>
        public AppCodeDetailMaintenanceListResponse List(AppCodeDetailMaintenanceListRequest request)
        {
            return List<AppCodeDetail, AppCodeDetailMaintenanceListResponse, AppCodeDetailMaintenanceListRequest>(request);
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
            var filterRequest = (AppCodeDetailMaintenanceListRequest)request;
            var filterQuery = (IQueryable<AppCodeDetail>)query;

            // apply filters to the IQueryable
            if (!String.IsNullOrEmpty(filterRequest.CodeGroup))
            {
                filterQuery = filterQuery.Where(p => p.CodeGroup.Contains(filterRequest.CodeGroup));
            }
            if (!String.IsNullOrEmpty(filterRequest.CodeValue))
            {
                filterQuery = filterQuery.Where(p => p.CodeValue.Contains(filterRequest.CodeValue));
            }
            if (!String.IsNullOrEmpty(filterRequest.Description))
            {
                filterQuery = filterQuery.Where(p => p.Description.Contains(filterRequest.Description));
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
            var listRequest = request as AppCodeDetailMaintenanceListRequest;

            if (string.IsNullOrEmpty(listRequest.OrderBy) == true)
            {
                listRequest.OrderBy = "CodeGroup ASC, Sort ASC, CodeValue ASC";
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
        public AppCodeDetailMaintenanceGetResponse Get(AppCodeDetailMaintenanceGetRequest request)
        {
            return Get<AppCodeDetail, AppCodeDetailMaintenanceGetResponse, int>(request);
        }
        #endregion

        #region Save Method
        /// <summary>
        /// Save the records, either adding a new record or updating the appropriate record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual AppCodeDetailMaintenanceSaveResponse Save(AppCodeDetailMaintenanceSaveRequest request)
        {
            return Save<AppCodeDetail, AppCodeDetailMaintenanceSaveResponse, AppCodeDetailMaintenanceSaveRequest, int>(request);
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
            var saveRequest = request as AppCodeDetailMaintenanceSaveRequest;

            //Simply return null to indicate success
            //Ensure the codegroup, codevalue, and appcodedetailid are unique.
            //
            var duplicateRecords = _repository.GetAll<AppCodeDetail>()
                        .Where(p => p.CodeGroup == saveRequest.CodeGroup && p.CodeValue == saveRequest.CodeValue && p.AppCodeDetailId != saveRequest.AppCodeDetailId)
                        .ToList();
            if (duplicateRecords.Any())
            {
                var response = Activator.CreateInstance<R>();
                response.IsSuccessful = false;
                response.Message = String.Format("Unable to save the record. The CodeGroup of {0} or CodeValue of {1} already exists on another record.", saveRequest.CodeGroup, saveRequest.CodeValue);
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
            var saveRequest = request as AppCodeDetailMaintenanceSaveRequest;
            var AppCodeDetail = record as AppCodeDetail;

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
            var saveRequest = request as AppCodeDetailMaintenanceSaveRequest;
            var AppCodeDetail = record as AppCodeDetail;

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
        public AppCodeDetailMaintenanceDeleteResponse Delete(AppCodeDetailMaintenanceDeleteRequest request)
        {
            return Delete<AppCodeDetail, AppCodeDetailMaintenanceDeleteResponse, AppCodeDetailMaintenanceDeleteRequest, int>(request);
        }

        #endregion
    }
}
