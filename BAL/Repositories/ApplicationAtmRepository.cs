using BAL.Entities;
using DAL.Repositories;

namespace BAL.Repositories
{
    public class ApplicationAtmRepository
    {
        private readonly ApplicationDataRepository _applicationDataRepository;

        public ApplicationAtmRepository(string dbType)
        {
            _applicationDataRepository = new ApplicationDataRepository(dbType);
        }

        public void IncrementUserCountWithOne()
        {
            _applicationDataRepository.IncrementUserCountWithOne();
        }

        public void DecrementUserCountWithOne()
        {
            _applicationDataRepository.DecrementUserCountWithOne();
        }

        public ApplicationAtm GetApplication()
        {
            var Application = _applicationDataRepository.GetApplication();
            return new ApplicationAtm
            {
                AllowedUsersCount = Application.ActualUsersCount,
                ActualUsersCount = Application.ActualUsersCount,
                DelayMS = Application.DelayMS
            };
    }
    }
}
