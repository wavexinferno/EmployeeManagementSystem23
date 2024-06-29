using EmployeeManagementSystem.DataAccess;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            var result = await _employeeRepository.InsertEmployeeAsync(employee);
            if (result)
            {
                return CreatedAtAction(nameof(CreateEmployee), new { id = employee.EmployeeId }, employee);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<bool> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            var result = await _employeeRepository.UpdateEmployeeAsync(employee);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteEmployee(int id)
        {
            var result = await _employeeRepository.DeleteEmployeeAsync(id);
            return result;
        }
    }
}
