using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDIClient.Service;
using HDIClient.Service.Interface;

namespace TestProject
{
    public class ReportTest
    {
        private readonly IPolicyService _service;
        private readonly IReportService _reportService;

        public ReportTest(IPolicyService service, IReportService reportService)
        {
            _service = service;
            _reportService = reportService;

        }

        [Fact]
        public void TestGetReportById()
        {

        }
    }
}
