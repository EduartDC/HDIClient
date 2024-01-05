using HDIClient.DTOs;

namespace HDIClient.Models
{
    public class EmployeeListViewModel
    {
        public string IdAccident { get; set; }
        public string Location { get; set; }
        public string AccidentDate { get; set; }
        public List<EmployeeDTO> employeeList { get; set; }
    }
}
