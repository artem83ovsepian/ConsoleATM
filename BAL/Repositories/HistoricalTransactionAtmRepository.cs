using BAL.Entities;
using DAL.Repositories;
using DAL.Interfaces;
using System.Linq;

namespace BAL.Repositories
{
    public class HistoricalTransactionAtmRepository
    {
        private readonly IHistoricalTransactionDataRepository _historicalTransactionDataRepository;

        public HistoricalTransactionAtmRepository()
        {
            _historicalTransactionDataRepository = new HistoricalTransactionDataRepository();
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
                        Datetime = transaction.Datetime,
                        UserName = transaction.UserName
                    }).ToList();
        }
        public void SaveTransactionHistory(int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy)
        {
            _historicalTransactionDataRepository.SaveTransactionHistory(accountId, dateTime, ammount, balanceAfter, modifiedBy);
        }

    }
}