using System.Xml.Linq;

namespace DAL.XMLData
{
    public class XMLDb
    {

        private readonly string fileName = "XMLData\\ATMdb.xml";

        public XElement Xelement;

        public XMLDb()
        {
            Xelement = XElement.Load(fileName);
        }

        public void Save()
        {
            Xelement.Save(fileName);
        }

    }
}
