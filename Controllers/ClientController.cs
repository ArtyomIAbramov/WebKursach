using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using WebKursach.ApplicationCore.Models;
using WebKursach.ApplicationCore.Interfaces.Services;
using System.Data.Common;

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
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() =>_clientService.CreateClient(
                client.Cars.FirstOrDefault(),
                client.Name,
                client.Surname, 
                client.Phonenumber,
                client.Address,
                client.Passport));

            return CreatedAtAction("PostClient", new { id = client.Id }, client);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }
            try
            {
                await Task.Run(() => _clientService.UpdateClient(client));
            }
            catch (DbException)
            {
                if (!_clientService.GetAllClients().Any(x => x.Id == id))
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

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var cars = _clientService.GetClient(id).Cars;
                foreach (Car car in cars)
                {
                    _carService.DeleteCar(car.Id);
                }

                await Task.Run(() => _clientService.DeleteClient(id));
            }
            catch (DbException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
