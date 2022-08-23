using DAL.Entities;
using DAL.Repositories.Logging.Csv;
using DAL.RepositoriesBase;

namespace DAL.Repositories
{
 
    public class HistoricalTransactionDataRepository : HistoricalTransactionDataRepositoryBase
    {
        public HistoricalTransactionDataRepository(string dbSource) : base(dbSource)
        {
        }
        public IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory(int accountId)
        {
            return _historicalTransactionData.GetAccountTransactionHistory(accountId);
        }
        public IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory()
        {
            return _historicalTransactionData.GetAccountTransactionHistory();
        }
        public int SaveTransactionHistory(int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy)
        {
            var transactionLogId = _historicalTransactionData.SaveTransactionHistory(accountId, dateTime, ammount, balanceAfter, modifiedBy);

            var transactionLog = new TransactionLog(_dbSource);
            transactionLog.WriteRecord(new List<string>
                                       {
                                           transactionLogId.ToString(),
                                           accountId.ToString(),
                                           TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local).ToString(),
                                           ammount.ToString("0.00"),
                                           balanceAfter.ToString("0.00"),
                                           modifiedBy
                                       });

            return transactionLogId;
        }
    }
}
