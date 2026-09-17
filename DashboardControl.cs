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
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
        }


        public DashboardControl(IReadOnlyList<UI.TaskViewData> tasks, Action add, Action viewAll, Action<UI.TaskViewData> details)
        {
            Font = new Font("Segoe UI", 11);
            var page = UI.WorkshopStyle.Page(this, "WELCOME TO YOUR WORKSHOP", "A clear plan for a well-cared-for home.");
            var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            body.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            body.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            var banner = new TableLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, ColumnCount = 1, BackColor = UI.WorkshopStyle.Navy, Padding = new Padding(20), Margin = new Padding(0, 12, 0, 16) };
            var heading = UI.WorkshopStyle.Label("ON THE WORKBENCH", 18, true);
            heading.ForeColor = UI.WorkshopStyle.Gold;
            var summary = UI.WorkshopStyle.Label($"{tasks.Count(t => !t.IsComplete)} to do     /     {tasks.Count(t => t.IsComplete)} completed", 12);
            summary.ForeColor = UI.WorkshopStyle.Paper;
            banner.Controls.Add(heading);
            banner.Controls.Add(summary);
            banner.Controls.Add(UI.WorkshopStyle.Button("+ Add Task", add, true));
            body.Controls.Add(banner, 0, 0);
            var actions = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill };
            actions.Controls.Add(UI.WorkshopStyle.Label("YOUR NEXT TASKS", 16, true));
            actions.Controls.Add(UI.WorkshopStyle.Button("View all tasks", viewAll));
            body.Controls.Add(actions, 0, 1);
            body.Controls.Add(UI.WorkshopStyle.TaskList(tasks.Where(t => !t.IsComplete).OrderBy(t => t.DueDate).Take(3), details), 0, 2);
            page.Controls.Add(body, 0, 2);
        }
    }
}
