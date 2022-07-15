using DAL.Entities;

namespace DAL.Interfaces
{
    interface IApplicationUserDataRepository
    {
        ApplicationUserData GetUser(string userName, string password);
    }
}
