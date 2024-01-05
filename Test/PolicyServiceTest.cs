using HDIClient.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class PolicyServiceTest
    {
        private readonly PolicyService _service;

        public PolicyServiceTest()
        {
            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);

            // Configura la propiedad BaseAddress del HttpClient directamente
            httpClient.BaseAddress = new Uri("https://localhost:7026/");

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _service = new(httpClientFactory.Object);
        }

        [Fact]
        public async Task TestGetPolicyByDriverSuccess()
        {
            var result = await _service.GetPolicyByDriver("token", "c1");

            Assert.Equal(HttpStatusCode.OK, result.Item2);
        }

        [Fact]
        public async Task TestGetPolicyByDriverNotFound()
        {
            var result = await _service.GetPolicyByDriver("token", "c12234");

            Assert.Equal(HttpStatusCode.NotFound, result.Item2);
        }

        [Fact]
        public async Task TestGetPolicyByDriverInternalServerError()
        {
            var result = await _service.GetPolicyByDriver("token", "c12234");

            Assert.Equal(HttpStatusCode.InternalServerError, result.Item2);
        }


    }
}
