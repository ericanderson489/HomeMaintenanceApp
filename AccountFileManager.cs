using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMaintenanceApp
{
    internal class AccountFileReading
    {
        public string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SavedAccounts.txt");

        List<Account> accountsInFile = new List<Account>();

        public AccountFileReading()
        {
            if (!File.Exists(path)) { // Added this line here to create file path in case it does not already exist
                File.WriteAllText(path, "");
            }

            List<string> fileLines = new List<string>();
            fileLines = File.ReadAllLines(path).ToList();

            foreach (string line in fileLines) //Iterates through the account file and contructs a list of each account contained within.
            {
                string[] accountPart = line.Split(' ');

                Account account = new Account(accountPart[0], accountPart[1], accountPart[2], accountPart[3]);

                accountsInFile.Add(account);
            }
        }

        public List<Account> GetAccountList()
        {
            return accountsInFile;
        }

        void FullRewrite(List<Account> accounts) //Rewrites the file containing all saved accounts 
        {
            List<string> lines = new List<string>();

            foreach (Account account in accounts)
            {
                lines.Add(account.GiveFileString());
            }

            File.WriteAllLines(path, lines);
        }
    }
}
