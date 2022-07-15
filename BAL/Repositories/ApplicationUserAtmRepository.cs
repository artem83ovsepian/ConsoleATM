using BAL.Entities;
using DAL.Repositories;

namespace BAL.Repositories
{
    public class ApplicationUserAtmRepository
    {
        private readonly ApplicationUserDataRepository _applicationUserDataRepository = new ApplicationUserDataRepository();

        public ApplicationUserAtm GetUser(string userName, string password)
        {
            var atmUserData = _applicationUserDataRepository.GetUser(userName, password);

            return new ApplicationUserAtm 
            {
                Id = atmUserData.Id,
                Name = atmUserData.Name,
                FullName = atmUserData.FullName,
                CurrentAccountId = atmUserData.CurrentAccountId,
                IsActive = atmUserData.IsActive
            };
        }

    }
}
