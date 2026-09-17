// Program: UI data contract and temporary sample data for the workshop preview.
// Author: Murdock MacAskill
// Date: 09/16/2026
namespace HomeMaintenanceApp.UI;

public sealed record TaskViewData(Guid Id, string Title, string Notes, DateTime DueDate, bool IsComplete);
public sealed record TaskFormData(string Title, string Notes, DateTime DueDate);

/// <summary>UI boundary for the team's task service. Writes must return only after success.
/// Implementations report failures by throwing an exception; the UI retains unsaved form data.</summary>
public interface ITaskPresentationSource
{
    bool IsPreview { get; }
    IReadOnlyList<TaskViewData> GetTasks();
    void Save(Guid? id, TaskFormData fields);
    void Complete(Guid id);
}

/// <summary>Session-only sample data for UI demonstrations; no files or database are written.</summary>
public sealed class PreviewTaskSource : ITaskPresentationSource
{
    private readonly List<TaskViewData> tasks = new()
    {
        new(Guid.NewGuid(), "Replace HVAC filter", "Check the size on the existing filter before buying a replacement.", DateTime.Today.AddDays(2), false),
        new(Guid.NewGuid(), "Test smoke detectors", "Test each detector using its test button.", DateTime.Today.AddDays(3), false),
        new(Guid.NewGuid(), "Clean the gutters", "Clear leaves and check the downspouts.", DateTime.Today.AddDays(7), false),
        new(Guid.NewGuid(), "Inspect water heater", "Checked the surrounding area for visible leaks.", DateTime.Today.AddDays(-1), true)
    };

    public bool IsPreview => true;
    public IReadOnlyList<TaskViewData> GetTasks() => tasks.ToArray();

    /// <summary>Updates sample fields or adds a sample task. Unknown edit IDs are rejected.</summary>
    public void Save(Guid? id, TaskFormData fields)
    {
        if (id is null)
        {
            tasks.Add(new(Guid.NewGuid(), fields.Title, fields.Notes, fields.DueDate.Date, false));
            return;
        }
        int index = tasks.FindIndex(task => task.Id == id);
        if (index < 0) throw new InvalidOperationException("Task no longer exists.");
        tasks[index] = tasks[index] with { Title = fields.Title, Notes = fields.Notes, DueDate = fields.DueDate.Date };
    }

    /// <summary>Marks a sample task complete. Unknown IDs are rejected.</summary>
    public void Complete(Guid id)
    {
        int index = tasks.FindIndex(task => task.Id == id);
        if (index < 0) throw new InvalidOperationException("Task no longer exists.");
        tasks[index] = tasks[index] with { IsComplete = true };
    }
}
