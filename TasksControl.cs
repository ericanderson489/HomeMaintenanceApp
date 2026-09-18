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
        public TasksControl()
        {
            InitializeComponent();
        }

        private void ShowPage(UserControl page)
        {
            tasksControlPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            tasksControlPanel.Controls.Add(page);
        }

        private void addTaskButton_Click(object sender, EventArgs e)
        {
            ShowPage(new MaintenanceTaskControl());
        }
    }
}
