using Microsoft.Toolkit.Uwp.Notifications;
using System.Diagnostics;

namespace HomeMaintenanceApp
{
	// Shows a Windows reminder for unfinished, unarchived tasks that are overdue
	// or due within the next seven calendar days.
	static class Reminder
	{
		public static void ShowSoonToDo(Account account)
		{
			DateTime today = DateTime.Today;

			var dueTasks = account.GetTaskList()
				.Where(task => task.GetStatus() != Status.Complete)
				.Select(task => new
				{
					Name = task.GetName(),
					DaysRemaining = (task.GetDate().Date - today).Days
				})
				.Where(task => task.DaysRemaining <= 7)
				.OrderBy(task => task.DaysRemaining)
				.ToList();

			// Don't notify for completed/archived tasks or deadlines more than a week away.
			if (dueTasks.Count == 0) return;

			string summary = dueTasks.Count == 1
				? "1 maintenance task needs attention"
				: $"{dueTasks.Count} maintenance tasks need attention";

			string details = string.Join("; ", dueTasks.Take(2).Select(task =>
				task.DaysRemaining < 0 ? $"{task.Name}: overdue" :
				task.DaysRemaining == 0 ? $"{task.Name}: due today" :
				$"{task.Name}: due in {task.DaysRemaining} day{(task.DaysRemaining == 1 ? "" : "s")}"));

			if (dueTasks.Count > 2)
				details += $"; and {dueTasks.Count - 2} more";

			// A notification failure should not prevent a successful login.
			try
			{
				new ToastContentBuilder()
					.AddText("Home Maintenance Reminder")
					.AddText(summary)
					.AddText(details)
					.Show();
			}
			catch (Exception ex)
			{
				Trace.TraceWarning("Could not show task reminder: {0}", ex);
			}
		}
	}
}
