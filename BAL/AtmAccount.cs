using DAL;

namespace BAL
{
    public class AtmAccount
    { 
        public AtmAccount(AtmDatabase atmDatabase) { atmDB = atmDatabase; }

        public int Id;

        public int UserId;

        public decimal Balance;

        public int IsActive;

        AtmDatabase atmDB;

        AccountData atmAccountData = new AccountData();

        public void Init(int userId)
        {
            atmAccountData = atmDB.GetAtmAccount(userId);

            Id = atmAccountData.Id;

            UserId = atmAccountData.UserId;

            Balance = atmAccountData.Balance;
        }

        public string CashDeposite(string? deposit)
        {
            string result = "";

            decimal decDeposit = 0;

            try
            {
                decDeposit = Math.Round(decimal.Parse(deposit), 2);

                if (decDeposit <= 0)
                {
                    result = "Deposite can't be negative or equial 0.00";
                }
                else
                {

                    Balance += decDeposit;

                    SaveBalance(Id, Balance);
                }
            }
            catch
            {
                result = "Enter valid Deposit number";
            }   

            return result;
        }

        public string CashWithdraw(string? deposit)
        {
            string result = "";

            decimal decDeposit = 0;

            try
            {
                decDeposit = Math.Round(decimal.Parse(deposit), 2);

                if (decDeposit <= 0)
                {
                    result = "Withdrow can't be negative or equial 0.00";
                }
                else
                {

                    Balance -= decDeposit;

                    SaveBalance(Id, Balance);
                }
            }
            catch
            {
                result = "Enter valid number";
            }

            return result;
        }

        private void SaveBalance(int accountId, decimal balance)
        {
            atmDB.SaveAtmAccount(accountId, balance);
        }
    }
}
