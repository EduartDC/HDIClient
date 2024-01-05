using AseguradoraApp.Models;
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

        //Jonathan
        [Fact]
        public async Task RegisterDriverSucces()
        {
            DriverClient driverTest = new DriverClient()
            {
                IdDriverClient = "",
                NameDriver = "juanito3",
                LastNameDriver = "perez",
                AgeString = "2024/01/01",
                FullNameDriver = "",
                TelephoneNumber = "2281749118",
                LicenseNumber = "12345621",
                Password = "123"
            };
            var result = await _service.RegisterNewClientDriver(driverTest);
            Xunit.Assert.Equal("200", result.ToString());
        }

        //Jonathan
        [Fact]
        public async Task RegisterDriverErrorLicenceUsed()
        {
            DriverClient driverTest = new DriverClient()
            {
                IdDriverClient = "",
                NameDriver = "juanito3",
                LastNameDriver = "perez",
                AgeString = "2024/01/01",
                FullNameDriver = "",
                TelephoneNumber = "2281749118",
                LicenseNumber = "12345621",
                Password = "123"
            };
            var result = await _service.RegisterNewClientDriver(driverTest);
            Xunit.Assert.Equal("409", result.ToString());
        }

        //Jonathan
        [Fact]
        public async Task RegisterDriverConectionError()
        {
            DriverClient driverTest = new DriverClient()
            {
                IdDriverClient = "",
                NameDriver = "juanito3",
                LastNameDriver = "perez",
                AgeString = "2024/01/01",
                FullNameDriver = "",
                TelephoneNumber = "2281749118",
                LicenseNumber = "12345621",
                Password = "123"
            };
            var result = await _service.RegisterNewClientDriver(driverTest);
            Xunit.Assert.Equal("500", result.ToString());
        }

    }
}
