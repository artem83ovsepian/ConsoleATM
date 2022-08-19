using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IApplicationData
    {
        string GetApplicationPropertyByName(string propertyName);
        void IncrementUserCountWithOne();
        void DecrementUserCountWithOne();

    }
}
