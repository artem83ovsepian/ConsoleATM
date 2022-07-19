using BAL.Entities;
using DAL.Repositories;

namespace BAL.Repositories
{
    public class HistoricalTransactionAtmRepository
    {
        private readonly HistoricalTransactionDataRepository _historicalTransactionDataRepository = new HistoricalTransactionDataRepository();

        public IEnumerable<HistoricalTransactionAtm> GetAccountTransactionHistory(int accountId)
        {
            var result = new List<HistoricalTransactionAtm>();

            foreach (var transaction in _historicalTransactionDataRepository.GetAccountTransactionHistory(accountId))
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
        
        public void SaveTransactionHistory(int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy)
        {
            _historicalTransactionDataRepository.SaveTransactionHistory(accountId, dateTime, ammount, balanceAfter, modifiedBy);
        }

    }
}
