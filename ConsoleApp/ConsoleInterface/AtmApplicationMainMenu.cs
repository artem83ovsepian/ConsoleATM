namespace BAL
{
    public class AtmApplicationMainMenu
    {
        public readonly string[] Items =
        {
             " 1) Show Balance"
            ," 2) Cash Deposit"
            ," 3) Cash Withdraw" 
            ," 4) Print Transactions History"
            ," 5) Show Limits"
            ," 6) Logout"
        };

        public int BalanceMenuNumber = 1;
        public int DepositMenuNumber = 2;
        public int WithdrawMenuNumber = 3;
        public int HistoryMenuNumber = 4;
        public int LimitsMenuNumber = 5;
        public int LogoutMenuNumber = 6;

    }
}

