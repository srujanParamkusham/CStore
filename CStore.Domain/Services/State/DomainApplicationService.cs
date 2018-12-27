using Catalyst.MVC.Infrastructure.Services.State;

namespace CStore.Domain.Services.State
{
    /// <summary>
    /// A service used to get and set values across the entire application. This will pull its values
    /// by default from web.config or app.config which have a name matching a value in the VariableNames enum.
    /// </summary>
    public class DomainApplicationService : ApplicationService
    {

        #region Enum
        /// <summary>
        /// Variable Name
        /// </summary>
        public new enum VariableNames
        {
            IocContainer,
            ExampleAppVariable //TODO remove me in a real implementation
        }
        #endregion

        #region Internals
        /// <summary>
        /// Example application variable pulled from web.config or app.config. This will pull 
        /// the value in app settings setup like <add key="ExampleAppVariable" value="My Variable Value" />
        /// </summary>
        private string _exampleAppVariable; //TODO remove me in a real implementation
        #endregion


        #region Instance Property
        /// <summary>
        /// Cache Instance
        /// </summary>
        public new static DomainApplicationService Instance
        {
            get
            {
                return new DomainApplicationService();
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        protected DomainApplicationService() :
            base()
        {
        }
        #endregion

        #region Properties

        /// <summary>
        /// Example application variable pulled from web.config or app.config. This will pull 
        /// the value in app settings setup like <add key="ExampleAppVariable" value="My Variable Value" />
        /// </summary>
        public string ExampleAppVariable
        {
            //TODO remove me in a real implementation
            get
            {
                if (_exampleAppVariable == null)
                {
                    _exampleAppVariable = GetValue(VariableNames.ExampleAppVariable.ToString());
                }

                return _exampleAppVariable;
            }
            set { _exampleAppVariable = value; }
        }

        #endregion
    }
}
