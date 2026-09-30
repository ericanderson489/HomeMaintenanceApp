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
    public IReadOnlyList<Tasks> GetTaskList() => m_taskList.AsReadOnly();

    private AccountRecord Snapshot(IEnumerable<Tasks> tasks) =>
        new(1, revision, m_userName, m_firstName, m_lastName, password, tasks.Select(t => t.Snapshot()).ToList());

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

    public void AddTask(string name, string description, string type, DateTime dueDate)
    {
        TaskRules.Validate(name, description, type, dueDate);
        var task = new Tasks(name.Trim(), description.Trim(), type, dueDate.Date);
        Persist(m_taskList.Append(task));
        m_taskList.Add(task);
    }

    // An overdue task may keep its existing date; newly selected dates must not be in the past.
    public void UpdateTask(Guid id, string name, string description, string type, DateTime dueDate, bool complete = false)
    {
        var task = FindTask(id);
        TaskRules.Validate(name, description, type, dueDate, task.GetDate());
        Commit(task, task.Snapshot() with { Name = name.Trim(), Description = description.Trim(),
            Type = type, DueDate = dueDate.Date, Status = complete ? Status.Complete : task.GetStatus() });
    }

    public void CompleteTask(Guid id)
    {
        var task = FindTask(id);
        Commit(task, task.Snapshot() with { Status = Status.Complete });
    }

    private void Commit(Tasks original, TaskRecord updated)
    {
        Persist(m_taskList.Select(t => t.Id == original.Id ? Tasks.Restore(updated) : t));
        original.Apply(updated);
    }

    private Tasks FindTask(Guid id) => m_taskList.SingleOrDefault(t => t.Id == id)
        ?? throw new ArgumentException("Task no longer exists.");
}
