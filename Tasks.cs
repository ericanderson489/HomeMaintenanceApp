/* Task Class
 * Kiefer
 * Enables creation of tasks, helper functions, etc.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMaintenanceApp
{
    // Status of task. Is it pending, in progress or done?
    public enum Status
    {
        Pending,
        WIP,
        Complete
    }

    // Frequency for recurring tasks
    public enum Frequency
    {
        Weekly,
        Monthly,
        Quarterly,
        Annually,
    }
    internal class Tasks
    {
        private string m_name;
        private string m_description;
        private string m_type;
        // To be implemented in a future build when UI is established
        // private Frequency m_frequency;
        // private Status m_status;
        private DateTime m_dueDate;

        public Tasks (string name, string description, string type, DateTime dueDate)
        {
            m_name = name;
            m_description = description;
            m_type = type;
            m_dueDate = dueDate;
        }

        // Set and Get methods for Tasks objects
        public string GetName() { return m_name; }
        public void SetName(string name) { m_name = name; }

        public string GetDescription() { return m_description; }
        public void SetDescription(string description) { m_description = description; }

        public string GetTaskType() { return m_type; }
        public void SetType(string type) { m_type = type; }

        public Frequency GetFrequency() { return m_frequency; }
        public void SetFrequency(Frequency frequency) { m_frequency = frequency; }

        public Status GetStatus() { return m_status; }
        public void SetStatus(Status status) { m_status = status; }
    }
}
