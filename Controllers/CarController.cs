using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using WebKursach.ApplicationCore.Models;
using WebKursach.ApplicationCore.Interfaces.Services;

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
        [HttpGet("GetAllCars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            return await Task.Run(_carService.GetAllCars);
        }

        [HttpGet("GetAllAvailableCars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllAvailableCars()
        {
            return await Task.Run(_carService.GetAllAvailableCars);
        }

        [HttpGet("GetAllSoldCars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllSoldCars()
        {
            return await Task.Run(_carService.GetAllSoldCars);
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
            var carCreated = await Task.Run(() => _carService.CreateCar(
                car.Brand,
                car.Model,
                car.Cost,
                car.Color,
                car.Max_speed,
                car.Power,
                car.Url));

            if(carCreated)
            {
                return CreatedAtAction("PostCar", new { id = car.Id }, car);
            }

            return BadRequest();
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            var carUpdated = await Task.Run(() => _carService.UpdateCar(car));
            if (carUpdated)
            {
                return Ok(car);
            }
            return NotFound();
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var carDeleted = await Task.Run(() => _carService.DeleteCar(id));

            if (carDeleted)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
