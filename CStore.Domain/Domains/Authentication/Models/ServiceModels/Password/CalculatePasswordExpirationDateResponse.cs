using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.Password
{
    /// <summary>
    /// Service response object for the method to calculate the password expiration date
    /// </summary>
    public class CalculatePasswordExpirationDateResponse : DomainServiceResponse
    {
        public DateTime? PasswordExpirationDate { get; set; }
    }
}
