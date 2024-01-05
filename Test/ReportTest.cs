using HDIClient.Service;
using HDIClient.Service.Interface;
using Moq;
using System.Net;

namespace TestProject
{
    public class ReportTest
    {
        private readonly IPolicyService _service;
        private readonly IReportService _reportService;
        private readonly ReportService _reportService1;


        public ReportTest()
        {

            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);

            // Configura la propiedad BaseAddress del HttpClient directamente
            httpClient.BaseAddress = new Uri("https://localhost:7026/");

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _reportService1 = new (httpClientFactory.Object);

        }

        [Fact]

        public async Task TestGetReportById()
        {
            string token = "";
            var idReport = "a1";
            var (report, code) = await _reportService1.GetReportById(token, idReport);
            var x = report;
            Assert.IsType<HttpStatusCode>(code);

        }

    
    }
}
