using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using Catalyst.MVC.Infrastructure.Services.Entity;

namespace CStore.Domain.Services.Entity
{
    /// <summary>
    /// This is a base entity service class. Classes providing basic maintenance functionality on entities
    /// should extend this class to use its capabilities for making searching, getting, saving and deleting entities
    /// easier from maintenance screens.
    /// </summary>
    public abstract class DomainEntityService : BaseEntityService
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public DomainEntityService(IRepository repository, ISendMailProvider sendMail) : base(repository, sendMail)
        {
        }
        #endregion

    }
}
