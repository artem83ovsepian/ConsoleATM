using System.Collections;

namespace BAL
{
    public class AtmConsole
    {
        private readonly ConsoleColor InfoMessageColor = ConsoleColor.Green;

        private readonly ConsoleColor WarningMessageColor = ConsoleColor.Yellow;

        private readonly ConsoleColor ErrorMessageColor = ConsoleColor.Red;

        private readonly ConsoleColor DefaultMessageColor = ConsoleColor.White;

        private readonly char PasswordHideChar = '*';

        private readonly int DefaultPauseMs;

        public AtmConsole(int pause) { DefaultPauseMs = pause; }

        public void Write(string message, char messageType = 'd')
        {
            Console.ForegroundColor = messageType switch
            {
                'i' => InfoMessageColor,
                'w' => WarningMessageColor,
                'e' => ErrorMessageColor,
                'd' => DefaultMessageColor,
                _ => DefaultMessageColor,
            };

            Console.Write(message);

            Console.ResetColor();
        }

        public void WriteLine(string message = "", char messageType = 'd')
        {
            Write(message, messageType);

            Console.WriteLine();
        }

        public static void Pause(int delayMs)
        {
            Thread.Sleep(delayMs);
        }

        public void Pause()
        {
            Thread.Sleep(DefaultPauseMs);
        }

        public void WaitUser()
        {
            WriteLine("Press Enter...", 'w');

            ConsoleKey key;

            do
            {
                var readKeyInfo = Console.ReadKey(intercept: true);

                key = readKeyInfo.Key;
            } while (key != ConsoleKey.Enter);
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }

        private string ReadCredentials(string credentialType)
        {
            var credentialString = String.Empty;

            ConsoleKey key;

            do
            {
                var readKeyInfo = Console.ReadKey(intercept: true);

                key = readKeyInfo.Key;

                if (key == ConsoleKey.Backspace && credentialString.Length > 0)
                {
                    Console.Write("\b \b");

                    credentialString = credentialString[0..^1];
                }
                else if (!char.IsControl(readKeyInfo.KeyChar))
                {

                    if (credentialType == "username")
                    {
                        Console.Write(readKeyInfo.KeyChar);
                    }
                    else if (credentialType == "password")
                    {
                        Console.Write(PasswordHideChar);
                    }

                    credentialString += readKeyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            WriteLine();

            return credentialString;
        }

        public string ReadUserName()
        {
            return ReadCredentials("username");
        }

        public string ReadPassword()
        {
            return ReadCredentials("password");
        }

        public void PrintMenu(AtmApplicationMainMenu atmApplicationMenu)
        {
            foreach (var menuItem in atmApplicationMenu.Items)
            {
                WriteLine(menuItem.ToString());
            }

            WriteLine();
        }

        public void PrintTable(List<string> tableHeader, IEnumerable<AtmHistoricalTransaction> tableData)
        {

            WriteLine(" _________________________ _________________________ _________________________ _________________________ _________________________ _________________________", 'i');
            
            for (int i = 0; i < tableHeader.Count; i++)
            {
                Write("|" + tableHeader[i].ToString().PadRight(25), 'i');
            }

            WriteLine("|", 'i');

            WriteLine(" ------------------------- ------------------------- ------------------------- ------------------------- ------------------------- -------------------------", 'i');

            //for (int j = 0; j < tableData.Count; j++)
            IEnumerator atmHistoricalTransactionEnumerator = tableData.GetEnumerator();

            while (atmHistoricalTransactionEnumerator.MoveNext())
            {
                AtmHistoricalTransaction atmHistoricalTransaction = (AtmHistoricalTransaction)atmHistoricalTransactionEnumerator.Current;



                Write("|" + atmHistoricalTransaction.Type?.PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.CashAmount?.PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.BalanceBefore?.PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.BalanceAfter?.PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.Datetime?.PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.UserName?.PadRight(25), 'i');

                Write("|", 'i');

                WriteLine();
            }
            WriteLine(" _________________________ _________________________ _________________________ _________________________ _________________________ _________________________", 'i');

            WaitUser();
        }
    }
}
