using BAL.Entities;
using DAL.Repositories;

namespace BAL.Repositories
{
    public class ApplicationUserAtmRepository
    {
        private readonly ApplicationUserDataRepository _applicationUserDataRepository;

        public ApplicationUserAtmRepository(string dbType)
        {
            _applicationUserDataRepository = new ApplicationUserDataRepository(dbType);
        }

        public ApplicationUserAtm GetUser(string userName, string password)
        {
            var atmUserData = _applicationUserDataRepository.GetUser(userName, password);

            return new ApplicationUserAtm 
            {
                Id = atmUserData.Id,
                Name = atmUserData.Name,
                FullName = atmUserData.FullName,
                IsActive = atmUserData.IsActive
            };
        }

    }
}
