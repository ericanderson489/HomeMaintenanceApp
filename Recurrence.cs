// Program: Calendar-based recurrence rules for maintenance tasks.
// Author: Murdock MacAskill
// Date: 09/29/2026
namespace HomeMaintenanceApp;

public enum RepeatSchedule
{
    None,
    Weekly,
    Monthly,
    Quarterly,
    Yearly
}

internal static class Recurrence
{
    /// <summary>Shares the anchored next-date calculation between the preview and completion.
    /// Nonrepeating jobs have no successor; an exhausted calendar range throws a validation error.</summary>
    internal static DateTime? NextDueDate(TaskRecord task) => task.Repeat == RepeatSchedule.None ? null
        : DueDate(task.ScheduleAnchor!.Value, task.Repeat, checked(task.Occurrence + 1));

    internal static string Label(RepeatSchedule repeat) => repeat switch
    {
        RepeatSchedule.None => "Does not repeat",
        RepeatSchedule.Weekly => "Weekly",
        RepeatSchedule.Monthly => "Monthly",
        RepeatSchedule.Quarterly => "Every 3 months",
        RepeatSchedule.Yearly => "Yearly",
        _ => throw new ArgumentException("Please select a supported repeat schedule.")
    };

    /// <summary>Calculates from the original anchor, avoiding month-end and leap-year drift.
    /// Throws when no date within the app's supported range can be represented.</summary>
    internal static DateTime DueDate(DateTime anchor, RepeatSchedule repeat, int occurrence)
    {
        if (!Enum.IsDefined(repeat) || repeat == RepeatSchedule.None || occurrence < 0)
            throw new ArgumentException("The repeat schedule is invalid.");
        try
        {
            DateTime date = repeat switch
            {
                RepeatSchedule.Weekly => anchor.Date.AddDays(checked(7 * occurrence)),
                RepeatSchedule.Monthly => anchor.Date.AddMonths(occurrence),
                RepeatSchedule.Quarterly => anchor.Date.AddMonths(checked(3 * occurrence)),
                RepeatSchedule.Yearly => anchor.Date.AddYears(occurrence),
                _ => throw new ArgumentException("The repeat schedule is invalid.")
            };
            if (date < new DateTime(1753, 1, 1) || date > new DateTime(9998, 12, 31))
                throw new ArgumentOutOfRangeException(nameof(anchor));
            return date;
        }
        catch (Exception ex) when (ex is ArgumentOutOfRangeException or OverflowException)
        {
            throw new ArgumentException("There is no later supported date for this schedule. Change the due date or choose Does not repeat before completing this job.", ex);
        }
    }

    /// <summary>Validates persisted recurrence metadata without changing damaged records.</summary>
    internal static bool IsValid(TaskRecord task)
    {
        if (!Enum.IsDefined(task.Repeat) || task.PreviousOccurrenceId == Guid.Empty
            || (task.CompletedAt is not null && task.Status != Status.Complete)) return false;
        if (task.Repeat == RepeatSchedule.None) return task.ScheduleAnchor is null && task.Occurrence == 0;
        if (task.ScheduleAnchor is not DateTime anchor || anchor.TimeOfDay != TimeSpan.Zero) return false;
        try { return DueDate(anchor, task.Repeat, task.Occurrence) == task.DueDate; }
        catch (ArgumentException) { return false; }
    }
}
