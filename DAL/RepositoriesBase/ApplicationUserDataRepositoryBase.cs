using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.RepositoriesBase
{
    public abstract class ApplicationUserDataRepositoryBase
    {
        protected readonly IApplicationUserData _applicationUserData;
        public ApplicationUserDataRepositoryBase(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _applicationUserData = new ApplicationUserDataXml(); break;
                case "json": _applicationUserData = new ApplicationUserDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
        }
    }
}