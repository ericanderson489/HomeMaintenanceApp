// Program: Shared create/edit task dialog for the Sprint 1 UI.
// Author: Murdock MacAskill
// Date: 09/16/2026
namespace HomeMaintenanceApp.UI;

internal sealed class TaskEditorForm : Form
{
    private readonly TextBox titleInput = new() { Dock = DockStyle.Fill, AccessibleName = "Task title" };
    private readonly TextBox notesInput = new() { Dock = DockStyle.Fill, Multiline = true, ScrollBars = ScrollBars.Vertical, AcceptsReturn = true, AccessibleName = "Description or notes" };
    private readonly DateTimePicker dateInput = new() { Dock = DockStyle.Top, Format = DateTimePickerFormat.Short, AccessibleName = "Due date" };
    private readonly Label feedback = WorkshopStyle.Label("");

    /// <summary>Edits a copy of task fields. The save callback owns persistence; failures keep this dialog open.</summary>
    internal TaskEditorForm(TaskViewData? task, Action<TaskFormData> save)
    {
        Text = task is null ? "Add Task" : "Edit Task";
        Font = new Font("Segoe UI", 11);
        BackColor = WorkshopStyle.Paper;
        StartPosition = FormStartPosition.CenterParent;
        ShowInTaskbar = false;
        MinimizeBox = false;
        MaximizeBox = false;
        ClientSize = new Size(550, 560);
        MinimumSize = new Size(480, 500);
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(24), ColumnCount = 1, RowCount = 9 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 9; i++) layout.RowStyles.Add(new RowStyle(i == 4 ? SizeType.Percent : SizeType.AutoSize, i == 4 ? 100 : 0));
        layout.Controls.Add(WorkshopStyle.Label(Text.ToUpperInvariant(), 24, true), 0, 0);
        layout.Controls.Add(WorkshopStyle.Label("Task &title"), 0, 1);
        layout.Controls.Add(titleInput, 0, 2);
        layout.Controls.Add(WorkshopStyle.Label("Description / &notes (optional)"), 0, 3);
        layout.Controls.Add(notesInput, 0, 4);
        layout.Controls.Add(WorkshopStyle.Label("&Due date"), 0, 5);
        layout.Controls.Add(dateInput, 0, 6);
        feedback.ForeColor = Color.DarkRed;
        feedback.MaximumSize = new Size(450, 0);
        layout.Controls.Add(feedback, 0, 7);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        var saveButton = WorkshopStyle.Button("&Save", () => SaveFields(save), true);
        var cancel = WorkshopStyle.Button("&Cancel", () => { DialogResult = DialogResult.Cancel; Close(); });
        actions.Controls.Add(saveButton);
        actions.Controls.Add(cancel);
        layout.Controls.Add(actions, 0, 8);
        Controls.Add(layout);
        AcceptButton = saveButton;
        CancelButton = cancel;
        titleInput.Text = task?.Title ?? "";
        notesInput.Text = task?.Notes ?? "";
        dateInput.Value = task?.DueDate ?? DateTime.Today;
        Shown += (_, _) => titleInput.Focus();
    }

    /// <summary>Checks the provisional required-title rule and submits fields. Cancel never submits.</summary>
    private void SaveFields(Action<TaskFormData> save)
    {
        if (string.IsNullOrWhiteSpace(titleInput.Text))
        {
            feedback.Text = "Please enter a task title.";
            titleInput.Focus();
            return;
        }
        try
        {
            save(new(titleInput.Text.Trim(), notesInput.Text.Trim(), dateInput.Value.Date));
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception)
        {
            feedback.Text = "The task could not be saved. Your entries are still here. Please try again.";
        }
    }
}
