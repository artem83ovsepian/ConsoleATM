namespace DAL
{
    public class HistoricalTransactionData
    {
        public string Type { get; set; }

        public decimal CashAmount { get; set; }

        public decimal BalanceBefore { get; set; }

        public decimal BalanceAfter { get; set; }

        public DateTime Datetime { get; set; }

        public string UserName { get; set; }
    }
}
