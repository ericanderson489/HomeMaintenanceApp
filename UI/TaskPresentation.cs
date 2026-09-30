// Program: Connect the workshop UI to durable account tasks.
// Author: Murdock MacAskill
// Date: 09/29/2026
namespace HomeMaintenanceApp.UI;

public sealed record TaskViewData(Guid Id, string Title, string Notes, DateTime DueDate, bool IsComplete, string TaskType);
public sealed record TaskFormData(string Title, string Notes, DateTime DueDate, string TaskType);

/// <summary>Writes return only after success; failures leave unsaved fields in the editor.</summary>
public interface ITaskPresentationSource
{
    bool IsPreview { get; }
    IReadOnlyList<TaskViewData> GetTasks();
    void Save(Guid? id, TaskFormData fields);
    void Complete(Guid id);
}

internal sealed class AccountTaskSource : ITaskPresentationSource
{
    private readonly Account account;
    internal AccountTaskSource(Account account) => this.account = account;
    public bool IsPreview => false;
    public IReadOnlyList<TaskViewData> GetTasks() => account.GetTaskList().Select(t =>
        new TaskViewData(t.Id, t.GetName(), t.GetDescription(), t.GetDate(),
            t.GetStatus() == Status.Complete, t.GetTaskType())).ToArray();

    public void Save(Guid? id, TaskFormData fields)
    {
        if (id is null) account.AddTask(fields.Title, fields.Notes, fields.TaskType, fields.DueDate);
        else account.UpdateTask(id.Value, fields.Title, fields.Notes, fields.TaskType, fields.DueDate);
    }

    public void Complete(Guid id) => account.CompleteTask(id);
}
