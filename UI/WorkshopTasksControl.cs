using HomeMaintenanceApp.UI;

namespace HomeMaintenanceApp;

internal sealed class WorkshopTasksControl : UserControl
{
    private readonly TableLayoutPanel workspace;
    private Control taskList;
    private readonly IReadOnlyList<TaskViewData> tasks;
    private readonly Action<TaskViewData> archive;
    internal TaskFilter Filter { get; private set; }
    private TaskDetailsControl? sheet;
    private readonly Action<TaskViewData> edit;
    private readonly Action<TaskViewData> complete;
    private bool arranging;

    /// <summary>Keeps the task list mounted while selection opens a responsive job sheet.</summary>
    public WorkshopTasksControl(IReadOnlyList<TaskViewData> tasks, Action add,
        Action<TaskViewData> edit, Action<TaskViewData> complete, Action<TaskViewData> archive,
        Guid? selected = null, TaskFilter filter = TaskFilter.All, string? feedback = null)
    {
        this.tasks = tasks;
        this.archive = archive;
        Filter = filter;
        this.edit = edit;
        this.complete = complete;
        Font = new Font("Segoe UI", 10);
        var page = WorkshopStyle.Page(this, "On the workbench", "Your next fix starts here.", add);
        var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = Padding.Empty };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        body.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        workspace = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Margin = Padding.Empty };
        workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0));
        workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 0));
        workspace.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        taskList = CreateList();
        workspace.Controls.Add(taskList, 0, 1);
        var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, 0, 0, 6) };
        toolbar.Controls.Add(WorkshopStyle.FieldLabel("&Show jobs"));
        var filters = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FormattingEnabled = true,
            AccessibleName = "Task filter", Width = 170, Margin = new Padding(8, 0, 12, 4) };
        filters.Format += (_, e) => { if (e.ListItem is TaskFilter value) e.Value = TaskFilters.Label(value); };
        filters.Items.AddRange(Enum.GetValues<TaskFilter>().Cast<object>().ToArray());
        filters.SelectedItem = Filter;
        toolbar.Controls.Add(filters);
        var notice = WorkshopStyle.Label(feedback ?? "", 10);
        notice.AccessibleName = "Task feedback";
        notice.Visible = !string.IsNullOrEmpty(feedback);
        toolbar.Controls.Add(notice);
        toolbar.SizeChanged += (_, _) => notice.MaximumSize = new Size(Math.Max(100, toolbar.ClientSize.Width - 12), 0);
        filters.SelectedIndexChanged += (_, _) =>
        {
            Filter = (TaskFilter)filters.SelectedItem!;
            CloseSheet();
            workspace.Controls.Remove(taskList);
            taskList.Dispose();
            taskList = CreateList();
            workspace.Controls.Add(taskList, 0, 1);
        };
        body.Controls.Add(toolbar, 0, 0);
        body.Controls.Add(workspace, 0, 1);
        page.Controls.Add(body, 0, 2);
        workspace.SizeChanged += (_, _) => ArrangeSheet();
        if (selected is Guid id && tasks.FirstOrDefault(t => t.Id == id && TaskFilters.Matches(t, Filter, DateTime.Today)) is { } task)
        {
            Open(task);
            var list = taskList.Controls.OfType<ListView>().Single();
            // Selection notifications during handle creation must not dispose a sheet
            // while Windows is still creating that sheet's child controls.
            list.HandleCreated += (_, _) => list.BeginInvoke((Action)(() =>
            {
                if (list.IsDisposed) return;
                foreach (ListViewItem item in list.Items)
                    if (item.Tag is TaskViewData row && row.Id == id) item.Selected = true;
            }));
        }
    }

    private Control CreateList() => WorkshopStyle.TaskList(
        tasks.Where(t => TaskFilters.Matches(t, Filter, DateTime.Today)), Open, true,
        Filter == TaskFilter.Archived ? "No archived jobs." : "No jobs match this filter.");

    /// <summary>Replaces only the sheet so list selection, scroll position, and focus survive.</summary>
    private void Open(TaskViewData task)
    {
        CloseSheet();
        sheet = new TaskDetailsControl(task, CloseSheet, () => edit(task), () => complete(task), () => archive(task)) { Margin = new Padding(10, 0, 0, 0) };
        workspace.Controls.Add(sheet, 1, 1);
        sheet.SizeChanged += (_, _) => { if (!arranging) ArrangeSheet(); };
        sheet.CompactSizeChanged += (_, _) => { if (!arranging) ArrangeSheet(); };
        ArrangeSheet();
    }

    private void CloseSheet()
    {
        if (sheet is not null) { workspace.Controls.Remove(sheet); sheet.Dispose(); sheet = null; }
        ArrangeSheet();
    }

    /// <summary>Stacks a compact sheet above the list when two useful columns no longer fit.</summary>
    private void ArrangeSheet()
    {
        if (arranging) return;
        arranging = true;
        workspace.SuspendLayout();
        try
        {
            float scale = DeviceDpi / 96f;
            bool wide = workspace.ClientSize.Width >= 610 * scale;
            workspace.ColumnStyles[1].Width = sheet is not null && wide ? 240 * scale : 0;
            workspace.RowStyles[0].SizeType = SizeType.Absolute;
            workspace.RowStyles[0].Height = 0;
            if (sheet is null) return;
            workspace.SetCellPosition(sheet, wide ? new TableLayoutPanelCellPosition(1, 1) : new TableLayoutPanelCellPosition(0, 0));
            sheet.Margin = wide ? new Padding(10, 0, 0, 0) : new Padding(0, 0, 0, 8);
            sheet.Dock = DockStyle.Fill;
            if (wide) sheet.ExpandToFill();
            else
            {
                sheet.Width = workspace.ClientSize.Width;
                sheet.SetCompact(true);
                // Preserve list space even with notes expanded in a short window.
                workspace.RowStyles[0].Height = Math.Min(sheet.CompactHeight + 8, Math.Max(0, workspace.Height * .50f));
                sheet.AutoScroll = true;
            }
        }
        finally { workspace.ResumeLayout(true); arranging = false; }
    }
}
