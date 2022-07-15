using BAL;
using BAL.Entities;
using BAL.Repositories;

namespace ConsoleATM.ConsoleApp
{
    class Program
    {

        public static void Main()
        {
            var atmDatabase = new AtmDatabase("xml");

            var atmApplication = (new AtmApplication(atmDatabase)).GetApplication();

            var atmConsole = new AtmConsole(atmApplication.DelayMS);

            ApplicationUserAtm user;

            AccountAtm account;


            while (true)
            {
                user = AuthinticateUser(atmConsole, atmDatabase);

                if (user.Id > 0)
                {
                    if (user.IsActive != 0)
                    {

                        atmConsole.WriteLine("Login Succeeded.", 'i');

                        if (atmApplication.AllowedUsersCount > atmApplication.ActualUsersCount)
                        {
                            atmConsole.WriteLine("Access allowed.", 'i');

                            account = (new AccountAtmRepository()).GetAccount(user.Id);

                            if (account.Id > 0)
                            {

                                atmDatabase.IncrementUserCountWithOne();

                                atmConsole.Pause();

                                WorkWithMainMenu(atmDatabase, atmConsole, user.FullName, account.Id);

                                atmConsole.WriteLine("Logout", 'i');

                                atmDatabase.DecrementUserCountWithOne();
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

        private static void WorkWithMainMenu(AtmDatabase atmDatabase, AtmConsole atmConsole, string atmApplicationUserFullName, int atmAccountId)
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
                        WorkWithMainMenuBalance(atmDatabase, atmConsole, atmAccountId);
                    }

                    if (consolKeyNumber == atmApplicationMainMenu.DepositMenuNumber)
                    {
                        WorkWithMainMenuDeposit(atmDatabase, atmConsole, atmApplicationUserFullName, atmAccountId);
                    }

                    if (consolKeyNumber == atmApplicationMainMenu.WithdrawMenuNumber)
                    {
                        WorkWithMainMenuWithdraw(atmDatabase, atmConsole, atmApplicationUserFullName, atmAccountId);
                    }

                    if (consolKeyNumber == atmApplicationMainMenu.HistoryMenuNumber)
                    {
                        WorkWithMainMenuPrintHistory(atmDatabase, atmConsole, atmAccountId);
                    }

                    if (consolKeyNumber == atmApplicationMainMenu.LimitsMenuNumber)
                    {
                        WorkWithMainMenuLimits(atmDatabase, atmConsole, atmAccountId);
                    }

                    if (consolKeyNumber == atmApplicationMainMenu.LogoutMenuNumber)
                    {
                        break;
                    }

                }

            }
        }

        private static void WorkWithMainMenuBalance(AtmDatabase atmDatabase, AtmConsole atmConsole, int accountId)
        {
            atmConsole.Write("Current Balance is: ");

            atmConsole.WriteLine((new AccountAtmRepository()).GetBalance(accountId).ToString("C"), 'i');

            atmConsole.Pause();
        }

        private static void WorkWithMainMenuLimits(AtmDatabase atmDatabase, AtmConsole atmConsole, int accountId)
        {
            atmConsole.Write("Current Cash Withdraw Overdraft is: ");

            atmConsole.WriteLine((new AccountAtmRepository()).GetUserOverdraft(accountId).ToString("C"), 'i');

            atmConsole.Pause();
        }
        

        private static void WorkWithMainMenuPrintHistory(AtmDatabase atmDatabase, AtmConsole atmConsole, int atmAccountId)
        {
            List<String> header = new() { "Type    ", "Cash Amount", "Balance Before", "Balance After", "Date Time", "User Name" };

            var transactionHistory = atmDatabase.GetAtmAccountTransactionHistory(atmAccountId);

            atmConsole.PrintTable(header, transactionHistory);
        }

        private static void WorkWithMainMenuWithdraw(AtmDatabase atmDatabase, AtmConsole atmConsole, string atmApplicationUserFullName, int atmAccountId)
        {
            atmConsole.Write("Enter Withdraw Ammount: ");

            var deposit = Console.ReadLine();

            var operationResult = (new AccountAtmRepository()).CashWithdraw(atmAccountId, deposit, out decimal balanceAfter);

            if (operationResult != "")
            {
                atmConsole.WriteLine(operationResult, 'w');
            }
            else
            {
                atmDatabase.SaveTransactionHistory(atmAccountId, DateTime.Now, Math.Round(decimal.Parse(deposit), 2) * (-1), balanceAfter, atmApplicationUserFullName);

                atmConsole.WriteLine("Operation Successful", 'i');
            }
            atmConsole.Pause();
        }

        private static void WorkWithMainMenuDeposit(AtmDatabase atmDatabase, AtmConsole atmConsole, string atmApplicationUserFullName, int atmAccountId)
        {
            atmConsole.Write("Enter Deposite Ammount: ");

            var deposit = Console.ReadLine();

            var operationResult = (new AccountAtmRepository()).CashDeposite(atmAccountId, deposit, out decimal balanceAfter);

            if (operationResult != "")
            {
                atmConsole.WriteLine(operationResult, 'w');
            }
            else
            {
                atmDatabase.SaveTransactionHistory(atmAccountId, DateTime.Now, Math.Round(decimal.Parse(deposit), 2), balanceAfter, atmApplicationUserFullName);

                atmConsole.WriteLine("Operation Successful", 'i');
            }

            atmConsole.Pause();
        }

        private static ApplicationUserAtm AuthinticateUser(AtmConsole atmConsole, AtmDatabase atmDatabase)
        {
            atmConsole.Write("Input username and press Enter: ");

            var inputUserName = atmConsole.ReadUserName();

            atmConsole.Write("Input password and press Enter: ");

            var inputUserPassword = atmConsole.ReadPassword();

            var atmUser = (new AtmApplicationUser(atmDatabase)).GetUser(inputUserName, inputUserPassword);

            return new ApplicationUserAtm
            {
                Id = atmUser.Id,
                Name = atmUser.Name,
                FullName = atmUser.FullName,
                CurrentAccountId = atmUser.CurrentAccountId,
                IsActive = atmUser.IsActive
            };

            //return atmApplicationUser.GetUser(inputUserName, inputUserPassword);
        }
    }


}
