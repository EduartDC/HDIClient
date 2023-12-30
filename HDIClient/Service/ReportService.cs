using HDIClient.DTOs;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text;
using System.Text.Json;

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

        public async Task<(ReportDTO, HttpStatusCode)> GetReportById(string token, string idReport)
        {
            ReportDTO result = null;
            HttpStatusCode statusCode = HttpStatusCode.OK;
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                var response =  await _cliente.GetAsync($"/api/Report/GetReportById/{idReport}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ReportDTO>();
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

        public async Task<HttpStatusCode> PostOpionion(NewOpinionadjusterDTO opinion)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            try
            {
                string opinionJson = JsonSerializer.Serialize(opinion);

                // Crea el contenido de la solicitud con el JSON
                var content = new StringContent(opinionJson, Encoding.UTF8, "application/json");

                // Realiza la solicitud post
                var response = await _cliente.PostAsync("/api/Report/PostOpinion", content);
                if (!response.IsSuccessStatusCode)
                {
                    statusCode = response.StatusCode;
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al conectarse con el servidor");
            }
            return statusCode;
        }

        public async Task<HttpStatusCode> PutOpionion(NewOpinionadjusterDTO opinion)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            try
            {
                string opinionJson = JsonSerializer.Serialize(opinion);

                // Crea el contenido de la solicitud con el JSON
                var content = new StringContent(opinionJson, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT
                var response = await _cliente.PutAsync("/api/Report/PutOpinion", content);
                if (!response.IsSuccessStatusCode)
                {
                    statusCode = response.StatusCode;
                }               
            }
            catch (Exception)
            {
                throw new Exception("Error al conectarse con el servidor");
            }
            return statusCode;
        }
    }
}
