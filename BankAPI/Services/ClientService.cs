using BankApi.Data;
using BankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDBContext _context;

        public ClientService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients.Include(c => c.Accounts).ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            var client = await _context.Clients
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                throw new ArgumentException("Client not found.");
            }
            return client;
        }
    }
}