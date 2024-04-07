using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using WebKursach.ApplicationCore.Models;
using WebKursach.ApplicationCore.Interfaces.Services;
using System.Data.Common;
using System.Collections.Generic;

namespace WebAPILab2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;
        private ICarService _carService;

        public ClientController(IClientService clientService, ICarService carService)
        {
            _clientService = clientService;
            _carService = carService;
        }

        // GET: api/<ClientController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await Task.Run(_clientService.GetAllClients);
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await Task.Run(() => _clientService.GetClient(id));
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        // POST api/<ClientController>
        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var clientCreated = await Task.Run(() =>_clientService.CreateClient(
                _carService.GetAllCars().Where(c => c.Position == Position.InShop && c.Id == client.Cars.FirstOrDefault().Id).FirstOrDefault(),
                client.Name,
                client.Surname, 
                client.Phonenumber,
                client.Address,
                client.Passport));

            if (clientCreated)
            {
                return CreatedAtAction("PostClient", new { id = client.Id }, client);
            }

            return BadRequest();
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            var clientUpdated = await Task.Run(() => _clientService.UpdateClient(client));

            if (clientUpdated)
            {
                return Ok(client);
            }
            return NotFound();
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                if (_clientService.GetClient(id) != null)
                {
                    var cars = _clientService.GetClient(id).Cars;

                    List<Car> cars2 = new List<Car>();
                    cars2.CopyTo(cars.ToArray());

                    if (cars != null && cars2.Any())
                    {
                        foreach (Car car in cars2)
                        {
                            _carService.DeleteCar(car.Id);
                        }
                    }
                }

                var clientDeleted = await Task.Run(() => _clientService.DeleteClient(id));

                if (clientDeleted)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (DbException)
            {
                return NotFound();
            }
        }
    }
}
