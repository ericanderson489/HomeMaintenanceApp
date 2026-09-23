using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeMaintenanceApp
{
    internal partial class CompletedTasksControl1 : UserControl
    {
        private Account? m_account;
        private List<string> taskNames = new List<string>();

        public CompletedTasksControl1()
        {
            InitializeComponent();
        }
        internal CompletedTasksControl1(Account account) : this()
        {
            m_account = account;

            // Add incomplete tasks to drop down
            foreach (Tasks task in m_account.GetTaskList())
            {
                if (task.GetStatus() != Status.Complete)
                {
                    taskNames.Add(task.GetName());
                }
            }
            // Displays tasks in combobox 
            taskNameDropdown.DataSource = taskNames;
        }
        private void Check_Click(object sender, EventArgs e)
        {
            // Make sure an account and task have been selected
            if (m_account == null || taskNameDropdown.SelectedItem == null)
            {
                MessageBox.Show("No task to complete yet!");
                return;
            }

            string taskToComplete = taskNameDropdown.SelectedItem.ToString()!;

            // Find the selected task
            foreach (Tasks task in m_account.GetTaskList())
            {
                if (task.GetName() == taskToComplete)
                {
                    // Mark the task as complete
                    task.SetStatus(Status.Complete);

                    MessageBox.Show("Task marked as complete.");

                    // Remove the completed task from the dropdown
                    taskNames.Remove(taskToComplete);

                    // Refresh the ComboBox with the remaining tasks
                    taskNameDropdown.DataSource = null;
                    taskNameDropdown.DataSource = taskNames;

                    return;
                }
            }

            MessageBox.Show("Task could not be found.");
        }
        private void completedTitleTextBox_TextChanged(object sender, EventArgs e){}
        private void completedTaskTitleLabel_Click(object sender, EventArgs e){}
    }
}


