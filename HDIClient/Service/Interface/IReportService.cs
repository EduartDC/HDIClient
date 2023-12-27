using HDIClient.DTOs;
using System.Net;

namespace HDIClient.Service.Interface
{
    public interface IReportService
    {
        public Task<(ReportDTO, HttpStatusCode)> GetReportById(string token, string idReport);
        public Task<HttpStatusCode> PutOpionion(NewOpinionadjusterDTO opinion);
        public Task<HttpStatusCode> PostOpionion(NewOpinionadjusterDTO opinion);
    }
}
