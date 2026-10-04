namespace HomeMaintenanceApp
{
    internal partial class EditTask : UserControl
    {
        internal event EventHandler? EditClosed;
        private Account? m_account;
        private List<string> taskNames = new List<string>();

        public EditTask()
        {
            InitializeComponent();
        }
        internal EditTask(Account account) : this()
        {
            m_account = account;
            foreach (Tasks task in m_account.GetTaskList())
            {// Add all incomplete tasks to the dropdown
                if (task.GetStatus() != Status.Complete)
                {
                    taskNames.Add(task.GetName());
                }
            }
            TaskNameDropdown.DataSource = taskNames;
            CategoryComboBox.DataSource = Enum.GetValues(typeof(Category));
        }
        private void TaskNameDropdown_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (m_account == null || TaskNameDropdown.SelectedItem == null) { return; }

            string selectedTask = TaskNameDropdown.SelectedItem.ToString()!;
            foreach (Tasks task in m_account.GetTaskList())
            {// Find the selected task and display its information
                if (task.GetName() == selectedTask)
                {
                    editTextBox.Text = task.GetDescription();
                    taskTypeComboBox.SelectedItem = task.GetTaskType();
                    editCalendar.Value = task.GetDate();
                    CategoryComboBox.SelectedItem = task.GetCategory();

                    taskCheckBox.Checked = task.GetStatus() == Status.Complete;
                    return;
                }
            }
        }
        private void editTaskButton_Click(object sender, EventArgs e)
        {
            if (m_account == null || TaskNameDropdown.SelectedItem == null)
            {
                MessageBox.Show("Please select a task");
                return;
            }
            if (string.IsNullOrWhiteSpace(editTextBox.Text) ||
                taskTypeComboBox.SelectedItem == null ||
                CategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill out all fields");
                return;
            }
            if (editCalendar.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Due date cannot be in the past");
                return;
            }
            string selectedTask = TaskNameDropdown.SelectedItem.ToString()!;
            foreach (Tasks task in m_account.GetTaskList())
            {// Finds selected task and saves changes
                if (task.GetName() == selectedTask)
                {
                    task.SetDescription(editTextBox.Text);
                    task.SetType(taskTypeComboBox.SelectedItem.ToString()!);
                    task.SetDate(editCalendar.Value);
                    task.SetCategory((Category)CategoryComboBox.SelectedItem);
                    // Mark task complete if checked
                    if (taskCheckBox.Checked)
                    {
                        task.SetStatus(Status.Complete);
                    }
                    // Delete task if checked
                    if (DeleteCheckBox.Checked)
                    {
                        m_account.DeleteTask(TaskNameDropdown.SelectedItem.ToString());
                        MessageBox.Show("Task deleted");
                        // Tells task control that edit is done
                        EditClosed?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                    MessageBox.Show("Task updated");
                    // Tells task control that edit is done
                    EditClosed?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        private void closeEditButton_Click(object sender, EventArgs e) { EditClosed?.Invoke(this, EventArgs.Empty); }
        // Clean up later, no worries, no touch
        private void editCalendar_ValueChanged(object sender, EventArgs e) { }
        private void taskCheckBox_CheckedChanged(object sender, EventArgs e) { }
        private void editTaskTitleLabel_Click(object sender, EventArgs e) { }
        private void editTitleBox_TextChanged(object sender, EventArgs e) { }
        private void editDueDateLabel_Click(object sender, EventArgs e) { }
        private void editTextBox_TextChanged(object sender, EventArgs e) { }
        private void descriptionLabel_Click(object sender, EventArgs e) { }
        private void DeleteCheckBox_CheckedChanged(object sender, EventArgs e) { }
    }
}