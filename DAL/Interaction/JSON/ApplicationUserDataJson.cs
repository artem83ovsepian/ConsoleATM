using DAL.JSONData;
using DAL.Interfaces;
using DAL.Entities;


namespace DAL.Interaction.JSON
{
    public class ApplicationUserDataJson : IApplicationUserData
    {
        private readonly JSONDb _jsonDb;

        public ApplicationUserDataJson()
        {
            _jsonDb = new JSONDb();
        }
        public ApplicationUserData GetUser(string userName, string password)
        {
            var userRecord = _jsonDb.DbRoot.User.Where(p => (p.Name.ToLower() == userName.ToLower()) && (p.Password == password)).LastOrDefault();

            return userRecord==null ? new ApplicationUserData() : new ApplicationUserData()
            {
                Name = userRecord.Name,
                Id = int.Parse(userRecord.Id),
                IsActive = int.Parse(userRecord.IsActive),
                FullName = userRecord.FullName
            };
        }
    }
}
