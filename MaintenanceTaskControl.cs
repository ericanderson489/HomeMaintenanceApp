namespace HomeMaintenanceApp
{
    internal partial class MaintenanceTaskControl : UserControl
    {
        private Account? m_account;
        // Notify the Tasks page when this page is finished.
        internal event EventHandler? TaskAdded;
        internal event EventHandler? AddTaskClosed;
        public MaintenanceTaskControl()
        {
            InitializeComponent();
        }
        internal MaintenanceTaskControl(Account account) : this()
        {
            m_account = account;
        }
        private void doneTaskButton_Click(object sender, EventArgs e)
        {
            if (m_account == null)
            {
                return;
            }

            // Make sure all fields are filled out
            if (string.IsNullOrWhiteSpace(taskTitleBox.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(tasktypetextBox.Text))
            {
                MessageBox.Show("Please fill out all task fields.");
                return;
            }

            DateTime dueDate = dateTimePicker1.Value;

            if (dueDate < DateTime.Today)
            {
                MessageBox.Show("Due date cannot be in the past.");
                return;
            }

            // Add the new task to the logged-in account.
            m_account.AddTask(
                taskTitleBox.Text,
                textBox1.Text,
                tasktypetextBox.Text,
                dueDate);

            MessageBox.Show("Task added!");

            // Tell the Tasks page that a task was successfully created.
            TaskAdded?.Invoke(this, EventArgs.Empty);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            // Return to the Tasks page without adding a task.
            AddTaskClosed?.Invoke(this, EventArgs.Empty);
        }

        private void label2_Click(object sender, EventArgs e) { }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void taskTitleBox_TextChanged(object sender, EventArgs e) { }

        private void tasktypetextBox_TextChanged(object sender, EventArgs e) { }
    }
}