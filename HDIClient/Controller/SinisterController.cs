using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HDIClient.Models;
using System.Security.Claims;
using HDIClient.DTOs;

namespace HDIClient.Controllers
{
    public class SinisterController : Controller
    {
        private readonly IAccidentService _accidentService;
        private readonly IEmployeeService _employeeService;

        public SinisterController(IAccidentService accidentService, IEmployeeService employeeService)
        {
            _accidentService = accidentService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> ConsultClaimHistory()
        {
            
            var result = await _accidentService.GetAccidents();

            if (result != null && result.Any())
            {
                var model = new AccidentViewModel
                {
                    accidentList = result
                };
                return View("ConsultClaimHistory", model);
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> AssignLossAdjuster()
        {

            var result = await _accidentService.GetAccidentsWithoutAdjuster();

            if (result != null && result.Any())
            {
                var model = new AccidentViewModel
                {
                    accidentList = result
                };
                return View("AssignLossAdjuster", model);
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> AssignLossAdjusterExtend(string idAccident, string location, string accidentDate)
        {

            var token = User.FindFirst("token").Value;
            var result = await _employeeService.GetEmployeeList(token);

            if (result.Item1 == 200)
            {
                var model = new EmployeeListViewModel
                {
                    IdAccident = idAccident,
                    Location = location,
                    AccidentDate = accidentDate,
                    employeeList = result.Item2
                };
                return View("AssignLossAdjusterExtend", model);
            }
            else
            {
                return RedirectToAction("ErrorServer", "Home");
            }
        }







        [HttpPost("SaveAssignmentInDB")]
        public async Task<IActionResult> SaveAssignmentInDB(string idEmployee, string idAccident)
        {
            var element = new AdjusterWithAccidentDTO
            {
                IdAccident = idAccident,
                IdAdjuster = idEmployee
            };

            var result = await _accidentService.AssignAdjusterToAccident(element);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
