//This file is the Interface for the Account Service
//It defines the methods that the Account Service must implement

using BankApi.Models;
using BankApi.Dtos;

namespace BankApi.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(Account account, Guid clientId);
        Task<Account> GetAccountByIdAsync(Guid id);
        Task<List<AccountSummaryDto>> GetAccountsByClientIdAsync(Guid clientId);

        // Money Movement Methods
        Task<TransactionDto> DepositAsync(Guid accountId, TransactionCreateDto dto);

    }
}