using BAL.Entities;
using DAL.Repositories;

namespace BAL.Repositories
{
    public class HistoricalTransactionAtmRepository
    {
        private readonly HistoricalTransactionDataRepository _historicalTransactionDataRepository;

        public HistoricalTransactionAtmRepository(string dbType)
        {
            _historicalTransactionDataRepository = new HistoricalTransactionDataRepository(dbType);
        }

        public IEnumerable<HistoricalTransactionAtm> GetAccountTransactionHistory(int accountId)
        {
            return (from transaction in _historicalTransactionDataRepository.GetAccountTransactionHistory(accountId)
                    select new HistoricalTransactionAtm
                    {
                        Type = transaction.Type,
                        CashAmount = transaction.CashAmount,
                        BalanceBefore = transaction.BalanceBefore,
                        BalanceAfter = transaction.BalanceAfter,
                        LogDatetime = transaction.LogDatetime,
                        UserName = transaction.UserName
                    }).ToList();
        }
        
        public void SaveTransactionHistory(int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy)
        {
            var transactionLogId = _historicalTransactionDataRepository.SaveTransactionHistory(accountId, dateTime, ammount, balanceAfter, modifiedBy);
        }

    }
}