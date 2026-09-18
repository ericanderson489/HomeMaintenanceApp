using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeMaintenanceApp
{
	public partial class LoginControl : UserControl
	{
		AccountFileReading accountFile;
		Account loopAccount = new Account(); //A place holder account used when iterating through lists
		Account selectedAccount;
		public LoginControl()
		{
			InitializeComponent();
			accountFile = new AccountFileReading(); //Initializes 

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void loginButton_Click(object sender, EventArgs e)
		{
			bool foundAccount = false;
			int index = 0;

			for (int i = 0; i < accountFile.GetAccountList().Count(); i++)
			{
				loopAccount.CopyFrom(accountFile.GetAccountList().ElementAt(i));

				if (loopAccount.GetAccountUserName() == UsernameTextbox.Text && loopAccount.GetAccountPassword() == passwordTextBox.Text)
				{
					foundAccount = true;
					index = i;
					break;
				}
			}

			if (foundAccount == true) 
			{
				selectedAccount = accountFile.GetAccountList().ElementAt(index);
				currentAccountLabel.Text = ($"Current Account: {selectedAccount.GetAccountFullName()}");
			}
			else 
			{
				MessageBox.Show("Could Not Find Account");
			}
		}
	}
}
