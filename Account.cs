using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMaintenanceApp
{
	internal class Account
	{
		private List<double> transactions = new List<double>(); //Tracks tansactions (goes unused)
		private int m_balance;
		int m_gamesPlayed, m_gamesWon, m_winRate;
		private string m_firstName, m_lastName;
		private bool winningToMuch; //Keeps track of whether to rig the game. (goes unused)
		private string m_password = "";

		public Account() //An empty place holder constructor - JD
		{
			m_balance = 0;
			m_firstName = "John";
			m_lastName = "Smith";
			winningToMuch = false;
			m_password = "password";
			m_gamesPlayed = 0;
			m_gamesWon = 0;
			m_winRate = 0;
			intitialRigStatus();
		}

		public Account(string FN, string LN, int startingBalance, string password, int gamesPlayed, int gamesWon, int winRate) //Constucts Account - JD
		{
			this.m_balance = startingBalance;
			this.m_firstName = FN;
			this.m_lastName = LN;
			winningToMuch = false;
			this.m_password = password;
			this.m_gamesPlayed = gamesPlayed;
			this.m_gamesWon = gamesPlayed;
			this.m_winRate = winRate;
			intitialRigStatus();
		}

		public void CopyFrom(Account account) //copies information from one account to another. - JD
		{
			m_balance = account.m_balance;
			m_firstName = account.m_firstName;
			m_lastName = account.m_lastName;
			winningToMuch = account.winningToMuch;
			m_password = account.m_password;
			m_gamesPlayed = account.m_gamesPlayed;
			m_gamesWon = account.m_gamesWon;
			m_winRate = account.m_winRate;
		}

		public string GetAccountName() //Gets the full account name. - JD
		{
			string name = (m_firstName + " " + m_lastName);

			return name;
		}

		public int GetAccountBalanceInt() //Returns an account balance as an integer - JD
		{
			return m_balance;
		}
		public string GetAccountBalanceString() //Returns an account balance as a string with a dollar sign in frot of it - JD
		{
			string balance = "$" + m_balance.ToString();
			return balance;
		}

		public string GetAccountPassword()
		{
			return m_password;
		}

		public void DepositMoney(int deposit) //Deposites Money - JD
		{
			if (deposit > 0)
			{
				m_balance += deposit;
				transactions.Add(deposit * -1);
			}
			else
			{
				Console.WriteLine("ERROR. DEPOSIT AMMOUNT MUST BE ENTERED AS A POSITIVE NUMBER");
			}
		}

		public void WithdrawMoney(int deposit) //Withdraws money - JD
		{
			if (deposit > 0)
			{
				m_balance -= deposit;
				transactions.Add(deposit);
			}
			else
			{
				Console.WriteLine("ERROR. WITHDRAW AMMOUNT MUST BE ENTERED AS A POSITIVE NUMBER");
			}
		}

		public override string ToString() //Causes the account name to be shown in lists instead of the variable name itself - JD
		{
			return GetAccountName();
		}

		//Goes unused
		private void intitialRigStatus() //Sets an initial rig status when opening up the program - JD
		{
			if (m_winRate >= 55)
			{
				winningToMuch = true;
			}
			else
			{
				winningToMuch = false;
			}
		}

		//Goes unused
		public void updateWinRate(bool Won) //keeps track of winrate and rigging bool value - JD
		{
			m_gamesPlayed += 1;

			if (Won)
			{
				m_gamesWon += 1;
			}

			m_winRate = (m_gamesWon / m_gamesPlayed) * 100;

			if (m_winRate >= 55)
			{
				winningToMuch = true;
			}
			else
			{
				winningToMuch = false;
			}
		}

		//Goes unused
		public bool shouldRig() //Returns true or false based on win rate. - JD
		{
			return winningToMuch;
		}


		public string GiveFileString() //Returns a string that is used in file-rewriting - JD
		{
			return ($"{m_firstName} {m_lastName} {m_balance} {m_password} {m_gamesPlayed} {m_gamesWon} {m_winRate}");
		}
	}
}
