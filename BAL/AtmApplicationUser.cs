using BAL.Entities;

namespace BAL
{
    public class AtmApplicationUser
    {
        private readonly AtmDatabase _atmDB;
        public AtmApplicationUser(AtmDatabase atmDatabase) 
        { 
            _atmDB = atmDatabase; 
        }

        public ApplicationUser GetUser(string userName, string password)
        {
            var atmUserData = _atmDB.GetUser(userName, password);

            return new ApplicationUser 
            {
                Id = atmUserData.Id,
                Name = atmUserData.Name,
                FullName = atmUserData.FullName,
                CurrentAccountId = atmUserData.CurrentAccountId,
                IsActive = atmUserData.IsActive
            };
        }

        public decimal GetLimit(int userId)
        {
            return _atmDB.GetUserOverdraft(userId);
        }
    }
}
