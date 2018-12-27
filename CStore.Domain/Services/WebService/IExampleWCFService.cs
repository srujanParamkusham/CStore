using CStore.Domain.Domains.Example.Models.ServiceModels.Example;
using System;
using System.ServiceModel;

namespace CStore.Domain.Services.WebService
{
    /// <summary>
    /// Interface for the Example WCF Web Service
    /// </summary>
    [ServiceContract]
    public interface IExampleWCFService : IDisposable
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        ExampleServiceMethodResponse ExampleServiceMethod(ExampleServiceMethodRequest request);
    }
}
