namespace HomeMaintenanceApp
{
    public partial class MainForm : Form
    {
        public MainForm() : this(new UI.PreviewTaskSource()) { }

        public MainForm(UI.ITaskPresentationSource taskSource)
        {
            this.taskSource = taskSource ?? throw new ArgumentNullException(nameof(taskSource));
            InitializeComponent();
            ApplyWorkshopStyle();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Navigate(true); //loads dashboard on launch
        }

        private void ShowPage(UserControl page)
        {
            // Dispose outgoing pages so navigation does not retain controls and handles.
            while (mainPanel.Controls.Count > 0)
            {
                var previous = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previous);
                previous.Dispose();
            }
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }
        private void dashBoardButton_Click(object sender, EventArgs e)
        {
            Navigate(true);
        }

        private void tasksButton_Click(object sender, EventArgs e)
        {
            Navigate(false);
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
