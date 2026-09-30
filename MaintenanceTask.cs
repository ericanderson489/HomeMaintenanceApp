using System;

public class MaintenanceTask
{
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public string Description { get; set; }

    public bool IsPriority { get; set; }
    public bool IsRecurring { get; set; }

    public MaintenanceTask(
        string title,
        DateTime dueDate,
        string description,
        bool isPriority,
        bool isRecurring)
    {
        Title = title;
        DueDate = dueDate;
        Description = description;
        IsPriority = isPriority;
        IsRecurring = isRecurring;
    }
}
