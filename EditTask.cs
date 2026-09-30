namespace HomeMaintenanceApp
{
    internal partial class EditTask : UserControl
    {
        internal event EventHandler? EditClosed;
        private Account? m_account;
        public EditTask()
        {
            InitializeComponent();
            taskTypeComboBox.Items.Clear();
            taskTypeComboBox.Items.AddRange(TaskRules.Types.Cast<object>().ToArray());
        }
        internal EditTask(Account account) : this()
        {
            m_account = account;
            // Bind objects, since two tasks may have the same display title.
            TaskNameDropdown.DataSource = account.GetTaskList().Where(t => t.GetStatus() != Status.Complete).ToList();
        }
        private void TaskNameDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TaskNameDropdown.SelectedItem is not Tasks task) return;
            editTextBox.Text = task.GetDescription();
            taskTypeComboBox.SelectedItem = task.GetTaskType();
            editCalendar.Value = task.GetDate();
            taskCheckBox.Checked = task.GetStatus() == Status.Complete;
        }
        private void editTaskButton_Click(object sender, EventArgs e)
        {
            if (m_account is null || TaskNameDropdown.SelectedItem is not Tasks task)
            {
                MessageBox.Show("Please select a task.");
                return;
            }
            try
            {
                m_account.UpdateTask(task.Id, task.GetName(), editTextBox.Text,
                    taskTypeComboBox.SelectedItem as string ?? "", editCalendar.Value, taskCheckBox.Checked);
            }
            catch (ArgumentException ex) { MessageBox.Show(ex.Message); return; }
            catch (Exception ex) when (ex is IOException or InvalidDataException or UnauthorizedAccessException)
            {
                MessageBox.Show("The task could not be saved. Your entries are still here. Check storage or reopen the app if another session changed this account.");
                return;
            }
            MessageBox.Show("Task updated");
            EditClosed?.Invoke(this, EventArgs.Empty);
        }
        private void closeEditButton_Click(object sender, EventArgs e) { EditClosed?.Invoke(this, EventArgs.Empty); }
        private void editCalendar_ValueChanged(object sender, EventArgs e) { }
        private void taskCheckBox_CheckedChanged(object sender, EventArgs e) { }
        private void editTaskTitleLabel_Click(object sender, EventArgs e) { }
        private void editTitleBox_TextChanged(object sender, EventArgs e) { }
        private void editDueDateLabel_Click(object sender, EventArgs e) { }
        private void editTextBox_TextChanged(object sender, EventArgs e) { }
        private void descriptionLabel_Click(object sender, EventArgs e) { }
    }
}
