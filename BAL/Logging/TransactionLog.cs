using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories;
using DAL.Interfaces;
using BAL.Entities;

namespace BAL.Logging
{
    public class TransactionLog
    {
        private readonly string fileName = "Logging\\TransactionLog.csv";
        private readonly IHistoricalTransactionDataRepository _historicalTransactionDataRepository;

        public TransactionLog()
        {
            _historicalTransactionDataRepository = new HistoricalTransactionDataRepository();
        }

        public void RecreateFileIfNotExists()
        {
                if (!File.Exists(fileName))
                { 
                    File.Create(fileName).Dispose();

                List<String> header = new() {"Id", "AccountId", "DateTime", "Ammount", "BalanceAfter", "ModifiedBy"};

                using (var file = File.CreateText(fileName))
                    {
                    file.WriteLine(string.Join(",", header));

                    foreach (var historicalTransactionData in _historicalTransactionDataRepository.GetAccountTransactionHistory())
                        {
                        List<String> arrayString = new()
                            {
                            historicalTransactionData.Id.ToString(),
                            historicalTransactionData.AccountId.ToString(),
                            historicalTransactionData.LogDatetime.ToString(),
                            historicalTransactionData.CashAmount.ToString(),
                            historicalTransactionData.BalanceAfter.ToString(),
                            historicalTransactionData.UserName.ToString()
                        };
                            file.WriteLine(string.Join(",", arrayString));
                        }
                    }
                }
        }

        //<Transaction id="1" accountId="4" dateTime="6/20/2022 9:09:40 AM" ammount="-60.15" balanceAfter="0.00" modifiedBy="User One" />
    }
}
