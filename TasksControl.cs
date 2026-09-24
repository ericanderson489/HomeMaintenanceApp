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
    internal partial class TasksControl : UserControl
    {
        private Account? m_account;
        public TasksControl()
        {
            InitializeComponent();
        }
        internal TasksControl(Account account) : this()
        {
            m_account = account;
            DisplayTasks();
        }
        private void ShowPage(UserControl page)
        {
            page.Dock = DockStyle.Fill;
            tasksControlPanel.Controls.Add(page);
            page.BringToFront();
        }
        private void addTaskButton_Click(object sender, EventArgs e)
        {
            if (m_account == null) { return; }

            MaintenanceTaskControl addTask = new MaintenanceTaskControl(m_account);
            addTask.TaskAdded += ReturnToTasks;
            addTask.AddTaskClosed += ReturnToTasks;
            ShowPage(addTask);
        }
        private void editTaskButton_Click(object sender, EventArgs e)
        {
            if (m_account == null) { return; }

            EditTask editTask = new EditTask(m_account);

            editTask.EditClosed += ReturnToTasks;

            ShowPage(editTask);
        }
        private void ReturnToTasks(object? sender, EventArgs e)
        {
            if (sender is UserControl page)
            {
                tasksControlPanel.Controls.Remove(page);
                page.Dispose();
            }
            DisplayTasks();
        }
        private void DisplayTasks()
        {
            if (m_account == null) { return; } 
            
            flowPanelPriorityTasks.Controls.Clear();
            foreach (Tasks task in m_account.GetTaskList())
            {
                if (task.GetStatus() == Status.Complete) { continue; }
                // Creates a small panel for each task
                Panel taskPanel = new Panel();
                taskPanel.Width = flowPanelPriorityTasks.ClientSize.Width - 25;
                taskPanel.Height = 55;
                // Shows task name
                Label nameLabel = new Label();
                nameLabel.Text = task.GetName();
                nameLabel.AutoSize = true;
                nameLabel.Location = new Point(10, 8);
                // Displays due date
                Label dateLabel = new Label();
                dateLabel.Text = $"Due: {task.GetDate():MM/dd/yyyy}";
                dateLabel.AutoSize = true;
                dateLabel.Location = new Point(10, 30);
                taskPanel.Controls.Add(nameLabel);
                taskPanel.Controls.Add(dateLabel);
                flowPanelPriorityTasks.Controls.Add(taskPanel);
            }
        }
        private void tasksControlPanel_Paint(object sender, PaintEventArgs e){}
    }
}