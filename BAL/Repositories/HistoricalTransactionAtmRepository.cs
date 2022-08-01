using BAL.Entities;
using DAL.Repositories;
using DAL.Interfaces;
using BAL.Logging;

namespace BAL.Repositories
{
    public class HistoricalTransactionAtmRepository
    {
        private readonly IHistoricalTransactionDataRepository _historicalTransactionDataRepository;

        private string _dbType;

        public HistoricalTransactionAtmRepository(string dbType)
        {
            switch (dbType)
            {
                case "xml": _historicalTransactionDataRepository = new XmlHistoricalTransactionDataRepository(); break;
                case "json": _historicalTransactionDataRepository = new JsonHistoricalTransactionDataRepository(); break;
                default: throw new ArgumentException(nameof(dbType));
            }
            _dbType = dbType;
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

            //write to csv transaction log file
            try
            {
                (new TransactionLog(_dbType)).WriteRecord(new List<string>
                                                            {
                                                                transactionLogId.ToString(),
                                                                accountId.ToString(),
                                                                TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local).ToString(),
                                                                ammount.ToString("0.00"),
                                                                balanceAfter.ToString("0.00"),
                                                                modifiedBy
                                                            });
            }
            catch (Exception ex)
            {
            }
        }

    }
}