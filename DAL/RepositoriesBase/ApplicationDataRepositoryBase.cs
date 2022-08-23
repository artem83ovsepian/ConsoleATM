using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.RepositoriesBase
{
    public abstract class ApplicationDataRepositoryBase
    {
        protected readonly IApplicationData _applicationData;
        public ApplicationDataRepositoryBase(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _applicationData = new ApplicationDataXml(); break;
                case "json": _applicationData = new ApplicationDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
        }
    }
}