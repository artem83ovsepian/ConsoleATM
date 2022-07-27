using BAL.Entities;
using DAL.Repositories;
using DAL.Interfaces;

namespace BAL.Repositories
{
    public class ApplicationUserAtmRepository
    {
        private readonly IApplicationUserDataRepository _applicationUserDataRepository;

        public ApplicationUserAtmRepository()
        {
            _applicationUserDataRepository = new ApplicationUserDataRepository();
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
