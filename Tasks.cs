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