﻿using HDIClient.Service;
using Moq;
using System.Net;

namespace TestProject
{
    public  class UserServiceTest
    {
        private readonly UserService _service;

        public UserServiceTest()
        {
            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);

            // Configura la propiedad BaseAddress del HttpClient directamente
            httpClient.BaseAddress = new Uri("https://localhost:7026/");

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _service = new(httpClientFactory.Object);
        }

        //Eduart
        [Fact]
        public async Task TestLoginSuccess()
        {
            var result = await _service.Login("12345","admin");
            Assert.Equal(HttpStatusCode.OK, result.Item2);
        }

        //Eduart
        [Fact]
        public async Task TestLoginNotFound()
        {
            var result = await _service.Login("123asd221234aas", "admin");
            Assert.Equal(HttpStatusCode.NotFound, result.Item2);
        }

        //Eduart
        [Fact]
        public async Task TestLoginInternalServerError()
        {
            var result = await _service.Login("123asd221234aas", "admin");
            Assert.Equal(HttpStatusCode.InternalServerError, result.Item2);
        }
    }
}
