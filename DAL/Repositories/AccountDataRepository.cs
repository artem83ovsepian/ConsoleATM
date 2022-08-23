using DAL.Entities;
using DAL.RepositoriesBase;


namespace DAL.Repositories
{
    public class AccountDataRepository : AccountDataRepositoryBase
    {
        public AccountDataRepository(string dbSource) : base(dbSource)
        {
        }
        public AccountData GetAccountByUserId(int userId)
        {
            return _accountData.GetAccountByUserId(userId);
        }
        public void SaveAccountBalance(int accountId, decimal balance)
        {
            _accountData.SaveAccountBalance(accountId, balance);
        }
        public decimal GetAccountBalance(int accountId)
        {
            return _accountData.GetAccountBalance(accountId);
        }
        public decimal? GetUserOverdraft(int accountId)
        {
            return _accountData.GetUserOverdraft(accountId);
        }
    }
}
