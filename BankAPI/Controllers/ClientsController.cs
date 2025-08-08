using BankApi.Models;
using BankApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    /// <summary>
    /// Controller to manage client-related operations.
    /// </summary>
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        /// <summary>
        /// Constructor for ClientsController.
        /// </summary>
        /// <param name="clientService">The client service instance.</param>
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Creates a new client profile.
        /// </summary>
        /// <param name="client">Client data in the request body.</param>
        /// <returns>The created client.</returns>
        /// <response code="201">Client successfully created.</response>
        /// <response code="400">Invalid client data supplied.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Client), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest(new { error = "Client data is required." });
            }

            try
            {
                var createdClient = await _clientService.CreateClientAsync(client);
                return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
            }
            catch (ArgumentException ex)
            {
                // ArgumentException is for invalid input or business rule violation
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                // Generic catch: so we don't give too much info to the client
                return StatusCode(500, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves all clients.
        /// </summary>
        /// <returns>A list of clients.</returns>
        /// <response code="200">Returns list of clients.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                return Ok(clients);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves a client by ID.
        /// </summary>
        /// <param name="id">The client unique identifier.</param>
        /// <returns>The client object.</returns>
        /// <response code="200">Returns the client with the specified ID.</response>
        /// <response code="400">Invalid ID supplied.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetClientById([FromRoute] Guid id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                if (client == null)
                {
                    return NotFound(new { error = "Client not found." });
                }
                return Ok(client);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
