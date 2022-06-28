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
            XmlNode? applicationProperties = db.SelectSingleNode(appNodePathXML);

            return applicationProperties.Attributes?.GetNamedItem(propertyName)?.Value!;
        }

        public void SetActualUsersCount(int incrementValue)
        {
            XmlNode applicationProperties = db.SelectSingleNode(appNodePathXML);

            applicationProperties.Attributes["actualUsersCount"].Value = (int.Parse(GetApplicationProperty("actualUsersCount")) + incrementValue).ToString();

            Save();
        }

        public ApplicationUserData GetAtmUser(string userName, string password)
        {
            XmlNodeList userTable = db.SelectNodes(userNodePathXML);

            ApplicationUserData atmApplicationUserData = new();

            foreach (XmlNode user in userTable)
            {
                int dbUserId = int.Parse(user.Attributes?.GetNamedItem("id")?.Value!);

                int dbUserIsActive = int.Parse(user.Attributes?.GetNamedItem("isActive")?.Value!);

                string dbUserName = user.Attributes?.GetNamedItem("name")?.Value!;

                string dbUserPassword = user.Attributes?.GetNamedItem("password")?.Value!;

                string dbUserFulllName = user.Attributes?.GetNamedItem("fullName")?.Value!;

                if (dbUserName == userName && dbUserPassword == password)
                {
                    atmApplicationUserData.Name = dbUserName;

                    atmApplicationUserData.Id = dbUserId;

                    atmApplicationUserData.IsActive = dbUserIsActive;

                    atmApplicationUserData.FullName = dbUserFulllName;

                    break;
                }
            }
            return atmApplicationUserData;

        }

        public AccountData GetAtmAccount(int userId)
        {
            XmlNodeList accountTable = db.SelectNodes(accountNodePathXML);            

            foreach (XmlNode account in accountTable)
            {
                int accountUserId = int.Parse(account.Attributes?.GetNamedItem("userId")?.Value!);

                int accountIsActive = int.Parse(account.Attributes?.GetNamedItem("isActive")?.Value!);

                int accountId = int.Parse(account.Attributes?.GetNamedItem("id")?.Value!);

                decimal accountBalance = decimal.Parse(account.Attributes?.GetNamedItem("balance")?.Value!);

                if (accountUserId == userId && accountIsActive == 1)
                {
                    atmAccountData.Id = accountId;

                    atmAccountData.UserId = accountUserId;

                    atmAccountData.Balance = accountBalance;

                    atmAccountData.IsActive = accountIsActive;

                    break;
                }
            }

            return atmAccountData;
        }

        public void SaveAtmAccount(int accountId, decimal balance)
        {
            XmlNodeList accountTable = db.SelectNodes(accountNodePathXML);

            foreach (XmlNode account in accountTable)
            {
                int Id = int.Parse(account.Attributes?.GetNamedItem("id")?.Value!);

                if (Id == accountId)
                {
                    account.Attributes.GetNamedItem("balance").Value = balance.ToString();
                }
            }

            Save();
        }

        public void SaveTransactionHistory (int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy)
        {
            XmlNodeList? transactionHistoryTableRecord = db.SelectNodes(transactionHistoryNodePathXML);

            int transactionTableMaxId = transactionHistoryTableRecord.Count + 1;

            XmlElement newTransaction = db.CreateElement("Transaction");

            XmlAttribute attributeId = db.CreateAttribute("id");
            attributeId.Value = transactionTableMaxId.ToString();
            newTransaction.Attributes.Append(attributeId);

            XmlAttribute attributeaccountId = db.CreateAttribute("accountId");
            attributeaccountId.Value = accountId.ToString();
            newTransaction.Attributes.Append(attributeaccountId);

            XmlAttribute attributeaccountdatetime = db.CreateAttribute("dateTime");
            attributeaccountdatetime.Value = dateTime.ToString();
            newTransaction.Attributes.Append(attributeaccountdatetime);

            XmlAttribute attributeammount = db.CreateAttribute("ammount");
            attributeammount.Value = ammount.ToString("0.00");
            newTransaction.Attributes.Append(attributeammount);

            XmlAttribute attributebalanceAfterg = db.CreateAttribute("balanceAfter");
            attributebalanceAfterg.Value = balanceAfter.ToString("0.00");
            newTransaction.Attributes.Append(attributebalanceAfterg);

            XmlAttribute attributemodifiedBy = db.CreateAttribute("modifiedBy");
            attributemodifiedBy.Value = modifiedBy;
            newTransaction.Attributes.Append(attributemodifiedBy);

            XmlNode? transactionHistoryTable = db.SelectSingleNode(transactionHistoryTablePathXML);

            transactionHistoryTable.AppendChild(newTransaction);

            Save();
        }

        public List<HistoricalTransactionData> GetAccountTransactionHistory(int accountId)
        {

            List<HistoricalTransactionData> result = new();

            XmlNodeList? historyTable = db.SelectNodes(transactionHistoryNodePathXML);

            foreach (XmlNode? record in historyTable)
            {

                if (accountId == int.Parse(record.Attributes?.GetNamedItem("accountId")?.Value!))
                {
                    HistoricalTransactionData transactionData = new();

                    transactionData.Type = decimal.Parse(record.Attributes.GetNamedItem("ammount").Value) > 0 ? "Deposit" : "Withdraw";

                    transactionData.CashAmount = decimal.Parse(record.Attributes.GetNamedItem("ammount").Value).ToString("C");

                    decimal balanceBefore = decimal.Parse(record.Attributes.GetNamedItem("balanceAfter").Value) - decimal.Parse(record.Attributes.GetNamedItem("ammount").Value);

                    transactionData.BalanceBefore = balanceBefore.ToString("C");

                    transactionData.BalanceAfter = decimal.Parse(record.Attributes.GetNamedItem("balanceAfter").Value).ToString("C"); 

                    transactionData.Datetime = record.Attributes?.GetNamedItem("dateTime")?.Value!;

                    transactionData.UserName = record.Attributes.GetNamedItem("modifiedBy").Value;

                    result.Add(transactionData);
                }
            }
            return result;
        }


    }
}
