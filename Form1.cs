namespace HomeMaintenanceApp
{
    public partial class MainForm : Form
    {
        private Size mainWindowSize; //this saves client size window, is used to adjust size after login
        public MainForm()
        {
            InitializeComponent();
            mainWindowSize = ClientSize; // this is where the client size window is saved
            ShowLogin(); // login window is shown on start up
        }
        private void Login_LoginSuccessful(object? sender, EventArgs e)
        {
            sidePanel.Visible = true; // once login is clicked the sidepanel returns
            ClientSize = mainWindowSize; // client size window returns to normal
            ShowPage(new DashboardControl()); // dashboard is shown in the main panel
        }
        private void ShowLogin()
        {
            sidePanel.Visible = false;
            LoginControl login = new LoginControl();
            ClientSize = login.Size; // Starts client window to login window size
            login.Dock = DockStyle.Fill;
            
            login.LoginSuccessful += Login_LoginSuccessful;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(login);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void ShowPage(UserControl page) // function for loading each page onto the main panel
        {
            mainPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }
        private void dashBoardButton_Click(object sender, EventArgs e)
        {
            ShowPage(new DashboardControl());
        }

        private void tasksButton_Click(object sender, EventArgs e)
        {
            ShowPage(new TasksControl());
        }

        private void goalsButton_Click(object sender, EventArgs e)
        {
            ShowPage(new GoalsControl());
        }

        private void calendarButton_Click(object sender, EventArgs e)
        {
            ShowPage(new CalendarControl());
        }

        private void historyButton_Click(object sender, EventArgs e)
        {
            ShowPage(new HistoryControl());
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            ShowPage(new TasksControl());
        }

        private void sidePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
