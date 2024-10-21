namespace SampleApi.Core.Services
{
    public interface IService
    {
    }

    public interface IAsyncService<TRequest, TResponse> : IService
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
}
