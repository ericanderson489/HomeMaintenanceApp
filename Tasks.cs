namespace HomeMaintenanceApp
{
    internal enum Status
    {
        Pending,
        Complete
    }
    internal enum Frequency
    {
        Weekly,
        Monthly,
        Quarterly,
        Annually
    }
    internal class Tasks
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        internal RepeatSchedule Repeat { get; private set; }
        private DateTime? scheduleAnchor;
        private int occurrence;
        internal Guid? PreviousOccurrenceId { get; private set; }
        internal DateTime? CompletedAt { get; private set; }
        internal bool IsArchived { get; private set; }
        internal TaskRecord Snapshot() => new(Id, m_name, m_description, m_type, m_dueDate, m_status,
            Repeat, scheduleAnchor, occurrence, PreviousOccurrenceId, CompletedAt, IsArchived);
        internal static Tasks Restore(TaskRecord record)
        {
            var task = new Tasks(record.Name, record.Description, record.Type, record.DueDate);
            task.Apply(record);
            return task;
        }
        internal void Apply(TaskRecord record)
        {
            Id = record.Id;
            m_name = record.Name;
            m_description = record.Description;
            m_type = record.Type;
            m_dueDate = record.DueDate;
            m_status = record.Status;
            Repeat = record.Repeat;
            scheduleAnchor = record.ScheduleAnchor;
            occurrence = record.Occurrence;
            PreviousOccurrenceId = record.PreviousOccurrenceId;
            CompletedAt = record.CompletedAt;
            IsArchived = record.IsArchived;
        }
        public override string ToString() => m_name;
        private string m_name;
        private string m_description;
        private string m_type;
        private Status m_status;
        private DateTime m_dueDate;
        public Tasks(string name, string description, string type, DateTime dueDate)
        {
            m_name = name;
            m_description = description;
            m_type = type;
            m_dueDate = dueDate;
            m_status = Status.Pending;
        }
        public string GetName() { return m_name; }
        public void SetName(string name) { m_name = name; }
        public string GetDescription() { return m_description; }
        public void SetDescription(string description) { m_description = description; }
        public string GetTaskType() { return m_type; }
        public void SetType(string type) { m_type = type; }
        public DateTime GetDate() { return m_dueDate; }
        public void SetDate(DateTime date) { m_dueDate = date; }
        public Status GetStatus() { return m_status; }
        public void SetStatus(Status status) { m_status = status; }
    }
}
