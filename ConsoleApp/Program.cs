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

            var atmApplicationMainMenu = new AtmApplicationMainMenu();

            while (true)
            {
                atmConsole.Write("Input username and press Enter: ");

                var inputUserName = atmConsole.ReadUserName();

                atmConsole.Write("Input password and press Enter: ");

                var inoutUserPassword = atmConsole.ReadPassword();

                atmApplicationUser.Init(inputUserName, inoutUserPassword);

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

                                ConsoleKeyInfo consoleKeyInput;

                                int? consolKeyNumber = null;

                                while (true)
                                {
                                    AtmConsole.ClearScreen();

                                    atmConsole.PrintMenu(atmApplicationMainMenu);

                                    consoleKeyInput = Console.ReadKey(intercept: true);

                                    if (char.IsDigit(consoleKeyInput.KeyChar))
                                    {
                                        consolKeyNumber = int.Parse(consoleKeyInput.KeyChar.ToString());

                                        if (consolKeyNumber == atmApplicationMainMenu.BalanceMenuNumber)
                                        {
                                            atmConsole.Write("Current Balance is: ");

                                            atmConsole.WriteLine(atmAccount.Balance.ToString("C"), 'i');

                                            atmConsole.Pause();
                                        }

                                        if (consolKeyNumber == atmApplicationMainMenu.DepositMenuNumber)
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

                                        if (consolKeyNumber == atmApplicationMainMenu.WithdrawMenuNumber)
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

                                        if (consolKeyNumber == atmApplicationMainMenu.HistoryMenuNumber)
                                        {                                           

                                            List<String> header = new() { "Type    " , "Cash Amount", "Balance Before" , "Balance After" , "Date Time" , "User Name" };

                                            IEnumerable<AtmHistoricalTransaction> transactionHistory = atmDatabase.GetAtmAccountTransactionHistory(atmAccount.Id);

                                            atmConsole.PrintTable(header, transactionHistory);
                                        }

                                        if (consolKeyNumber == atmApplicationMainMenu.LogoutMenuNumber)
                                        {
                                            break;
                                        }

                                    }

                                }

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

                AtmConsole.ClearScreen();
            }
        }

    }


}
