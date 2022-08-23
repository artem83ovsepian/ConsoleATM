using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.RepositoriesBase.Logging.Csv
{
    public abstract class TransactionLogBase
    {
        protected readonly string fileName = "Logging\\TransactionLog.csv";
        protected readonly IHistoricalTransactionData _historicalTransactionData;
        public TransactionLogBase(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _historicalTransactionData = new HistoricalTransactionDataXml(); break;
                case "json": _historicalTransactionData = new HistoricalTransactionDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
        }
    }
}