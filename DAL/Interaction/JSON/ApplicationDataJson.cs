using DAL.Interfaces;
using DAL.JSONData;
using DAL.Entities;

namespace DAL.Interaction.JSON
{
    public class ApplicationDataJson : IApplicationData
    {
        private readonly JSONDb _jsonDb;

        public ApplicationDataJson()
        {
            _jsonDb = new JSONDb();
        }

        //public string GetApplicationPropertyByName(string propertyName)
        //{
        //    switch (propertyName)
        //    {
        //        case "allowedUsersCount":
        //            return _jsonDb.DbRoot.Application.AllowedUsersCount;
        //        case "actualUsersCount":
        //            return _jsonDb.DbRoot.Application.ActualUsersCount;
        //        case "delayMS":
        //            return _jsonDb.DbRoot.Application.DelayMS;
        //        default:
        //            return null;
        //    }
        //}

        public ApplicationData GetApplication()
        {
            return new ApplicationData()
            {
                AllowedUsersCount = int.Parse(_jsonDb.DbRoot.Application.AllowedUsersCount),
                ActualUsersCount = int.Parse(_jsonDb.DbRoot.Application.ActualUsersCount),
                DelayMS = int.Parse(_jsonDb.DbRoot.Application.DelayMS)
            };
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
