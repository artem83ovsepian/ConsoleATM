using DAL;

namespace BAL
{
    public class AtmApplicationUser
    {
        private readonly AtmDatabase _atmDB;
        public AtmApplicationUser(AtmDatabase atmDatabase) 
        { 
            _atmDB = atmDatabase; 
        }

        public int Id;

        public string Name;

        public string FullName;

        public int CurrentAccountId;

        public int IsActive;

        public void Init(string userName, string password)
        {
            var atmUserData = _atmDB.GetAtmUser(userName, password);

            Id = atmUserData.Id;

            Name = atmUserData.Name;

            FullName = atmUserData.FullName;

            CurrentAccountId = atmUserData.CurrentAccountId;

            IsActive = atmUserData.IsActive;            
        }
    }
}
