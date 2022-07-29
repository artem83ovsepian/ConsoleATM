using DAL.JSONData;
using DAL.Interfaces;
using DAL.Entities;

namespace DAL.Repositories
{
    public class JsonAccountDataRepository: IAccountDataRepository
    {
        private readonly JSONDb _jsonDb;

        public JsonAccountDataRepository()
        {
            _jsonDb = new JSONDb();
        }

        public AccountData GetAccountByUserId(int userId)
        {
            var accountRecord = _jsonDb.DbRoot.Account.Where(p => p.UserId == userId.ToString() && p.IsActive == "1").LastOrDefault();

            return accountRecord==null ? new AccountData() : new AccountData()
            {
                Id = int.Parse(accountRecord.Id),
                UserId = int.Parse(accountRecord.UserId),
                Balance = decimal.Parse(accountRecord.Balance),
                IsActive = int.Parse(accountRecord.IsActive),
                OverDraft = decimal.Parse(accountRecord.Overdraft)
            };
        }
        public void SaveAccountBalance(int accountId, decimal balance)
        {
            _jsonDb.DbRoot.Account.LastOrDefault(p => (p.Id == accountId.ToString()) && (p.IsActive == "1")).Balance = balance.ToString();
            _jsonDb.Save();
        }
        public decimal GetAccountBalance(int accountId)
        {
            return decimal.Parse(_jsonDb.DbRoot.Account.LastOrDefault(p => p.Id == accountId.ToString() && p.IsActive == "1").Balance);
        }
        public decimal? GetUserOverdraft(int accountId)
        {
            return decimal.Parse(_jsonDb.DbRoot.Account.LastOrDefault(p => p.Id == accountId.ToString() && p.IsActive == "1").Overdraft);
        }
    }
}