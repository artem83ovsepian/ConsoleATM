using BAL;
using BAL.Entities;
using BAL.Repositories;

namespace ConsoleATM.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var dbType = args[0];

            var atmApplication = (new ApplicationAtmRepository(dbType)).GetApplication();
            var atmConsole = new AtmConsole(atmApplication.DelayMS);
            ApplicationUserAtm user;
            AccountAtm account;

            while (true)
            {
                user = AuthinticateUser(atmConsole, dbType);
                if (user.Id > 0)
                {
                    if (user.IsActive != 0)
                    {
                        atmConsole.WriteLine("Login Succeeded.", 'i');
                        if (atmApplication.AllowedUsersCount > atmApplication.ActualUsersCount)
                        {
                            atmConsole.WriteLine("Access allowed.", 'i');
                            account = (new AccountAtmRepository(dbType)).GetAccount(user.Id);
                            if (account.Id > 0)
                            {
                                (new ApplicationAtmRepository(dbType)).IncrementUserCountWithOne();
                                atmConsole.Pause();
                                WorkWithMainMenu(atmConsole, user.FullName, account.Id, dbType);
                                atmConsole.WriteLine("Logout", 'i');
                                (new ApplicationAtmRepository(dbType)).DecrementUserCountWithOne();
                            }
                            else
                            {
                                atmConsole.WriteLine("User doesn't have active Account.", 'e');
                                atmConsole.WriteLine("Logout.", 'e');
                            }
                        }
                        else
                        {
                            atmConsole.WriteLine("Access denied. Maximum limit of active users exceeded.", 'e');
                        }
                    }
                    else
                    {
                        atmConsole.WriteLine("Access denied. Account is locked.", 'e');
                    }
                }
                else
                {
                    atmConsole.WriteLine("Login Failed. Incorrect username or password.", 'e');
                }
                atmConsole.Pause();
                atmConsole.ClearScreen();
            }
        }

        private static void WorkWithMainMenu(AtmConsole atmConsole, string atmApplicationUserFullName, int atmAccountId, string dbType)
        {
            var atmApplicationMainMenu = new AtmApplicationMainMenu();
            
            while (true)
            {
                atmConsole.ClearScreen();
                atmConsole.PrintMenu(atmApplicationMainMenu);
                ConsoleKeyInfo consoleKeyInput;                
                consoleKeyInput = Console.ReadKey(intercept: true);
                if (char.IsDigit(consoleKeyInput.KeyChar))
                {
                    var consolKeyNumber = int.Parse(consoleKeyInput.KeyChar.ToString());
                    if (consolKeyNumber == atmApplicationMainMenu.BalanceMenuNumber)
                    {
                        WorkWithMainMenuBalance(atmConsole, atmAccountId, dbType);
                    }
                    if (consolKeyNumber == atmApplicationMainMenu.DepositMenuNumber)
                    {
                        WorkWithMainMenuDeposit(atmConsole, atmApplicationUserFullName, atmAccountId, dbType);
                    }
                    if (consolKeyNumber == atmApplicationMainMenu.WithdrawMenuNumber)
                    {
                        WorkWithMainMenuWithdraw(atmConsole, atmApplicationUserFullName, atmAccountId, dbType);
                    }
                    if (consolKeyNumber == atmApplicationMainMenu.HistoryMenuNumber)
                    {
                        WorkWithMainMenuPrintHistory(atmConsole, atmAccountId, dbType);
                    }
                    if (consolKeyNumber == atmApplicationMainMenu.LimitsMenuNumber)
                    {
                        WorkWithMainMenuLimits( atmConsole, atmAccountId, dbType);
                    }
                    if (consolKeyNumber == atmApplicationMainMenu.LogoutMenuNumber)
                    {
                        break;
                    }
                }
            }
        }

        private static void WorkWithMainMenuBalance(AtmConsole atmConsole, int accountId, string dbType)
        {
            atmConsole.Write("Current Balance is: ");
            atmConsole.WriteLine((new AccountAtmRepository(dbType)).GetBalance(accountId).ToString("C"), 'i');
            atmConsole.Pause();
        }

        private static void WorkWithMainMenuLimits(AtmConsole atmConsole, int accountId, string dbType)
        {
            atmConsole.Write("Current Cash Withdraw Overdraft is: ");
            atmConsole.WriteLine((new AccountAtmRepository(dbType)).GetUserOverdraft(accountId).Value.ToString("C"), 'i');
            atmConsole.Pause();
        }        

        private static void WorkWithMainMenuPrintHistory(AtmConsole atmConsole, int atmAccountId, string dbType)
        {
            var historicalTransactionAtmRepository = new HistoricalTransactionAtmRepository(dbType);
            List<String> header = new() { "Type    ", "Cash Amount", "Balance Before", "Balance After", "Date Time", "User Name" };
            var transactionHistory = historicalTransactionAtmRepository.GetAccountTransactionHistory(atmAccountId);
            atmConsole.PrintTable(header, transactionHistory);
        }

        private static void WorkWithMainMenuWithdraw(AtmConsole atmConsole, string atmApplicationUserFullName, int atmAccountId, string dbType)
        {
            atmConsole.Write("Enter Withdraw Ammount: ");
            var deposit = Console.ReadLine();
            var operationResult = (new AccountAtmRepository(dbType)).CashWithdraw(atmAccountId, deposit, out decimal balanceAfter);
            if (operationResult != "")
            {
                atmConsole.WriteLine(operationResult, 'w');
            }
            else
            {
                var historicalTransactionAtmRepository = new HistoricalTransactionAtmRepository(dbType);
                historicalTransactionAtmRepository.SaveTransactionHistory(atmAccountId, DateTime.Now, Math.Round(decimal.Parse(deposit), 2) * (-1), balanceAfter, atmApplicationUserFullName);
                atmConsole.WriteLine("Operation Successful", 'i');
            }
            atmConsole.Pause();
        }

        private static void WorkWithMainMenuDeposit(AtmConsole atmConsole, string atmApplicationUserFullName, int atmAccountId, string dbType)
        {
            var accountAtmRepository = new AccountAtmRepository(dbType);

            atmConsole.Write("Enter Deposite Ammount: ");
            var deposit = Console.ReadLine();
            var operationResult = accountAtmRepository.CashDeposite(atmAccountId, deposit, out decimal balanceAfter);
            if (operationResult != "")
            {
                atmConsole.WriteLine(operationResult, 'w');
            }
            else
            {
                var historicalTransactionAtmRepository = new HistoricalTransactionAtmRepository(dbType);
                historicalTransactionAtmRepository.SaveTransactionHistory(atmAccountId, DateTime.Now, Math.Round(decimal.Parse(deposit), 2), balanceAfter, atmApplicationUserFullName);
                atmConsole.WriteLine("Operation Successful", 'i');
            }
            atmConsole.Pause();
        }

        private static ApplicationUserAtm AuthinticateUser(AtmConsole atmConsole, string dbType)
        {
            atmConsole.Write("Input username and press Enter: ");
            var inputUserName = atmConsole.ReadUserName();
            atmConsole.Write("Input password and press Enter: ");
            var inputUserPassword = atmConsole.ReadPassword();
            var atmUser = (new ApplicationUserAtmRepository(dbType)).GetUser(inputUserName, inputUserPassword);
            return new ApplicationUserAtm
            {
                Id = atmUser.Id,
                Name = atmUser.Name,
                FullName = atmUser.FullName,
                IsActive = atmUser.IsActive
            };
        }
    }
}
