//This file is the Interface for the Client Service
//It defines the methods that the Client Service must implement

using BankApi.Models;

namespace BankApi.Services
{
    public interface IClientService
    {
        Task<Client> CreateClientAsync(Client client);
        Task<IEnumerable<Client>> GetAllClientsAsync();
    }
}