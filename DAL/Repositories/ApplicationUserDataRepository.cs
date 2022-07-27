using DAL.Entities;
using DAL.Interfaces;
using DAL.XMLData;

namespace DAL.Repositories
{
    public class ApplicationUserDataRepository: IApplicationUserDataRepository
    {
        private readonly XMLDb _xmlDb;

        public ApplicationUserDataRepository()
        {
            _xmlDb = new XMLDb();
        }
        public ApplicationUserData GetUser(string userName, string password)
        {
            var userRecord = _xmlDb.Xelement.Descendants("User").Where(m => (string)m.Attribute("name") == userName && (string)m.Attribute("password") == password).Take(1).ElementAt(0);

            return new ApplicationUserData()
            {
                Name = (string)userRecord.Attribute("name"),
                Id = (int)userRecord.Attribute("id"),
                IsActive = (int)userRecord.Attribute("isActive"),
                FullName = (string)userRecord.Attribute("fullName")
            };
        }
    }
}
