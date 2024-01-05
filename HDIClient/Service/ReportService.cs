using HDIClient.DTOs;
using HDIClient.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace HDIClient.Service
{
    public class ReportService : IReportService
    {
        HttpClient _cliente;

        public ReportService(IHttpClientFactory httpClientFactory)
        {
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");
        }

        public async Task<(int, List<PreviewReportDTO>)> GetPreviewReportsList(string idAdjuster, string token)
        {
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            List<PreviewReportDTO> previewReports = new List<PreviewReportDTO>();
            var code = 0;
            try
            {

                var json = new StringContent(JsonConvert.SerializeObject(idAdjuster), Encoding.UTF8, "application/json");
                var response = await _cliente.GetAsync("/api/Report/GetPreviewReportsByEmployee/" + idAdjuster);
                code = (int)response.StatusCode;
                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    // Console.WriteLine("BODY: " + content);
                    previewReports = JsonConvert.DeserializeObject<List<PreviewReportDTO>>(content);
                }
            }
            catch (HttpRequestException)
            {
                code = 500;
            }

            return (code, previewReports);
        }

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
                var response = await _cliente.GetAsync($"/api/Report/GetReportById/{idReport}");
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
                statusCode = HttpStatusCode.InternalServerError;
            }
            return (result, statusCode);
        }

        public async Task<HttpStatusCode> PostOpionion(NewOpinionadjusterDTO opinion, string token)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                string opinionJson = System.Text.Json.JsonSerializer.Serialize(opinion);

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
                statusCode = HttpStatusCode.InternalServerError;
            }
            return statusCode;
        }

        public async Task<HttpStatusCode> PostReport(NewReport report, string token)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                string opinionJson = System.Text.Json.JsonSerializer.Serialize(report);

                // Crea el contenido de la solicitud con el JSON
                var content = new StringContent(opinionJson, Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync("/api/Report/CreateReport", content);
                if (!response.IsSuccessStatusCode)
                {
                    statusCode = response.StatusCode;
                }
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            return statusCode;
        }

        public async Task<HttpStatusCode> PutOpionion(NewOpinionadjusterDTO opinion, string token)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                string opinionJson = System.Text.Json.JsonSerializer.Serialize(opinion);

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
                statusCode = HttpStatusCode.InternalServerError;
            }
            return statusCode;

        }
    }
}
