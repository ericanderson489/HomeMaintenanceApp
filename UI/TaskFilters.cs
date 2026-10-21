// Program: Consistent date and archive filters for maintenance jobs.
// Author: Murdock MacAskill
// Date: 09/29/2026
namespace HomeMaintenanceApp.UI;

internal enum TaskFilter { All, Active, Overdue, DueSoon, Completed, Archived }

internal static class TaskFilters
{
    internal static string Label(TaskFilter filter) => filter switch
    {
        TaskFilter.All => "All jobs",
        TaskFilter.Active => "All active",
        TaskFilter.Overdue => "Overdue",
        TaskFilter.DueSoon => "Due soon",
        TaskFilter.Completed => "Completed",
        TaskFilter.Archived => "Archived",
        _ => throw new ArgumentOutOfRangeException(nameof(filter))
    };

    /// <summary>Uses local calendar days. Due soon includes today and the next six days;
    /// archived jobs appear only in Archived, regardless of their completion status.</summary>
    internal static bool Matches(TaskViewData task, TaskFilter filter, DateTime today)
    {
        if (task.IsArchived) return filter == TaskFilter.Archived;
        return filter switch
        {
            TaskFilter.All => true,
            TaskFilter.Active => !task.IsComplete,
            TaskFilter.Overdue => !task.IsComplete && task.DueDate.Date < today.Date,
            TaskFilter.DueSoon => !task.IsComplete && task.DueDate.Date >= today.Date && task.DueDate.Date <= today.Date.AddDays(6),
            TaskFilter.Completed => task.IsComplete,
            _ => false
        };
    }
}
