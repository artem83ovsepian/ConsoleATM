using DAL.Entities;
using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.Repositories
{
    public class ApplicationDataRepository
    {
        private readonly IApplicationData _applicationData;
        public ApplicationDataRepository(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _applicationData = new ApplicationDataXml(); break;
                case "json": _applicationData = new ApplicationDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
        }
        public string GetApplicationPropertyByName(string propertyName)
        {
            return _applicationData.GetApplicationPropertyByName(propertyName);
        }
        public void IncrementUserCountWithOne()
        {
            _applicationData.IncrementUserCountWithOne();
        }
        public void DecrementUserCountWithOne()
        {
            _applicationData.DecrementUserCountWithOne();
        }
    }
}
