namespace HomeMaintenanceApp
{
    internal partial class EditTask : UserControl
    {
        private Account? m_account;
        private List<string> taskNames = new List<string>();

        public EditTask()
        {
            InitializeComponent();
        }

        public EditTask(Account account) : this()
        {
            m_account = account;
            // Adds incomplete tasks names to drop down
            for (int i = 0; i < m_account.GetTaskList().Count; i++)
            {
                if (m_account.GetTaskAt(i).GetStatus() != Status.Complete)
                {
                    taskNames.Add(m_account.GetTaskAt(i).GetName());
                }
            }
            // Displays available tasks in combobox
            TaskNameDropdown.DataSource = taskNames; 
            editCalendar.Enabled = false;
            editTextBox.Enabled = false;
            taskTypeBox.Enabled = false;
            editTaskButton.Enabled = false;
        }
        private void CheckIfValid_Click(object sender, EventArgs e)
        {
            // Make sure a task is actually selected.
            if (TaskNameDropdown.SelectedItem == null || m_account == null) 
            {
                MessageBox.Show("Please select a task.");
                return;
            }
            string toEdit = TaskNameDropdown.SelectedItem.ToString()!; 
            // Finds and displays specific task
            foreach (Tasks task in m_account.GetTaskList())
            {
                if (task.GetName() == toEdit)
                {
                    editTextBox.Text = task.GetDescription();
                    taskTypeBox.Text = task.GetTaskType();
                    editCalendar.Value = task.GetDate();

                    editCalendar.Enabled = true;
                    editTextBox.Enabled = true;
                    taskTypeBox.Enabled = true;
                    editTaskButton.Enabled = true;

                    MessageBox.Show("Proceed to edit.");
                    return;
                }
            }
        }
        private void doneTaskButton_Click(object sender, EventArgs e)
        {
            if (m_account == null ||
                TaskNameDropdown.SelectedItem == null) 
            {
                MessageBox.Show("Please select a task.");
                return;
            }

            if (string.IsNullOrWhiteSpace(editTextBox.Text))
            {
                MessageBox.Show("Description cannot be empty.");
                return;
            }
            if (string.IsNullOrWhiteSpace(taskTypeBox.Text))
            {
                MessageBox.Show("Task type cannot be empty.");
                return;
            }
            DateTime selectedDate = editCalendar.Value;

            if (selectedDate < DateTime.Today)
            {
                MessageBox.Show("Due date cannot be in the past.");
                return;
            }
            string taskName = TaskNameDropdown.SelectedItem.ToString()!; // I think he means a combo box, do not have - EA
            string description = editTextBox.Text;
            string type = taskTypeBox.Text;

            // Update the selected task.
            m_account.EditTask(
                taskName,
                description,
                type,
                selectedDate);

            MessageBox.Show("Task edited successfully!");

            editTextBox.Text = "";
            taskTypeBox.Text = "";
            editCalendar.Value = DateTime.Today;

            editCalendar.Enabled = false;
            editTextBox.Enabled = false;
            taskTypeBox.Enabled = false;
            editTaskButton.Enabled = false;
        }
        private void editTaskButton_Click(object sender, EventArgs e) { }
        private void editTaskTitleLabel_Click(object sender, EventArgs e) { }
        private void editTitleBox_TextChanged(object sender, EventArgs e) { }
        private void editDueDateLabel_Click(object sender, EventArgs e) { }
        private void editCalendar_ValueChanged(object sender, EventArgs e) { }
        private void editTextBox_TextChanged(object sender, EventArgs e) { }
        private void descriptionLabel_Click(object sender, EventArgs e) { }
        private void closeEditButton_Click(object sender, EventArgs e) { }
    }
}