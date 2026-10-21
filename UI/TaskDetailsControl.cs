// Program: Read-only job sheet with explicit edit and complete actions.
// Author: Murdock MacAskill
// Date: 09/16/2026
namespace HomeMaintenanceApp.UI;

internal sealed class TaskDetailsControl : UserControl
{
    private readonly TableLayoutPanel layout;
    private readonly TextBox notes;
    private readonly Button disclosure;
    private readonly Label heading;
    private readonly Label caption;
    private readonly Label status;
    private readonly FlowLayoutPanel actions;
    private readonly TaskViewData task;
    private bool compact;
    internal event EventHandler? CompactSizeChanged;
    internal int CompactHeight => layout.GetPreferredSize(new Size(ClientSize.Width, 0)).Height;

    /// <summary>Shows a task snapshot. Only supplied callbacks can mutate the account.</summary>
    internal TaskDetailsControl(TaskViewData task, Action back, Action edit, Action complete, Action archive)
    {
        this.task = task;
        Font = new Font("Segoe UI", 10);
        BackColor = WorkshopStyle.Paper;
        AccessibleName = "Selected job sheet";
        layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 6, Padding = new Padding(12), Margin = Padding.Empty };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 6; i++) layout.RowStyles.Add(new RowStyle(i == 4 ? SizeType.Percent : SizeType.AutoSize, i == 4 ? 100 : 0));
        var top = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 2, BackColor = WorkshopStyle.Navy, Padding = new Padding(6), Margin = new Padding(0, 0, 0, 8) };
        top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        top.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        caption = WorkshopStyle.Label("JOB SHEET", 11, true);
        caption.ForeColor = WorkshopStyle.Paper;
        top.Controls.Add(caption, 0, 0);
        var close = WorkshopStyle.Button("Close", back);
        close.MinimumSize = new Size(60, 30);
        close.Padding = Padding.Empty;
        top.Controls.Add(close, 1, 0);
        layout.Controls.Add(top, 0, 0);
        heading = WorkshopStyle.Label(task.Title, 18, true);
        heading.AccessibleName = "Task title";
        layout.Controls.Add(heading, 0, 1);
        status = WorkshopStyle.Label(StatusDescription(false), 10);
        layout.Controls.Add(status, 0, 2);
        disclosure = WorkshopStyle.Button("Show notes", ToggleNotes);
        disclosure.Visible = false;
        layout.Controls.Add(disclosure, 0, 3);
        notes = new TextBox { Text = string.IsNullOrWhiteSpace(task.Notes) ? "No notes added." : task.Notes,
            ReadOnly = true, Multiline = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill,
            BackColor = WorkshopStyle.Paper, ForeColor = WorkshopStyle.Ink, BorderStyle = BorderStyle.None, AccessibleName = "Description or notes" };
        layout.Controls.Add(notes, 0, 4);
        actions = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Margin = Padding.Empty };
        if (!task.IsArchived)
        {
            if (!task.IsComplete) actions.Controls.Add(WorkshopStyle.Button("Mark complete", complete, true));
            actions.Controls.Add(WorkshopStyle.Button("Edit task", edit));
        }
        actions.Controls.Add(WorkshopStyle.Button(task.IsArchived ? "Restore" : "Archive", archive, task.IsArchived));
        layout.Controls.Add(actions, 0, 5);
        Controls.Add(layout);
        SizeChanged += (_, _) => Fit();
    }

    /// <summary>Uses a collapsed notes section in the stacked layout; wide sheets always show notes.</summary>
    internal void SetCompact(bool value)
    {
        if (compact == value) return;
        compact = value;
        AutoScroll = compact;
        if (!compact) AutoScrollPosition = Point.Empty;
        layout.AutoSize = compact;
        layout.Dock = compact ? DockStyle.Top : DockStyle.Fill;
        heading.Visible = !compact;
        caption.Text = compact ? task.Title : "JOB SHEET";
        status.Text = StatusDescription(compact);
        if (compact) actions.Controls.Add(disclosure);
        else layout.Controls.Add(disclosure, 0, 3);
        disclosure.Visible = compact;
        notes.Visible = !compact;
        disclosure.Text = "Show notes";
        Fit();
    }

    private void ToggleNotes()
    {
        notes.Visible = !notes.Visible;
        disclosure.Text = notes.Visible ? "Hide notes" : "Show notes";
        Fit();
    }

    private string StatusDescription(bool compactLayout)
    {
        string separator = compactLayout ? "   •   " : "\n";
        string text = $"Due {task.DueDate:MMM d, yyyy}   •   {WorkshopStyle.StatusText(task)}{separator}Type: {task.TaskType}";
        text += separator + "Repeat: " + Recurrence.Label(task.Repeat);
        if (!task.IsComplete && !task.IsArchived && task.Repeat != RepeatSchedule.None)
            text += separator + (task.NextOccurrenceDate is DateTime next
                ? $"Next occurrence: {next:MMM d, yyyy} (on completion)"
                : "No later supported date. Edit this schedule before completing.");
        if (task.IsArchived) text += separator + "Restore to return this job to its previous status and schedule.";
        if (task.CompletedAt is DateTime completed) text += separator + $"Completed {completed.ToLocalTime():MMM d, yyyy}";
        return text;
    }

    private void Fit()
    {
        heading.MaximumSize = new Size(Math.Max(80, ClientSize.Width - layout.Padding.Horizontal - 8), 0);
        status.MaximumSize = heading.MaximumSize;
        caption.MaximumSize = new Size(Math.Max(80, ClientSize.Width - layout.Padding.Horizontal - 110 * DeviceDpi / 96), 0);
        if (!compact) return;
        layout.RowStyles[4].SizeType = SizeType.Absolute;
        layout.RowStyles[4].Height = notes.Visible ? 40 * DeviceDpi / 96f : 0;
        CompactSizeChanged?.Invoke(this, EventArgs.Empty);
    }

    internal void ExpandToFill()
    {
        SetCompact(false);
        layout.RowStyles[4].SizeType = SizeType.Percent;
        layout.RowStyles[4].Height = 100;
    }
}
