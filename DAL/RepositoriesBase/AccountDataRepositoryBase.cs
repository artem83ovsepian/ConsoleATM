using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.RepositoriesBase
{
    public abstract class AccountDataRepositoryBase
    {
        protected readonly IAccountData _accountData;
        public AccountDataRepositoryBase(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _accountData = new AccountDataXml(); break;
                case "json": _accountData = new AccountDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
        }
    }
}