using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IApplicationUserDataRepository
    {
        ApplicationUserData GetUser(string userName, string password);
    }
}
