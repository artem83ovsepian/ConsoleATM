using DAL.Entities;
using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.Repositories
{
    public class AccountDataRepository
    {
        private readonly IAccountData _accountData;
        public AccountDataRepository(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _accountData = new AccountDataXml(); break;
                case "json": _accountData = new AccountDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
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
