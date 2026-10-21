// Program: Connect the workshop UI to durable account tasks.
// Author: Murdock MacAskill
// Date: 09/29/2026
namespace HomeMaintenanceApp.UI;

public sealed record TaskViewData(Guid Id, string Title, string Notes, DateTime DueDate, bool IsComplete, string TaskType,
    RepeatSchedule Repeat = RepeatSchedule.None, Guid? PreviousOccurrenceId = null, DateTime? CompletedAt = null,
    bool IsArchived = false, DateTime? NextOccurrenceDate = null);
public sealed record TaskFormData(string Title, string Notes, DateTime DueDate, string TaskType,
    RepeatSchedule Repeat = RepeatSchedule.None);

/// <summary>Writes return only after success; failures leave unsaved fields in the editor.</summary>
public interface ITaskPresentationSource
{
    bool IsPreview { get; }
    IReadOnlyList<TaskViewData> GetTasks();
    void Save(Guid? id, TaskFormData fields);
    void Complete(Guid id);
    void SetArchived(Guid id, bool archived);
}

internal sealed class AccountTaskSource : ITaskPresentationSource
{
    private readonly Account account;
    internal AccountTaskSource(Account account) => this.account = account;
    public bool IsPreview => false;
    public IReadOnlyList<TaskViewData> GetTasks() => account.GetAllTasks().Select(ToView).ToArray();

    /// <summary>Previews the actual next scheduled date, retaining original month-end anchors.
    /// A range limit leaves the preview empty so the sheet can explain that the schedule needs editing.</summary>
    private static TaskViewData ToView(Tasks task)
    {
        DateTime? next = null;
        if (!task.IsArchived && task.GetStatus() != Status.Complete)
        {
            try { next = Recurrence.NextDueDate(task.Snapshot()); }
            catch (ArgumentException) { }
        }
        return new(task.Id, task.GetName(), task.GetDescription(), task.GetDate(),
            task.GetStatus() == Status.Complete, task.GetTaskType(), task.Repeat,
            task.PreviousOccurrenceId, task.CompletedAt, task.IsArchived, next);
    }

    public void Save(Guid? id, TaskFormData fields)
    {
        if (id is null) account.AddTask(fields.Title, fields.Notes, fields.TaskType, fields.DueDate, fields.Repeat);
        else account.UpdateTask(id.Value, fields.Title, fields.Notes, fields.TaskType, fields.DueDate, repeat: fields.Repeat);
    }

    public void Complete(Guid id) => account.CompleteTask(id);
    public void SetArchived(Guid id, bool archived) => account.SetArchived(id, archived);
}
