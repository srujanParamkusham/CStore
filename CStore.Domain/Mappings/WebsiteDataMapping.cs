using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Catalyst.MVC.Infrastructure.Attributes.Entity;
using CStore.Domain.Entities;

namespace CStore.Domain.Mappings 
{
    /// <summary>
    /// WebsiteData Entity Mapping
    /// </summary>
    public partial class WebsiteDataMapping : EntityTypeConfiguration<WebsiteData>
	{		                                    
		/// <summary>
		/// WebsiteDataMapping Constructor
		/// </summary>
		public WebsiteDataMapping()
		{
			ToTable("WebsiteData");
			HasKey(item => item.Title);
			Initialize();
		}
	}
}