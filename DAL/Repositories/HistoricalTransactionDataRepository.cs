using System.Xml;
using DAL.Entities;
using DAL.Interfaces;
using DAL.XMLData;

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
            var transactionTableMaxId = _xmlDb.HistoryTable.Count + 1;

            var newTransaction = _xmlDb.CreateElement("Transaction");

            var attributeId = _xmlDb.CreateAttribute("id");
            attributeId.Value = transactionTableMaxId.ToString();
            newTransaction.Attributes.Append(attributeId);

            var attributeaccountId = _xmlDb.CreateAttribute("accountId");
            attributeaccountId.Value = accountId.ToString();
            newTransaction.Attributes.Append(attributeaccountId);

            var attributeaccountdatetime = _xmlDb.CreateAttribute("dateTime");
            attributeaccountdatetime.Value = TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local).ToString();
            newTransaction.Attributes.Append(attributeaccountdatetime);

            var attributeammount = _xmlDb.CreateAttribute("ammount");
            attributeammount.Value = ammount.ToString("0.00");
            newTransaction.Attributes.Append(attributeammount);

            var attributebalanceAfterg = _xmlDb.CreateAttribute("balanceAfter");
            attributebalanceAfterg.Value = balanceAfter.ToString("0.00");
            newTransaction.Attributes.Append(attributebalanceAfterg);

            var attributemodifiedBy = _xmlDb.CreateAttribute("modifiedBy");
            attributemodifiedBy.Value = modifiedBy;
            newTransaction.Attributes.Append(attributemodifiedBy);

            var transactionHistoryTable = _xmlDb.SelectSingleNode();

            transactionHistoryTable.AppendChild(newTransaction);

            _xmlDb.Save();
        }

        public IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory(int accountId)
        {

            var result = new List<HistoricalTransactionData>();

            foreach (XmlNode record in _xmlDb.HistoryTable)
            {

                if (accountId == int.Parse(record.Attributes.GetNamedItem("accountId").Value!))
                {
                    var transactionData = new HistoricalTransactionData();

                    transactionData.Type = decimal.Parse(record.Attributes.GetNamedItem("ammount").Value) > 0 ? "Deposit" : "Withdraw";

                    transactionData.CashAmount = decimal.Parse(record.Attributes.GetNamedItem("ammount").Value);

                    transactionData.BalanceBefore = decimal.Parse(record.Attributes.GetNamedItem("balanceAfter").Value) - decimal.Parse(record.Attributes.GetNamedItem("ammount").Value); ;

                    transactionData.BalanceAfter = decimal.Parse(record.Attributes.GetNamedItem("balanceAfter").Value);

                    transactionData.Datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(record.Attributes.GetNamedItem("dateTime").Value), TimeZoneInfo.Local);

                    transactionData.UserName = record.Attributes.GetNamedItem("modifiedBy").Value;

                    result.Add(transactionData);
                }
            }
            return result;
        }

    }
}
