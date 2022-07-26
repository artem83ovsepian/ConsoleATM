using System.Xml;
using DAL.Entities;
using DAL.Interfaces;
using DAL.XMLData;

namespace DAL.Repositories
{
    public class AccountDataRepository: IAccountDataRepository
    {
        private readonly XMLDb _xmlDb;

        public AccountDataRepository()
        {
            _xmlDb = new XMLDb();
        }        

        public AccountData GetAccountByUserId(int userId)
        {
            var accountData = new AccountData();

            foreach (XmlNode account in _xmlDb.AccountTable)
            {
                if ((int.Parse(account.Attributes.GetNamedItem("userId").Value!) == userId) && (int.Parse(account.Attributes.GetNamedItem("isActive").Value!) == 1))
                {
                    accountData.Id = int.Parse(account.Attributes.GetNamedItem("id").Value!);
                    accountData.UserId = int.Parse(account.Attributes.GetNamedItem("userId").Value!);
                    accountData.Balance = decimal.Parse(account.Attributes.GetNamedItem("balance").Value!);
                    accountData.IsActive = int.Parse(account.Attributes.GetNamedItem("isActive").Value!);
                    accountData.OverDraft = decimal.Parse(account.Attributes.GetNamedItem("overdraft").Value!);
                    break;
                }
            }
            return accountData;
        }

        public void SaveAccountBalance(int accountId, decimal balance)
        {

            foreach (XmlNode account in _xmlDb.AccountTable)
            {
                var Id = int.Parse(account.Attributes.GetNamedItem("id").Value!);

                if (Id == accountId)
                {
                    account.Attributes.GetNamedItem("balance").Value = balance.ToString();
                    _xmlDb.Save();
                    break;
                }
            }
            
        }

        public decimal GetAccountBalance(int accountId)
        {
            var balance = 0m;

            foreach (XmlNode account in _xmlDb.AccountTable)
            {
                if (int.Parse(account.Attributes.GetNamedItem("id").Value!) == accountId)
                {
                    balance = decimal.Parse(account.Attributes.GetNamedItem("balance").Value!);
                }
            }
            return balance;
        }

        public decimal GetUserOverdraft(int accountId)
        {
            var overDraft = 0m;

            foreach (XmlNode account in _xmlDb.AccountTable)
            {
                if (int.Parse(account.Attributes.GetNamedItem("id").Value!) == accountId)
                {
                    overDraft = decimal.Parse(account.Attributes.GetNamedItem("overdraft").Value!);
                    break;
                }
            }
            return overDraft;       
        }
    }
}
