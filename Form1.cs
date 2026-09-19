namespace HomeMaintenanceApp
{
    public partial class MainForm : Form
    {
        private readonly Profile m_profile;
        public MainForm(Profile profile)
        {
            InitializeComponent();

            m_profile = profile;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowPage(new DashboardControl()); //loads dashboard on launch
        }

        private void ShowPage(UserControl page)
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
            ShowPage(new TasksControl(m_profile));
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
