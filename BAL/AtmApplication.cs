using DAL;
using BAL.Entities;

namespace BAL
{
    public class AtmApplication
    {
        private readonly AtmDatabase _atmDB;

        public AtmApplication(AtmDatabase atmDatabase) 
        { 
            _atmDB = atmDatabase;
        }

        public Application GetApplication()
        {
            return new Application
            {
                AllowedUsersCount = int.Parse(_atmDB.GetApplicationProperty("allowedUsersCount")),
                ActualUsersCount = int.Parse(_atmDB.GetApplicationProperty("actualUsersCount")),
                DelayMS = int.Parse(_atmDB.GetApplicationProperty("delayMS"))
            };
        }
    }
}
