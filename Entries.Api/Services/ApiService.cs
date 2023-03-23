using Entries.Api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Entries.Api.Services
{
    public class ApiService : IApiService
    {
        //private readonly ILogger _logger;

        private readonly ILogger<ApiService> _logger;
        private readonly string _urlBase;
        public ApiService(IConfiguration configuration, ILogger<ApiService> logger)
        {

            _urlBase = configuration["EntriesAPI:urlBase"];
            _logger= logger;
        }

        public async Task<Response> GetListEntriesAsync<T>()
        {
            try
            {
                HttpClient client = new()
                {
                    BaseAddress = new Uri(_urlBase),
                };
                string url = $"entries";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}

