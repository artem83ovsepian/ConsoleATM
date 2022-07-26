using System.Xml;
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

            var applicationUserData = new ApplicationUserData();

            foreach (XmlNode user in _xmlDb.UserTable)
            {
                if ((user.Attributes.GetNamedItem("name").Value! == userName) && (user.Attributes.GetNamedItem("password").Value! == password))
                {
                    applicationUserData.Name = user.Attributes.GetNamedItem("name").Value!;
                    applicationUserData.Id = int.Parse(user.Attributes.GetNamedItem("id").Value!);
                    applicationUserData.IsActive = int.Parse(user.Attributes.GetNamedItem("isActive").Value!);
                    applicationUserData.FullName = user.Attributes.GetNamedItem("fullName").Value!;

                    break;
                }
            }
            return applicationUserData;
        }
    }
}
