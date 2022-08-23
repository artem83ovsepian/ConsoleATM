using DAL.Entities;
using DAL.RepositoriesBase;


namespace DAL.Repositories
{
    public class ApplicationUserDataRepository : ApplicationUserDataRepositoryBase
    {
        public ApplicationUserDataRepository(string dbSource) : base(dbSource)
        {
        }
        public ApplicationUserData GetUser(string userName, string password) 
        { 
            return _applicationUserData.GetUser(userName, password); 
        }
    }
}
