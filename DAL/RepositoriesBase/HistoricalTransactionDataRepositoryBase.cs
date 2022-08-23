using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.RepositoriesBase
{
    public abstract class HistoricalTransactionDataRepositoryBase
    {
        protected readonly IHistoricalTransactionData _historicalTransactionData;
        protected string _dbSource;

        public HistoricalTransactionDataRepositoryBase(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _historicalTransactionData = new HistoricalTransactionDataXml(); break;
                case "json": _historicalTransactionData = new HistoricalTransactionDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
            _dbSource = dbSource;
        }
    }
}