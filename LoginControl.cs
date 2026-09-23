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
    internal partial class LoginControl : UserControl
    {// This sends the account over to Form1 where it can be used
        internal class LoginEventArgs : EventArgs
        {
            public Account Account { get; }
            public LoginEventArgs(Account account)
            {
                Account = account;
            }
        }
        // Let's Form1 know login was successful, you'll see it in ShowLogin() and Login_LoginSuccessful in Form1
        internal event EventHandler<LoginEventArgs>? LoginSuccessful; 
        private AccountFileReading accountFile;
        public LoginControl()
        {   // Initialize and then load the accounts - EA
            InitializeComponent();
            // Loads saved accounts when log in page is initialized 
            accountFile = new AccountFileReading();
        }
        private void createAccountButton_Click(object sender, EventArgs e)
        {   // Creates create account control window, doesn't clear the controls, just brings it to front
            CreateAccountControl createaccount = new CreateAccountControl();
            createaccount.Dock = DockStyle.Fill;
            // Creating or canceling will bring us back to the log in page 
            createaccount.AccountCanceled += CloseCreateAccount;
            createaccount.AccountCreated += CloseCreateAccount;
            mainPanelLogin.Controls.Add(createaccount);
            createaccount.BringToFront();
        }
        private void CloseCreateAccount(object? sender, EventArgs e)
        {
            if (sender is CreateAccountControl createAccount)
            {// Reloads account file in case new account is created so you can login immediately afterwards
                accountFile = new AccountFileReading();
                mainPanelLogin.Controls.Remove(createAccount);
                createAccount.Dispose();
            }
        }
        private void loginButton_Click(object sender, EventArgs e)
        {// Checks for empty fields
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(passwordTextextBox.Text))
            {
                MessageBox.Show("Fields are empty.");
                return;
            }
            foreach (Account account in accountFile.GetAccountList())
            {// Searches for matching username and password in account
                if (account.GetAccountUserName() == usernameTextBox.Text &&
                    account.GetAccountPassword() == passwordTextextBox.Text)
                {// Logs in and sends account info over to Form1
                    LoginSuccessful?.Invoke(this, new LoginEventArgs(account));
                    return;
                }
            }
            MessageBox.Show("Mission Failed!!! We'll get'em next time"); // all is lost
        }
        private void cancelButton_Click(object sender, EventArgs e){Application.Exit();} 
        // Clean up later
        private void usernameTextBox_TextChanged(object sender, EventArgs e){}
        private void passwordTextextBox_TextChanged(object sender, EventArgs e){}
    }
}
