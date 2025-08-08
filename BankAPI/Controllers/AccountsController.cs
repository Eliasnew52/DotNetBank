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

        /// <summary>
        /// Create a new bank account for a specific client.
        /// </summary>
        /// <param name="clientId">The ID of the client who owns the account.</param>
        /// <param name="dto">Account creation data transfer object.</param>
        /// <returns>The created account details.</returns>
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

        /// <summary>
        /// Get account details by account ID.
        /// </summary>
        /// <param name="clientId">The client ID who owns the account.</param>
        /// <param name="id">The account ID.</param>
        /// <returns>The account details.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAccountById(Guid clientId, Guid id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        /// <summary>
        /// Get all accounts belonging to a specific client.
        /// </summary>
        /// <param name="clientId">The client ID.</param>
        /// <returns>List of accounts.</returns>
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

        /// <summary>
        /// Deposit money into a specific account.
        /// </summary>
        /// <param name="clientId">The client ID who owns the account.</param>
        /// <param name="id">The account ID.</param>
        /// <param name="dto">Transaction data including the deposit amount.</param>
        /// <returns>The transaction details.</returns>
        [HttpPost("{id:guid}/deposit")]
        public async Task<IActionResult> Deposit(Guid clientId, Guid id, [FromBody] TransactionCreateDto dto)
        {
            var result = await _accountService.DepositAsync(id, dto);
            return Ok(result);
        }

        /// <summary>
        /// Withdraw money from a specific account.
        /// </summary>
        /// <param name="clientId">The client ID who owns the account.</param>
        /// <param name="id">The account ID.</param>
        /// <param name="dto">Transaction data including the withdrawal amount.</param>
        /// <returns>The transaction details.</returns>
        [HttpPost("{id:guid}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid clientId, Guid id, [FromBody] TransactionCreateDto dto)
        {
            var result = await _accountService.WithdrawAsync(id, dto);
            return Ok(result);
        }

        /// <summary>
        /// Get the transaction history of a specific account.
        /// </summary>
        /// <param name="id">The account ID.</param>
        /// <returns>A list of transactions for the account.</returns>
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
