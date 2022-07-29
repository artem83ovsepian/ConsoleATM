using BAL.Entities;
using DAL.Repositories;
using DAL.Interfaces;

namespace BAL.Repositories
{
    public class ApplicationAtmRepository
    {
        private readonly IApplicationDataRepository _applicationDataRepository;

        public ApplicationAtmRepository(string dbType)
        {            
            switch (dbType)
            {
                case "xml": _applicationDataRepository = new XmlApplicationDataRepository(); break;
                case "json": _applicationDataRepository = new JsonApplicationDataRepository(); break;
                    default: throw new ArgumentException(nameof(dbType));
            }
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
