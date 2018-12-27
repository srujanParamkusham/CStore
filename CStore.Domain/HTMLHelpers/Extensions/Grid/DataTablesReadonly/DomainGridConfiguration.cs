using Catalyst.MVC.Infrastructure.HTMLHelpers;
using Catalyst.MVC.Infrastructure.HTMLHelpers.Extensions.Grid;
using Catalyst.MVC.Infrastructure.HTMLHelpers.Extensions.Grid.DataTablesReadonly;

namespace CStore.Domain.HTMLHelpers.Extensions.Grid.DataTablesReadonly
{
    public class DomainGridConfiguration : BaseGridConfiguration
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public DomainGridConfiguration()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ajaxUrl"></param>
        /// <param name="keyColumnName"></param>
        /// <param name="parameterForm"></param>
        /// <param name="modal"></param>
        /// <param name="attributes"></param>
        public DomainGridConfiguration(string id, string ajaxUrl, string keyColumnName = "", string parameterForm = "", bool modal = false, object attributes = null)
            : base(id, ajaxUrl, keyColumnName, parameterForm, modal, attributes)
        {
        }

        /// <summary>
        /// New Button Factory Method
        /// </summary>
        /// <returns></returns>
        protected override BaseGridButton NewButton()
        {
            DomainGridButton button = new DomainGridButton();
            button.ModalDetails = this.DefaultModalDetails;

            return (BaseGridButton)button;
        }

        /// <summary>
        /// New Column Factory Method
        /// </summary>
        /// <returns></returns>
        protected override BaseGridColumn NewColumn()
        {
            DomainGridColumn column = new DomainGridColumn();
            column.ModalDetails = this.DefaultModalDetails;

            return (BaseGridColumn)column;
        }

        /// <summary>
        /// Get Provider
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public override IGridProvider GetProvider(CatalystHtmlHelper html)
        {
            return new DomainGridProvider(this, html);
        }
    }
}
