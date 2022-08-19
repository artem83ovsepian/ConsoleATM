using DAL.Entities;
using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.Repositories
{
    public class HistoricalTransactionDataRepository
    {
        private readonly IHistoricalTransactionData _historicalTransactionData;
        public HistoricalTransactionDataRepository(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _historicalTransactionData = new HistoricalTransactionDataXml(); break;
                case "json": _historicalTransactionData = new HistoricalTransactionDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
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
            return _historicalTransactionData.SaveTransactionHistory(accountId, dateTime, ammount, balanceAfter, modifiedBy);
        }
    }
}
