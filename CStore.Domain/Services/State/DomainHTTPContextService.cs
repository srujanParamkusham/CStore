using Catalyst.MVC.Infrastructure.Services.State;
using System;

namespace CStore.Domain.Services.State
{
    /// <summary>
    /// A service used to get and set values for a specific HTTP Context (a single rendering of a page). This will maintain the values in
    /// the HTTPContext.Current.Items dictionary.
    /// </summary>
    public class DomainHTTPContextService : HTTPContextService
    {
        #region Enum
        /// <summary>
        /// Context Variables - To be overwritten in domain class
        /// </summary>
        public new enum ContextVariableNames
        {
            ExampleHTTPContextVariable ////TODO remove me in a real implementation            
        }
        #endregion

        #region Instance Property
        /// <summary>
        /// Cache Instance
        /// </summary>
        public new static DomainHTTPContextService Instance
        {
            get
            {
                return new DomainHTTPContextService();
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Example HTTP Context Variable
        /// </summary>
        public virtual String ExampleHTTPContextVariable
        {
            //TODO remove me in a real implementation
            get
            {
                return GetContextValue<String>(ContextVariableNames.ExampleHTTPContextVariable.ToString());
            }
            set
            {
                SetContextValue(ContextVariableNames.ExampleHTTPContextVariable.ToString(), value);
            }
        }
        #endregion

    }
}
