using DAL;
using BAL.Entities;

namespace BAL.Repositories
{
    public class AtmApplicationRepository
    {
        private readonly AtmDatabase _atmDB;

        public AtmApplicationRepository(AtmDatabase atmDatabase) 
        { 
            _atmDB = atmDatabase;
        }

        public ApplicationAtm GetApplication()
        {
            return new ApplicationAtm
            {
                AllowedUsersCount = int.Parse(_atmDB.GetApplicationProperty("allowedUsersCount")),
                ActualUsersCount = int.Parse(_atmDB.GetApplicationProperty("actualUsersCount")),
                DelayMS = int.Parse(_atmDB.GetApplicationProperty("delayMS"))
            };
        }
    }
}
