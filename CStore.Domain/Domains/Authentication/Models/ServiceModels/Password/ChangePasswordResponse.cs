using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.Password
{
    /// <summary>
    /// Service response object for the method to change a users password
    /// </summary>
    public class ChangePasswordResponse : DomainServiceResponse
    {
        /// <summary>
        /// If the password change was for a security user record, this record
        /// </summary>
        public DateTime? PasswordExpirationDate { get; set; }
    }
}
