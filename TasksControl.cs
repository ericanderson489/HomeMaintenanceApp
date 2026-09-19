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
    public partial class TasksControl : UserControl
    {
        private Profile? m_profile;
        public TasksControl()
        {
            InitializeComponent();
        }

        public TasksControl(Profile profile) : this()
        {
            m_profile = profile;
        }

        private void ShowPage(UserControl page)
        {
            tasksControlPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            tasksControlPanel.Controls.Add(page);
        }

        private void addTaskButton_Click(object sender, EventArgs e)
        {
            ShowPage(new MaintenanceTaskControl(m_profile));
        }

        private void editTask_Click(object sender, EventArgs e)
        {
            ShowPage(new EditTask(m_profile));
        }

        private void flowPanelPriorityTasks_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CompleteTask_Click(object sender, EventArgs e)
        {
            ShowPage(new CompleteTaskControl(m_profile));
        }
    }
}
