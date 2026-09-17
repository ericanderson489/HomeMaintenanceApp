// Program: Read-only task details and explicit edit/complete actions.
// Author: Murdock MacAskill
// Date: 09/16/2026
namespace HomeMaintenanceApp.UI;

internal sealed class TaskDetailsControl : UserControl
{
    /// <summary>Shows the selected snapshot; delegates every action to the main window.</summary>
    internal TaskDetailsControl(TaskViewData task, Action back, Action edit, Action complete)
    {
        Font = new Font("Segoe UI", 11);
        var page = WorkshopStyle.Page(this, "TASK DETAILS", "Everything you need for this job.");
        var body = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = WorkshopStyle.Paper, Padding = new Padding(18), ColumnCount = 1, RowCount = 5 };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 5; i++) body.RowStyles.Add(new RowStyle(i == 3 ? SizeType.Percent : SizeType.AutoSize, i == 3 ? 100 : 0));
        body.Controls.Add(WorkshopStyle.Button("Back", back), 0, 0);
        body.Controls.Add(new TextBox { Text = task.Title, ReadOnly = true, Multiline = true, Height = 65, Dock = DockStyle.Fill, BorderStyle = BorderStyle.None, BackColor = WorkshopStyle.Paper, ForeColor = WorkshopStyle.Navy, Font = new Font("Segoe UI", 17, FontStyle.Bold), AccessibleName = "Task title" }, 0, 1);
        body.Controls.Add(WorkshopStyle.Label($"Due: {task.DueDate:MMM d, yyyy}    •    Status: {(task.IsComplete ? "Completed" : "To do")}"), 0, 2);
        body.Controls.Add(new TextBox { Text = string.IsNullOrEmpty(task.Notes) ? "No notes added." : task.Notes, ReadOnly = true, Multiline = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill, BackColor = WorkshopStyle.Paper, BorderStyle = BorderStyle.None, AccessibleName = "Description or notes" }, 0, 3);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        actions.Controls.Add(WorkshopStyle.Button("Edit task", edit));
        if (!task.IsComplete) actions.Controls.Add(WorkshopStyle.Button("Mark complete", complete, true));
        body.Controls.Add(actions, 0, 4);
        page.Controls.Add(body, 0, 2);
    }
}
