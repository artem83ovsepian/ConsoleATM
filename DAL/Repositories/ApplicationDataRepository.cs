using System.Xml;
using DAL.Entities;
using DAL.Interfaces;
using DAL.XMLData;

namespace DAL.Repositories
{
    public class ApplicationDataRepository: IApplicationDataRepository
    {

        private readonly XMLDb _xmlDb;
        private readonly XmlDocument _xmlDocument;
        private readonly XmlNode _applicationProperties;

        public ApplicationDataRepository()
        {
            _xmlDb = new XMLDb();
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(_xmlDb.FileName);
            _applicationProperties = _xmlDocument.SelectSingleNode(_xmlDb.AppNodePathXML);
        }

        public string GetApplicationPropertyByName(string propertyName)
        {
            return _applicationProperties.Attributes.GetNamedItem(propertyName).Value!;
        }

        public void IncrementUserCountWithOne()
        {
            SetActualUsersCount(1);
        }

        public void DecrementUserCountWithOne()
        {
            SetActualUsersCount(-1);
        }

        private void SetActualUsersCount(int incrementValue)
        {
            _applicationProperties.Attributes["actualUsersCount"].Value = (int.Parse(GetApplicationPropertyByName("actualUsersCount")) + incrementValue).ToString();

            _xmlDocument.Save(_xmlDb.FileName);
        }

    }
}
