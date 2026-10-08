using HomeMaintenanceApp.UI;

namespace HomeMaintenanceApp;

internal sealed class WorkshopDashboardControl : UserControl
{
    /// <summary>Summarizes live account tasks and puts the earliest unfinished jobs first.</summary>
    public WorkshopDashboardControl(IReadOnlyList<TaskViewData> tasks, Action add, Action viewAll, Action<TaskViewData> details)
    {
        Font = new Font("Segoe UI", 10);
        var page = WorkshopStyle.Page(this, "Back in the workshop.", "Here’s what needs your attention.", add);
        var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Margin = Padding.Empty };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int row = 0; row < 4; row++) body.RowStyles.Add(new RowStyle(row == 3 ? SizeType.Percent : SizeType.AutoSize, row == 3 ? 100 : 0));
        var stats = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 4, BackColor = WorkshopStyle.Paper, CellBorderStyle = TableLayoutPanelCellBorderStyle.Single, Margin = new Padding(0, 8, 0, 14) };
        tasks = tasks.Where(t => !t.IsArchived).ToArray();
        var active = tasks.Where(t => !t.IsComplete).ToArray();
        var values = new[] { ("On the bench", active.Length), ("Due soon", active.Count(t => t.DueDate.Date >= DateTime.Today && t.DueDate.Date <= DateTime.Today.AddDays(6))), ("Overdue", active.Count(t => t.DueDate.Date < DateTime.Today)), ("Finished", tasks.Count(t => t.IsComplete)) };
        for (int i = 0; i < values.Length; i++)
        {
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            var label = WorkshopStyle.Label($"{values[i].Item2}  {values[i].Item1}", 13, true);
            label.Dock = DockStyle.Fill;
            label.Padding = new Padding(8, 10, 8, 10);
            if (i == 2) label.ForeColor = WorkshopStyle.Red;
            stats.Controls.Add(label, i, 0);
        }
        body.Controls.Add(stats, 0, 1);
        var header = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, BackColor = WorkshopStyle.Navy, Padding = new Padding(10, 4, 10, 4), Margin = Padding.Empty };
        var title = WorkshopStyle.Label("UPCOMING JOBS", 15, true);
        title.ForeColor = WorkshopStyle.Paper;
        title.Margin = new Padding(0, 10, 20, 0);
        header.Controls.Add(title);
        header.Controls.Add(WorkshopStyle.Button("View all tasks", viewAll));
        body.Controls.Add(header, 0, 2);
        body.Controls.Add(WorkshopStyle.TaskList(active.OrderBy(t => t.DueDate).Take(4), details), 0, 3);
        page.Controls.Add(body, 0, 2);
    }
}
