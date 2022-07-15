using BAL.Entities;

namespace BAL
{
    public class AtmConsole
    {
        private readonly ConsoleColor infoMessageColor = ConsoleColor.Green;

        private readonly ConsoleColor warningMessageColor = ConsoleColor.Yellow;

        private readonly ConsoleColor errorMessageColor = ConsoleColor.Red;

        private readonly ConsoleColor defaultMessageColor = ConsoleColor.White;

        private readonly char passwordHideChar = '*';

        private readonly int defaultPauseMs;

        public AtmConsole(int pause) { defaultPauseMs = pause; }

        public void Write(string message, char messageType = 'd')
        {
            Console.ForegroundColor = messageType switch
            {
                'i' => infoMessageColor,
                'w' => warningMessageColor,
                'e' => errorMessageColor,
                'd' => defaultMessageColor,
                _ => defaultMessageColor,
            };

            Console.Write(message);

            Console.ResetColor();
        }

        public void WriteLine(string message = "", char messageType = 'd')
        {
            Write(message, messageType);

            Console.WriteLine();
        }

        public void Pause(int delayMs)
        {
            Thread.Sleep(delayMs);
        }

        public void Pause()
        {
            Thread.Sleep(defaultPauseMs);
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

        public void ClearScreen()
        {
            Console.Clear();
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

        public void PrintTable(IEnumerable<string> tableHeader, IEnumerable<HistoricalTransaction> tableData)
        {

            WriteLine(" _________________________ _________________________ _________________________ _________________________ _________________________ _________________________", 'i');

            var atmHistoricalTransactionHeaderEnumerator = tableHeader.GetEnumerator();

            while (atmHistoricalTransactionHeaderEnumerator.MoveNext())
            {
                Write("|" + (string)atmHistoricalTransactionHeaderEnumerator.Current.ToString().PadRight(25), 'i');
            }

            WriteLine("|", 'i');

            WriteLine(" ------------------------- ------------------------- ------------------------- ------------------------- ------------------------- -------------------------", 'i');

            var atmHistoricalTransactionEnumerator = tableData.GetEnumerator();

            while (atmHistoricalTransactionEnumerator.MoveNext())
            {
                var atmHistoricalTransaction = (HistoricalTransaction)atmHistoricalTransactionEnumerator.Current;

                Write("|" + atmHistoricalTransaction.Type.PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.CashAmount.ToString("C").PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.BalanceBefore.ToString("C").PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.BalanceAfter.ToString("C").PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.Datetime.ToString().PadRight(25), 'i');

                Write("|" + atmHistoricalTransaction.UserName.PadRight(25), 'i');

                Write("|", 'i');

                WriteLine();
            }
            WriteLine(" _________________________ _________________________ _________________________ _________________________ _________________________ _________________________", 'i');

            WaitUser();
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
                        Console.Write(passwordHideChar);
                    }

                    credentialString += readKeyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            WriteLine();

            return credentialString;
        }

    }
}
