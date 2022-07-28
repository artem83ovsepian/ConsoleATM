using DAL.Repositories;
using DAL.Interfaces;


namespace DAL.Logging
{
    public class TransactionLog
    {
        private readonly string fileName = "Logging\\TransactionLog.csv";
        private readonly IHistoricalTransactionDataRepository _historicalTransactionDataRepository;

        public TransactionLog()
        {
            _historicalTransactionDataRepository = new HistoricalTransactionDataRepository();            
        }

        public void WriteRecord(List<string> record)
        {
            if (RecreateFileIfNotExists() == 0)
                {
                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        writer.WriteLine(string.Join(",", record));
                    }
                }
        }

        private int RecreateFileIfNotExists()
        {
            if (!File.Exists(fileName))
                { 
                    File.Create(fileName).Dispose();

                    List<string> header = new() {"Id", "AccountId", "DateTime", "Ammount", "BalanceAfter", "ModifiedBy"};
                    WriteRecord(header);
                //await File.WriteAllLinesAsync(fileName, lines);
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
                    return 1;    
                }
            return 0;
        }
    }
}
