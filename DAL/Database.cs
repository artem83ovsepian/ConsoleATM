using System.Xml;

namespace DAL
{
    public class Database
    {
        public Database(string dataSource) { Datasource = dataSource; Init(); }

        private readonly string Datasource;

        private readonly string dbFile = "XMLData\\ATMdb.xml";

        private readonly string appNodePathXML = "/dbo/Application";

        private readonly string userNodePathXML = "/dbo/UserTable/User";

        private readonly string accountNodePathXML = "/dbo/AccountTable/Account";

        private readonly string transactionHistoryNodePathXML = "/dbo/TransactionHistoryTable/Transaction";

        private readonly string transactionHistoryTablePathXML = "/dbo/TransactionHistoryTable";

        private XmlDocument db = new();

        private AccountData atmAccountData = new();

        public void Init()
        {
            if (Datasource == "xml")
            {
                db.Load(dbFile);
            }
            else
            {
                throw new Exception("Datasource not supported");
            }

        }

        private void Save()
        {
            db.Save(dbFile);
        }

        public string GetApplicationProperty(string propertyName)
        {
            var applicationProperties = db.SelectSingleNode(appNodePathXML);

            return applicationProperties.Attributes.GetNamedItem(propertyName).Value!;
        }

        public void SetActualUsersCount(int incrementValue)
        {
            var applicationProperties = db.SelectSingleNode(appNodePathXML);

            applicationProperties.Attributes["actualUsersCount"].Value = (int.Parse(GetApplicationProperty("actualUsersCount")) + incrementValue).ToString();

            Save();
        }

        public ApplicationUserData GetAtmUser(string userName, string password)
        {
            var userTable = db.SelectNodes(userNodePathXML);

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

        public AccountData GetAtmAccount(int userId)
        {
            var accountTable = db.SelectNodes(accountNodePathXML);            

            foreach (XmlNode account in accountTable)
            {
                if ((int.Parse(account.Attributes.GetNamedItem("userId").Value!) == userId) && (int.Parse(account.Attributes.GetNamedItem("isActive").Value!) == 1))
                {
                    atmAccountData.Id = int.Parse(account.Attributes.GetNamedItem("id").Value!);

                    atmAccountData.UserId = int.Parse(account.Attributes.GetNamedItem("userId").Value!);

                    atmAccountData.Balance = decimal.Parse(account.Attributes.GetNamedItem("balance").Value!);

                    atmAccountData.IsActive = int.Parse(account.Attributes.GetNamedItem("isActive").Value!);

                    atmAccountData.OverDraft = decimal.Parse(account.Attributes.GetNamedItem("overdraft").Value!);

                    break;
                }
            }

            return atmAccountData;
        }

        public void SaveAtmAccount(int accountId, decimal balance)
        {
            var accountTable = db.SelectNodes(accountNodePathXML);

            foreach (XmlNode account in accountTable)
            {
                var Id = int.Parse(account.Attributes.GetNamedItem("id").Value!);

                if (Id == accountId)
                {
                    account.Attributes.GetNamedItem("balance").Value = balance.ToString();
                }
            }

            Save();
        }

        public void SaveTransactionHistory (int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy)
        {
            var transactionHistoryTableRecord = db.SelectNodes(transactionHistoryNodePathXML);

            var transactionTableMaxId = transactionHistoryTableRecord.Count + 1;

            var newTransaction = db.CreateElement("Transaction");

            var attributeId = db.CreateAttribute("id");
            attributeId.Value = transactionTableMaxId.ToString();
            newTransaction.Attributes.Append(attributeId);

            var attributeaccountId = db.CreateAttribute("accountId");
            attributeaccountId.Value = accountId.ToString();
            newTransaction.Attributes.Append(attributeaccountId);

            var attributeaccountdatetime = db.CreateAttribute("dateTime");
            attributeaccountdatetime.Value = TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local).ToString();            
            newTransaction.Attributes.Append(attributeaccountdatetime);

            var attributeammount = db.CreateAttribute("ammount");
            attributeammount.Value = ammount.ToString("0.00");
            newTransaction.Attributes.Append(attributeammount);

            var attributebalanceAfterg = db.CreateAttribute("balanceAfter");
            attributebalanceAfterg.Value = balanceAfter.ToString("0.00");
            newTransaction.Attributes.Append(attributebalanceAfterg);

            var attributemodifiedBy = db.CreateAttribute("modifiedBy");
            attributemodifiedBy.Value = modifiedBy;
            newTransaction.Attributes.Append(attributemodifiedBy);

            var transactionHistoryTable = db.SelectSingleNode(transactionHistoryTablePathXML);

            transactionHistoryTable.AppendChild(newTransaction);

            Save();
        }

        public IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory(int accountId)
        {

            var result = new List<HistoricalTransactionData>();

            var historyTable = db.SelectNodes(transactionHistoryNodePathXML);

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


    }
}
