using DAL.Interfaces;
using DAL.XMLData;

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
            return _xmlDb.ApplicationProperties.Attributes.GetNamedItem(propertyName).Value!;
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
            _xmlDb.ApplicationProperties.Attributes["actualUsersCount"].Value = (int.Parse(GetApplicationPropertyByName("actualUsersCount")) + incrementValue).ToString();
            _xmlDb.Save();
        }

    }
}
