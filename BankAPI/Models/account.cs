using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankApi.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        [Required]
        public string AccountNumber { get; set; } = null!;

        [Required]
        public string AccountType { get; set; } = null!;

        [Required]
        public string Currency { get; set; } = null!;

        public decimal Balance { get; set; }

        public DateTime CreatedAt { get; set; }

        // Foreign key to Client
        [JsonIgnore]
        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;
    }
}