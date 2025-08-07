using BankApi.Data;
using BankApi.Models;
using Microsoft.EntityFrameworkCore;
using BankApi.Dtos;

namespace BankApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDBContext _context;

        public AccountService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccountAsync(Account account, Guid clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);

            if (client == null)
            {
                throw new ArgumentException("Client not found.");
            }

            account.Id = Guid.NewGuid();
            account.ClientId = clientId;
            account.CreatedAt = DateTime.UtcNow;

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }


        //Get account by ID
        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            var account = await _context.Accounts
                .Include(a => a.Client)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (account == null)
            {
                throw new ArgumentException("Account not found.");
            }
            return account;
        }

        //Get accounts by client ID
        public async Task<List<AccountSummaryDto>> GetAccountsByClientIdAsync(Guid clientId)
        {
            return await _context.Accounts
                .Where(a => a.ClientId == clientId)
                .Select(a => new AccountSummaryDto
                {
                    Id = a.Id,
                    AccountNumber = a.AccountNumber,
                    AccountType = a.AccountType,
                    Currency = a.Currency,
                    Balance = a.Balance,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();
        }

        //Deposit Method
        public async Task<TransactionDto> DepositAsync(Guid accountId, TransactionCreateDto dto)
        {
            if (dto.Amount <= 0)
                throw new ArgumentException("Deposit Amount Must be Greater than zero.");

            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
                throw new ArgumentException("Account not found.");

            account.Balance += dto.Amount;

            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = dto.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Deposit"
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                TransactionType = transaction.TransactionType,
                AccountId = transaction.AccountId
            };
        }
        
    }
}
