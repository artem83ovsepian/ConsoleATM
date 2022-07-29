using BAL.Entities;
using DAL.Repositories;
using DAL.Interfaces;

namespace BAL.Repositories
{
    public class ApplicationUserAtmRepository
    {
        private readonly IApplicationUserDataRepository _applicationUserDataRepository;

        public ApplicationUserAtmRepository(string dbType)
        {
            _applicationUserDataRepository = new XmlApplicationUserDataRepository();

            switch (dbType)
            {
                case "xml": _applicationUserDataRepository = new XmlApplicationUserDataRepository(); break;
                case "json": _applicationUserDataRepository = new JsonApplicationUserDataRepository(); break;
                default: throw new ArgumentException(nameof(dbType));
            }
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
