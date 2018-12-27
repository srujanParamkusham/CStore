using Catalyst.MVC.Infrastructure.HTMLHelpers;
using Catalyst.MVC.Infrastructure.HTMLHelpers.Extensions.Grid;
using Catalyst.MVC.Infrastructure.HTMLHelpers.Extensions.Grid.DataTablesReadonly;
using Catalyst.MVC.Infrastructure.HTMLHelpers.Extensions.SelectList.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalyst.MVC.Infrastructure.HTMLHelpers.Extensions.SelectList;

namespace CStore.Domain.HTMLHelpers.Extensions.SelectList.Select2
{
    public class DomainSelectListConfiguration : BaseSelectListConfiguration
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DomainSelectListConfiguration() : base()
        {
        }

        /// <summary>
        /// New Dependency Factory Method
        /// </summary>
        /// <returns></returns>
        protected override BaseSelectListDependency NewDependency()
        {
            DomainSelectListDependency dependency = new DomainSelectListDependency();

            return (BaseSelectListDependency)dependency;
        }

        /// <summary>
        /// Get Provider
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public override ISelectListProvider GetProvider(CatalystHtmlHelper html)
        {
            return new DomainSelectListProvider(this, html);
        }
    }
}
