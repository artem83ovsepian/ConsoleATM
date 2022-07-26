using System.Xml;

namespace DAL.XMLData
{
    public class XMLDb
    {
        private readonly string fileName = "XMLData\\ATMdb.xml";
        private readonly string appNodePathXML = "/dbo/Application";
        private readonly string userNodePathXML = "/dbo/UserTable/User";
        private readonly string accountNodePathXML = "/dbo/AccountTable/Account";
        private readonly string transactionHistoryNodePathXML = "/dbo/TransactionHistoryTable/Transaction";
        private readonly string transactionHistoryTablePathXML = "/dbo/TransactionHistoryTable";

        private XmlDocument _xmlDocument;

        public XmlNodeList AccountTable { get; set; }
        public XmlNodeList UserTable { get; set; }
        public XmlNodeList HistoryTable;
        public XmlNode ApplicationProperties { get; set; }

        public XMLDb()
        {
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(fileName);
            ApplicationProperties = _xmlDocument.SelectSingleNode(appNodePathXML);
            AccountTable = _xmlDocument.SelectNodes(accountNodePathXML);
            UserTable = _xmlDocument.SelectNodes(userNodePathXML);
            HistoryTable = _xmlDocument.SelectNodes(transactionHistoryNodePathXML);
        }

        public void Save()
        {
            _xmlDocument.Save(fileName);
        }

        public XmlElement CreateElement(string elementName)
        {
            return _xmlDocument.CreateElement(elementName);
        }

        public XmlAttribute CreateAttribute(string attributeName)
        {
            return _xmlDocument.CreateAttribute(attributeName);

        }

        public XmlNode SelectSingleNode()
        {
            return _xmlDocument.SelectSingleNode(transactionHistoryTablePathXML);
        }

    }
}
