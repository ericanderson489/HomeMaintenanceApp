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
        public event EventHandler? LoginSuccessful;
        private Account? selectedAccount;
        private AccountFileReading accountFile;
        public LoginControl()
        {
            InitializeComponent();
            accountFile = new AccountFileReading();
        }
        

        private void ShowPage(UserControl page)
        {
            
        }
        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwordTextextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void createAccountButton_Click(object sender, EventArgs e)
        {
            CreateAccountControl createaccount = new CreateAccountControl();
            createaccount.Dock = DockStyle.Fill;
            createaccount.AccountCanceled += Createaccount_AccountCanceled;
            createaccount.AccountCreated += Createaccount_AccountCreated;
            mainPanelLogin.Controls.Add(createaccount);
            createaccount.BringToFront();
        }

        private void Createaccount_AccountCreated(object? sender, EventArgs e)
        {
            if (sender is CreateAccountControl createAccount)
            {
                Controls.Remove(createAccount);
                createAccount.Dispose();
            }
        }

        private void Createaccount_AccountCanceled(object? sender, EventArgs e)
        {
            if (sender is CreateAccountControl createAccount)
            {
                Controls.Remove(createAccount);
                createAccount.Dispose();
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(passwordTextextBox.Text))
            {
                MessageBox.Show("Fields are empty.");
            }
            foreach (Account account in accountFile.GetAccountList())
            {
                if (account.GetAccountUserName() == usernameTextBox.Text &&
                    account.GetAccountPassword() == passwordTextextBox.Text)
                {
                    selectedAccount = account;
                    LoginSuccessful?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
            MessageBox.Show("Mission Failed!!! We'll get'em next time"); // all is lost
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}
