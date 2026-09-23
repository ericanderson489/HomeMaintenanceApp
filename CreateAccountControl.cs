using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeMaintenanceApp
{
    internal partial class CreateAccountControl : UserControl
    {
        internal event EventHandler? AccountCreated;
        internal event EventHandler? AccountCanceled;
        public CreateAccountControl()
        {
            InitializeComponent();
        }
        private void createAccountButton_Click(object sender, EventArgs e)
        {// Checks to see if every field has something 
            if (string.IsNullOrWhiteSpace(fnameTextBox.Text) || 
                string.IsNullOrWhiteSpace(lnameTextBox.Text) ||
                string.IsNullOrWhiteSpace(usernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(passwordTextBox.Text)) {
                MessageBox.Show("Please fill out all fields");
                return; 
            }
            AccountFileReading accountFile = new AccountFileReading(); 
            foreach (Account account in accountFile.GetAccountList())
            {// This checks for any duplicate usernames in the account list
                if (account.GetAccountUserName() == usernameTextBox.Text)
                {
                    MessageBox.Show("That username already exists");
                    return;
                }
            } // The new account is created using entered text from fields 
            Account newAccount = new Account(
                usernameTextBox.Text,
                passwordTextBox.Text, 
                fnameTextBox.Text, 
                lnameTextBox.Text);
            accountFile.AddAccount(newAccount);
            
            MessageBox.Show("Account created!");
            AccountCreated?.Invoke(this, EventArgs.Empty);
        }
        private void cancelCreateButton_Click(object sender, EventArgs e)
        {// Brings you back to the log in page
            AccountCanceled?.Invoke(this, EventArgs.Empty);
        }
        // Everything below this line can be ignored for now
        private void CreateAccountControl_Load(object sender, EventArgs e){}
        private void fnameTextBox_TextChanged(object sender, EventArgs e){}
        private void lnameTextBox_TextChanged(object sender, EventArgs e){}
        private void usernameTextBox_TextChanged(object sender, EventArgs e){}
        private void passwordTextBox_TextChanged(object sender, EventArgs e){}
    }
}
