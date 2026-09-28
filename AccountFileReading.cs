using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMaintenanceApp
{
    internal class AccountFileReading
    {
        // Stores account file in same directory of the app
        private string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SavedAccounts.txt");
        // Stores accounts loaded from file while apps running
        private List<Account> accountsInFile = new List<Account>();
        public AccountFileReading()
        {   // Added this line here to create empty file in case it does not already exist - EA
            if (!File.Exists(path)) { 
                File.WriteAllText(path, "");
            }
            List<string> fileLines = new List<string>();
            fileLines = File.ReadAllLines(path).ToList(); // Reads each saved account from the file
            // Iterates through the account file and contructs a list of each account contained within. - EA
            foreach (string line in fileLines) 
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
        public void AddAccount(Account account)
        {// This adds the accont object to the list
            accountsInFile.Add(account);
            SaveAccount();
        }
        public void SaveAccount()
        {// This creates the new file, writes everything and then closes it 
            List<string> lines = new List<string>();

            foreach (Account account in accountsInFile)
            {
                lines.Add(account.GiveFileString());
            }
            File.WriteAllLines(path, lines);
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
