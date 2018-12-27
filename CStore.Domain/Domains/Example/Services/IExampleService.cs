using CStore.Domain.Domains.Example.Models.ServiceModels.Example;

namespace CStore.Domain.Domains.Example.Services
{
    public interface IExampleService
    {
        ExampleServiceMethodResponse ExampleServiceMethod(ExampleServiceMethodRequest request);
    }
}
