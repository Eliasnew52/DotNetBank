using BankApi.Dtos;
using BankApi.Models;
using BankApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/clients/{clientId:guid}/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromRoute] Guid clientId, [FromBody] AccountCreateDto dto)
        {
            Console.WriteLine($"CreateAccount called for clientId: {clientId}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = new Account
            {
                AccountNumber = dto.AccountNumber,
                AccountType = dto.AccountType,
                Currency = dto.Currency,
                Balance = dto.Balance
                // CreatedAt and ClientId will be set in the service
            };

            try
            {
                var createdAccount = await _accountService.CreateAccountAsync(account, clientId);
                return CreatedAtAction(nameof(GetAccountById), new { id = createdAccount.Id }, createdAccount);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/api/accounts/{id:guid}")]
        public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }
        
        [HttpGet("/api/clients/{clientId:guid}/accounts")]
        public async Task<IActionResult> GetAccountsByClientId([FromRoute] Guid clientId)
        {
            var accounts = await _accountService.GetAccountsByClientIdAsync(clientId);
            if (accounts == null || !accounts.Any())
            {
                return NotFound($"No accounts found for client with id {clientId}");
            }
            return Ok(accounts);
        }



    }
}
