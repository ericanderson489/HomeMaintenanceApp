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
    public partial class EditTask : UserControl
    {
        private Profile? m_profile;
        private List<string> taskName = new List<string>();
        public EditTask()
        {
            InitializeComponent();
        }

        public EditTask(Profile profile) : this()
        {
            m_profile = profile;

            // Initialize list of task names to be used as data source for combo box
            for (int i = 0; i < m_profile.GetTaskList().Count; i++)
            {
                if (m_profile.GetTaskAt(i).GetStatus() != Status.Complete)
                {
                    taskName.Add(m_profile.GetTaskAt(i).GetName());
                }
            }

            // Setting data source for combo box
            TaskNameDropdown.DataSource = taskName;

            dateTimePicker1.Enabled = false;
            descriptionBox.Enabled = false;
            taskTypeBox.Enabled = false;
            doneTaskButton.Enabled = false;
        }

        private void ShowPage(UserControl page)
        {
            EditTaskPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            EditTaskPanel.Controls.Add(page);
        }

        private void taskTypeLabel_Click(object sender, EventArgs e) // Ignore this
        {

        }

        private void doneTaskButton_Click(object sender, EventArgs e)
        {
            // Input validation

            if (descriptionBox.Text == "")
            {
                MessageBox.Show("Description cannot be empty.");
                return;
            }

            if (taskTypeBox.Text == "")
            {
                MessageBox.Show("Task type cannot be empty.");
                return;
            }

            DateTime selectedDate = dateTimePicker1.Value;
            if (selectedDate < DateTime.Today)
            {
                MessageBox.Show("Due date cannot be in the past.");
                return;
            }

            string desc = descriptionBox.Text;
            string type = taskTypeBox.Text;
            DateTime date = selectedDate;

            // Edit task
            m_profile.EditTask(TaskNameDropdown.SelectedValue.ToString(), desc, type, selectedDate);
            MessageBox.Show("Task edited successfully!");

            // Reset fields to default
            descriptionBox.Text = "";
            taskTypeBox.Text = "";
            dateTimePicker1.Value = DateTime.Today;
        }

        private void CheckIfValid_Click(object sender, EventArgs e)
        {
            // Name of task to edit
            string toEdit = TaskNameDropdown.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(toEdit))
            {
                // Find task in list and populate fields to be edited
                foreach (var task in m_profile.GetTaskList())
                {
                    if (task.GetName() == toEdit)
                    {
                        descriptionBox.Text = task.GetDescription();
                        taskTypeBox.Text = task.GetTaskType();
                        dateTimePicker1.Value = task.GetDate();

                        dateTimePicker1.Enabled = true;
                        descriptionBox.Enabled = true;
                        taskTypeBox.Enabled = true;
                        doneTaskButton.Enabled = true;

                        MessageBox.Show("Proceed to edit.");

                        break;
                    }
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            ShowPage(new TasksControl(m_profile));
        }
    }
}
