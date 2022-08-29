using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IApplicationData
    {
        ApplicationData GetApplication();
        //string GetApplicationPropertyByName(string propertyName);
        void IncrementUserCountWithOne();
        void DecrementUserCountWithOne();

    }
}
