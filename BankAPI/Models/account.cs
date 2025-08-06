using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; } = null!;

        [Required]
        public string AccountType { get; set; } = null!;

        [Required]
        public string Currency { get; set; } = null!;

        public decimal Balance { get; set; }

        public DateTime CreatedAt { get; set; }

        // Foreign key to Client
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
    }
}