namespace HomeMaintenanceApp
{
    internal partial class MainForm : Form
    {
        private Account? currentAccount;
        private Size mainWindowSize; //this saves client size window, is used to adjust size after login
        public MainForm()
        {
            InitializeComponent(); // The component is initialized
            ApplyWorkshopStyle(); // workshop shell around the team's account flow.
            mainWindowSize = ClientSize; // this is where the client size window is saved
            ShowLogin(); // login window is shown on start up
        }
        public void ShowLogin()
        { // The reason i do not use ShowPage() here is because the client window size is different
            sidePanel.Visible = false; // Side panel is hidden, the log in page is docked to the main panel
            LoginControl login = new LoginControl(); // New page created
            ClientSize = mainWindowSize; // Keep the responsive workshop sign-in layout.
            login.Dock = DockStyle.Fill; // This is where it is docked

            login.LoginSuccessful += Login_LoginSuccessful; // Adds event login successful
            taskSource = null;
            taskFilter = UI.TaskFilter.All;
            currentAccount = null;
            ShowPage(login);
        }
        private void Login_LoginSuccessful(object? sender, LoginControl.LoginEventArgs e)
        {
            currentAccount = e.Account; // The account is passed through and saved here
            taskSource = new UI.AccountTaskSource(currentAccount);
            sidePanel.Visible = true; // once login is clicked the sidepanel returns
            ClientSize = mainWindowSize; // client size window returns to normal
            ShowDashboard(); // dashboard is shown in the main panel
        }
        private void ShowDashboard()
        {
            if (currentAccount != null)
            {
                Navigate(true);
            }
        }
        private void ShowPage(UserControl page) // function for loading each page onto the main panel
        {
            // release outgoing controls instead of leaving detached pages alive.
            while (mainPanel.Controls.Count > 0)
            {
                var previous = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previous);
                previous.Dispose();
            }
            page.Dock = DockStyle.Fill; // It docks the new page to the panel
            mainPanel.Controls.Add(page);
        }
        private void MainForm_Load(object sender, EventArgs e){}
        private void dashBoardButton_Click(object sender, EventArgs e){ShowDashboard();}
        private void tasksButton_Click(object sender, EventArgs e){
            if (currentAccount != null)
            {// Brings account info over to tasks, may have to do this for the rest of them. or just use JSON. He's a good guy
                Navigate(false);
            }
        }
        // Clean up later, no touch
        private void goalsButton_Click(object sender, EventArgs e){ShowPage(new GoalsControl());}
        private void calendarButton_Click(object sender, EventArgs e){ShowPage(new CalendarControl());}
        private void historyButton_Click(object sender, EventArgs e){ShowPage(new HistoryControl());}
        private void settingsButton_Click(object sender, EventArgs e){ShowPage(new TasksControl());}
        private void sidePanel_Paint(object sender, PaintEventArgs e){}
        private void mainPanel_Paint(object sender, PaintEventArgs e){}
    }
}
