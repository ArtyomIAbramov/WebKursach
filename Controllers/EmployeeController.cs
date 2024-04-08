using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebKursach.ApplicationCore.Interfaces.Services;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await Task.Run(_employeeService.GetAllEmployees);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await Task.Run(() => _employeeService.GetEmployee(id));
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeeCreated = await Task.Run(() => _employeeService.CreateEmployee(
                employee.Name,
                employee.Surname,
                employee.Post,
                employee.Empphonenumber,
                employee.Empaddress,
                employee.Emppassport,
                employee.Email,
                employee.Salary));

            if (employeeCreated)
            {
                return CreatedAtAction("PostEmployee", new { id = employee.Id }, employee);
            }

            return BadRequest();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var employeeUpdated = await Task.Run(() => _employeeService.UpdateEmployee(employee));
            if (employeeUpdated)
            {
                return Ok(employee);
            }
            return NotFound();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_employeeService.GetEmployee(id) != null)
            {
                var employeeDeleted = await Task.Run(() => _employeeService.DeleteEmployee(id));

                if (employeeDeleted)
                {
                    return Ok();
                }
                return NotFound();
            }
            return NotFound();
        }
    }
}
