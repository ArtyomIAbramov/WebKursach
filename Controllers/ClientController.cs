using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using WebKursach.ApplicationCore.Models;
using WebKursach.ApplicationCore.Interfaces.Services;

namespace WebAPILab2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/<ClientController>
        [HttpGet("GetClients")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await Task.Run(_clientService.GetAllClients);
        }

        [HttpGet("GetAllNewClients")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllNewClients()
        {
            return await Task.Run(_clientService.GetAllNewClients);
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
                client.Name,
                client.Surname, 
                client.Phonenumber,
                client.Address,
                client.Passport,
                client.Cars.FirstOrDefault()));

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
    }
}
