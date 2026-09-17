
using HomeMaintenanceApp.UI;

namespace HomeMaintenanceApp;

public partial class MainForm
{
    private readonly ITaskPresentationSource taskSource;
    private bool homeSelected = true;

    /// <summary>Styles the existing shell and hides future screens only from Sprint 1 navigation.</summary>
    private void ApplyWorkshopStyle()
    {
        SuspendLayout();
        Font = new Font("Segoe UI", 11);
        Text = "Home Maintenance Manager" + (taskSource.IsPreview ? " — UI Preview" : "");
        ClientSize = new Size(1100, 760);
        MinimumSize = new Size(900, 680);
        sidePanel.Width = 215;
        sidePanel.BackColor = WorkshopStyle.Red;
        sidePanel.BorderStyle = BorderStyle.None;
        sidePanel.Padding = new Padding(14);
        mainPanel.BorderStyle = BorderStyle.None;
        foreach (var button in new[] { goalsButton, calendarButton, historyButton, settingsButton }) button.Visible = false;
        dashBoardButton.Text = "&Home";
        dashBoardButton.SetBounds(14, 150, 187, 54);
        tasksButton.Text = "&Tasks";
        tasksButton.SetBounds(14, 218, 187, 54);
        foreach (var button in new[] { dashBoardButton, tasksButton })
        {
            button.AutoSize = false;
            button.FlatStyle = FlatStyle.Flat;
            button.Padding = new Padding(12, 0, 0, 0);
            button.ForeColor = WorkshopStyle.Paper;
            button.FlatAppearance.BorderColor = Color.FromArgb(224, 154, 124);
            button.Paint += (_, e) =>
            {
                using var handle = new SolidBrush(Color.Silver);
                e.Graphics.FillRectangle(handle, button.Width / 3, button.Height - 8, button.Width / 3, 3);
            };
        }
        var sign = WorkshopStyle.Label("HOME\nMAINTENANCE", 13, true);
        sign.AutoSize = false;
        sign.TextAlign = ContentAlignment.MiddleCenter;
        sign.BackColor = WorkshopStyle.Gold;
        sign.SetBounds(14, 24, 187, 100);
        sidePanel.Controls.Add(sign);
        var footer = WorkshopStyle.Label("Roll up your sleeves.\nMake yourself at home.", 9);
        footer.AutoSize = false;
        footer.Height = 92;
        footer.Dock = DockStyle.Bottom;
        footer.ForeColor = WorkshopStyle.Paper;
        sidePanel.Controls.Add(footer);
        if (taskSource.IsPreview)
        {
            var preview = WorkshopStyle.Label("UI PREVIEW — sample data only. Changes reset when you close the app.", 10);
            preview.AutoSize = false;
            preview.Height = 32;
            preview.Dock = DockStyle.Bottom;
            preview.BackColor = WorkshopStyle.Gold;
            preview.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(preview);
            preview.SendToBack(); // Reserve the bottom strip before the fill panel is laid out.
        }
        ResumeLayout(true);
    }

    /// <summary>Loads before replacing a screen; failure preserves the current screen.</summary>
    private void Navigate(bool home)
    {
        try
        {
            var tasks = taskSource.GetTasks();
            UserControl page = home
                ? new DashboardControl(tasks, () => EditTask(null), () => Navigate(false), ShowDetails)
                : new TasksControl(tasks, () => EditTask(null), ShowDetails);
            homeSelected = home;
            ShowPage(page);
            dashBoardButton.BackColor = home ? Color.FromArgb(191, 73, 55) : WorkshopStyle.Red;
            tasksButton.BackColor = home ? WorkshopStyle.Red : Color.FromArgb(191, 73, 55);
        }
        catch (Exception) { ShowFailure("Tasks could not be loaded. Please try again."); }
    }

    private void ShowDetails(TaskViewData task) => ShowPage(new TaskDetailsControl(task,
        () => Navigate(homeSelected), () => EditTask(task), () => CompleteTask(task)));

    /// <summary>Edits copied fields. Successful saves refresh the list; cancellation changes nothing.</summary>
    private void EditTask(TaskViewData? task)
    {
        using var editor = new TaskEditorForm(task, fields => taskSource.Save(task?.Id, fields));
        if (editor.ShowDialog(this) == DialogResult.OK) Navigate(false);
    }

    private void CompleteTask(TaskViewData task)
    {
        try
        {
            taskSource.Complete(task.Id);
            ShowDetails(task with { IsComplete = true });
        }
        catch (Exception) { ShowFailure("The task could not be marked complete. Please try again."); }
    }

    private void ShowFailure(string message) => MessageBox.Show(this, message, "Home Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
