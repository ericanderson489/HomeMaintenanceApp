// Program: Shared validation for the workshop and original task forms.
// Author: Murdock MacAskill
// Date: 09/29/2026
namespace HomeMaintenanceApp;

internal static class TaskRules
{
    internal static readonly IReadOnlyList<string> Types = Array.AsReadOnly(new[] { "Priority", "Recurring" });

    // Existing historical dates may remain unchanged when editing; new past dates are rejected.
    internal static string? Error(string title, string notes, string? type, DateTime dueDate, DateTime? originalDate = null)
    {
        if (string.IsNullOrWhiteSpace(title)) return "Please enter a task title.";
        if (string.IsNullOrWhiteSpace(notes)) return "Please enter a description or notes.";
        if (type is null || !Types.Contains(type)) return "Please select a task type.";
        if (dueDate.Date < DateTime.Today && dueDate.Date != originalDate?.Date) return "Due date cannot be in the past.";
        return null;
    }

    internal static void Validate(string title, string notes, string type, DateTime dueDate, DateTime? originalDate = null)
    {
        var error = Error(title, notes, type, dueDate, originalDate);
        if (error is not null) throw new ArgumentException(error);
    }
}
