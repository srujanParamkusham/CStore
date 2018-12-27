using System.Collections.Generic;
using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Example.Models.ServiceModels.Example
{
    public class ExampleServiceMethodResponse : DomainServiceResponse
    {
        public List<SecurityUser> Users { get; set; }
    }
}
