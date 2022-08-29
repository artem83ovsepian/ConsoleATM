using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IAccountData
    {
        AccountData GetAccount(int userId);
        void SaveAccountBalance(int accountId, decimal balance);
        decimal GetAccountBalance(int accountId);
        decimal? GetUserOverdraft(int accountId);

    }
}
