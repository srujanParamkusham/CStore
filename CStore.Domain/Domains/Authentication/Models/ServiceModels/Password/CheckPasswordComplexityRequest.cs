using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.Password
{
    /// <summary>
    /// Service request object for the method to check the complexity of a users password
    /// </summary>
    public class CheckPasswordComplexityRequest : DomainServiceRequest
    {
        /// <summary>
        /// The new password to set for the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The username for whom the password is for.
        /// </summary>
        public string UserName { get; set; }
    }
}
