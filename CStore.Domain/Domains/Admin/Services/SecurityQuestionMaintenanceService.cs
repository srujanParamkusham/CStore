using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityQuestionMaintenance;
using CStore.Domain.Services.Entity;
using System;
using System.Linq;

namespace CStore.Domain.Domains.Admin.Services
{
    /// <summary>
    /// Service for the Security Question maintenance screens
    /// </summary>
    public class SecurityQuestionMaintenanceService : DomainEntityService, ISecurityQuestionMaintenanceService
    {
        #region Internals
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public SecurityQuestionMaintenanceService(IRepository repository, ISendMailProvider sendMail)
            : base(repository, sendMail)
        {
        }
        #endregion

        #region List Method
        /// <summary>
        /// Lists records matching the search criteria
        /// </summary>
        /// <param name="request"></param>
        public SecurityQuestionMaintenanceListResponse List(SecurityQuestionMaintenanceListRequest request)
        {
            return List<SecurityQuestion, SecurityQuestionMaintenanceListResponse, SecurityQuestionMaintenanceListRequest>(request);
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
            var filterRequest = (SecurityQuestionMaintenanceListRequest)request;
            var filterQuery = (IQueryable<SecurityQuestion>)query;

            // apply filters to the IQueryable
            if (!String.IsNullOrEmpty(filterRequest.Question))
            {
                filterQuery = filterQuery.Where(p => p.Question.Contains(filterRequest.Question));
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
            var listRequest = request as SecurityQuestionMaintenanceListRequest;

            if (string.IsNullOrEmpty(listRequest.OrderBy) == true)
            {
                listRequest.OrderBy = "Question ASC";
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
        public SecurityQuestionMaintenanceGetResponse Get(SecurityQuestionMaintenanceGetRequest request)
        {
            return Get<SecurityQuestion, SecurityQuestionMaintenanceGetResponse, int>(request);
        }
        #endregion

        #region Save Method
        /// <summary>
        /// Save the records, either adding a new record or updating the appropriate record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual SecurityQuestionMaintenanceSaveResponse Save(SecurityQuestionMaintenanceSaveRequest request)
        {
            return Save<SecurityQuestion, SecurityQuestionMaintenanceSaveResponse, SecurityQuestionMaintenanceSaveRequest, int>(request);
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
            var saveRequest = request as SecurityQuestionMaintenanceSaveRequest;

            //
            //Ensure the Security Question ID is unique
            //
            var duplicateRecords = _repository.GetAll<SecurityQuestion>()
                        .Where(p => p.Question == saveRequest.Question && p.SecurityQuestionId != saveRequest.SecurityQuestionId)
                        .ToList();
            if (duplicateRecords.Any())
            {
                var response = Activator.CreateInstance<R>();
                response.IsSuccessful = false;
                response.Message = String.Format("Unable to save the record. The Security Question ID {0} already exists on another record.", saveRequest.SecurityQuestionId);
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
            var saveRequest = request as SecurityQuestionMaintenanceSaveRequest;
            var SecurityQuestionTemplate = record as SecurityQuestion;

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
            var saveRequest = request as SecurityQuestionMaintenanceSaveRequest;
            var sercuritySecurableTemplate = record as SecurityQuestion;

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
        public SecurityQuestionMaintenanceDeleteResponse Delete(SecurityQuestionMaintenanceDeleteRequest request)
        {
            return Delete<SecurityQuestion, SecurityQuestionMaintenanceDeleteResponse, SecurityQuestionMaintenanceDeleteRequest, int>(request);
        }

        #endregion
    }
}
