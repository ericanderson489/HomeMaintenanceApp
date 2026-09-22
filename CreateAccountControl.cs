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
    public partial class CreateAccountControl : UserControl
    {
        public CreateAccountControl()
        {
            InitializeComponent();
        }
        public event EventHandler? AccountCreated;
        public event EventHandler? AccountCanceled;

        

        private void createAccountButton_Click(object sender, EventArgs e)
        {// This checks to see if there are any fields with white space
            if (string.IsNullOrWhiteSpace(fnameTextBox.Text) || 
                string.IsNullOrWhiteSpace(lnameTextBox.Text) ||
                string.IsNullOrWhiteSpace(usernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(passwordTextBox.Text)) {
                MessageBox.Show("Please fill out all fields");
            }
            AccountFileReading accountFile = new AccountFileReading(); 
            foreach (Account account in accountFile.GetAccountList())
            {
                if (account.GetAccountUserName() == usernameTextBox.Text)
                {
                    MessageBox.Show("That username already exists");
                    return;
                }
            }
            Account newAccount = new Account(
                usernameTextBox.Text,
                passwordTextBox.Text, 
                fnameTextBox.Text, 
                lnameTextBox.Text);
            accountFile.AddAcount(newAccount);
            
            MessageBox.Show("Account created!");
            AccountCreated?.Invoke(this, EventArgs.Empty);
        }

        private void cancelCreateButton_Click(object sender, EventArgs e)
        {
            AccountCanceled?.Invoke(this, EventArgs.Empty);
        }
        // Everything below this line is just label
        private void CreateAccountControl_Load(object sender, EventArgs e){}
        private void fnameTextBox_TextChanged(object sender, EventArgs e){}
        private void lnameTextBox_TextChanged(object sender, EventArgs e){}
        private void usernameTextBox_TextChanged(object sender, EventArgs e){}
        private void passwordTextBox_TextChanged(object sender, EventArgs e){}
    }
}
