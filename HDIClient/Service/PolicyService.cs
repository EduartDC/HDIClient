using HDIClient.DTOs;
using HDIClient.Service.Interface;
using Microsoft.Extensions.Caching.Memory;

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


        public async Task<List<PolicyDTO>> GetPolicyByDriver(string token, string idDriver)
        {
            List<PolicyDTO> result = null;

            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                result = await _cliente.GetFromJsonAsync<List<PolicyDTO>>($"/api/Policy/GetPolicyByDriver/{idDriver}");
            }
            catch (Exception)
            {
                throw new Exception("Error al conectarse con el servidor");
            }
            return result;
        }
    }
}
