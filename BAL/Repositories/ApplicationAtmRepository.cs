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
            return new ApplicationAtm
            {
                AllowedUsersCount = int.Parse(_applicationDataRepository.GetApplicationPropertyByName("allowedUsersCount")),
                ActualUsersCount = int.Parse(_applicationDataRepository.GetApplicationPropertyByName("actualUsersCount")),
                DelayMS = int.Parse(_applicationDataRepository.GetApplicationPropertyByName("delayMS"))
            };
        }
    }
}
