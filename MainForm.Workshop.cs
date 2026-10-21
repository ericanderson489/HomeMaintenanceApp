// Program: Workshop UI navigation and task-service integration boundary.
// Author: Murdock MacAskill
// Date: 09/16/2026
using HomeMaintenanceApp.UI;

namespace HomeMaintenanceApp;

internal partial class MainForm
{
    private ITaskPresentationSource? taskSource;
    private TaskFilter taskFilter = TaskFilter.All;

    /// <summary>Styles the shared shell while preserving the team's navigation handlers.</summary>
    private void ApplyWorkshopStyle()
    {
        SuspendLayout();
        Font = new Font("Segoe UI", 10);
        Text = "Breachless Bungalow — Workshop";
        ClientSize = new Size(1180, 760);
        MinimumSize = new Size(850, 660);
        sidePanel.Width = 205;
        sidePanel.BackColor = WorkshopStyle.Navy;
        sidePanel.BorderStyle = BorderStyle.None;
        sidePanel.Padding = new Padding(12);
        mainPanel.BorderStyle = BorderStyle.None;
        mainPanel.BackColor = WorkshopStyle.Background;
        foreach (var button in new[] { goalsButton, calendarButton, historyButton, settingsButton }) button.Visible = false;
        dashBoardButton.Text = "&Home";
        tasksButton.Text = "&Tasks";
        var nav = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 5, BackColor = WorkshopStyle.Navy };
        nav.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        nav.RowStyles.Add(new RowStyle(SizeType.Absolute, 155));
        nav.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        nav.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        nav.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        nav.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        nav.Controls.Add(new WorkshopSign { Dock = DockStyle.Fill }, 0, 0);
        int row = 1;
        foreach (var button in new[] { dashBoardButton, tasksButton })
        {
            WorkshopStyle.StyleButton(button);
            button.Dock = DockStyle.Top;
            button.MinimumSize = new Size(0, 46);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(12, 5, 8, 5);
            button.ForeColor = WorkshopStyle.Paper;
            button.BackColor = WorkshopStyle.Navy;
            button.FlatAppearance.BorderColor = WorkshopStyle.Muted;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(56, 86, 99);
            nav.Controls.Add(button, 0, row++);
        }
        var signOut = WorkshopStyle.Button("Sign out", ShowLogin);
        signOut.ForeColor = WorkshopStyle.Paper;
        signOut.BackColor = WorkshopStyle.Navy;
        nav.Controls.Add(signOut, 0, 4);
        sidePanel.Controls.Add(nav);
        nav.BringToFront();
        ResumeLayout(true);
    }

    /// <summary>Loads before replacing a screen; a storage failure leaves the current screen intact.</summary>
    private void Navigate(bool home, Guid? selected = null, string? feedback = null)
    {
        if (taskSource is null) return;
        try
        {
            if (mainPanel.Controls.OfType<WorkshopTasksControl>().FirstOrDefault() is { } current)
                taskFilter = current.Filter;
            var tasks = taskSource.GetTasks();
            UserControl page = home
                ? new WorkshopDashboardControl(tasks, () => EditTask(null), () => Navigate(false), ShowDetails)
                : new WorkshopTasksControl(tasks, () => EditTask(null), EditTask, CompleteTask, ArchiveTask, selected, taskFilter, feedback);
            ShowPage(page);
            dashBoardButton.BackColor = home ? WorkshopStyle.Ink : WorkshopStyle.Navy;
            tasksButton.BackColor = home ? WorkshopStyle.Navy : WorkshopStyle.Ink;
            dashBoardButton.FlatAppearance.BorderColor = home ? WorkshopStyle.Gold : WorkshopStyle.Muted;
            tasksButton.FlatAppearance.BorderColor = home ? WorkshopStyle.Muted : WorkshopStyle.Gold;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Task navigation failed: {0}", ex);
            ShowFailure("Tasks could not be loaded. Please try again.");
        }
    }

    private void ShowDetails(TaskViewData task)
    {
        taskFilter = TaskFilter.All;
        Navigate(false, task.Id);
    }

    /// <summary>Edits copied fields; a successful save reloads the account and retains the selected job.</summary>
    private void EditTask(TaskViewData? task)
    {
        if (taskSource is null) return;
        using var editor = new TaskEditorForm(task, fields => taskSource.Save(task?.Id, fields));
        if (editor.ShowDialog(this) == DialogResult.OK) Navigate(false, task?.Id, "Task saved.");
    }

    private void CompleteTask(TaskViewData task)
    {
        if (taskSource is null) return;
        try
        {
            taskSource.Complete(task.Id);
            Navigate(false, task.Id, "Task completed." + (task.Repeat == RepeatSchedule.None ? "" : " Next occurrence created."));
        }
        catch (ArgumentException ex) { ShowFailure(ex.Message); }
        catch (Exception) { ShowFailure("The task could not be marked complete. Please try again."); }
    }

    /// <summary>Archives or restores one job through the same atomic persistence boundary as editing.
    /// A failed write keeps the current sheet and reports the failure.</summary>
    private void ArchiveTask(TaskViewData task)
    {
        if (taskSource is null) return;
        try
        {
            taskSource.SetArchived(task.Id, !task.IsArchived);
            Navigate(false, feedback: task.IsArchived ? "Task restored." : "Task archived. Find it under Archived to restore it.");
        }
        catch (ArgumentException ex) { ShowFailure(ex.Message); }
        catch (Exception) { ShowFailure("The task could not be saved. Its archive status has not changed."); }
    }

    private void ShowFailure(string message) => MessageBox.Show(this, message, "Breachless Bungalow", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
