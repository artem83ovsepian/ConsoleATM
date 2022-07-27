using DAL.Entities;


namespace DAL.Interfaces
{
    public interface IHistoricalTransactionDataRepository
    {
        IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory(int accountId);
        IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory();
        void SaveTransactionHistory(int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy);
    }

}
