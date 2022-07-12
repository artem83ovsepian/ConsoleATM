using DAL;

namespace BAL
{
    public class AtmApplication
    {
        private readonly AtmDatabase _atmDB;

        public AtmApplication(AtmDatabase atmDatabase) 
        { 
            _atmDB = atmDatabase;
            Init(); 
        }

        public int AllowedUsersCount;

        public int ActualUsersCount;

        public int DelayMS;

        public void Init()
        {
            AllowedUsersCount = int.Parse(_atmDB.GetApplicationProperty("allowedUsersCount"));

            ActualUsersCount = int.Parse(_atmDB.GetApplicationProperty("actualUsersCount"));

            DelayMS = int.Parse(_atmDB.GetApplicationProperty("delayMS")); 
        }

        public void IncrementUserCountWithOne()
        {
            _atmDB.SetActualUsersCount(1);
        }

        public void DecrementUserCountWithOne()
        {
            _atmDB.SetActualUsersCount(-1);
        }
    }
}
