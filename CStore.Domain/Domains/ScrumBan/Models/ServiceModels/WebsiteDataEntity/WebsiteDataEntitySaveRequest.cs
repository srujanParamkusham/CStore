using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Catalyst.MVC.Infrastructure.Attributes.Validation;
using CStore.Domain.Entities;
using CStore.Domain.Domains.ScrumBan.Models.ServiceModels;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.ScrumBan.Models.ServiceModels.WebsiteDataEntity
{
  /// <summary>
  /// Service request object for SaveRequest 
  /// </summary>
  public class WebsiteDataEntitySaveRequest : DomainServiceSaveRequest
  {
    
		/// <summary>
		/// Title
		/// 
		/// </summary>
		[Key]
		public string Title
		{
			get;
			set;
		}

		/// <summary>
		/// ImgLink
		/// 
		/// </summary>
		public string ImgLink
		{
			get;
			set;
		}

		/// <summary>
		/// Description
		/// 
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Publishdate
		/// 
		/// </summary>
		public DateTime? Publishdate
		{
			get;
			set;
		}

		/// <summary>
		/// Active
		/// 
		/// </summary>
		public bool? Active
		{
			get;
			set;
		}

  }
}