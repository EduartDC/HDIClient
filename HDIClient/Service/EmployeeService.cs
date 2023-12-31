﻿using HDIClient.DTOs;
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

        public async Task<(int, EmployeeDTO)> GetEmployeeById(string idEmployee, string token)
        {
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            EmployeeDTO employeeDTO = new EmployeeDTO();
            var code = 0;
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(idEmployee), Encoding.UTF8, "application/json");
                var response = await _cliente.GetAsync("/Employee/GetEmployeeById/" + idEmployee);
                code = (int)response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    employeeDTO = JsonConvert.DeserializeObject<EmployeeDTO>(content);
                }
            }
            catch (HttpRequestException)
            {
                code = 500;
            }

            return (code, employeeDTO);
        }

        public async Task<(int, List<EmployeeDTO>)> GetEmployeeList(string token)
        {
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            List<EmployeeDTO> employeeList = new List<EmployeeDTO>();
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

        public async Task<int> RegisterNewEmployee(EmployeeDTO newEmployee, string token)
        {
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
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

        public async Task<int> SetUpdateEmployee(EmployeeDTO employee, string token)
        {
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
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
