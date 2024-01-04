using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDIClient.Service;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject
{
    public class ReportTest
    {
        private readonly IPolicyService _service;
        private readonly IReportService _reportService;

        public ReportTest(IPolicyService service, IReportService reportService)
        {
            var configuration = new Mock<IReportService>();
            _service = service;
            _reportService = reportService;

        }

        [Fact]
        public async Task TestGetReportById()
        {
            string token = "";
            var idReport = "e1";
            var (report, code) = await _reportService.GetReportById(token, idReport);

            Assert.IsType<OkObjectResult>(code);
        }
    }
}
