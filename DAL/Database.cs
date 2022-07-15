using System.Xml;
using DAL.Entities;

namespace DAL
{
    public class Database
    {
 
        private readonly string dbFile = "XMLData\\ATMdb.xml";

        private readonly string appNodePathXML = "/dbo/Application";

        private readonly string userNodePathXML = "/dbo/UserTable/User";

        //private readonly string accountNodePathXML = "/dbo/AccountTable/Account";

        private readonly string transactionHistoryNodePathXML = "/dbo/TransactionHistoryTable/Transaction";

        private readonly string transactionHistoryTablePathXML = "/dbo/TransactionHistoryTable";

        private readonly string _datasource;

        private readonly XmlDocument _db;

        public Database(string dataSource)
        {
            _datasource = dataSource;

            _db = new XmlDocument();

            if (_datasource == "xml")
            {
                _db.Load(dbFile);
            }
            else
            {
                throw new Exception("Datasource not supported");
            }
        }

        public string GetApplicationProperty(string propertyName)
        {
            var applicationProperties = _db.SelectSingleNode(appNodePathXML);

            return applicationProperties.Attributes.GetNamedItem(propertyName).Value!;
        }

        public void SetActualUsersCount(int incrementValue)
        {
            var applicationProperties = _db.SelectSingleNode(appNodePathXML);

            applicationProperties.Attributes["actualUsersCount"].Value = (int.Parse(GetApplicationProperty("actualUsersCount")) + incrementValue).ToString();

            Save();
        }

        public ApplicationUserData GetUser(string userName, string password)
        {
            var userTable = _db.SelectNodes(userNodePathXML);

            var atmApplicationUserData = new ApplicationUserData();

            foreach (XmlNode user in userTable)
            {
                if ((user.Attributes.GetNamedItem("name").Value! == userName) && (user.Attributes.GetNamedItem("password").Value! == password))
                {
                    atmApplicationUserData.Name = user.Attributes.GetNamedItem("name").Value!;

                    atmApplicationUserData.Id = int.Parse(user.Attributes.GetNamedItem("id").Value!);

                    atmApplicationUserData.IsActive = int.Parse(user.Attributes.GetNamedItem("isActive").Value!);

                    atmApplicationUserData.FullName = user.Attributes.GetNamedItem("fullName").Value!;

                    break;
                }
            }
            return atmApplicationUserData;

        }

        public void SaveTransactionHistory (int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy)
        {
            var transactionHistoryTableRecord = _db.SelectNodes(transactionHistoryNodePathXML);

            var transactionTableMaxId = transactionHistoryTableRecord.Count + 1;

            var newTransaction = _db.CreateElement("Transaction");

            var attributeId = _db.CreateAttribute("id");
            attributeId.Value = transactionTableMaxId.ToString();
            newTransaction.Attributes.Append(attributeId);

            var attributeaccountId = _db.CreateAttribute("accountId");
            attributeaccountId.Value = accountId.ToString();
            newTransaction.Attributes.Append(attributeaccountId);

            var attributeaccountdatetime = _db.CreateAttribute("dateTime");
            attributeaccountdatetime.Value = TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local).ToString();            
            newTransaction.Attributes.Append(attributeaccountdatetime);

            var attributeammount = _db.CreateAttribute("ammount");
            attributeammount.Value = ammount.ToString("0.00");
            newTransaction.Attributes.Append(attributeammount);

            var attributebalanceAfterg = _db.CreateAttribute("balanceAfter");
            attributebalanceAfterg.Value = balanceAfter.ToString("0.00");
            newTransaction.Attributes.Append(attributebalanceAfterg);

            var attributemodifiedBy = _db.CreateAttribute("modifiedBy");
            attributemodifiedBy.Value = modifiedBy;
            newTransaction.Attributes.Append(attributemodifiedBy);

            var transactionHistoryTable = _db.SelectSingleNode(transactionHistoryTablePathXML);

            transactionHistoryTable.AppendChild(newTransaction);

            Save();
        }

        public IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory(int accountId)
        {

            var result = new List<HistoricalTransactionData>();

            var historyTable = _db.SelectNodes(transactionHistoryNodePathXML);

            foreach (XmlNode record in historyTable)
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

        public void IncrementUserCountWithOne()
        {
            SetActualUsersCount(1);
        }

        public void DecrementUserCountWithOne()
        {
            SetActualUsersCount(-1);
        }

        private void Save()
        {
            _db.Save(dbFile);
        }

    }
}
