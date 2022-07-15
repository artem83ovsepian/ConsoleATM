using BAL.Entities;

namespace BAL.Repositories
{
    public class AtmApplicationUser
    {
        private readonly AtmDatabase _atmDB;
        public AtmApplicationUser(AtmDatabase atmDatabase) 
        { 
            _atmDB = atmDatabase; 
        }

        public ApplicationUserAtm GetUser(string userName, string password)
        {
            var atmUserData = _atmDB.GetUser(userName, password);

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
