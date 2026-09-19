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
        public EditTask()
        {
            InitializeComponent();
        }

        public EditTask(Profile profile) : this()
        {
            m_profile = profile;
            dateTimePicker1.Enabled = false;
            descriptionBox.Enabled = false;
            taskTypeBox.Enabled = false;
            doneTaskButton.Enabled = false;
        }

        private void taskTypeLabel_Click(object sender, EventArgs e) // Ignore this
        {

        }

        private void doneTaskButton_Click(object sender, EventArgs e)
        {
            // Input validation
            if (taskTitleBox.Text == "")
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

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

            string name = taskTitleBox.Text;
            string desc = descriptionBox.Text;
            string type = taskTypeBox.Text;
            DateTime date = selectedDate;

            // Edit task
            m_profile.EditTask(name, desc, type, selectedDate);
            MessageBox.Show("Task edited successfully!");

            // Reset fields to default
            taskTitleBox.Text = "";
            descriptionBox.Text = "";
            taskTypeBox.Text = "";
            dateTimePicker1.Value = DateTime.Today;
        }

        private void CheckIfValid_Click(object sender, EventArgs e)
        {
            bool isValid = false;
            foreach (var task in m_profile.GetTaskList())
            {
                if (task.GetName().ToLower().Trim() == taskTitleBox.Text.ToLower().Trim())
                {
                    isValid = true;
                }
            }

            if (isValid)
            {
                dateTimePicker1.Enabled = true;
                descriptionBox.Enabled = true;
                taskTypeBox.Enabled = true;
                doneTaskButton.Enabled = true;
                MessageBox.Show("Task found! Continue to edit.");
            }
            else
            {
                MessageBox.Show("No task with that name in profile!");
            }
        }
    }
}
