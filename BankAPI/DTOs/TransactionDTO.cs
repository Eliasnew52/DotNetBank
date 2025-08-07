using System.ComponentModel.DataAnnotations;

namespace BankApi.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } = null!;
        public Guid AccountId { get; set; }
    }
}
