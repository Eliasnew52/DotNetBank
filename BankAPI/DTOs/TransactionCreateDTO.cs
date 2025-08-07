using System.ComponentModel.DataAnnotations;

namespace BankApi.Dtos
{
    public class TransactionCreateDto
    {
        [Required]
        public decimal Amount { get; set; }
    }
}
