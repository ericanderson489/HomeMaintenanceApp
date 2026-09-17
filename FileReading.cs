using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMaintenanceApp
{
	internal class FileReading
	{
		public string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SavedAccounts.txt");

		public List<string> accountsInFile = new List<string>();

		public FileReading()
		{
			accountsInFile = File.ReadAllLines(path).ToList();
		}

		public void FullRewrite(List<Account> accounts) //Rewrites the file containing all saved accounts 
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
