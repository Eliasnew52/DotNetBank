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

        // POST /api/clients/{clientId}/accounts
        [HttpPost]
        public async Task<IActionResult> CreateAccount(Guid clientId, [FromBody] AccountCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = new Account
            {
                AccountNumber = dto.AccountNumber,
                AccountType = dto.AccountType,
                Currency = dto.Currency,
                Balance = dto.Balance
            };

            try
            {
                var createdAccount = await _accountService.CreateAccountAsync(account, clientId);
                return CreatedAtAction(nameof(GetAccountById), new { clientId, id = createdAccount.Id }, createdAccount);
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

        // GET /api/clients/{clientId}/accounts/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAccountById(Guid clientId, Guid id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        // GET /api/clients/{clientId}/accounts
        [HttpGet]
        public async Task<IActionResult> GetAccountsByClientId(Guid clientId)
        {
            var accounts = await _accountService.GetAccountsByClientIdAsync(clientId);
            if (accounts == null || !accounts.Any())
            {
                return NotFound($"No accounts found for client with id {clientId}");
            }
            return Ok(accounts);
        }

        // POST /api/clients/{clientId}/accounts/{id}/deposit
        [HttpPost("{id:guid}/deposit")]
        public async Task<IActionResult> Deposit(Guid clientId, Guid id, [FromBody] TransactionCreateDto dto)
        {
            var result = await _accountService.DepositAsync(id, dto);
            return Ok(result);
        }

        // POST /api/clients/{clientId}/accounts/{id}/withdraw
        [HttpPost("{id:guid}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid clientId, Guid id, [FromBody] TransactionCreateDto dto)
        {
            var result = await _accountService.WithdrawAsync(id, dto);
            return Ok(result);
        }
        [HttpGet("{id:guid}/transactions")]
        public async Task<IActionResult> GetTransactionHistory([FromRoute] Guid id)
        {
            try
            {
                var history = await _accountService.GetTransactionHistoryAsync(id);
                if (history == null || !history.Any())
                    return NotFound($"No transactions found for this account {id}");

                return Ok(history);
            }
            catch (ArgumentException ex)
            {
                // Account not found or invalid argument
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // General internal server error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}
