using HDIClient.Service;
using Moq;

using System.Net;

namespace TestProject
{
    public class ClientServiceTest
    {
        private readonly ClientService _service;

        public ClientServiceTest()
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
