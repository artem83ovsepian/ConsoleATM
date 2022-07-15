using DAL.Entities;

namespace DAL.Interfaces
{
    interface IApplicationDataRepository
    {
        string GetApplicationPropertyByName(string propertyName);
        void IncrementUserCountWithOne();
        void DecrementUserCountWithOne();

    }
}
