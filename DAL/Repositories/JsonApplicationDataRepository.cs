using DAL.Interfaces;
using DAL.JSONData;

namespace DAL.Repositories
{
    public class JsonApplicationDataRepository : IApplicationDataRepository
    {
        private readonly JSONDb _jsonDb;

        public JsonApplicationDataRepository()
        {
            _jsonDb = new JSONDb();
        }

        public string GetApplicationPropertyByName(string propertyName)
        {
            switch (propertyName)
            {
                case "allowedUsersCount":
                    return _jsonDb.DbRoot.Application.AllowedUsersCount;
                case "actualUsersCount":
                    return _jsonDb.DbRoot.Application.ActualUsersCount;
                case "delayMS":
                    return _jsonDb.DbRoot.Application.DelayMS;
                default:
                    return null;
            }
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
            _jsonDb.DbRoot.Application.ActualUsersCount = (int.Parse(_jsonDb.DbRoot.Application.ActualUsersCount) + 1).ToString();
            _jsonDb.Save();
        }
    }
}
