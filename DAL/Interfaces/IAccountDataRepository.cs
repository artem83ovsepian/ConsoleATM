using DAL.Entities;

namespace DAL.Interfaces
{
    interface IAccountDataRepository
    {
        AccountData GetAccountByUserId(int userId);
        void SaveAccountBalance(int accountId, decimal balance);
        decimal GetAccountBalance(int accountId);
        decimal GetUserOverdraft(int accountId);

    }
}
