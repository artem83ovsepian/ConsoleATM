using DAL;

namespace BAL
{
    public class AtmApplication
    {
        public AtmApplication(AtmDatabase atmDatabase) { atmDB = atmDatabase; Init(); }

        public int AllowedUsersCount;

        public int ActualUsersCount;

        public int DelayMS;

        private AtmDatabase atmDB;

        public void Init()
        {
            AllowedUsersCount = int.Parse(atmDB.GetApplicationProperty("allowedUsersCount"));

            ActualUsersCount = int.Parse(atmDB.GetApplicationProperty("actualUsersCount"));

            DelayMS = int.Parse(atmDB.GetApplicationProperty("delayMS")); 
        }

        public void IncrementUserCountWithOne()
        {
            atmDB.SetActualUsersCount(1);
        }

        public void DecrementUserCountWithOne()
        {
            atmDB.SetActualUsersCount(-1);
        }
    }
}
