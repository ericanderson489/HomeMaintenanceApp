namespace HomeMaintenanceApp
{
    internal partial class CreateAccountControl : UserControl
    {
        internal event EventHandler? AccountCreated;
        internal event EventHandler? AccountCanceled;
        private readonly Label feedback = UI.WorkshopStyle.Label("");
        public CreateAccountControl()
        {
            InitializeComponent();
            ApplyWorkshopLayout();
        }
        private void createAccountButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fnameTextBox.Text) || string.IsNullOrWhiteSpace(lnameTextBox.Text) ||
                string.IsNullOrWhiteSpace(usernameTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                feedback.Text = "Please fill out all fields.";
                return;
            }
            try
            {
                var account = new Account(usernameTextBox.Text, passwordTextBox.Text, fnameTextBox.Text, lnameTextBox.Text);
                new AccountFileReading().AddAccount(account);
            }
            catch (ArgumentException ex) { feedback.Text = ex.Message; return; }
            catch (Exception ex) when (ex is IOException or InvalidDataException or UnauthorizedAccessException)
            {
                feedback.Text = "The account could not be saved. Your entries are still here. Check the account folder and try again.";
                return;
            }
            AccountCreated?.Invoke(this, EventArgs.Empty);
        }
        private void cancelCreateButton_Click(object sender, EventArgs e) => AccountCanceled?.Invoke(this, EventArgs.Empty);
        private void CreateAccountControl_Load(object sender, EventArgs e) { }
        private void fnameTextBox_TextChanged(object sender, EventArgs e) { }
        private void lnameTextBox_TextChanged(object sender, EventArgs e) { }
        private void usernameTextBox_TextChanged(object sender, EventArgs e) { }
        private void passwordTextBox_TextChanged(object sender, EventArgs e) { }
    }
}
