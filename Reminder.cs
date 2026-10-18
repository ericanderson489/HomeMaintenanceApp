using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

//The class is deticated to giving the user reminders of their task's upcoming due dates.

namespace HomeMaintenanceApp
{
	static class Reminder
	{
		public static void ShowSoonToDo(Account account)
		{
			List<Tasks> taskList = account.GetListOfTasks();

			List<Tasks> dueList = new List<Tasks>();

			DateTime currDate = DateTime.Now;

			string message = "";

			for (int i = 0; i < taskList.Count; i++)
			{
				int daysRemaining = (taskList[i].GetDate() - currDate).Days;

				if (daysRemaining <= 7 && daysRemaining > 0)
				{
					message = message + taskList[i].GetName() + " is due in " + daysRemaining + "!\n";
				}
				else if (daysRemaining == 0)
				{
					message = message + taskList[i].GetName() + " is due today!" + '\n';
				}
				else 
				{
					message = message + taskList[i].GetName() + " is over do!" + '\n';
				}
			}

			MessageBox.Show(message);

		}
	}
}
