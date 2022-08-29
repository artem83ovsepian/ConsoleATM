using DAL.Interfaces;
using DAL.XMLData;
using System.Xml.Linq;
using DAL.Entities;

namespace DAL.Interaction.XML
{
    public class ApplicationDataXml: IApplicationData
    {
        private readonly XMLDb _xmlDb;

        public ApplicationDataXml()
        {
            _xmlDb = new XMLDb();
        }
        public ApplicationData GetApplication()
        {
            return new ApplicationData()
            {
                AllowedUsersCount = (int)_xmlDb.Xelement.Descendants("Application").Take(1).ElementAt(0).Attribute("allowedUsersCount"),
                ActualUsersCount = (int)_xmlDb.Xelement.Descendants("Application").Take(1).ElementAt(0).Attribute("actualUsersCount"),
                DelayMS = (int)_xmlDb.Xelement.Descendants("Application").Take(1).ElementAt(0).Attribute("delayMS")
            };
        }

        //public string GetApplicationPropertyByName(string propertyName)
        //{
        //    return (string)_xmlDb.Xelement.Descendants("Application").Take(1).ElementAt(0).Attribute(propertyName);
        //}

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
