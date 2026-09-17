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


        public TasksControl(IReadOnlyList<UI.TaskViewData> tasks, Action add, Action<UI.TaskViewData> details)
        {
            Font = new Font("Segoe UI", 11);
            var page = UI.WorkshopStyle.Page(this, "YOUR MAINTENANCE TASKS", "All the jobs around your home, in one place.");
            var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            body.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            body.Controls.Add(UI.WorkshopStyle.Button("+ Add Task", add, true), 0, 0);
            body.Controls.Add(UI.WorkshopStyle.TaskList(tasks, details), 0, 1);
            page.Controls.Add(body, 0, 2);
        }
    }
}
