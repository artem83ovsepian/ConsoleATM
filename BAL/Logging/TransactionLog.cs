using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BAL.Logging
{
    public class TransactionLog
    {
        private readonly string fileName = "Logging\\TransactionLog.txt";
        public bool CheckTxtLogFileExists()
        {
            return File.Exists(fileName);

        }
    }
}
