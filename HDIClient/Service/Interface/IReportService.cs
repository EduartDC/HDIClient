using HDIClient.DTOs;

namespace HDIClient.Service.Interface
{
    public interface IReportService
    {
        public Task<ReportDTO> GetReportById(string token, string idReport);
    }
}
