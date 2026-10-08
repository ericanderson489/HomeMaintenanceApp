namespace HomeMaintenanceApp;

internal class Account
{
    private readonly string m_firstName, m_lastName, m_userName;
    private readonly PasswordRecord password;
    private readonly List<Tasks> m_taskList = new();
    private AccountStore? store;
    private int revision;

    public Account(string username, string password, string firstName, string lastName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        m_userName = username.Trim();
        m_firstName = firstName.Trim();
        m_lastName = lastName.Trim();
        this.password = PasswordSecurity.Hash(password);
    }

    private Account(AccountRecord record, AccountStore store)
    {
        m_userName = record.UserName;
        m_firstName = record.FirstName;
        m_lastName = record.LastName;
        password = record.Password;
        revision = record.Revision;
        this.store = store;
        m_taskList.AddRange(record.Tasks.Select(Tasks.Restore));
    }

    internal static Account Restore(AccountRecord record, AccountStore store) => new(record, store);
    public string GetAccountFullName() => m_firstName + " " + m_lastName;
    public string GetAccountFirstName() => m_firstName;
    public string GetAccountUserName() => m_userName;
    public bool VerifyPassword(string candidate) => PasswordSecurity.Verify(candidate, password);
    public override string ToString() => GetAccountFullName();
    public IReadOnlyList<Tasks> GetTaskList() => m_taskList.Where(t => !t.IsArchived).ToList().AsReadOnly();
    internal IReadOnlyList<Tasks> GetAllTasks() => m_taskList.AsReadOnly();

    private AccountRecord Snapshot(IEnumerable<Tasks> tasks) =>
        new(3, revision, m_userName, m_firstName, m_lastName, password, tasks.Select(t => t.Snapshot()).ToList());

    internal void CreateIn(AccountStore destination)
    {
        revision = destination.Save(Snapshot(m_taskList), true).Revision;
        store = destination;
    }

    // Commit to disk before changing live objects, so a failed save leaves the account unchanged.
    private void Persist(IEnumerable<Tasks> tasks)
    {
        if (store is null) throw new InvalidOperationException("Create or sign in to an account before saving tasks.");
        revision = store.Save(Snapshot(tasks), false).Revision;
    }

    public void AddTask(string name, string description, string type, DateTime dueDate, RepeatSchedule repeat = RepeatSchedule.None)
    {
        TaskRules.Validate(name, description, type, dueDate);
        var task = new Tasks(name.Trim(), description.Trim(), type, dueDate.Date);
        task.Apply(WithSchedule(task.Snapshot(), repeat, dueDate.Date));
        Persist(m_taskList.Append(task));
        m_taskList.Add(task);
    }

    // An overdue task may keep its existing date; newly selected dates must not be in the past.
    public void UpdateTask(Guid id, string name, string description, string type, DateTime dueDate, bool complete = false, RepeatSchedule? repeat = null)
    {
        var task = FindTask(id);
        RequireUnarchived(task);
        TaskRules.Validate(name, description, type, dueDate, task.GetDate());
        var previous = task.Snapshot();
        RepeatSchedule schedule = repeat ?? previous.Repeat;
        if (previous.Status == Status.Complete && (schedule != previous.Repeat || dueDate.Date != previous.DueDate))
            throw new ArgumentException("A completed job's date and repeat schedule are part of its history. Edit the next open occurrence instead.");
        var updated = WithSchedule(previous with { Name = name.Trim(), Description = description.Trim(), Type = type }, schedule, dueDate.Date);
        if (complete && previous.Status != Status.Complete) CommitCompletion(task, updated);
        else Commit(task, updated);
    }

    public void CompleteTask(Guid id)
    {
        var task = FindTask(id);
        RequireUnarchived(task);
        if (task.GetStatus() == Status.Complete) return;
        CommitCompletion(task, task.Snapshot());
    }

    /// <summary>Hides or restores one occurrence without changing its status, date, or repeat history.
    /// Writes atomically before updating the live task; repeating the same action is a no-op.</summary>
    public void SetArchived(Guid id, bool archived)
    {
        var task = FindTask(id);
        if (task.IsArchived == archived) return;
        Commit(task, task.Snapshot() with { IsArchived = archived });
    }

    private static void RequireUnarchived(Tasks task)
    {
        if (task.IsArchived) throw new ArgumentException("Restore this archived job before editing or completing it.");
    }

    /// <summary>Rebases only an explicitly changed date or frequency. Notes/type edits retain the original day.</summary>
    private static TaskRecord WithSchedule(TaskRecord task, RepeatSchedule repeat, DateTime dueDate)
    {
        if (!Enum.IsDefined(repeat)) throw new ArgumentException("Please select a supported repeat schedule.");
        if (repeat == RepeatSchedule.None) return task with { DueDate = dueDate, Repeat = repeat, ScheduleAnchor = null, Occurrence = 0 };
        if (task.Repeat == repeat && task.DueDate == dueDate && task.ScheduleAnchor is not null) return task;
        return task with { DueDate = dueDate, Repeat = repeat, ScheduleAnchor = dueDate, Occurrence = 0 };
    }

    /// <summary>Completes and creates exactly one successor in one atomic save. Failures leave both disk and memory unchanged.</summary>
    private void CommitCompletion(Tasks original, TaskRecord record)
    {
        var completed = record with { Status = Status.Complete, CompletedAt = DateTime.UtcNow };
        Tasks? next = null;
        if (record.Repeat != RepeatSchedule.None)
        {
            int index = checked(record.Occurrence + 1);
            DateTime due = Recurrence.NextDueDate(record)!.Value;
            next = Tasks.Restore(record with { Id = Guid.NewGuid(), Status = Status.Pending, CompletedAt = null,
                DueDate = due, Occurrence = index, PreviousOccurrenceId = original.Id });
        }
        var proposed = m_taskList.Select(t => t.Id == original.Id ? Tasks.Restore(completed) : t).ToList();
        if (next is not null) proposed.Add(next);
        Persist(proposed);
        original.Apply(completed);
        if (next is not null) m_taskList.Add(next);
    }

    private void Commit(Tasks original, TaskRecord updated)
    {
        Persist(m_taskList.Select(t => t.Id == original.Id ? Tasks.Restore(updated) : t));
        original.Apply(updated);
    }

    private Tasks FindTask(Guid id) => m_taskList.SingleOrDefault(t => t.Id == id)
        ?? throw new ArgumentException("Task no longer exists.");
}
