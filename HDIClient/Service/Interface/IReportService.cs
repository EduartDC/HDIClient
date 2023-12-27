using HDIClient.DTOs;

namespace HDIClient.Service.Interface
{
    public interface IReportService
    {
        public Task<(int, List<PreviewReportDTO>)> GetPreviewReportsList(string idAdjuster);
    }
}
