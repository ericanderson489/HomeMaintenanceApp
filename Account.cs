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
		private string m_firstName, m_lastName, m_userName;
		private string m_password = "";

		public Account() //An empty place holder constructor
		{
			m_balance = 0;
			m_userName = "JohnSmith";
			m_firstName = "John";
			m_lastName = "Smith";
			m_password = "password";
		}

		public Account(string userName, string password, string firstName, string lastName) //Constucts Account
		{
			this.m_userName = userName;
			this.m_firstName = firstName;
			this.m_lastName = lastName;
			this.m_password = password;
		}

		public void CopyFrom(Account account) //copies information from one account to another.
		{
			m_userName = account.m_userName;
			m_balance = account.m_balance;
			m_firstName = account.m_firstName;
			m_lastName = account.m_lastName;
			m_password = account.m_password;
		}

		public string GetAccountFullName() //Gets the full account name.
		{
			string name = (m_firstName + " " + m_lastName);

			return name;
		}

		public string GetAccountUserName() 
		{
			return m_userName;
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
			return GetAccountFullName();
		}


		public string GiveFileString() //Returns a string that is used in file-rewriting - JD
		{
			return ($"{m_userName} {m_password} {m_firstName} {m_lastName}");
		}
	}
}
