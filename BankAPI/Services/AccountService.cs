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
        //Deposit Service
        public async Task<TransactionDto> DepositAsync(Guid accountId, TransactionCreateDto dto)
        {
            if (dto.Amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
                throw new ArgumentException("Account not found.");

            try
            {
                account.Balance += dto.Amount;

                var transaction = new Transaction
                {
                    AccountId = accountId,
                    Amount = dto.Amount,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = "Deposit",
                    BalanceAfterTransaction = account.Balance
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return new TransactionDto
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    TransactionDate = transaction.TransactionDate,
                    TransactionType = transaction.TransactionType,
                    AccountId = transaction.AccountId,
                    BalanceAfterTransaction = transaction.BalanceAfterTransaction
                };
            }
            catch (DbUpdateException dbEx)
            {
                // Log the error as needed
                throw new Exception("Database update error during deposit. Please try again.", dbEx);
            }
            catch (Exception ex)
            {
                // Log the error as needed
                throw new Exception("An unexpected error occurred during deposit.", ex);
            }
        }
        //Withdraw Service
        public async Task<TransactionDto> WithdrawAsync(Guid accountId, TransactionCreateDto dto)
        {
            if (dto.Amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
                throw new ArgumentException("Account not found.");

            if (account.Balance < dto.Amount)
                throw new InvalidOperationException("Insufficient funds for withdrawal.");

            try
            {
                account.Balance -= dto.Amount;

                var transaction = new Transaction
                {
                    AccountId = accountId,
                    Amount = dto.Amount,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = "Withdrawal",
                    BalanceAfterTransaction = account.Balance
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return new TransactionDto
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    TransactionDate = transaction.TransactionDate,
                    TransactionType = transaction.TransactionType,
                    AccountId = transaction.AccountId,
                    BalanceAfterTransaction = transaction.BalanceAfterTransaction
                };
            }
            catch (DbUpdateException dbEx)
            {
                // Log the error as needed
                throw new Exception("Database update error during withdrawal. Please try again.", dbEx);
            }
            catch (Exception ex)
            {
                // Log the error as needed
                throw new Exception("An unexpected error occurred during withdrawal.", ex);
            }
        }

        //Get Transaction History
        public async Task<List<TransactionHistoryDto>> GetTransactionHistoryAsync(Guid accountId)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .Select(t => new TransactionHistoryDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    TransactionType = t.TransactionType,
                    BalanceAfterTransaction = t.BalanceAfterTransaction
                })
                .ToListAsync();
        }


    }
}
