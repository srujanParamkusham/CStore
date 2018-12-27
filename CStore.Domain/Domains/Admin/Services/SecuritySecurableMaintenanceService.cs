using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using CStore.Domain.Domains.Admin.Models.ServiceModels.SecuritySecurableMaintenance;
using CStore.Domain.Services.Entity;
using System;
using System.Linq;

namespace CStore.Domain.Domains.Admin.Services
{
    /// <summary>
    /// Service for the security securable maintenance screens
    /// </summary>
    public class SecuritySecurableMaintenanceService : DomainEntityService, ISecuritySecurableMaintenanceService
    {
        #region Internals
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public SecuritySecurableMaintenanceService(IRepository repository, ISendMailProvider sendMail)
            : base(repository, sendMail)
        {
        }
        #endregion

        #region List Method
        /// <summary>
        /// Lists records matching the search criteria
        /// </summary>
        /// <param name="request"></param>
        public SecuritySecurableMaintenanceListResponse List(SecuritySecurableMaintenanceListRequest request)
        {
            return List<SecuritySecurable, SecuritySecurableMaintenanceListResponse, SecuritySecurableMaintenanceListRequest>(request);
        }

        /// <summary>
        /// Create the IQueryable object for the list so we can include additional navigation properties.
        /// </summary>
        /// <param name="request"></param>
        public override IQueryable<TEntity> CreateListIQueryable<TEntity>(BaseServiceListRequest request, IRepository repository)
        {
            return _repository.GetAll<SecuritySecurable>(p => p.ParentSecuritySecurable) as IQueryable<TEntity>;
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
            var filterRequest = (SecuritySecurableMaintenanceListRequest)request;
            var filterQuery = (IQueryable<SecuritySecurable>)query;

            // apply filters to the IQueryable
            if (!String.IsNullOrEmpty(filterRequest.Name))
            {
                filterQuery = filterQuery.Where(p => p.Name.Contains(filterRequest.Name));
            }


            // apply filters to the IQueryable
            if (!String.IsNullOrEmpty(filterRequest.ParentSecuritySecurableName))
            {
                filterQuery = filterQuery.Where(p => p.ParentSecuritySecurable.Name.Contains(filterRequest.ParentSecuritySecurableName));
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
            var listRequest = request as SecuritySecurableMaintenanceListRequest;

            if (string.IsNullOrEmpty(listRequest.OrderBy) == true)
            {
                listRequest.OrderBy = "Name ASC";
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
        public SecuritySecurableMaintenanceGetResponse Get(SecuritySecurableMaintenanceGetRequest request)
        {
            return Get<SecuritySecurable, SecuritySecurableMaintenanceGetResponse, int>(request);
        }
        #endregion

        #region Save Method
        /// <summary>
        /// Save the records, either adding a new record or updating the appropriate record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual SecuritySecurableMaintenanceSaveResponse Save(SecuritySecurableMaintenanceSaveRequest request)
        {
            return Save<SecuritySecurable, SecuritySecurableMaintenanceSaveResponse, SecuritySecurableMaintenanceSaveRequest, int>(request);
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
            var saveRequest = request as SecuritySecurableMaintenanceSaveRequest;

            //
            //Ensure the username is unique
            //
            var duplicateRecords = _repository.GetAll<SecuritySecurable>()
                        .Where(p => p.Name == saveRequest.Name && p.SecuritySecurableId != saveRequest.SecuritySecurableId)
                        .ToList();
            if (duplicateRecords.Any())
            {
                var response = Activator.CreateInstance<R>();
                response.IsSuccessful = false;
                response.Message = String.Format("Unable to save the record. The Name {0} already exists on another record.", saveRequest.Name);
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
            var saveRequest = request as SecuritySecurableMaintenanceSaveRequest;
            var securitySecurableTemplate = record as SecuritySecurable;

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
            var saveRequest = request as SecuritySecurableMaintenanceSaveRequest;
            var sercuritySecurableTemplate = record as SecuritySecurable;

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
        public SecuritySecurableMaintenanceDeleteResponse Delete(SecuritySecurableMaintenanceDeleteRequest request)
        {
            return Delete<SecuritySecurable, SecuritySecurableMaintenanceDeleteResponse, SecuritySecurableMaintenanceDeleteRequest, int>(request);
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
            var deleteRequest = request as SecuritySecurableMaintenanceDeleteRequest;
            var securitySecurable = record as SecuritySecurable;

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
            var deleteRequest = request as SecuritySecurableMaintenanceDeleteRequest;
            var securitySecurable = record as SecuritySecurable;

            //Security Access
            var securityAccesses =
                _repository.GetAll<SecurityAccess>()
                           .Where(p => p.SecuritySecurableId == securitySecurable.SecuritySecurableId).ToList();
            _repository.DeleteAll(securityAccesses);

            //Security Securable Actions
            var securitySecurableActions =
                _repository.GetAll<SecuritySecurableAction>()
                           .Where(p => p.SecuritySecurableId == securitySecurable.SecuritySecurableId).ToList();
            _repository.DeleteAll(securitySecurableActions);
        }
        #endregion


        #region Method to get all Securables, used for a select list
        /// <summary>
        /// Method to get all Securables used for a select list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SecuritySecurableMaintenanceGetAllSecurablesResponse GetAllSecurables(
            SecuritySecurableMaintenanceGetAllSecurablesRequest request)
        {
            var securablesQueryable = _repository.GetAll<SecuritySecurable>();
            if (request != null && request.SecuritySecurableIdExcludeList != null &&
                request.SecuritySecurableIdExcludeList.Any())
            {
                securablesQueryable = securablesQueryable.Where(p => !request.SecuritySecurableIdExcludeList.Contains(p.SecuritySecurableId));
            }
            var securables = securablesQueryable.OrderBy(p => p.Name).ToList();

            return new SecuritySecurableMaintenanceGetAllSecurablesResponse()
            {
                SecuritySecurables = securables,
                IsSuccessful = true
            };
        }
        #endregion

    }
}
