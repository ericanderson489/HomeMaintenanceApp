using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMaintenanceApp
{
    internal class Account
    {
        //List<Tasks> tasks = new List<Tasks>(); //Just in case we have a list of tasks tied to each account
        private string m_firstName, m_lastName, m_userName;
        private string m_password = "";
        private List<Tasks> m_taskList;
        public Account(string username, string password, string firstName, string lastName)
        {// Constructor for the account
            m_userName = username;
            m_firstName = firstName;
            m_lastName = lastName;
            m_password = password;
            m_taskList = new List<Tasks>();
        }
        public string GetAccountFullName()
        {// Gets the full account name
            string name = (m_firstName + " " + m_lastName);
            return name;
        }
        public string GetAccountFirstName()
        {// Gets the first name
            return m_firstName;
        }
        public string GetAccountUserName()
        {
            return m_userName;
        }
        public string GetAccountPassword()
        {
            return m_password;
        }
        public override string ToString() //Causes the account name to be shown in lists instead of the variable name itself - JD
        {
            return GetAccountFullName();
        }
        public string GiveFileString() //Returns a string that is used in file-rewriting - JD
        {
            return ($"{m_userName} {m_password} {m_firstName} {m_lastName}");

        }
        public List<Tasks> GetTaskList()
        {// Returns the complete task list for this account
            return m_taskList;
        }      
        // Creates and adds a new task to this account
        public void AddTask(
            string name,
            string description,
            string type,
            DateTime dueDate)
        {
            Tasks newTask = new Tasks(name, description, type, dueDate);
            m_taskList.Add(newTask);
        }
    }
}
