using DAL.Entities;
using DAL.Interfaces;
using DAL.XMLData;
using System.Xml.Linq;

namespace DAL.Interaction.XML
{
    public class AccountDataXml: IAccountData
    {
        private readonly XMLDb _xmlDb;

        public AccountDataXml()
        {
            _xmlDb = new XMLDb();
        }

        public AccountData GetAccount(int userId)
        { 
            var accountRecord = _xmlDb.Xelement.Descendants("Account").Where(m => (int)m.Attribute("userId") == userId && (int)m.Attribute("isActive") == 1).Take(1).ElementAt(0);

            return new AccountData()
            {
                Id = (int)accountRecord.Attribute("id"),
                UserId = (int)accountRecord.Attribute("userId"),
                Balance = (decimal)accountRecord.Attribute("balance"),
                IsActive = (int)accountRecord.Attribute("isActive"),
                OverDraft = (decimal)accountRecord.Attribute("overdraft")
            };        
        }

        public void SaveAccountBalance(int accountId, decimal balance)
        {
            _xmlDb.Xelement.Descendants("Account").FirstOrDefault(m => (int)m.Attribute("id") == accountId).Attributes("balance").ElementAt(0).Value = balance.ToString();
            _xmlDb.Save();
        }

        public decimal GetAccountBalance(int accountId)
        {
            return (decimal)_xmlDb.Xelement.Descendants("Account").FirstOrDefault(m => (int)m.Attribute("id") == accountId).Attribute("balance");
        }

        public decimal? GetUserOverdraft(int accountId)
        {
            return (decimal?)_xmlDb.Xelement.Descendants("Account").FirstOrDefault(m => (int)m.Attribute("id") == accountId)?.Attribute("overdraft");
        }
    }
}
