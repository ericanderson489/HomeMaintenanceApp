namespace HomeMaintenanceApp
{
    internal partial class LoginControl : UserControl
    {
        internal class LoginEventArgs : EventArgs
        {
            public Account Account { get; }
            public LoginEventArgs(Account account) => Account = account;
        }
        internal event EventHandler<LoginEventArgs>? LoginSuccessful;
        private readonly AccountFileReading accountFile = new();
        private readonly Label feedback = UI.WorkshopStyle.Label("");
        public LoginControl()
        {
            InitializeComponent();
            ApplyWorkshopLayout();
        }
        private void createAccountButton_Click(object sender, EventArgs e)
        {
            var createaccount = new CreateAccountControl { Dock = DockStyle.Fill };
            createaccount.AccountCanceled += CloseCreateAccount;
            createaccount.AccountCreated += (_, _) =>
            {
                CloseCreateAccount(createaccount, EventArgs.Empty);
                feedback.Text = "Account created. You can sign in now.";
            };
            foreach (Control child in mainPanelLogin.Controls) child.Visible = false;
            mainPanelLogin.Controls.Add(createaccount);
            createaccount.BringToFront();
        }
        // Returning from either action only restores the screen; Cancel never reads storage.
        private void CloseCreateAccount(object? sender, EventArgs e)
        {
            if (sender is not CreateAccountControl createAccount) return;
            mainPanelLogin.Controls.Remove(createAccount);
            createAccount.Dispose();
            foreach (Control child in mainPanelLogin.Controls) child.Visible = true;
            usernameTextBox.Focus();
        }
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextextBox.Text))
            {
                feedback.Text = "Please enter your username and password.";
                return;
            }
            Account? match;
            try
            {
                match = accountFile.GetAccountList().SingleOrDefault(a =>
                    string.Equals(a.GetAccountUserName(), usernameTextBox.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                if (match is null || !match.VerifyPassword(passwordTextextBox.Text))
                {
                    feedback.Text = "Username or password is incorrect.";
                    return;
                }
            }
            catch (Exception ex) when (ex is IOException or InvalidDataException or UnauthorizedAccessException)
            {
                feedback.Text = "Accounts could not be loaded. Your entries are still here. Check the account folder and try again.";
                return;
            }
            LoginSuccessful?.Invoke(this, new LoginEventArgs(match));
        }
        private void cancelButton_Click(object sender, EventArgs e) { Application.Exit(); }
        private void usernameTextBox_TextChanged(object sender, EventArgs e) { }
        private void passwordTextextBox_TextChanged(object sender, EventArgs e) { }
    }
}
