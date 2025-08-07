using System.ComponentModel.DataAnnotations;

namespace BankApi.Dtos
{
    public class AccountCreateDto
    {
        [Required]
        public string AccountNumber { get; set; } = null!;

        [Required]
        public string AccountType { get; set; } = null!;

        [Required]
        public string Currency { get; set; } = null!;

        public decimal Balance { get; set; }
    }
}
