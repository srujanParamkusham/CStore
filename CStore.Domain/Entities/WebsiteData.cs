using System;
using System.Collections.Generic;
using Catalyst.MVC.Infrastructure.Entities;
using Catalyst.MVC.Infrastructure.Attributes.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CStore.Domain.Entities
{
	/// <summary>
	/// WebsiteData Entity
	/// </summary>
	[DataContext("DefaultConnection")]
	public partial class WebsiteData : DomainEntity
	{
		/// <summary>
		/// Title
		/// </summary>
		public string Title
		{
			get;
			set;
		}

		/// <summary>
		/// ImgLink
		/// </summary>
		public string ImgLink
		{
			get;
			set;
		}

		/// <summary>
		/// Description
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Publishdate
		/// </summary>
		public DateTime? Publishdate
		{
			get;
			set;
		}

		/// <summary>
		/// Active
		/// </summary>
		public bool? Active
		{
			get;
			set;
		}

	}
}
