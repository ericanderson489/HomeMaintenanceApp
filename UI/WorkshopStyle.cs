// Program: Shared workshop colors and Windows Forms layout helpers.
// Author: Murdock MacAskill
// Date: 09/16/2026
namespace HomeMaintenanceApp.UI;

internal static class WorkshopStyle
{
    internal static readonly Color Navy = Color.FromArgb(32, 67, 91);
    internal static readonly Color Red = Color.FromArgb(165, 54, 44);
    internal static readonly Color Gold = Color.FromArgb(242, 205, 111);
    internal static readonly Color Paper = Color.FromArgb(255, 252, 243);
    internal static readonly Color Wood = Color.FromArgb(188, 142, 84);
    internal static readonly Color Pegboard = Color.FromArgb(234, 216, 185);

    /// <summary>Creates an autosizing, readable label; headings use a bold workshop face.</summary>
    internal static Label Label(string text, float size = 11, bool heading = false) => new()
    {
        Text = text, AutoSize = true, ForeColor = Navy, BackColor = Color.Transparent,
        Font = new Font(heading ? "Arial Narrow" : "Segoe UI", size, heading ? FontStyle.Bold : FontStyle.Regular),
        Margin = new Padding(0, 4, 0, 8)
    };

    /// <summary>Creates a native keyboard-accessible button and attaches its action.</summary>
    internal static Button Button(string text, Action action, bool primary = false)
    {
        var button = new Button
        {
            Text = text, AutoSize = true, MinimumSize = new Size(110, 42),
            Padding = new Padding(12, 6, 12, 6), FlatStyle = FlatStyle.Flat,
            BackColor = primary ? Gold : Paper, ForeColor = Navy,
            Margin = new Padding(0, 4, 10, 4), UseVisualStyleBackColor = false
        };
        button.Click += (_, _) => action();
        return button;
    }

    /// <summary>Creates a single-column layout. The last row fills available space.</summary>
    internal static TableLayoutPanel Page(Control owner, string title, string subtitle)
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3,
            Padding = new Padding(24), BackColor = Pegboard
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Paint += (_, e) =>
        {
            using var holes = new SolidBrush(Color.FromArgb(192, 167, 126));
            using var rail = new SolidBrush(Wood);
            for (int y = 16; y < layout.Height; y += 18)
                for (int x = 10; x < layout.Width; x += 18)
                    e.Graphics.FillEllipse(holes, x, y, 2, 2);
            e.Graphics.FillRectangle(rail, 0, 0, layout.Width, 7);
        };
        var heading = Label(title, 20, true);
        heading.BackColor = Paper;
        heading.Padding = new Padding(8);
        layout.SizeChanged += (_, _) => heading.MaximumSize = new Size(Math.Max(100, layout.ClientSize.Width - layout.Padding.Horizontal), 0);
        layout.Controls.Add(heading, 0, 0);
        layout.Controls.Add(Label(subtitle), 0, 1);
        owner.Controls.Add(layout);
        return layout;
    }

    /// <summary>Builds a task list with an explicit details action; no row edits are allowed.</summary>
    internal static Control TaskList(IEnumerable<TaskViewData> tasks, Action<TaskViewData> openTask)
    {
        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, BackColor = Paper, Padding = new Padding(12) };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        var list = new ListView
        {
            View = View.Details, FullRowSelect = true, MultiSelect = false, HideSelection = false,
            Dock = DockStyle.Fill, BackColor = Paper, ForeColor = Navy, BorderStyle = BorderStyle.None,
            AccessibleName = "Maintenance tasks", HeaderStyle = ColumnHeaderStyle.Nonclickable
        };
        list.Columns.Add("Task", 300);
        list.Columns.Add("Due date", 140);
        list.Columns.Add("Status", 120);
        foreach (var task in tasks)
        {
            var item = new ListViewItem(task.Title) { Tag = task };
            item.SubItems.Add(task.DueDate.ToString("MMM d, yyyy"));
            item.SubItems.Add(task.IsComplete ? "Completed" : "To do");
            list.Items.Add(item);
        }
        void SizeColumns()
        {
            list.Columns[1].Width = 140;
            list.Columns[2].Width = 120;
            list.Columns[0].Width = Math.Max(140, list.ClientSize.Width - 290);
        }
        list.Resize += (_, _) => SizeColumns();
        list.HandleCreated += (_, _) => list.BeginInvoke((Action)SizeColumns);
        var open = Button("View details", () =>
        {
            if (list.SelectedItems.Count == 1 && list.SelectedItems[0].Tag is TaskViewData task) openTask(task);
        });
        open.Enabled = false;
        list.SelectedIndexChanged += (_, _) => open.Enabled = list.SelectedItems.Count == 1;
        list.ItemActivate += (_, _) => open.PerformClick();
        panel.Controls.Add(list, 0, 0);
        var footer = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        footer.Controls.Add(open);
        footer.Controls.Add(Label(list.Items.Count == 0 ? "No tasks yet. Add a task to get started." : "Select a task to view its details."));
        panel.Controls.Add(footer, 0, 1);
        return panel;
    }
}
