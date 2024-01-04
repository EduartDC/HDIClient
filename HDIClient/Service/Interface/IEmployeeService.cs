using HDIClient.DTOs;

namespace HDIClient.Service.Interface
{
    public interface IEmployeeService
    {
        public Task<int> RegisterNewEmployee(EmployeeDTO newEmployee);
        public Task<(int, EmployeeDTO)> GetEmployeeById(string idEmployee);

        public Task<int> SetUpdateEmployee(EmployeeDTO newEmployee);

        public Task<(int, List<EmployeeDTO>)> GetEmployeeList(string token);



    }
}
