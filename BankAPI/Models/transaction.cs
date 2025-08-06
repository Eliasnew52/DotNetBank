using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string TransactionType { get; set; } = null!;

        // Foreign key to Account
        [Required]
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}   