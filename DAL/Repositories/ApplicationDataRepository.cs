using DAL.Interfaces;
using DAL.XMLData;
using System.Xml.Linq;

namespace DAL.Repositories
{
    public class ApplicationDataRepository: IApplicationDataRepository
    {
        private readonly XMLDb _xmlDb;

        public ApplicationDataRepository()
        {
            _xmlDb = new XMLDb();
        }

        public string GetApplicationPropertyByName(string propertyName)
        {
            return (string)_xmlDb.Xelement.Descendants("Application").Take(1).ElementAt(0).Attribute(propertyName);
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
            _xmlDb.Xelement.Descendants("Application").Attributes("actualUsersCount").ElementAt(0).Value = ((int)_xmlDb.Xelement.Descendants("Application").Attributes("actualUsersCount").ElementAt(0) + incrementValue).ToString();
            _xmlDb.Save();
        }
    }
}
