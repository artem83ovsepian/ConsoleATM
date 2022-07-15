
using BAL.Entities;

namespace BAL
{
    public class AtmAccount
    {

        private readonly AtmDatabase _atmDB;

        public AtmAccount(AtmDatabase atmDatabase) 
        {
            _atmDB = atmDatabase; 
        }

        public Account GetAccount(int userId)
        {
            var atmAccountData = _atmDB.GetUserAccount(userId);

            return new Account
            {                
                Id = atmAccountData.Id,
                UserId = atmAccountData.UserId,
                Balance = atmAccountData.Balance,
                OverDraft = atmAccountData.OverDraft
            };
        }

        public string CashDeposite(int accountId, string deposit, out decimal balanceAfter)
        {
            var result = "";

            try
            {
                var decDeposit = Math.Round(decimal.Parse(deposit), 2);

                if (decDeposit <= 0)
                {
                    result = "Deposite can't be negative or equial 0.00";
                }
                else
                {
                    var balance = _atmDB.GetAccountBalance(accountId);
                    balance += decDeposit;

                    SaveBalance(accountId, balance);
                }
            }
            catch
            {
                result = "Enter valid Deposit number";
            }

            balanceAfter = _atmDB.GetAccountBalance(accountId);

            return result;
        }

        public string CashWithdraw(int accountId, string deposit, out decimal balanceAfter)
        {
            var result = "";

            try
            {
                var decDeposit = Math.Round(decimal.Parse(deposit), 2);

                if (decDeposit <= 0)
                {
                    result = "Withdrow can't be negative or equial 0.00";
                }
                else
                {
                    var balance = _atmDB.GetAccountBalance(accountId);
                    var overDraft = _atmDB.GetUserOverdraft(accountId);

                    if ((balance - decDeposit) < (-overDraft))
                    {
                        result = "Account Credit limit exceeded. Try withdrow lower number.";
                    }
                    else
                    {
                        balance -= decDeposit;

                        SaveBalance(accountId, balance);
                    }
                }
            }
            catch
            {
                result = "Enter valid number";
            }

            balanceAfter = _atmDB.GetAccountBalance(accountId);

            return result;
        }

        public decimal GetBalance(int accountId)
        {
            return _atmDB.GetAccountBalance(accountId);
        }

        private void SaveBalance(int accountId, decimal accountBalance)
        {
            _atmDB.SaveAccountBalance(accountId, accountBalance);
        }

    }
}
