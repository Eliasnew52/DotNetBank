using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class Exchange
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FromCurrency { get; set; } = null!;

        [Required]
        public string ToCurrency { get; set; } = null!;

        [Required]
        public decimal ExchangeRate { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        public string RateType { get; set; } = null!;

    }
}