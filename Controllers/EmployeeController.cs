using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
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
        private ICarService _carService;

        public EmployeeController(IEmployeeService employeeService, ICarService carService)
        {
            _employeeService = employeeService;
            _carService = carService;
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
            await Task.Run(() => _employeeService.CreateEmployee(
                employee.Name,
                employee.Surname,
                employee.Post,
                employee.Empphonenumber,
                employee.Empaddress,
                employee.Emppassport,
                employee.Email,
                employee.Salary));

            return CreatedAtAction("PostEmployee", new { id = employee.Id }, employee);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(() => _employeeService.UpdateEmployee(employee));
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_employeeService.GetAllEmployees().Any(x => x.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var cars = _employeeService.GetEmployee(id).SoldCars;
                foreach (Car car in cars)
                {
                    _carService.DeleteCar(car.Id);
                }

                await Task.Run(() => _employeeService.DeleteEmployee(id));
            }
            catch (DbException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
