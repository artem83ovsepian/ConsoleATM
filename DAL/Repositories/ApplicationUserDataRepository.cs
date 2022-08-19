using DAL.Entities;
using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.Repositories
{
    public class ApplicationUserDataRepository
    {
        private readonly IApplicationUserData _applicationUserData;
        public ApplicationUserDataRepository(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _applicationUserData = new ApplicationUserDataXml(); break;
                case "json": _applicationUserData = new ApplicationUserDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
        }
        public ApplicationUserData GetUser(string userName, string password) 
        { 
            return _applicationUserData.GetUser(userName, password); 
        }

    }
}
