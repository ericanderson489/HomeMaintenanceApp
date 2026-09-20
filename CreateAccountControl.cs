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
    public partial class CreateAccountControl : UserControl
    {
        public CreateAccountControl()
        {
            InitializeComponent();
        }

        private void fnameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void lnameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void createAccountButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fnameTextBox.Text) || 
                string.IsNullOrWhiteSpace(lnameTextBox.Text) ||
                string.IsNullOrWhiteSpace(usernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(passwordTextBox.Text)) {
                MessageBox.Show("Please fill out all fields");
            }
            AccountFileManager accountFile = new AccountFileManager(); 
        }

        private void cancelCreateButton_Click(object sender, EventArgs e)
        {

        }

        private void CreateAccountControl_Load(object sender, EventArgs e)
        {

        }
    }
}
