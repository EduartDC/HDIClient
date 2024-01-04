using HDIClient.DTOs;
using HDIClient.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Net;

namespace HDIClient.Service
{
    public class PolicyService : IPolicyService
    {
        HttpClient _cliente;
        IMemoryCache _memoryCache;

        public PolicyService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");


        }

        public async Task<(List<PolicyDTO>?, HttpStatusCode)> GetPolicyByDriver(string token, string idDriver)
        {
            List<PolicyDTO> result = null;
            HttpStatusCode statusCode = HttpStatusCode.OK;

            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                var response = await _cliente.GetAsync($"/api/Policy/GetPolicyByDriver/{idDriver}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<List<PolicyDTO>>();
                }
                else
                {
                    statusCode = response.StatusCode;
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al conectarse con el servidor");
            }
            return (result, statusCode);
        }
    }
}
