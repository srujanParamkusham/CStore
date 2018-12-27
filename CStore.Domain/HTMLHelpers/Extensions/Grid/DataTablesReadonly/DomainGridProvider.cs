using Catalyst.MVC.Infrastructure.HTMLHelpers;
using Catalyst.MVC.Infrastructure.HTMLHelpers.Extensions.Grid.DataTablesReadonly;

namespace CStore.Domain.HTMLHelpers.Extensions.Grid.DataTablesReadonly
{
    public class DomainGridProvider : BaseGridProvider
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="html"></param>
        public DomainGridProvider(BaseGridConfiguration config, CatalystHtmlHelper html)
            : base(config, html)
        {
        }

    }
}
