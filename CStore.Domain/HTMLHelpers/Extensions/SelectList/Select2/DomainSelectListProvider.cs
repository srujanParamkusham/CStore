using Catalyst.MVC.Infrastructure.HTMLHelpers;
using Catalyst.MVC.Infrastructure.HTMLHelpers.Extensions.Grid.DataTablesReadonly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalyst.MVC.Infrastructure.HTMLHelpers.Extensions.SelectList.Select2;

namespace CStore.Domain.HTMLHelpers.Extensions.SelectList.Select2
{
    public class DomainSelectListProvider : BaseSelectListProvider
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="html"></param>
        public DomainSelectListProvider(BaseSelectListConfiguration config, CatalystHtmlHelper html)
            : base(config, html)
        {
        }
    }
}
