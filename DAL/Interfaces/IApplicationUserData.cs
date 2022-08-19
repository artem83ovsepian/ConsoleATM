using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IApplicationUserData
    {
        ApplicationUserData GetUser(string userName, string password);
    }
}
