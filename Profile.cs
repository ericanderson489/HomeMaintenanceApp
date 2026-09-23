using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMaintenanceApp
{
    public class Profile
    {
        private string m_username;
        private string m_password;
        private int m_taskCount = 0;
        private List<Tasks> m_taskList;

        // Could be useful to store unique IDs for users?
        // private Guid m_ID;

        public Profile (string username, string password)
        {
            m_username = username;
            m_password = password;
            m_taskList = new List<Tasks>();
        }

        // Get and Set methods for Profile variables

        public int GetTaskCount() { return m_taskCount; }
        public void SetTaskCount(int taskCount) { m_taskCount = taskCount; }

        public List<Tasks> GetTaskList() { return m_taskList; }
        public void SetTaskList(List<Tasks> taskList) {  m_taskList = taskList; }

        // Add Tasks. 
        public void AddTask(string name, string description, string type, DateTime time)
        {
            Tasks newTask = new Tasks(name, description, type, time);
            m_taskList.Add(newTask);
        }

        public void AddTask(Tasks newTask)
        {
            m_taskList.Add(newTask);
        }

        // Delete Tasks. Returns bool so if task not found, prompts user that task is not in list.
        public bool DeleteTask(string taskName)
        {
            bool isFound = false;

            foreach (var task in GetTaskList())
            {
                if (taskName.ToLower().TrimEnd() == task.GetName().ToLower().TrimEnd())
                {
                    GetTaskList().Remove(task);
                    isFound = true;
                    return isFound;
                }
            }

            return isFound;
        }

        // Edit Tasks. Returns bool so if task not found, prompts user that task is not in list.
        public void EditTask(string name, string description, string type, DateTime time)
        {
            foreach (var task in GetTaskList())
            {
                if (name.ToLower().Trim() == task.GetName().ToLower().Trim())
                {
                    task.SetName(name);
                    task.SetDescription(description);
                    task.SetType(type);
                    task.SetDate(time);
                }
            }
        }

        // Print tasks (may be useful in list boxes?)
        public string DisplayTask(Tasks task)
        {
            return $"{task.GetName()}: {task.GetDescription()}. Due: {task.GetDate()}";
        }

        // Return name of task at specified index. For creation of task name drop downs.
        public Tasks GetTaskAt(int idx)
        {
            return m_taskList[idx];
        }
    }
}
