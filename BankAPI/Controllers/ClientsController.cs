using BankApi.Models;
using BankApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    // This controller handles client-related operations
    // The route is prefixed with "api/clients"


    [ApiController]
    [Route("api/clients")]

    // ClientsController is responsible for managing client-related requests
    // It uses the IClientService to interact with client data
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest("Client data is required.");
            }

            var createdClient = await _clientService.CreateClientAsync(client);
            return CreatedAtAction(nameof(CreateClient), new { id = createdClient.Id }, createdClient);
        }
    }
}           