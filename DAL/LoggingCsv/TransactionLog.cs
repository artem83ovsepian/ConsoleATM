using DAL.Repositories;
using DAL.Interfaces;


namespace DAL.Logging
{
    public class TransactionLog
    {
        private readonly string fileName = "Logging\\TransactionLog.csv";

        private readonly IHistoricalTransactionDataRepository _historicalTransactionDataRepository;
        private int FileExists
        {
            get
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Dispose();

                    WriteRecord(new List<string>() { "Id", "AccountId", "DateTime", "Ammount", "BalanceAfter", "ModifiedBy" });

                    foreach (var historicalTransactionData in _historicalTransactionDataRepository.GetAccountTransactionHistory())
                    {
                        WriteRecord(new List<string>()
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
        }

        public TransactionLog()
        {
            _historicalTransactionDataRepository = new XmlHistoricalTransactionDataRepository();            
        }

        public void WriteRecord(List<string> record)
        {
            if (FileExists == 1)
                {
                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        writer.WriteLine(string.Join(",", record));
                    }
                }
        }

    }
}
