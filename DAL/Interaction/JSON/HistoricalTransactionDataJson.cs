using DAL.JSONData;
using DAL.Interfaces;
using DAL.Entities;

namespace DAL.Interaction.JSON
{
    public class HistoricalTransactionDataJson : IHistoricalTransactionData
    {
        private readonly JSONDb _jsonDb;

        public HistoricalTransactionDataJson()
        {
            _jsonDb = new JSONDb();
        }

        public int SaveTransactionHistory(int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy)
        {
            var result = _jsonDb.DbRoot.Transaction.Max(m => int.Parse(m.Id) + 1);
            var newRecord = new Transaction()
            {
                Id = result.ToString(),
                AccountId = accountId.ToString(),
                DateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local).ToString(),
                Ammount = ammount.ToString("0.00"),
                BalanceAfter = balanceAfter.ToString("0.00"),
                ModifiedBy = modifiedBy
            };

            _jsonDb.DbRoot.Transaction.Add(newRecord);            
            _jsonDb.Save();

            return result;
        }

        public IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory()
        {
            return GetAccountTransactionHistory(0);
        }

        public IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory(int accountId)
        {
            return (_jsonDb.DbRoot.Transaction.Where(p => (int.Parse(p.AccountId) == accountId && accountId != 0) | (accountId == 0)).ToList().Select(
                record => new HistoricalTransactionData()
                {
                    Id = int.Parse(record.Id),
                    AccountId = int.Parse(record.AccountId),
                    Type = decimal.Parse(record.Ammount) > 0 ? "Deposit" : "Withdraw",
                    CashAmount = decimal.Parse(record.Ammount),
                    BalanceBefore = decimal.Parse(record.BalanceAfter) - decimal.Parse(record.Ammount),
                    BalanceAfter = decimal.Parse(record.BalanceAfter),
                    LogDatetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(record.DateTime), TimeZoneInfo.Local),
                    UserName = record.ModifiedBy
                })).ToList();
        }
    }
}
