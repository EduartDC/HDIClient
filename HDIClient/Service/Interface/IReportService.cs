using HDIClient.DTOs;

using System.Net;


namespace HDIClient.Service.Interface
{
    public interface IReportService
    {

        public Task<(int, List<PreviewReportDTO>)> GetPreviewReportsList(string idAdjuster);

        public Task<(ReportDTO, HttpStatusCode)> GetReportById(string token, string idReport);
        public Task<HttpStatusCode> PutOpionion(NewOpinionadjusterDTO opinion, string token);
        public Task<HttpStatusCode> PostOpionion(NewOpinionadjusterDTO opinion, string token);
        public Task<HttpStatusCode> PostReport(NewReport report, string token);
    }
}
