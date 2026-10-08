// Program: Shared workshop colors and Windows Forms layout helpers.
// Author: Murdock MacAskill
// Date: 09/16/2026
namespace HomeMaintenanceApp.UI;

internal static class WorkshopStyle
{
    internal static readonly Color Navy = Color.FromArgb(32, 62, 75);
    internal static readonly Color Ink = Color.FromArgb(25, 45, 53);
    internal static readonly Color Red = Color.FromArgb(184, 68, 50);
    internal static readonly Color Gold = Color.FromArgb(240, 190, 69);
    internal static readonly Color Paper = Color.FromArgb(255, 252, 245);
    internal static readonly Color Background = Color.FromArgb(244, 240, 230);
    internal static readonly Color Steel = Color.FromArgb(183, 192, 190);
    internal static readonly Color Muted = Color.FromArgb(82, 103, 113);
    internal static readonly Color Selected = Color.FromArgb(223, 233, 236);
    internal static readonly Color Wood = Gold;
    internal static readonly Color Pegboard = Background;

    /// <summary>Creates a readable label; Windows substitutes a missing condensed font.</summary>
    internal static Label Label(string text, float size = 11, bool heading = false) => new()
    {
        Text = text, AutoSize = true, ForeColor = Ink, BackColor = Color.Transparent,
        Font = new Font(heading ? "Bahnschrift SemiCondensed" : "Segoe UI", size, heading ? FontStyle.Bold : FontStyle.Regular),
        Margin = new Padding(0, 3, 0, 6), UseMnemonic = false
    };

    /// <summary>Enables access-key markers only on fixed form labels, never on user-entered task names.</summary>
    internal static Label FieldLabel(string text)
    {
        var label = Label(text);
        label.UseMnemonic = true;
        return label;
    }

    /// <summary>Styles an existing button without replacing its event handlers.</summary>
    internal static void StyleButton(Button button, bool primary = false)
    {
        button.AutoSize = true;
        button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        button.MinimumSize = new Size(90, 38);
        button.Padding = new Padding(10, 5, 10, 5);
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderColor = primary ? Color.FromArgb(184, 139, 43) : Steel;
        button.FlatAppearance.MouseOverBackColor = primary ? Color.FromArgb(246, 205, 104) : Background;
        button.BackColor = primary ? Gold : Paper;
        button.ForeColor = Ink;
        button.Font = new Font("Segoe UI", 10);
        button.Margin = new Padding(0, 3, 8, 3);
        button.UseVisualStyleBackColor = false;
    }

    /// <summary>Creates a native keyboard-accessible button and attaches its action.</summary>
    internal static Button Button(string text, Action action, bool primary = false)
    {
        var button = new Button { Text = text };
        StyleButton(button, primary);
        button.Click += (_, _) => action();
        return button;
    }

    /// <summary>Creates heading and ruler rows; the caller places filling content in row two.</summary>
    internal static TableLayoutPanel Page(Control owner, string title, string subtitle, Action? add = null)
    {
        var page = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3,
            Padding = new Padding(20, 16, 20, 16), BackColor = Background };
        page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        page.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var heading = Label(title, 22, true);
        var header = new TableLayoutPanel { AutoSize = true, Dock = DockStyle.Top, ColumnCount = 2, Margin = Padding.Empty };
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        header.Controls.Add(heading, 0, 0);
        if (add is not null) header.Controls.Add(Button("+ Add task", add, true), 1, 0);
        var sub = new TableLayoutPanel { AutoSize = true, Dock = DockStyle.Top, ColumnCount = 1, Margin = new Padding(0, 0, 0, 10) };
        var description = Label(subtitle, 10);
        description.ForeColor = Muted;
        sub.Controls.Add(description);
        sub.Controls.Add(new WorkshopRuler { Width = 140, Height = 12, Margin = new Padding(0, 3, 0, 4) });
        page.Controls.Add(header, 0, 0);
        page.Controls.Add(sub, 0, 1);
        page.SizeChanged += (_, _) =>
        {
            int width = Math.Max(80, page.ClientSize.Width - page.Padding.Horizontal);
            heading.MaximumSize = description.MaximumSize = new Size(width, 0);
        };
        owner.Controls.Add(page);
        return page;
    }

    /// <summary>Computes status using local dates. Completion takes precedence over an old due date.</summary>
    internal static string StatusText(TaskViewData task) => task.IsArchived ? (task.IsComplete ? "Archived / done" : "Archived") : task.IsComplete ? "Completed"
        : task.DueDate.Date < DateTime.Today ? "Overdue"
        : task.DueDate.Date == DateTime.Today ? "Due today"
        : task.DueDate.Date <= DateTime.Today.AddDays(6) ? "Due soon" : "To do";

    /// <summary>Builds a native task list with an explicit open action and keyboard activation.</summary>
    internal static Control TaskList(IEnumerable<TaskViewData> tasks, Action<TaskViewData> openTask, bool selectOpens = false,
        string emptyMessage = "No tasks yet. Add your first job.")
    {
        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, BackColor = Paper, Padding = new Padding(8), Margin = Padding.Empty, CellBorderStyle = TableLayoutPanelCellBorderStyle.Single };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        var list = new ListView { View = View.Details, FullRowSelect = true, MultiSelect = false, HideSelection = false,
            Dock = DockStyle.Fill, BackColor = Paper, ForeColor = Ink, BorderStyle = BorderStyle.None,
            AccessibleName = "Maintenance tasks", HeaderStyle = ColumnHeaderStyle.Nonclickable, Font = new Font("Segoe UI", 10) };
        list.Columns.Add("Task", 280);
        list.Columns.Add("Due date", 105);
        list.Columns.Add("Status", 100);
        foreach (var task in tasks)
        {
            string title = task.Repeat == RepeatSchedule.None ? task.Title : $"{task.Title}  ·  {Recurrence.Label(task.Repeat)}";
            var item = new ListViewItem(title) { Tag = task, ToolTipText = title };
            item.SubItems.Add(task.DueDate.ToString("MMM d, yyyy"));
            item.SubItems.Add(StatusText(task));
            list.Items.Add(item);
        }
        list.ShowItemToolTips = true;
        list.OwnerDraw = true;
        list.DrawColumnHeader += (_, e) =>
        {
            using var fill = new SolidBrush(SystemInformation.HighContrast ? SystemColors.Control : Steel);
            e.Graphics.FillRectangle(fill, e.Bounds);
            TextRenderer.DrawText(e.Graphics, e.Header?.Text ?? "", list.Font, Rectangle.Inflate(e.Bounds, -6, 0),
                SystemInformation.HighContrast ? SystemColors.ControlText : Ink, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        };
        list.DrawItem += (_, e) => { if (list.View != View.Details) e.DrawDefault = true; };
        list.DrawSubItem += (_, e) =>
        {
            bool selected = e.Item?.Selected == true;
            Color background = SystemInformation.HighContrast ? (selected ? SystemColors.Highlight : SystemColors.Window) : selected ? Selected : Paper;
            Color foreground = SystemInformation.HighContrast ? (selected ? SystemColors.HighlightText : SystemColors.WindowText) : Ink;
            using var fill = new SolidBrush(background);
            using var rule = new Pen(Steel);
            e.Graphics.FillRectangle(fill, e.Bounds);
            e.Graphics.DrawLine(rule, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
            TextRenderer.DrawText(e.Graphics, e.SubItem?.Text ?? "", list.Font, Rectangle.Inflate(e.Bounds, -6, 0), foreground,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine);
            if (selected && list.Focused) ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(e.Bounds, -1, -1));
        };
        void SizeColumns()
        {
            float scale = list.DeviceDpi / 96f;
            list.Columns[1].Width = (int)(105 * scale);
            list.Columns[2].Width = (int)(95 * scale);
            list.Columns[0].Width = Math.Max((int)(130 * scale), list.ClientSize.Width - (int)(220 * scale));
        }
        list.Resize += (_, _) => SizeColumns();
        list.HandleCreated += (_, _) => list.BeginInvoke((Action)(() => { if (!list.IsDisposed) SizeColumns(); }));
        list.DpiChangedAfterParent += (_, _) => SizeColumns();
        var open = Button("View details", () =>
        {
            if (list.SelectedItems.Count == 1 && list.SelectedItems[0].Tag is TaskViewData task) openTask(task);
        });
        open.Enabled = false;
        list.SelectedIndexChanged += (_, _) =>
        {
            open.Enabled = list.SelectedItems.Count == 1;
            if (selectOpens && open.Enabled) open.PerformClick();
        };
        list.ItemActivate += (_, _) =>
        {
            if (list.Disposing || list.IsDisposed || list.SelectedItems.Count != 1
                || list.SelectedItems[0].Tag is not TaskViewData task) return;
            // Opening a dashboard job disposes this list. Let its native mouse/key
            // event finish before navigating, and discard work from a closed page.
            list.BeginInvoke((Action)(() =>
            {
                if (!list.Disposing && !list.IsDisposed && panel.Parent is not null)
                    openTask(task);
            }));
        };
        panel.Controls.Add(list, 0, 0);
        var footer = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, Margin = Padding.Empty };
        footer.Controls.Add(open);
        footer.Controls.Add(Label(list.Items.Count == 0 ? emptyMessage : $"{list.Items.Count} jobs", 10));
        panel.Controls.Add(footer, 0, 1);
        return panel;
    }
}
