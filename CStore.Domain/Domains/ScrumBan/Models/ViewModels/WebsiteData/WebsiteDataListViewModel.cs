using System;
using System.Collections.Generic;
using Catalyst.MVC.Infrastructure.Entities;
using Catalyst.MVC.Infrastructure.Attributes.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CStore.Domain.Domains.ScrumBan.Models.ViewModels.WebsiteData
{
  /// <summary>
  /// ViewModel for WebsiteData 
  /// </summary>
  public class WebsiteDataListViewModel :  CStore.Domain.Models.ViewModels.DomainListViewModel
  {
    
		/// <summary>
		/// Title
		/// 
		/// </summary>
		[Display(Name = "Title")]
		[StringLength(100)]
		public string Title
		{
			get;
			set;
		}

		/// <summary>
		/// ImgLink
		/// 
		/// </summary>
		[Display(Name = "Img Link")]
		[StringLength(500)]
		public string ImgLink
		{
			get;
			set;
		}

		/// <summary>
		/// Description
		/// 
		/// </summary>
		[Display(Name = "Description")]
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Publishdate
		/// 
		/// </summary>
		[Display(Name = "Publishdate")]
		public DateTime? Publishdate
		{
			get;
			set;
		}

		/// <summary>
		/// Active
		/// 
		/// </summary>
		[Display(Name = "Active")]
		public bool? Active
		{
			get;
			set;
		}

  }
}