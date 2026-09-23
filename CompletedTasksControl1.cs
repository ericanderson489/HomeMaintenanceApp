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
    public partial class CompletedTasksControl1 : UserControl
    {
        private Account? m_account;
        private List<string> taskName = new List<string>();
        public CompletedTasksControl1()
        {
            InitializeComponent();
        }

        private void Check_Click(object sender, EventArgs e)
        {
            // Task to complete name
            string toComplete = TaskNameDropdown.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(toComplete))
            {
                foreach (var task in m_account.GetTaskList())
                {
                    if (task.GetName() == toComplete)
                    {
                        // Completes task
                        task.SetStatus(Status.Complete);
                        MessageBox.Show("Task marked as complete.");

                        // Selects completed task to remove and removes it
                        var selectedItem = (string)TaskNameDropdown.SelectedItem;
                        taskName.Remove(selectedItem);

                        // Resets the dropdown
                        TaskNameDropdown.DataSource = null;
                        TaskNameDropdown.DataSource = taskName;

                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("No task to complete yet!");
            }
        }
        internal CompletedTasksControl1(Account account) : this()
        {
            m_account = account;

            // Initialize list of task names for combo box
            for (int i = 0; i < m_account.GetTaskList().Count; i++)
            {
                taskName.Add(m_account.GetTaskAt(i).GetName());
            }

            // Set combo box data source to task names
            TaskNameDropdown.DataSource = taskName;
        }
    }
}


