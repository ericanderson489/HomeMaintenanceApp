using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeMaintenanceApp
{
    public partial class CompleteTaskControl : UserControl
    {
        private Profile? m_profile;
        private List<string> taskName = new List<string>();
        public CompleteTaskControl()
        {
            InitializeComponent();  
        }

        public CompleteTaskControl(Profile profile) : this()
        {
            m_profile = profile;

            // Initialize list of task names for combo box
            for (int i = 0; i < m_profile.GetTaskList().Count; i++)
            {
                taskName.Add(m_profile.GetTaskAt(i).GetName());
            }

            // Set combo box data source to task names
            TaskNameDropdown.DataSource = taskName;
        }

        private void ShowPage(UserControl page)
        {
            CompleteTaskPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            CompleteTaskPanel.Controls.Add(page);
        }

        private void CheckIfValid_Click(object sender, EventArgs e)
        {
            // Task to complete name
            string toComplete = TaskNameDropdown.SelectedValue.ToString();
            
            if (!string.IsNullOrEmpty(toComplete))
            {
                foreach (var task in m_profile.GetTaskList())
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

        private void closeButton_Click(object sender, EventArgs e)
        {
            ShowPage(new TasksControl(m_profile));
        }

        private void panel1_Paint(object sender, PaintEventArgs e) // Ignore this
        {

        }
    }
}
