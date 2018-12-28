using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Catalyst.MVC.Infrastructure.Attributes.Validation;
using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using CStore.Domain.Entities;
using CStore.Domain.Domains.ScrumBan.Models.ServiceModels.WebsiteDataEntity;
using CStore.Domain.Models.ServiceModels;


namespace CStore.Domain.Domains.ScrumBan.Services
{
    /// <summary>
    /// Service for WebsiteDataEntity
    /// </summary>
    public class WebsiteDataEntityService : CStore.Domain.Services.Entity.DomainEntityService, IWebsiteDataEntityService
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public WebsiteDataEntityService(IRepository repository, ISendMailProvider sendMail)
            : base(repository, sendMail)
        {
        }
        #endregion

        #region GetWebsiteDataList Methods
        /// <summary>
        /// Gets a list of records matching the search criteria
        /// </summary>
        /// <param name="request"></param>
        public WebsiteDataEntityPagedListResponse GetWebsiteDataList(WebsiteDataEntityPagedListRequest request)
        {
            return List<CStore.Domain.Entities.WebsiteData, WebsiteDataEntityPagedListResponse, WebsiteDataEntityPagedListRequest>(request);
        }

        /// <summary>
        /// Gets a list of records matching the search criteria
        /// </summary>
        /// <param name="request"></param>
        public WebsiteDataEntityListResponse GetWebsiteDataList(WebsiteDataEntityListRequest request)
        {
            return List<CStore.Domain.Entities.WebsiteData, WebsiteDataEntityListResponse, WebsiteDataEntityListRequest>(request);
        }

        /// <summary>
        /// Apply List Filter - Applies the Filters from the Request to the Query
        /// </summary>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <param name="request">Request Type</param>
        /// <param name="query">Query</param>
        /// <returns>Updated Query</returns>
        public override IQueryable<TEntity> ApplyListFilter<TEntity>(BaseServiceListRequest request, IQueryable<TEntity> query)
        {
            var filterRequest = (WebsiteDataEntityPagedListRequest)request;
            var filterQuery = (IQueryable<CStore.Domain.Entities.WebsiteData>)query;

            // apply filters to the IQueryable
            if (!String.IsNullOrEmpty(filterRequest.SearchText))
            {
                filterQuery = filterQuery.Where(p => p.Title.Contains(filterRequest.SearchText) ||  p.ImgLink.Contains(filterRequest.SearchText) ||  p.Description.Contains(filterRequest.SearchText));
            }
    
            return (IQueryable<TEntity>)filterQuery;
        }

        /// <summary>
        /// On Validate List Request / Used for Custom Validation or Tweaking of Parameters
        /// </summary>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <typeparam name="TResponse">Request Type</typeparam>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>
        public override TResponse OnValidateListRequest<TEntity, TResponse>(BaseServiceListRequest request)
        {
            var listRequest = request as WebsiteDataEntityPagedListRequest;

            return null;
        }

        /// <summary>
        /// Method To Allow Additional Custom Updates to the response of the List method.
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="response">Response</param>
        /// <returns>Response</returns>
        public override TResponse OnListResponse<TEntity, TResponse>(BaseServiceListRequest request, TResponse response)
        {
            var listRequest = request as WebsiteDataEntityPagedListRequest;

            return response;
        }

        #endregion

        #region Get Record Method
        /// <summary>
        /// Get the record at the specified id.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public WebsiteDataEntityGetResponse Get(WebsiteDataEntityGetRequest request)
        {
            return Get<CStore.Domain.Entities.WebsiteData, WebsiteDataEntityGetResponse, string>(request);
        }
        #endregion

        #region Save Method
        /// <summary>
        /// Save the records, either adding a new record or updating the appropriate record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual WebsiteDataEntitySaveResponse Save(WebsiteDataEntitySaveRequest request)
        {
            return Save<CStore.Domain.Entities.WebsiteData, WebsiteDataEntitySaveResponse, WebsiteDataEntitySaveRequest, int>(request);
        }

        /// <summary>
        /// Save Record - Override OnValidateRecordToSave to implement Custom Validation. Annotate Request Object to implement Basic Validation
        /// </summary>
        /// <typeparam name="TEntity">Type of Entity</typeparam>
        /// <typeparam name="TResponse">Response Type</typeparam>
        /// <typeparam name="TRequest">Request Type</typeparam>
        /// <typeparam name="TId">Type of ID Column</typeparam>
        /// <param name="useMapper">Optional parameter to specify whether the request object to you be automaticalled mapped to the new entity</param>
        /// <param name="request">Save Request Object</param>
        /// <returns>Save Response</returns>
        public override TResponse Save<TEntity, TResponse, TRequest, TId>(TRequest request, bool useMapper = true)
        {
            var saveRequest = request as WebsiteDataEntitySaveRequest;

            return base.Save<TEntity, TResponse, TRequest, TId>(request, useMapper);
        }

        /// <summary>
        /// On Save Mapping Occurs after the automapping is applied but before the change has been commited. Allows you to alter the 
        /// entity before it is committed
        /// </summary>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <typeparam name="TRequest">Request Type</typeparam>
        /// <typeparam name="TResponse">Response Type</typeparam>
        /// <param name="originalRecord">Original Entity Record</param>
        /// <param name="record">That that has been Mapped</param>
        public override TResponse OnSave<TEntity, TResponse, TRequest>(TRequest request, TEntity originalRecord, TEntity record)
        {
            var saveRequest = request as WebsiteDataEntitySaveRequest;
            var item = record as CStore.Domain.Entities.WebsiteData;

            //Simply return null to indicate success
            return null;
        }

        /// <summary>
        /// Post Save - Allows for additional processes to occur after the save and commit has occured. The method with return true
        /// or false based on whether the calling method need to commit any changes that occured in this method
        /// </summary>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <typeparam name="TRequest">Request Type</typeparam>
        /// <typeparam name="TResponse">Response Type</typeparam>
        /// <param name="request">Request</param>
        /// <param name="originalRecord">Original Record</param>
        /// <param name="record">Saved Record</param>
        /// <param name="response">The service response</param>
        /// <returns>A indicator when a Commit is required based on the current activity</returns>
        public override bool PostSave<TEntity, TResponse, TRequest>(TRequest request, TEntity originalRecord, TEntity record, TResponse response)
        {
            var saveRequest = request as WebsiteDataEntitySaveRequest;
            var item = record as CStore.Domain.Entities.WebsiteData;

            //Dont need to commit after this processing is done
            return false;
        }

        /// <summary>
        /// Method To Allow Additional Custom Validation for the Save method. The user can return a successful response object
        /// or null if the validation was successful. This method can also be used to update the request object
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>
        public override TResponse OnValidateRecordToSave<TEntity, TResponse, TId>(BaseServiceSaveRequest request)
        {
            var saveRequest = request as WebsiteDataEntitySaveRequest;
            return base.OnValidateRecordToSave<TEntity, TResponse, TId>(request);
        }

        #endregion

        #region Delete Method
        /// <summary>
        /// Delete the records
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public WebsiteDataEntityDeleteResponse Delete(WebsiteDataEntityDeleteRequest request)
        {
            return Delete<CStore.Domain.Entities.WebsiteData, WebsiteDataEntityDeleteResponse, WebsiteDataEntityDeleteRequest, string>(request);
        }

        /// <summary>
        /// Method To Allow Additional Custom Delete Logic. THis method occurs before the record is deleted
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns></returns>
        public override void OnRecordDelete<TEntity, TResponse, TId>(BaseServiceDeleteRequest<TId> request, TEntity record)
        {
            var deleteRequest = request as WebsiteDataEntityDeleteRequest;
            var item = record as CStore.Domain.Entities.WebsiteData;
        }

        /// <summary>
        /// Method To Allow Additional Custom Validation for the Delete method. The user can return a successful response object
        /// or null if the validation was successful. This method can also be used to update the request object
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>
        public override TResponse OnValidateRecordToDelete<TEntity, TResponse, TId>(BaseServiceDeleteRequest<TId> request, TEntity record)
        {
            var deleteRequest = request as WebsiteDataEntityDeleteRequest;
            var item = record as CStore.Domain.Entities.WebsiteData;

            return null;
        }

        #endregion
    }
}