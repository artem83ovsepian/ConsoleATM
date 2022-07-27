using BAL.Entities;
using DAL.Repositories;
using DAL.Interfaces;

namespace BAL.Repositories
{
    public class ApplicationAtmRepository
    {
        private readonly IApplicationDataRepository _applicationDataRepository;

        public ApplicationAtmRepository()
        {
            _applicationDataRepository = new ApplicationDataRepository();
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
