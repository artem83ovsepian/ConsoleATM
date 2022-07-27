namespace BAL.Entities
{
    public class HistoricalTransactionAtm
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public decimal CashAmount { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
        public DateTime LogDatetime { get; set; }
        public string UserName { get; set; }
    }
}
