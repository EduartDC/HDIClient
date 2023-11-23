using AseguradoraApp.Models;
using HDIClient.DTOs;
using HDIClient.Service.Interface;
using Newtonsoft.Json;
using System.Text;

namespace HDIClient.Service
{
    public class ClientService : IClientService
    {

        HttpClient _cliente;
        public ClientService(IHttpClientFactory httpClientFactory)
        {
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");
        }

        public async Task<int> RegisterNewClientDriver(DriverClient driverClient)
        {
            var result = 0;
            try
            {

                var json = new StringContent(JsonConvert.SerializeObject(driverClient), Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync("/Driver/SetNewDriver", json);
                result = (int)response.StatusCode;
            }
            catch (HttpRequestException)
            {
                result = 500;
            }

            return result;
        }
    }
}
