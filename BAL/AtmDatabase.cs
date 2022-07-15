using DAL;
using BAL.Entities;

namespace BAL
{
    public class AtmDatabase : Database
    {
        public AtmDatabase(string dataSource) : base(dataSource)
        {
        }

        public IEnumerable<HistoricalTransactionAtm> GetAtmAccountTransactionHistory(int accountId)
        {
            var result = new List<HistoricalTransactionAtm>();

            foreach (var transaction in GetAccountTransactionHistory(accountId))
            {
                result.Add(new HistoricalTransactionAtm
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
