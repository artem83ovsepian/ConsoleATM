using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IApplicationDataRepository
    {
        string GetApplicationPropertyByName(string propertyName);
        void IncrementUserCountWithOne();
        void DecrementUserCountWithOne();

    }
}
