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

        //When a User sends money to another user, this field will represent the transaction ID of the related transaction
        // Is a Nullable string, cause it may not always be applicable
        public string? RelatedTransactionId { get; set; }

        // Foreign key to Account
        [Required]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;

    }
}   