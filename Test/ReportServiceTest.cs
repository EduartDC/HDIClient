using HDIClient.Service;
using Moq;


namespace TestProject
{
    public  class ReportServiceTest
    {
        private readonly ReportService _service;

        public ReportServiceTest()
        {
            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);

            // Configura la propiedad BaseAddress del HttpClient directamente
            httpClient.BaseAddress = new Uri("https://localhost:7026/");

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _service = new(httpClientFactory.Object);
        }

        //Jonathan
        [Fact]
        public async Task GetReportListByIdEmployeeSuccess()
        {
            var result = await _service.GetPreviewReportsList("e1");
            Xunit.Assert.Equal("200", result.Item1.ToString());
        }

        //Jonathan
        [Fact]
        public async Task GetReportListByIdEmployeeConnectionError()
        {
            var result = await _service.GetPreviewReportsList("e1");
            Xunit.Assert.Equal("500", result.Item1.ToString());
        }
    }
}
