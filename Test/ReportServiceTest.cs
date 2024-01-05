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
    }
}
