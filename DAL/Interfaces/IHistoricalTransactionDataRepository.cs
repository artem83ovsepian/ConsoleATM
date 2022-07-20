﻿using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IHistoricalTransactionDataRepository
    {
        IEnumerable<HistoricalTransactionData> GetAccountTransactionHistory(int accountId);
        void SaveTransactionHistory(int accountId, DateTime dateTime, decimal ammount, decimal balanceAfter, string modifiedBy);
    }
}