using AseguradoraApp.Models;
using HDIClient.DTOs;
using HDIClient.Service;
using Moq;


namespace TestProject
{
    public class EmployeeServiceTest
    {
        private readonly EmployeeService _service;

        public EmployeeServiceTest()
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
        public async Task RegisterEmployeeSuccess()
        {
            EmployeeDTO employeeTest = new EmployeeDTO()
            {
                IdEmployee = "",
                NameEmployee = "Test",
                LastnameEmployee = "Test",
                Username = "Test",
                Password = "123",
                Rol = "admin",
                RegistrationDate = DateTime.Now,
            };
            var result = await _service.RegisterNewEmployee(employeeTest);
            Xunit.Assert.Equal("200", result.ToString());
        }

        //Jonathan
        [Fact]
        public async Task RegisterEmployeeErrorUsernameUsed()
        {
            EmployeeDTO employeeTest = new EmployeeDTO()
            {
                IdEmployee = "",
                NameEmployee = "Test",
                LastnameEmployee = "Test",
                Username = "Test",
                Password = "123",
                Rol = "admin",
                RegistrationDate = DateTime.Now,
            };
            var result = await _service.RegisterNewEmployee(employeeTest);
            Xunit.Assert.Equal("409", result.ToString());
        }

        //Jonathan
        [Fact]
        public async Task RegisterEmployeeConnectionError()
        {
            EmployeeDTO employeeTest = new EmployeeDTO()
            {
                IdEmployee = "",
                NameEmployee = "Test",
                LastnameEmployee = "Test",
                Username = "Test",
                Password = "123",
                Rol = "admin",
                RegistrationDate = DateTime.Now,
            };
            var result = await _service.RegisterNewEmployee(employeeTest);
            Xunit.Assert.Equal("500", result.ToString());
        }

        //Jonathan
        [Fact]
        public async Task UpdateEmployeeSuccess()
        {
            EmployeeDTO employeeTest = new EmployeeDTO()
            {
                IdEmployee = "FVZJ9WBNRJ",
                NameEmployee = "Tes2t",
                LastnameEmployee = "Test",
                Username = "Test",
                Password = "123",
                Rol = "admin",
                RegistrationDate = DateTime.Now,
            };
            var result = await _service.SetUpdateEmployee(employeeTest);
            Xunit.Assert.Equal("200", result.ToString());
        }

        //Jonathan
        [Fact]
        public async Task UpdateEmployeeErrorUsernameUsedForOtherEmployee()
        {
            EmployeeDTO employeeTest = new EmployeeDTO()
            {
                IdEmployee = "FVZJ9WBNRJ",
                NameEmployee = "Tes2t",
                LastnameEmployee = "Test",
                Username = "jon1000",
                Password = "123",
                Rol = "admin",
                RegistrationDate = DateTime.Now,
            };
            var result = await _service.SetUpdateEmployee(employeeTest);
            Xunit.Assert.Equal("409", result.ToString());
        }

        //Jonathan
        [Fact]
        public async Task UpdateEmployeeConnectionError()
        {
            EmployeeDTO employeeTest = new EmployeeDTO()
            {
                IdEmployee = "FVZJ9WBNRJ",
                NameEmployee = "Tes2t",
                LastnameEmployee = "Test",
                Username = "jon1000",
                Password = "123",
                Rol = "admin",
                RegistrationDate = DateTime.Now,
            };
            var result = await _service.SetUpdateEmployee(employeeTest);
            Xunit.Assert.Equal("500", result.ToString());
        }


        //Jonathan
        [Fact]
        public async Task GetEmployeeListSuccess()
        {
            var result = await _service.GetEmployeeList("");
            Xunit.Assert.Equal("200", result.Item1.ToString());
        }

        //Jonathan
        [Fact]
        public async Task GetEmployeeListConnectionError()
        {
            var result = await _service.GetEmployeeList("");
            Xunit.Assert.Equal("500", result.Item1.ToString());
        }

    }
}
