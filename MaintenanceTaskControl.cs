using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeMaintenanceApp
{
    public partial class MaintenanceTaskControl : UserControl
    {
        private Profile? m_profile;
        public MaintenanceTaskControl()
        {
            InitializeComponent();
        }

        public MaintenanceTaskControl(Profile profile) : this()
        {
            m_profile = profile;
        }

        private void ShowPage(UserControl page)
        {
            maintenanceTaskPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            maintenanceTaskPanel.Controls.Add(page);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
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

            // Adding task to list
            Tasks newTask = new Tasks(name, desc, type, date);
            m_profile.AddTask(newTask);
            MessageBox.Show("Task added successfully!");

            // Reset fields to default
            taskTitleBox.Text = "";
            descriptionBox.Text = "";
            taskTypeBox.Text = "";
            dateTimePicker1.Value = DateTime.Today;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // Description Box!
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            ShowPage(new TasksControl(m_profile));
        }
    }
}
