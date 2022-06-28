using DAL;

namespace BAL
{
    public class AtmApplicationUser
    {
        private AtmDatabase atmDB;
        public AtmApplicationUser(AtmDatabase atmDatabase) { atmDB = atmDatabase; }

        public int Id;

        public string? Name;

        public string? FullName;

        public int CurrentAccountId;

        public int IsActive;

        public void Init(string userName, string password)
        {
            var atmUserData = atmDB.GetAtmUser(userName, password);

            Id = atmUserData.Id;

            Name = atmUserData.Name;

            FullName = atmUserData.FullName;

            CurrentAccountId = atmUserData.CurrentAccountId;

            IsActive = atmUserData.IsActive;            
        }
    }
}
