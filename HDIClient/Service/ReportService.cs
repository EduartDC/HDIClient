using HDIClient.DTOs;
using HDIClient.Service.Interface;
using Newtonsoft.Json;
using System.Text;

namespace HDIClient.Service
{
    public class ReportService : IReportService
    {
        HttpClient _cliente;
        public ReportService(IHttpClientFactory httpClientFactory)
        {
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");
        }

        public async Task<(int, List<PreviewReportDTO>)> GetPreviewReportsList(string idAdjuster)
        {
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
    }
}
