using DAL;

namespace BAL
{
    public class AtmDatabase : Database
    {
        public AtmDatabase(string dataSource) : base(dataSource)
        {
        }

        public IEnumerable<AtmHistoricalTransaction> GetAtmAccountTransactionHistory(int accountId)
        {
            List<HistoricalTransactionData> historicalTransactionData = GetAccountTransactionHistory(accountId);

            List<AtmHistoricalTransaction> atmHistoricalTransaction = new();

            foreach (HistoricalTransactionData transaction in historicalTransactionData)
            {
                var atmHistoricalTransactionObject = new AtmHistoricalTransaction
                {
                    Type = transaction.Type,

                    CashAmount = transaction.CashAmount,

                    BalanceBefore = transaction.BalanceBefore,

                    BalanceAfter = transaction.BalanceAfter,

                    Datetime = transaction.Datetime,

                    UserName = transaction.UserName
                };

                atmHistoricalTransaction.Add(atmHistoricalTransactionObject);
            }
            return atmHistoricalTransaction;
        }
    }
}
