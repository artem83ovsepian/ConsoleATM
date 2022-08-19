using DAL.Entities;
using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;
using DAL.Logging;

namespace DAL.Repositories
{
    public class HistoricalTransactionDataRepository
    {
        private readonly IHistoricalTransactionData _historicalTransactionData;
        private string _dbSource;
        public HistoricalTransactionDataRepository(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _historicalTransactionData = new HistoricalTransactionDataXml(); break;
                case "json": _historicalTransactionData = new HistoricalTransactionDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
            _dbSource = dbSource;
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
            //save to csv
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
