using DAL.RepositoriesBase;

namespace DAL.Repositories
{
    public class ApplicationDataRepository : ApplicationDataRepositoryBase
    {
        public ApplicationDataRepository(string dbSource) : base(dbSource)
        {
        }
        public string GetApplicationPropertyByName(string propertyName)
        {
            return _applicationData.GetApplicationPropertyByName(propertyName);
        }
        public void IncrementUserCountWithOne()
        {
            _applicationData.IncrementUserCountWithOne();
        }
        public void DecrementUserCountWithOne()
        {
            _applicationData.DecrementUserCountWithOne();
        }
    }
}
