using BAL;

namespace ConsoleATM.ConsoleApp
{
    class Program
    {
        public static void Main()
        {
            var atmDatabase = new AtmDatabase("xml");

            var atmApplication = new AtmApplication(atmDatabase);

            var atmConsole = new AtmConsole(atmApplication.DelayMS);

            var atmApplicationUser = new AtmApplicationUser(atmDatabase);

            var atmAccount = new AtmAccount(atmDatabase);

            while (true)
            {
                AuthinticateUser(atmConsole, atmApplicationUser);

                if (atmApplicationUser.Id > 0)
                {
                    if (atmApplicationUser.IsActive != 0)
                    {

                        atmConsole.WriteLine("Login Succeeded.", 'i');

                        if (atmApplication.AllowedUsersCount > atmApplication.ActualUsersCount)
                        {
                            atmConsole.WriteLine("Access allowed.", 'i');

                            atmAccount.Init(atmApplicationUser.Id);

                            if (atmAccount.Id > 0)
                            {

                                atmApplication.IncrementUserCountWithOne();

                                atmConsole.Pause();

                                WorkWithMainMenu(atmDatabase, atmConsole, atmApplicationUser, atmAccount);

                                atmConsole.WriteLine("Logout", 'i');

                                atmApplication.DecrementUserCountWithOne();
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

        private static void WorkWithMainMenu(AtmDatabase atmDatabase, AtmConsole atmConsole, AtmApplicationUser atmApplicationUser, AtmAccount atmAccount)
        {
            var atmApplicationMainMenu = new AtmApplicationMainMenu();

            ConsoleKeyInfo consoleKeyInput;

            int? consolKeyNumber = null;

            while (true)
            {
                atmConsole.ClearScreen();

                atmConsole.PrintMenu(atmApplicationMainMenu);

                consoleKeyInput = Console.ReadKey(intercept: true);

                if (char.IsDigit(consoleKeyInput.KeyChar))
                {
                    consolKeyNumber = int.Parse(consoleKeyInput.KeyChar.ToString());

                    if (consolKeyNumber == atmApplicationMainMenu.BalanceMenuNumber)
                    {
                        WorkWithMainMenuBalance(atmConsole, atmAccount);
                    }

                    if (consolKeyNumber == atmApplicationMainMenu.DepositMenuNumber)
                    {
                        WorkWithMainMenuDeposit(atmDatabase, atmConsole, atmApplicationUser, atmAccount);
                    }

                    if (consolKeyNumber == atmApplicationMainMenu.WithdrawMenuNumber)
                    {
                        WorkWithMainMenuWithdraw(atmDatabase, atmConsole, atmApplicationUser, atmAccount);
                    }

                    if (consolKeyNumber == atmApplicationMainMenu.HistoryMenuNumber)
                    {
                        WorkWithMainMenuPrintHistory(atmDatabase, atmConsole, atmAccount);
                    }

                    if (consolKeyNumber == atmApplicationMainMenu.LogoutMenuNumber)
                    {
                        break;
                    }

                }

            }
        }

        private static void WorkWithMainMenuBalance(AtmConsole atmConsole, AtmAccount atmAccount)
        {
            atmConsole.Write("Current Balance is: ");

            atmConsole.WriteLine(atmAccount.Balance.ToString("C"), 'i');

            atmConsole.Pause();
        }

        private static void WorkWithMainMenuPrintHistory(AtmDatabase atmDatabase, AtmConsole atmConsole, AtmAccount atmAccount)
        {
            List<String> header = new() { "Type    ", "Cash Amount", "Balance Before", "Balance After", "Date Time", "User Name" };

            var transactionHistory = atmDatabase.GetAtmAccountTransactionHistory(atmAccount.Id);

            atmConsole.PrintTable(header, transactionHistory);
        }

        private static void WorkWithMainMenuWithdraw(AtmDatabase atmDatabase, AtmConsole atmConsole, AtmApplicationUser atmApplicationUser, AtmAccount atmAccount)
        {
            atmConsole.Write("Enter Withdraw Ammount: ");

            string? deposit = Console.ReadLine();

            string operationResult = atmAccount.CashWithdraw(deposit);

            if (operationResult != "")
            {
                atmConsole.WriteLine(operationResult, 'w');
            }
            else
            {
                atmDatabase.SaveTransactionHistory(atmAccount.Id, DateTime.Now, Math.Round(decimal.Parse(deposit), 2) * (-1), atmAccount.Balance, atmApplicationUser.FullName);

                atmConsole.WriteLine("Operation Successful", 'i');
            }
            atmConsole.Pause();
        }

        private static void WorkWithMainMenuDeposit(AtmDatabase atmDatabase, AtmConsole atmConsole, AtmApplicationUser atmApplicationUser, AtmAccount atmAccount)
        {
            atmConsole.Write("Enter Deposite Ammount: ");

            string? deposit = Console.ReadLine();

            string operationResult = atmAccount.CashDeposite(deposit);

            if (operationResult != "")
            {
                atmConsole.WriteLine(operationResult, 'w');
            }
            else
            {
                atmDatabase.SaveTransactionHistory(atmAccount.Id, DateTime.Now, Math.Round(decimal.Parse(deposit), 2), atmAccount.Balance, atmApplicationUser.FullName);

                atmConsole.WriteLine("Operation Successful", 'i');
            }

            atmConsole.Pause();
        }

        private static void AuthinticateUser(AtmConsole atmConsole, AtmApplicationUser atmApplicationUser)
        {
            atmConsole.Write("Input username and press Enter: ");

            var inputUserName = atmConsole.ReadUserName();

            atmConsole.Write("Input password and press Enter: ");

            var inputUserPassword = atmConsole.ReadPassword();

            atmApplicationUser.Init(inputUserName, inputUserPassword);
        }
    }


}
