using DAL.Interfaces;
using DAL.Interaction.JSON;
using DAL.Interaction.XML;

namespace DAL.Logging
{
    public class TransactionLog
    {
        private readonly string fileName = "Logging\\TransactionLog.csv";

        private readonly IHistoricalTransactionData _historicalTransactionData;
        public TransactionLog(string dbSource)
        {
            switch (dbSource)
            {
                case "xml": _historicalTransactionData = new HistoricalTransactionDataXml(); break;
                case "json": _historicalTransactionData = new HistoricalTransactionDataJson(); break;
                default: throw new ArgumentException(nameof(dbSource));
            }
        }

        public void WriteRecord(List<string> record)
        {
            if (FileExists(fileName) == 1)
            {
                FileWriteLine(record);
            }
        }

        private int FileExists(string logFileName)
        {
           if (!File.Exists(logFileName))
           {
               File.Create(logFileName).Dispose();

                FileWriteLine(new List<string>() { "Id", "AccountId", "DateTime", "Ammount", "BalanceAfter", "ModifiedBy" });

                foreach (var historicalTransactionData in _historicalTransactionData.GetAccountTransactionHistory())
               {
                    FileWriteLine(new List<string>()
                           {
                               historicalTransactionData.Id.ToString(),
                               historicalTransactionData.AccountId.ToString(),
                               historicalTransactionData.LogDatetime.ToString(),
                               historicalTransactionData.CashAmount.ToString(),
                               historicalTransactionData.BalanceAfter.ToString(),
                               historicalTransactionData.UserName.ToString()
                           });
               }
               //await File.WriteAllLinesAsync(fileName, lines);
               return 0;
           }
           return 1;
        }

        private void FileWriteLine(List<string> record)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(string.Join(",", record));
            }
        }
    }
}
