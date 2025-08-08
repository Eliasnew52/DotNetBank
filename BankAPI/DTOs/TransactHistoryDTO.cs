namespace BankApi.Dtos
{
    public class TransactionHistoryDto
    {
        public int Id { get; set; }
        public string TransactionType { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
    }
}
