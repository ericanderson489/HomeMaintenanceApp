using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMaintenanceApp
{
	internal class Account
	{
		//List<Tasks> tasks = new List<Tasks>(); //Just in case we have a list of tasks tied to each account
		private int m_balance;
		private string m_firstName, m_lastName;
		private string m_password = "";

		public Account() //An empty place holder constructor
		{
			m_balance = 0;
			m_firstName = "John";
			m_lastName = "Smith";
			m_password = "password";
		}

		public Account(string FN, string LN, int startingBalance, string password) //Constucts Account
		{
			this.m_balance = startingBalance;
			this.m_firstName = FN;
			this.m_lastName = LN;
			this.m_password = password;
		}

		public void CopyFrom(Account account) //copies information from one account to another.
		{
			m_balance = account.m_balance;
			m_firstName = account.m_firstName;
			m_lastName = account.m_lastName;
			m_password = account.m_password;
		}

		public string GetAccountName() //Gets the full account name.
		{
			string name = (m_firstName + " " + m_lastName);

			return name;
		}

		public int GetAccountBalanceInt() //Returns an account balance as an integer
		{
			return m_balance;
		}
		public string GetAccountBalanceString() //Returns an account balance as a string with a dollar sign in frot of it
		{
			string balance = "$" + m_balance.ToString();
			return balance;
		}

		public string GetAccountPassword()
		{
			return m_password;
		}


		public override string ToString() //Causes the account name to be shown in lists instead of the variable name itself - JD
		{
			return GetAccountName();
		}


		public string GiveFileString() //Returns a string that is used in file-rewriting - JD
		{
			return ($"{m_firstName} {m_lastName} {m_balance} {m_password}");
		}
	}
}
