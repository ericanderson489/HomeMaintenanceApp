namespace HomeMaintenanceApp
{
    internal partial class MaintenanceTaskControl : UserControl
    {
        private Account? m_account;
        internal event EventHandler? TaskAdded;
        internal event EventHandler? AddTaskClosed;
        public MaintenanceTaskControl()
        {
            InitializeComponent();
            addTaskTypeComboBox.Items.Clear();
            addTaskTypeComboBox.Items.AddRange(TaskRules.Types.Cast<object>().ToArray());
        }
        internal MaintenanceTaskControl(Account account) : this() { m_account = account; }
        private void doneTaskButton_Click(object sender, EventArgs e)
        {
            if (m_account is null) return;
            try
            {
                m_account.AddTask(taskTitleBox.Text, textBox1.Text,
                    addTaskTypeComboBox.SelectedItem as string ?? "", dateTimePicker1.Value);
            }
            catch (ArgumentException ex) { MessageBox.Show(ex.Message); return; }
            catch (Exception ex) when (ex is IOException or InvalidDataException or UnauthorizedAccessException)
            {
                MessageBox.Show("The task could not be saved. Your entries are still here. Check storage or reopen the app if another session changed this account.");
                return;
            }
            MessageBox.Show("Task added!");
            TaskAdded?.Invoke(this, EventArgs.Empty);
        }
        private void closeButton_Click(object sender, EventArgs e) => AddTaskClosed?.Invoke(this, EventArgs.Empty);
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void taskTitleBox_TextChanged(object sender, EventArgs e) { }
    }
}
