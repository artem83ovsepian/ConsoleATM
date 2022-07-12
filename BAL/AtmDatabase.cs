using DAL;

namespace BAL
{
    public class AtmDatabase : Database
    {
        public AtmDatabase(string dataSource) : base(dataSource)
        {
        }

        public IEnumerable<HistoricalTransaction> GetAtmAccountTransactionHistory(int accountId)
        {
            var result = new List<HistoricalTransaction>();

            foreach (var transaction in GetAccountTransactionHistory(accountId))
            {
                result.Add(new HistoricalTransaction
                {
                    Type = transaction.Type,
                    CashAmount = transaction.CashAmount,
                    BalanceBefore = transaction.BalanceBefore,
                    BalanceAfter = transaction.BalanceAfter,
                    Datetime = transaction.Datetime,
                    UserName = transaction.UserName
                });
            }

            return result;
        }
    }
}
