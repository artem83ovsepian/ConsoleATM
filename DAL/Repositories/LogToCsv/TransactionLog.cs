using DAL.RepositoriesBase.Logging.Csv;

namespace DAL.Repositories.Logging.Csv
{
    public class TransactionLog : TransactionLogBase
    {
        public TransactionLog(string dbSource) : base(dbSource)
        {
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
