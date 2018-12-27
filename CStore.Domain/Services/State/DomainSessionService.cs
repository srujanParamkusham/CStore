using Catalyst.MVC.Infrastructure.Services.State;
using System;

namespace CStore.Domain.Services.State
{
    /// <summary>
    /// A service used to get and set values for a specific HTTP session. This will maintain the values in
    /// the users session, using whatever session state provider your application is configured to use.
    /// </summary>
    public class DomainSessionService : SessionService
    {
        #region Enum
        /// <summary>
        /// Session Variables - To be overwritten in domain class
        /// </summary>
        public new enum SessionVariableNames
        {
            CurrentUser,
            ExampleSessionVariable ////TODO remove me in a real implementation
        }
        #endregion

        #region Internals

        #endregion

        #region Instance Property
        /// <summary>
        /// Cache Instance
        /// </summary>
        public new static DomainSessionService Instance
        {
            get
            {
                return new DomainSessionService();
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Example Session Variable
        /// </summary>
        public virtual String ExampleSessionVariable
        {
            //TODO remove me in a real implementation
            get
            {
                return GetFromSession<String>(SessionVariableNames.ExampleSessionVariable.ToString(), () => null);
            }
            set
            {
                lock (this)
                {
                    SetSessionValue(SessionVariableNames.ExampleSessionVariable.ToString(), value);
                }
            }
        }

        #endregion


    }
}
