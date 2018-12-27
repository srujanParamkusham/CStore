using System;
using System.Linq;
using System.Web.Mvc;
using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Models.SelectList.Select2;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityRoleMaintenance;
using CStore.Domain.Domains.SelectList.Models.ViewModels.SecurityRoleSelectList;
using CStore.Domain.Models.ViewModels;
using CStore.Domain.Domains.Admin.Services;
using CStore.Domain.Entities;

namespace CStore.Web.Areas.SelectList.Controllers
{
    /// <summary>
    /// Controller used for handling the SecurityRole select lists
    /// </summary>
    public partial class SecurityRoleSelectListController : DomainSelectListController
    {
        #region Member Variables
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public SecurityRoleSelectListController()
            : base()
        {
        }
        #endregion

        #region List Records Screen. Populates the select list.
        /// <summary>
        /// Get a list of the select list items. This returns an AJAX response for the select list items to display.
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="dataTablesModel">Data Table Model</param>
        /// <returns></returns>
        public virtual ActionResult List(SecurityRoleSelectListViewModel model = null)
        {
            return List<SecurityRoleSelectListViewModel, SecurityRoleMaintenanceService, SecurityRoleMaintenanceListRequest, SecurityRoleMaintenanceListResponse, SecurityRole>(model);
        }

        /// <summary>
        /// Map the search text for auto completes to the service list request so the appropriate field gets searched.
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="model"></param>
        /// <param name="request"></param>
        protected override void OnListMapping<TViewModel, TRequest>(TViewModel model, TRequest request)
        {
            var viewModel = model as SecurityRoleSelectListViewModel;
            var serviceRequest = request as SecurityRoleMaintenanceListRequest;
            serviceRequest.Name = viewModel.SearchText;
        }
        #endregion

        #region GenerateListResponseObject - Populates an individual select list item with its description
        /// <summary>
        /// GenerateListResponseObject generates the specific JSON needed for the select list to render.
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TSelectListService"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        /// <param name="service"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected override Object GenerateListResponseObject<TViewModel, TSelectListService, TRequest, TResponse, TEntity>(TViewModel model,
            TSelectListService service, TRequest request, TResponse response)
        {
            var serviceResponse = response as SecurityRoleMaintenanceListResponse;

            var selectListItems = serviceResponse.Items.Select(p => new Select2SelectListItem { id = p.SecurityRoleId.ToString(), text = p.Name }).ToList();
            var select2ListResult = new Select2ListResult()
            {
                TotalRows = response.TotalRows,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Items = selectListItems
            };

            return select2ListResult;
        }
        #endregion

        #region Get Record - Populates an individual select list item with its description
        /// <summary>
        /// Get a specific select list item for a given ID (which is the select list's value typically)
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Get(int? id = null, DomainViewModel model = null)
        {
            int itemId = id.HasValue ? id.Value : 0;
            return this.Get<DomainViewModel, int, SecurityRoleMaintenanceService, SecurityRoleMaintenanceGetRequest, SecurityRoleMaintenanceGetResponse, SecurityRole>(itemId, model);
        }
        #endregion

        #region GenerateGetResponseObject
        /// <summary>
        /// Get the select list item corresponding to the specified ID
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override object GenerateGetResponseObject<TViewModel, T, TRequest, TResponse, TEntity>(TRequest request, TResponse response,
            TViewModel model)
        {
            var serviceResponse = response as SecurityRoleMaintenanceGetResponse;

            Select2SelectListItem selectListItem = null;
            if (serviceResponse == null || serviceResponse.Entity == null)
            {
                selectListItem = new Select2SelectListItem()
                {
                    id = (request.Id != null ? request.Id.ToString() : null),
                    text = null
                };
            }
            else
            {
                selectListItem = new Select2SelectListItem()
                {
                    id = serviceResponse.Entity.SecurityRoleId.ToString(),
                    text = serviceResponse.Entity.Name
                };
            }

            return selectListItem;
        }
        #endregion

    }
}
