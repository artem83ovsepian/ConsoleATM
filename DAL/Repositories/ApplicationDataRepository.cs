using DAL.RepositoriesBase;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ApplicationDataRepository : ApplicationDataRepositoryBase
    {
        public ApplicationDataRepository(string dbSource) : base(dbSource)
        {
        }
        public ApplicationData GetApplication()
        {
            return _applicationData.GetApplication();
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
