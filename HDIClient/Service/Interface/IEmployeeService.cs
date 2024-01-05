using HDIClient.DTOs;

namespace HDIClient.Service.Interface
{
    public interface IEmployeeService
    {
        public Task<int> RegisterNewEmployee(EmployeeDTO newEmployee, string token);
        public Task<(int, EmployeeDTO)> GetEmployeeById(string idEmployee, string token);

        public Task<int> SetUpdateEmployee(EmployeeDTO newEmployee, string token);

        public Task<(int, List<EmployeeDTO>)> GetEmployeeList(string token);



    }
}
