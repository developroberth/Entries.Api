using Entries.Api.Models;

namespace Entries.Api.Services
{
    public interface IApiService
    {
        Task<Response> GetListEntriesAsync<T>();
    }
}
