using DAL.Entities;
using DAL.Interfaces;
using DAL.XMLData;
using System.Xml.Linq;

namespace DAL.Repositories
{
    public class HistoricalTransactionDataRepository: IHistoricalTransactionDataRepository
    {
        private readonly XMLDb _xmlDb;

        public HistoricalTransactionDataRepository()
        {
            _xmlDb = new XMLDb();
        }

        public void SaveTransactionHistory(int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy)
        {
            _xmlDb.Xelement.Element("TransactionHistoryTable").Add
                 (
                     new XElement
                         (
                             "Transaction", 
                             new XAttribute("id", ((int)_xmlDb.Xelement.Descendants("Transaction").Max(m => (int)m.Attribute("id")) + 1).ToString()),
                             new XAttribute("accountId", accountId.ToString()),
                             new XAttribute("dateTime", TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local).ToString()),
                             new XAttribute("ammount", ammount.ToString("0.00")),
                             new XAttribute("balanceAfter", balanceAfter.ToString("0.00")),
                             new XAttribute("modifiedBy", modifiedBy)
                         )
                  );
            _xmlDb.Save();
        }

        public IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory()
        {
            return GetAccountTransactionHistory(0);
        }
        public IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory(int accountId)
        {           
            return (_xmlDb.Xelement.Descendants("Transaction").Where(m => ((int)m.Attribute("accountId") == accountId && accountId != 0) | (accountId == 0)).ToList().Select(record => new HistoricalTransactionData()
            {
                Id = (int)record.Attribute("id"),
                AccountId = (int)record.Attribute("accountId"),
                Type = (decimal)record.Attribute("ammount") > 0 ? "Deposit" : "Withdraw",
                CashAmount = (decimal)record.Attribute("ammount"),
                BalanceBefore = (decimal)record.Attribute("balanceAfter") - (decimal)record.Attribute("ammount"),
                BalanceAfter = (decimal)record.Attribute("balanceAfter"),
                LogDatetime = TimeZoneInfo.ConvertTimeFromUtc((DateTime)record.Attribute("dateTime"), TimeZoneInfo.Local),
                UserName = (string)record.Attribute("modifiedBy")
            })).ToList();
        }
    }
}
