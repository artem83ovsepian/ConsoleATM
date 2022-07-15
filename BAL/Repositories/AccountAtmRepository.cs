using BAL.Entities;
using DAL.Repositories;

namespace BAL.Repositories
{
    public class AccountAtmRepository
    {

        private readonly AccountDataRepository _accountDataRepository = new AccountDataRepository();

        public AccountAtm GetAccount(int userId)
        {
            var atmAccountData = _accountDataRepository.GetAccountByUserId(userId);

            return new AccountAtm
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
                    var balance = _accountDataRepository.GetAccountBalance(accountId);
                    balance += decDeposit;

                    SaveBalance(accountId, balance);
                }
            }
            catch
            {
                result = "Enter valid Deposit number";
            }

            balanceAfter = _accountDataRepository.GetAccountBalance(accountId);

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
                    var balance = _accountDataRepository.GetAccountBalance(accountId);
                    var overDraft = _accountDataRepository.GetUserOverdraft(accountId);

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

            balanceAfter = _accountDataRepository.GetAccountBalance(accountId);

            return result;
        }

        public decimal GetBalance(int accountId)
        {
            return _accountDataRepository.GetAccountBalance(accountId);
        }

        private void SaveBalance(int accountId, decimal accountBalance)
        {
            _accountDataRepository.SaveAccountBalance(accountId, accountBalance);
        }

        public decimal GetUserOverdraft(int accountId)
        {
            return _accountDataRepository.GetUserOverdraft(accountId);
        }
    }
}
