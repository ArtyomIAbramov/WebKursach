using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using WebKursach.ApplicationCore.Models;
using WebKursach.ApplicationCore.Interfaces.Services;
using System.Data.Common;

namespace WebAPILab2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class CarController : ControllerBase
    {
        private ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/<CarController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await Task.Run(_carService.GetAllCars);
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await Task.Run(() => _carService.GetCar(id));
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        // POST api/<CarController>
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => _carService.CreateCar(
                car.Brand,
                car.Model,
                car.Color,
                car.Max_speed,
                car.Power,
                car.Position));

            return CreatedAtAction("PostCar", new { id = car.Id }, car);
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(() => _carService.UpdateCar(car));
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_carService.GetAllCars().Any(x => x.Id == id))
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

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await Task.Run(() => _carService.DeleteCar(id));
            }
            catch (DbException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
