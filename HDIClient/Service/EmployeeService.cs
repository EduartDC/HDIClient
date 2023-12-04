using AseguradoraApp.Models;
using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Newtonsoft.Json;
using System.Text;

namespace HDIClient.Service
{
    public class EmployeeService : IEmployeeService
    {

        HttpClient _cliente;
        public EmployeeService(IHttpClientFactory httpClientFactory)
        {
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");
        }

        public async Task<(int, EmployeeDTO)> GetEmployeeById(string idEmployee)
        {
            EmployeeDTO employeeDTO = new EmployeeDTO();
            var code = 0;
            try
            {

                var json = new StringContent(JsonConvert.SerializeObject(idEmployee), Encoding.UTF8, "application/json");
                var response = await _cliente.GetAsync("/Employee/GetEmployeeById/" + idEmployee);
                code = (int)response.StatusCode;
                if(response.IsSuccessStatusCode)
                {
                    
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("BODY: " + content);
                    employeeDTO = JsonConvert.DeserializeObject<EmployeeDTO>(content);
                }
            }
            catch (HttpRequestException)
            {
                code = 500;
            }

            return (code,employeeDTO);
        }

        public async Task<(int, List<EmployeeDTO>)> GetEmployeeList()
        {
            List<EmployeeDTO> employeeList = new List<EmployeeDTO> ();
            var code = 0;
            try
            {

                
                var response = await _cliente.GetAsync("/Employee/GetEmployeeList");
                code = (int)response.StatusCode;
                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    
                    employeeList = JsonConvert.DeserializeObject<List<EmployeeDTO>>(content);
                }
            }
            catch (HttpRequestException)
            {
                code = 500;
            }

            return (code, employeeList);
        }

        public async Task<int> RegisterNewEmployee(EmployeeDTO newEmployee)
        {
            var result = 0;
            try
            {

                var json = new StringContent(JsonConvert.SerializeObject(newEmployee), Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync("/Employee/SetNewEmployee", json);
                result = (int)response.StatusCode;
            }
            catch (HttpRequestException)
            {
                result = 500;
            }

            return result;
        }

        public async Task<int> SetUpdateEmployee(EmployeeDTO employee)
        {
            var result = 0;
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync("/Employee/SetUpdateEmployee", json);
                result = (int)response.StatusCode;
            }
            catch (HttpRequestException)
            {
                result = 500;
            }

            return result;
        }
    }
}
