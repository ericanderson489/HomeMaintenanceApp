// Program: Shared create/edit task dialog for the Sprint 1 UI.
// Author: Murdock MacAskill
// Date: 09/16/2026
namespace HomeMaintenanceApp.UI;

internal sealed class TaskEditorForm : Form
{
    private readonly TextBox titleInput = new() { Dock = DockStyle.Fill, AccessibleName = "Task title" };
    private readonly TextBox notesInput = new() { Dock = DockStyle.Fill, Multiline = true, ScrollBars = ScrollBars.Vertical, AcceptsReturn = true, AccessibleName = "Description or notes" };
    private readonly DateTimePicker dateInput = new() { Dock = DockStyle.Top, Format = DateTimePickerFormat.Short, AccessibleName = "Due date" };
    private readonly ComboBox typeInput = new() { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList, AccessibleName = "Task type" };
    private readonly ComboBox repeatInput = new() { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList, AccessibleName = "Repeat schedule" };
    private readonly Label feedback = WorkshopStyle.Label("");
    private readonly DateTime? originalDate;

    /// <summary>Edits a copy of task fields. The save callback owns persistence; failures keep this dialog open.</summary>
    internal TaskEditorForm(TaskViewData? task, Action<TaskFormData> save)
    {
        originalDate = task?.DueDate;
        Text = task is null ? "Add task" : "Edit task";
        Font = new Font("Segoe UI", 11);
        BackColor = WorkshopStyle.Background;
        StartPosition = FormStartPosition.CenterParent;
        ShowInTaskbar = false;
        MinimizeBox = false;
        MaximizeBox = false;
        ClientSize = new Size(580, 680);
        MinimumSize = new Size(420, 360);
        AutoScroll = true;
        notesInput.MinimumSize = new Size(0, 120);
        var layout = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, BackColor = WorkshopStyle.Paper, Padding = new Padding(24), ColumnCount = 1, RowCount = 14 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 14; i++) layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.Controls.Add(WorkshopStyle.Label(Text.ToUpperInvariant(), 24, true), 0, 0);
        layout.Controls.Add(WorkshopStyle.FieldLabel("Task &title"), 0, 1);
        layout.Controls.Add(titleInput, 0, 2);
        layout.Controls.Add(WorkshopStyle.FieldLabel("Description / &notes (required)"), 0, 3);
        layout.Controls.Add(notesInput, 0, 4);
        layout.Controls.Add(WorkshopStyle.FieldLabel("&Due date"), 0, 5);
        layout.Controls.Add(dateInput, 0, 6);
        feedback.ForeColor = Color.DarkRed;
        feedback.MaximumSize = new Size(450, 0);
        layout.Controls.Add(WorkshopStyle.FieldLabel("Task t&ype"), 0, 7);
        typeInput.Items.AddRange(TaskRules.Types.Cast<object>().ToArray());
        layout.Controls.Add(typeInput, 0, 8);
        layout.Controls.Add(WorkshopStyle.FieldLabel("&Repeat"), 0, 9);
        repeatInput.FormattingEnabled = true;
        repeatInput.Format += (_, e) => { if (e.ListItem is RepeatSchedule value) e.Value = Recurrence.Label(value); };
        repeatInput.Items.AddRange(Enum.GetValues<RepeatSchedule>().Cast<object>().ToArray());
        layout.Controls.Add(repeatInput, 0, 10);
        var help = WorkshopStyle.Label(task?.IsComplete == true
            ? "Completed dates and schedules stay in history. To change future repeats, edit the next open job."
            : "Completing a repeating job creates its next scheduled occurrence. Dates follow the original schedule; missed dates remain overdue.", 9);
        help.ForeColor = WorkshopStyle.Muted;
        layout.Controls.Add(help, 0, 11);
        layout.Controls.Add(feedback, 0, 12);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        var saveButton = WorkshopStyle.Button("&Save", () => SaveFields(save), true);
        var cancel = WorkshopStyle.Button("&Cancel", () => { DialogResult = DialogResult.Cancel; Close(); });
        actions.Controls.Add(saveButton);
        actions.Controls.Add(cancel);
        layout.Controls.Add(actions, 0, 13);
        Controls.Add(layout);
        AcceptButton = saveButton;
        CancelButton = cancel;
        titleInput.Text = task?.Title ?? "";
        notesInput.Text = task?.Notes ?? "";
        dateInput.Value = task?.DueDate ?? DateTime.Today;
        typeInput.SelectedItem = task?.TaskType;
        repeatInput.SelectedItem = task?.Repeat ?? RepeatSchedule.None;
        dateInput.Enabled = repeatInput.Enabled = task?.IsComplete != true;
        titleInput.TextChanged += (_, _) => feedback.Text = "";
        notesInput.TextChanged += (_, _) => feedback.Text = "";
        dateInput.ValueChanged += (_, _) => feedback.Text = "";
        typeInput.SelectedIndexChanged += (_, _) => feedback.Text = "";
        repeatInput.SelectedIndexChanged += (_, _) =>
        {
            feedback.Text = "";
            if (repeatInput.SelectedItem is RepeatSchedule schedule && schedule != RepeatSchedule.None && typeInput.SelectedIndex < 0)
                typeInput.SelectedItem = "Recurring";
        };
        layout.SizeChanged += (_, _) =>
        {
            int width = Math.Max(120, layout.ClientSize.Width - layout.Padding.Horizontal - 8);
            help.MaximumSize = feedback.MaximumSize = new Size(width, 0);
        };
        Shown += (_, _) => titleInput.Focus();
    }

    /// <summary>Mirrors the team's form rules before submitting; Cancel never changes the account.</summary>
    private void SaveFields(Action<TaskFormData> save)
    {
        string? taskType = typeInput.SelectedItem as string;
        var repeat = repeatInput.SelectedItem is RepeatSchedule schedule ? schedule : RepeatSchedule.None;
        string? error = TaskRules.Error(titleInput.Text, notesInput.Text, taskType, dateInput.Value, originalDate);
        // New recurring jobs need an interval. Existing jobs may deliberately stop
        // repeating, and old type labels are not a substitute for a saved schedule.
        if (error is null && originalDate is null && taskType == "Recurring" && repeat == RepeatSchedule.None)
            error = "Choose a repeat interval for this recurring task, or change its task type.";
        if (error is not null)
        {
            feedback.Text = error;
            return;
        }
        try
        {
            save(new(titleInput.Text.Trim(), notesInput.Text.Trim(), dateInput.Value.Date, taskType!, repeat));
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (ArgumentException ex)
        {
            feedback.Text = ex.Message;
        }
        catch (Exception)
        {
            feedback.Text = "The task could not be saved. Your entries are still here. Check storage or reopen the app if another session changed this account.";
        }
    }
}
