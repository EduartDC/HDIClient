using HDIClient.Service;
using HDIClient.Service.Interface;

namespace TestProject
{
    public class UnitTest1
    {
        private readonly IPolicyService _service;
        private readonly IReportService _reportService;

        public UnitTest1(IPolicyService service, IReportService reportService)
        {
            _service = service;
            _reportService = reportService;

        }
        [Fact]
        public void Test1()
        {

        }
    }
}