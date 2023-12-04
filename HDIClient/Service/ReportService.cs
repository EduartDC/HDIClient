using HDIClient.DTOs;
using HDIClient.Service.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace HDIClient.Service
{
    public class ReportService : IReportService
    {
        HttpClient _cliente;
        IMemoryCache _memoryCache;

        public ReportService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");


        }

        public async Task<ReportDTO> GetReportById(string token, string idReport)
        {
            ReportDTO result = null;
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                result = await _cliente.GetFromJsonAsync<ReportDTO>($"/api/Report/GetReportById/{idReport}");

            }
            catch (Exception)
            {
                throw new Exception("Error al conectarse con el servidor");
            }
            return result;
        }
    }
}
