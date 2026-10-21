using System.Reflection;
using HomeMaintenanceApp;
using HomeMaintenanceApp.UI;

internal static class Program
{
    private static readonly Assembly App = typeof(ITaskPresentationSource).Assembly;
    private static int checks;
    [STAThread]
    private static int Main(string[] args)
    {
        try
        {
            // Send UI-thread exceptions through the same failure-reporting boundary as assertions.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            if (args.Contains("--verify-failure-reporting"))
                throw new InvalidOperationException("FAIL: Intentional failure-reporting check.");
            Run();
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("INTEGRATION TEST FAILED");
            Console.Error.WriteLine(ex);
            Console.Error.WriteLine("Exit code: 1");
            return 1;
        }
    }

    private static void Run()
    {
        ApplicationConfiguration.Initialize();
        System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
        using var watchdog = new System.Threading.Timer(_ =>
        {
            Console.Error.WriteLine("INTEGRATION TEST FAILED\nFAIL: UI checks timed out.\nExit code: 1");
            Environment.Exit(1);
        }, null, 90_000, Timeout.Infinite);
        string accountPath = Path.Combine(AppContext.BaseDirectory, "TestResults", Guid.NewGuid().ToString("N"));
        Environment.SetEnvironmentVariable("HMM_DATA_DIRECTORY", accountPath);
        using var main = (Form)New("MainForm");
        main.StartPosition = FormStartPosition.Manual;
        main.Location = new Point(-30000, -30000);
        main.ShowInTaskbar = false;
        main.Show(); Application.DoEvents();
        Capture(main, "login");
        Require(All(main).OfType<TextBox>().Single(t => t.Name == "passwordTextextBox").UseSystemPasswordChar, "login password masked");
        All(main).OfType<Button>().Single(b => b.Name == "createAccountButton").PerformClick();
        Application.DoEvents(); Capture(main, "create-account");
        var create = All(main).Single(c => c.GetType().Name == "CreateAccountControl");
        Require(create.Visible && create.Width > 500, "create account is visible and fills the surface");
        CaptureControl(create, "create-account-surface");
        Require(All(create).OfType<TextBox>().All(t => t.Text == ""), "account placeholder text cleared");
        All(create).OfType<Button>().Single(b => b.Name == "cancelCreateButton").PerformClick();
        Require(!All(main).Any(c => c.GetType().Name == "CreateAccountControl"), "team cancel event returns to login");
        All(main).OfType<Button>().Single(b => b.Name == "createAccountButton").PerformClick();
        create = All(main).Single(c => c.GetType().Name == "CreateAccountControl");
        Text(create, "fnameTextBox").Text = "Workshop First";
        Text(create, "lnameTextBox").Text = "Test Last";
        Text(create, "usernameTextBox").Text = "workshop_test";
        Text(create, "passwordTextBox").Text = "test-only-password with spaces";
        Button(create, "createAccountButton").PerformClick();
        Require(!All(main).Any(c => c.GetType().Name == "CreateAccountControl"), "create account button saves and returns to login");
        string savedFile = Directory.GetFiles(accountPath, "*.json").Single();
        Require(!File.ReadAllText(savedFile).Contains("test-only-password"), "stored credentials contain no plaintext password");
        var loaded = (System.Collections.IList)Call(New("AccountFileReading"), "GetAccountList")!;
        Require((string)Call(loaded[0]!, "GetAccountFullName")! == "Workshop First Test Last", "names containing spaces round-trip");
        All(main).OfType<TextBox>().Single(t => t.Name == "usernameTextBox").Text = "workshop_test";
        Text(main, "passwordTextextBox").Text = "wrong";
        Button(main, "loginButton").PerformClick();
        Require(All(main).OfType<Label>().Any(l => l.Text == "Username or password is incorrect."), "incorrect password stays on login");
        Text(main, "passwordTextextBox").Text = "test-only-password with spaces";
        All(main).OfType<Button>().Single(b => b.Name == "loginButton").PerformClick();
        Application.DoEvents();
        Require(All(main).Any(c => c.GetType().Name == "WorkshopDashboardControl"), "actual team login opens workshop dashboard");
        var source = (ITaskPresentationSource)Field(main, "taskSource")!;
        Require(!source.IsPreview && source.GetTasks().Count == 0, "real account starts without sample tasks");
        Capture(main, "empty-home");
        AddThroughUi(main);
        Require(All(main).OfType<Label>().Any(l => l.AccessibleName == "Task feedback" && l.Text == "Task saved."), "successful save displays confirmation");
        Require(source.GetTasks().Count == 1, "workshop save adds to team account");
        source.Save(null, new("Replace HVAC filter", "Different room", DateTime.Today.AddDays(2), "Priority"));
        var snapshot = source.GetTasks();
        Require(snapshot[0].Id != snapshot[1].Id, "duplicate titles have distinct IDs");
        var secondId = snapshot[1].Id;
        source.Save(secondId, new("Replace HVAC filter", "Only second task changed", DateTime.Today.AddDays(3), "Recurring"));
        Require(source.GetTasks()[0].Notes == "Use the correct filter size." && source.GetTasks()[1].Notes == "Only second task changed", "duplicate-title edit targets exact object");
        source.Complete(secondId);
        Require(!source.GetTasks()[0].IsComplete && source.GetTasks()[1].IsComplete, "completion targets exact task");
        ExpectValidation(source, new("Bad task", "", DateTime.Today, "Priority"));
        ExpectValidation(source, new("Bad task", "Notes", DateTime.Today.AddDays(-1), "Priority"));
        ExpectValidation(source, new("Bad task", "Notes", DateTime.Today, "Unknown"));
        var other = (ITaskPresentationSource)New("UI.AccountTaskSource", New("Account", "other", "test", "Other", "User"));
        Require(other.GetTasks().Count == 0, "account isolation");
        All(main).OfType<Button>().Single(b => b.Text == "&Home").PerformClick();
        Application.DoEvents(); Capture(main, "integrated-home");
        CheckDashboardActivation(main);
        All(main).OfType<Button>().Single(b => b.Text == "&Tasks").PerformClick();
        Application.DoEvents();
        var list = All(main).OfType<ListView>().Single();
        Require(list.Items.Count == 2 && list.Items[1].SubItems[2].Text == "Completed", "completed items remain visible");
        list.Items[0].Selected = true;
        All(main).OfType<Button>().Single(b => b.Text == "View details").PerformClick();
        Application.DoEvents(); Capture(main, "integrated-details");
        Require(All(main).OfType<Label>().Any(l => l.Text.Contains("Type: Priority")), "type appears in details");
        CheckWorkshopLayout(main);
        CancelEdit(main, source.GetTasks()[0]);
        Require(source.GetTasks()[0].Title == "Replace HVAC filter", "cancel does not change real account");
        WithDialog(main, "Edit task", editor =>
        {
            var notes = All(editor).OfType<TextBox>().Single(t => t.AccessibleName == "Description or notes");
            notes.Text = "Successfully edited through the screen.";
            using (var locked = new FileStream(Path.Combine(accountPath, ".write.lock"), FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                All(editor).OfType<Button>().Single(b => b.Text == "&Save").PerformClick();
                Require(editor.Visible && notes.Text == "Successfully edited through the screen.", "failed save retains editor fields");
                Require(source.GetTasks()[0].Notes == "Use the correct filter size.", "failed save does not mutate account");
                Require(All(editor).OfType<Label>().Any(l => l.Text.Contains("could not be saved")), "failed save displays feedback");
            }
            All(editor).OfType<Button>().Single(b => b.Text == "&Save").PerformClick();
            Require(editor.DialogResult == DialogResult.OK, "successful edit closes dialog");
        });
        Require(source.GetTasks()[0].Notes == "Successfully edited through the screen.", "UI edit updates correct task");
        All(main).OfType<ListView>().Single().Items[0].Selected = true;
        All(main).OfType<Button>().Single(b => b.Text == "View details").PerformClick();
        All(main).OfType<Button>().Single(b => b.Text == "Mark complete").PerformClick();
        Require(source.GetTasks().All(t => t.IsComplete), "workshop complete updates account");
        var activeAccount = Field(main, "currentAccount")!;
        using var teamTasks = (Control)New("TasksControl", activeAccount);
        Require(All(teamTasks).OfType<Label>().All(l => l.Text != "Replace HVAC filter"), "team's original task screen sees completed state too");
        var loadedAccounts = (System.Collections.IList)Call(New("AccountFileReading"), "GetAccountList")!;
        Require(loadedAccounts.Count == 1, "saved account reloads");
        var reloadedSource = (ITaskPresentationSource)New("UI.AccountTaskSource", loadedAccounts[0]!);
        Require(reloadedSource.GetTasks().SequenceEqual(source.GetTasks()), "all fields, IDs and completed status survive reload");
        var stale = reloadedSource;
        source.Save(null, new("New change", "Saved by first session", DateTime.Today, "Priority"));
        try { stale.Save(null, new("Stale change", "Must not overwrite", DateTime.Today, "Priority")); throw new Exception("Expected stale save rejection"); }
        catch (IOException) { Require(stale.GetTasks().Count == 2, "stale session rejected without in-memory mutation"); }
        string valid = File.ReadAllText(savedFile);
        File.WriteAllText(savedFile, "{ truncated");
        try { Call(New("AccountFileReading"), "GetAccountList"); throw new Exception("Expected damaged file rejection"); }
        catch (TargetInvocationException ex) when (ex.InnerException is InvalidDataException) { Require(File.ReadAllText(savedFile) == "{ truncated", "damaged file rejected without overwriting"); }
        File.WriteAllText(savedFile, valid);
        CheckAccountFailures(accountPath);
        CheckOverdueEdit(savedFile);
        CheckRecurrence(main, accountPath);
        CheckSheetLifetime(main);
        CheckTaskPolish(main, accountPath);
        var selectedTask = ((ITaskPresentationSource)Field(main, "taskSource")!).GetTasks()[0];
        Call(main, "ShowDetails", selectedTask);
        main.Close();
        Application.DoEvents();
        Require(main.IsDisposed, "closing the window safely cancels pending task selection");
        Console.WriteLine($"PASS: {checks} integration assertions. Screenshots: {AppContext.BaseDirectory}");
    }

    // Follow the dashboard double-click path through the native list window procedure.
    private static void CheckDashboardActivation(Form main)
    {
        var list = All(main).OfType<ListView>().Single();
        list.Focus();
        list.Items[0].Selected = true;
        list.Items[0].Focused = true;
        var bounds = list.Items[0].Bounds;
        IntPtr point = new((bounds.Left + 10) | ((bounds.Top + bounds.Height / 2) << 16));
        IntPtr window = list.Handle;
        PostMessage(window, 0x0201, new IntPtr(1), point);
        PostMessage(window, 0x0202, IntPtr.Zero, point);
        PostMessage(window, 0x0203, new IntPtr(1), point);
        PostMessage(window, 0x0202, IntPtr.Zero, point);
        Application.DoEvents();
        Require(All(main).Any(c => c.GetType().Name == "TaskDetailsControl"), "dashboard double-click opens the selected job without disposing an active native event");
        foreach (bool keyboard in new[] { true, false })
        {
            All(main).OfType<Button>().Single(b => b.Text == "&Home").PerformClick();
            Application.DoEvents();
            list = All(main).OfType<ListView>().Single();
            list.Focus();
            list.Items[0].Selected = true;
            list.Items[0].Focused = true;
            var selected = (TaskViewData)list.Items[0].Tag!;
            if (keyboard)
            {
                PostMessage(list.Handle, 0x0100, new IntPtr((int)Keys.Enter), IntPtr.Zero);
                PostMessage(list.Handle, 0x0101, new IntPtr((int)Keys.Enter), IntPtr.Zero);
            }
            else ClickNative(All(main).OfType<Button>().Single(b => b.Text == "View details"));
            Application.DoEvents();
            var sheet = All(main).Single(c => c.GetType().Name == "TaskDetailsControl");
            Require(((TaskViewData)Field(sheet, "task")!).Id == selected.Id,
                keyboard ? "Enter opens the correct dashboard job" : "View details button opens the correct dashboard job");
        }
        All(main).OfType<Button>().Single(b => b.Text == "&Home").PerformClick();
        Application.DoEvents();
        list = All(main).OfType<ListView>().Single();
        list.Items[0].Selected = true;
        typeof(ListView).GetMethod("OnItemActivate", BindingFlags.Instance | BindingFlags.NonPublic)!
            .Invoke(list, new object[] { EventArgs.Empty });
        All(main).OfType<Button>().Single(b => b.Text == "&Tasks").PerformClick();
        Application.DoEvents();
        Require(All(main).Any(c => c.GetType().Name == "WorkshopTasksControl")
            && !All(main).Any(c => c.GetType().Name == "TaskDetailsControl"),
            "pending dashboard activation is discarded after navigating away");
    }

    private static void CheckRecurrence(Form main, string previousDirectory)
    {
        string directory = Path.Combine(previousDirectory, "recurrence");
        Environment.SetEnvironmentVariable("HMM_DATA_DIRECTORY", directory);
        try
        {
            var account = New("Account", "repeat_test", "repeat-test-password", "Repeat", "Tester");
            Call(account, "CreateIn", New("AccountStore"));
            var source = (ITaskPresentationSource)New("UI.AccountTaskSource", account);
            source.Save(null, new("Legacy recurring label", "No schedule was selected in the old version.", DateTime.Today, "Recurring"));
            string saved = Directory.GetFiles(directory, "*.json").Single();
            var oldJson = System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(saved))!;
            oldJson["Schema"] = 1;
            foreach (var item in oldJson["Tasks"]!.AsArray())
                foreach (string field in new[] { "Repeat", "ScheduleAnchor", "Occurrence", "PreviousOccurrenceId", "CompletedAt", "IsArchived" }) item!.AsObject().Remove(field);
            File.WriteAllText(saved, oldJson.ToJsonString());
            ITaskPresentationSource Reload()
            {
                var loaded = (System.Collections.IList)Call(New("AccountFileReading"), "GetAccountList")!;
                return (ITaskPresentationSource)New("UI.AccountTaskSource", loaded[0]!);
            }
            source = Reload();
            Require(source.GetTasks().Single().Repeat == RepeatSchedule.None, "legacy recurring labels load without guessing a frequency");
            source.Complete(source.GetTasks().Single().Id);
            Require(source.GetTasks().Count == 1, "legacy unscheduled completion creates no extra job");
            Require(System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(saved))!["Schema"]!.GetValue<int>() == 3, "legacy account upgrades on a successful save");

            Call(main, "ShowLogin");
            Text(main, "usernameTextBox").Text = "repeat_test";
            Text(main, "passwordTextextBox").Text = "repeat-test-password";
            Button(main, "loginButton").PerformClick();
            Application.DoEvents();
            source = (ITaskPresentationSource)Field(main, "taskSource")!;
            DateTime firstDate = new(DateTime.Today.Year + 2, 1, 31);
            WithDialog(main, "+ Add task", editor =>
            {
                All(editor).OfType<TextBox>().Single(t => t.AccessibleName == "Task title").Text = "Monthly filter";
                All(editor).OfType<TextBox>().Single(t => t.AccessibleName == "Description or notes").Text = "Check and replace the filter.";
                All(editor).OfType<DateTimePicker>().Single().Value = firstDate;
                var type = All(editor).OfType<ComboBox>().Single(c => c.AccessibleName == "Task type");
                var repeat = All(editor).OfType<ComboBox>().Single(c => c.AccessibleName == "Repeat schedule");
                var save = All(editor).OfType<Button>().Single(b => b.Text == "&Save");
                type.SelectedItem = "Recurring";
                editor.ScrollControlIntoView(save);
                save.PerformClick();
                Require(All(editor).OfType<Label>().Any(l => l.Text.StartsWith("Choose a repeat interval")), "recurring creation requires an actual interval");
                type.SelectedItem = "Priority";
                repeat.SelectedItem = RepeatSchedule.Monthly;
                Capture(editor, "recurring-task-editor");
                save.PerformClick();
                Require(editor.DialogResult == DialogResult.OK, "recurring task saves through the editor");
            });
            var first = source.GetTasks().Single(t => t.Title == "Monthly filter");
            Require(first.Repeat == RepeatSchedule.Monthly && first.TaskType == "Priority", "priority and repeat schedule coexist");
            Require(Reload().GetTasks().Single(t => t.Id == first.Id) == first, "repeat interval survives reopening the account");

            // A failed atomic save must create neither half of the completion transaction.
            var before = source.GetTasks().ToArray();
            string bytes = File.ReadAllText(saved);
            using (var locked = new FileStream(Path.Combine(directory, ".write.lock"), FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                try { source.Complete(first.Id); throw new Exception("Expected locked completion rejection"); }
                catch (IOException) { Require(source.GetTasks().SequenceEqual(before) && File.ReadAllText(saved) == bytes, "failed recurrence save leaves history and next occurrence unchanged"); }
            }
            var stale = Reload();
            var list = All(main).OfType<ListView>().Single();
            foreach (ListViewItem row in list.Items) if (row.Tag is TaskViewData t && t.Id == first.Id) row.Selected = true;
            Application.DoEvents();
            Require(All(main).OfType<Label>().Any(l => l.Text.Contains("Repeat: Monthly")), "job sheet shows the saved repeat interval");
            All(main).OfType<Button>().Single(b => b.Text == "Mark complete").PerformClick();
            Application.DoEvents();
            var completed = source.GetTasks().Single(t => t.Id == first.Id);
            var next = source.GetTasks().Single(t => t.PreviousOccurrenceId == first.Id);
            Require(completed.IsComplete && completed.CompletedAt is not null && completed.DueDate == firstDate, "completion preserves dated job history");
            Require(!next.IsComplete && next.Id != first.Id && next.DueDate == firstDate.AddMonths(1) && next.Repeat == RepeatSchedule.Monthly, "UI completion creates one monthly successor from the due date");
            Require(first.NextOccurrenceDate == next.DueDate && next.NextOccurrenceDate == new DateTime(firstDate.Year, 3, 31), "preview agrees with completion and preserves the month-end anchor after February");
            Require(next.Title == first.Title && next.Notes == first.Notes && next.TaskType == first.TaskType, "next occurrence preserves job content and type");
            Capture(main, "completed-recurring-job");
            bytes = File.ReadAllText(saved);
            source.Complete(first.Id);
            Require(source.GetTasks().Count(t => t.PreviousOccurrenceId == first.Id) == 1 && File.ReadAllText(saved) == bytes, "duplicate completion is a no-op");
            try { stale.Complete(first.Id); throw new Exception("Expected stale completion rejection"); }
            catch (IOException) { Require(!stale.GetTasks().Single(t => t.Id == first.Id).IsComplete && Reload().GetTasks().Count(t => t.PreviousOccurrenceId == first.Id) == 1, "stale session cannot duplicate the next occurrence"); }
            source = Reload();
            Require(source.GetTasks().Single(t => t.Id == completed.Id) == completed && source.GetTasks().Single(t => t.Id == next.Id) == next, "completed history and successor survive restart");
            source.Complete(first.Id);
            Require(source.GetTasks().Count(t => t.PreviousOccurrenceId == first.Id) == 1, "duplicate completion after restart is a no-op");

            using (var editor = (Form)New("UI.TaskEditorForm", next, new Action<TaskFormData>(fields => source.Save(next.Id, fields))))
            {
                editor.ShowInTaskbar = false;
                editor.StartPosition = FormStartPosition.Manual;
                editor.Location = new Point(-30000, -30000);
                editor.Show();
                Require((RepeatSchedule)All(editor).OfType<ComboBox>().Single(c => c.AccessibleName == "Repeat schedule").SelectedItem! == RepeatSchedule.Monthly, "edit loads the saved repeat selection");
                All(editor).OfType<ComboBox>().Single(c => c.AccessibleName == "Repeat schedule").SelectedItem = RepeatSchedule.Yearly;
                All(editor).OfType<Button>().Single(b => b.Text == "&Cancel").PerformClick();
                Require(source.GetTasks().Single(t => t.Id == next.Id).Repeat == RepeatSchedule.Monthly, "canceling a schedule edit changes nothing");
            }
            source.Save(next.Id, new(next.Title, "Edited notes", next.DueDate, next.TaskType, next.Repeat));
            source.Complete(next.Id);
            var march = source.GetTasks().Single(t => t.PreviousOccurrenceId == next.Id);
            Require(march.DueDate == new DateTime(firstDate.Year, 3, 31), "month-end anchor returns to March 31 after February and a notes edit");
            Require(march.Notes == "Edited notes", "edited notes carry into the next occurrence");
            source.Save(march.Id, new(march.Title, march.Notes, new DateTime(firstDate.Year, 4, 15), march.TaskType, RepeatSchedule.Monthly));
            source.Complete(march.Id);
            var may = source.GetTasks().Single(t => t.PreviousOccurrenceId == march.Id);
            Require(may.DueDate == new DateTime(firstDate.Year, 5, 15), "explicit due-date edit starts a new schedule anchor");
            source.Save(may.Id, new(may.Title, may.Notes, may.DueDate, may.TaskType, RepeatSchedule.None));
            int total = source.GetTasks().Count;
            source.Complete(may.Id);
            Require(source.GetTasks().Count == total, "turning repeats off stops future occurrence creation");
            try { source.Save(first.Id, new(first.Title, first.Notes, first.DueDate, first.TaskType, RepeatSchedule.Yearly)); throw new Exception("Expected completed schedule rejection"); }
            catch (ArgumentException) { Require(source.GetTasks().Single(t => t.Id == first.Id).Repeat == RepeatSchedule.Monthly, "completed schedule history cannot be rewritten"); }

            foreach (var scenario in new[] { (RepeatSchedule.Weekly, firstDate.AddDays(7)), (RepeatSchedule.Quarterly, firstDate.AddMonths(3)), (RepeatSchedule.Yearly, firstDate.AddYears(1)) })
            {
                source.Save(null, new(scenario.Item1.ToString(), "Calendar check", firstDate, "Recurring", scenario.Item1));
                var task = source.GetTasks().Last();
                source.Complete(task.Id);
                Require(source.GetTasks().Single(t => t.PreviousOccurrenceId == task.Id).DueDate == scenario.Item2, scenario.Item1 + " advances by its calendar interval");
            }
            var stop = source.GetTasks().Single(t => t.Title == "Quarterly" && !t.IsComplete);
            using (var editor = (Form)New("UI.TaskEditorForm", stop, new Action<TaskFormData>(fields => source.Save(stop.Id, fields))))
            {
                editor.ShowInTaskbar = false;
                editor.StartPosition = FormStartPosition.Manual;
                editor.Location = new Point(-30000, -30000);
                editor.Show();
                All(editor).OfType<ComboBox>().Single(c => c.AccessibleName == "Repeat schedule").SelectedItem = RepeatSchedule.None;
                var save = All(editor).OfType<Button>().Single(b => b.Text == "&Save");
                editor.ScrollControlIntoView(save);
                save.PerformClick();
                Require(editor.DialogResult == DialogResult.OK && source.GetTasks().Single(t => t.Id == stop.Id).Repeat == RepeatSchedule.None, "editor can stop repeating without changing the legacy task type");
            }
            total = source.GetTasks().Count;
            source.Complete(stop.Id);
            Require(source.GetTasks().Count == total, "stopping a repeat in the editor prevents another occurrence");
            int leapYear = DateTime.Today.Year + 1;
            while (!DateTime.IsLeapYear(leapYear)) leapYear++;
            source.Save(null, new("Leap day", "Annual check", new DateTime(leapYear, 2, 29), "Recurring", RepeatSchedule.Yearly));
            var leap = source.GetTasks().Last();
            for (int i = 1; i <= 4; i++)
            {
                source.Complete(leap.Id);
                leap = source.GetTasks().Single(t => t.PreviousOccurrenceId == leap.Id);
                Require(leap.DueDate == new DateTime(leapYear + i, 2, DateTime.DaysInMonth(leapYear + i, 2)), "yearly leap-day anchor is preserved at year " + i);
            }

            // The original team editor completes through UpdateTask, so it must use the same transaction.
            var loadedAccount = ((System.Collections.IList)Call(New("AccountFileReading"), "GetAccountList")!)[0]!;
            source = (ITaskPresentationSource)New("UI.AccountTaskSource", loadedAccount);
            var weekly = source.GetTasks().Single(t => t.Title == "Weekly" && !t.IsComplete);
            Call(loadedAccount, "UpdateTask", weekly.Id, weekly.Title, "Team editor update", weekly.TaskType, weekly.DueDate, true, null!);
            Require(source.GetTasks().Single(t => t.PreviousOccurrenceId == weekly.Id).DueDate == weekly.DueDate.AddDays(7), "team editor completion also creates the next occurrence");

            source.Save(null, new("Late monthly job", "Catch-up check", DateTime.Today, "Recurring", RepeatSchedule.Monthly));
            Guid lateId = source.GetTasks().Last().Id;
            var lateJson = System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(saved))!;
            var lateRecord = lateJson["Tasks"]!.AsArray().Single(t => t!["Id"]!.GetValue<Guid>() == lateId)!;
            var lateDate = DateTime.Today.AddMonths(-3);
            lateRecord["DueDate"] = lateDate;
            lateRecord["ScheduleAnchor"] = lateDate;
            File.WriteAllText(saved, lateJson.ToJsonString());
            source = Reload();
            source.Complete(lateId);
            Require(source.GetTasks().Single(t => t.PreviousOccurrenceId == lateId).DueDate == lateDate.AddMonths(1), "late completion creates one scheduled occurrence without skipping missed jobs");

            source.Save(null, new("Last supported date", "Range check", new DateTime(9998, 12, 31), "Recurring", RepeatSchedule.Yearly));
            var terminal = source.GetTasks().Last();
            bytes = File.ReadAllText(saved);
            try { source.Complete(terminal.Id); throw new Exception("Expected calendar range rejection"); }
            catch (ArgumentException) { Require(!source.GetTasks().Single(t => t.Id == terminal.Id).IsComplete && File.ReadAllText(saved) == bytes, "date overflow leaves the original job pending and storage intact"); }
            try { source.Save(null, new("Invalid interval", "Range check", DateTime.Today, "Recurring", (RepeatSchedule)99)); throw new Exception("Expected invalid interval rejection"); }
            catch (ArgumentException) { Require(File.ReadAllText(saved) == bytes, "unknown interval cannot be saved"); }

            var corrupt = System.Text.Json.Nodes.JsonNode.Parse(bytes)!;
            corrupt["Tasks"]![1]!["Repeat"] = 99;
            string damaged = corrupt.ToJsonString();
            File.WriteAllText(saved, damaged);
            try { Reload(); throw new Exception("Expected corrupt schedule rejection"); }
            catch (TargetInvocationException ex) when (ex.InnerException is InvalidDataException) { Require(File.ReadAllText(saved) == damaged, "invalid stored schedule is rejected without overwriting data"); }
            File.WriteAllText(saved, bytes);
            Require(Reload().GetTasks().SequenceEqual(source.GetTasks()), "all recurrence cases survive the final reload");
        }
        finally { Environment.SetEnvironmentVariable("HMM_DATA_DIRECTORY", previousDirectory); }
    }

    // Verify reversible archive writes, calendar filter boundaries, and the real UI actions.
    private static void CheckTaskPolish(Form main, string previousDirectory)
    {
        string directory = Path.Combine(previousDirectory, "task-polish");
        Environment.SetEnvironmentVariable("HMM_DATA_DIRECTORY", directory);
        try
        {
            var account = New("Account", "polish_test", "polish-test-password", "Task", "Tester");
            Call(account, "CreateIn", New("AccountStore"));
            var source = (ITaskPresentationSource)New("UI.AccountTaskSource", account);
            foreach (var item in new[] { ("Overdue", 0), ("Today", 0), ("Soon", 6), ("Later", 7), ("Done", 0) })
                source.Save(null, new(item.Item1, "Filter boundary example", DateTime.Today.AddDays(item.Item2), "Priority"));
            source.Complete(source.GetTasks().Single(t => t.Title == "Done").Id);
            DateTime anchor = new(DateTime.Today.Year + 2, 1, 31);
            source.Save(null, new("Monthly", "Keep the original month-end anchor.", anchor, "Recurring", RepeatSchedule.Monthly));
            string saved = Directory.GetFiles(directory, "*.json").Single();
            var json = System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(saved))!;
            json["Tasks"]!.AsArray().Single(t => t!["Name"]!.GetValue<string>() == "Overdue")!["DueDate"] = DateTime.Today.AddDays(-1);
            // Schema 2 did not contain archive state; migration must preserve existing repeat data.
            json["Schema"] = 2;
            foreach (var item in json["Tasks"]!.AsArray()) item!.AsObject().Remove("IsArchived");
            File.WriteAllText(saved, json.ToJsonString());
            ITaskPresentationSource Reload()
            {
                var loaded = (System.Collections.IList)Call(New("AccountFileReading"), "GetAccountList")!;
                return (ITaskPresentationSource)New("UI.AccountTaskSource", loaded[0]!);
            }
            Require(Reload().GetTasks().All(t => !t.IsArchived), "schema 2 loads existing jobs as unarchived");
            Call(main, "ShowLogin");
            Text(main, "usernameTextBox").Text = "polish_test";
            Text(main, "passwordTextextBox").Text = "polish-test-password";
            Button(main, "loginButton").PerformClick();
            All(main).OfType<Button>().Single(b => b.Text == "&Tasks").PerformClick();
            Application.DoEvents();
            source = (ITaskPresentationSource)Field(main, "taskSource")!;
            void Filter(string name)
            {
                var filter = All(main).OfType<ComboBox>().Single(c => c.AccessibleName == "Task filter");
                filter.SelectedItem = filter.Items.Cast<object>().Single(v => v.ToString() == name);
                Application.DoEvents();
            }
            TaskViewData[] VisibleJobs() => All(main).OfType<ListView>().Single().Items.Cast<ListViewItem>().Select(i => (TaskViewData)i.Tag!).ToArray();
            void Open(Guid id)
            {
                var list = All(main).OfType<ListView>().Single();
                list.Items.Cast<ListViewItem>().Single(i => ((TaskViewData)i.Tag!).Id == id).Selected = true;
                Application.DoEvents();
            }
            foreach (var filter in new[] { ("All", 6), ("Active", 5), ("Overdue", 1), ("DueSoon", 2), ("Completed", 1), ("Archived", 0) })
            {
                Filter(filter.Item1);
                Require(VisibleJobs().Length == filter.Item2, filter.Item1 + " filter has the expected jobs");
                if (filter.Item1 == "DueSoon") Require(VisibleJobs().Select(t => t.Title).Order().SequenceEqual(new[] { "Soon", "Today" }), "due soon includes today and day six, excluding overdue and day seven");
            }
            Filter("All");
            var monthly = source.GetTasks().Single(t => t.Title == "Monthly");
            Open(monthly.Id);
            Require(All(main).OfType<Label>().Any(l => l.Text.Contains($"Next occurrence: {anchor.AddMonths(1):MMM d, yyyy}")), "job sheet previews the anchored successor before completion");
            Capture(main, "repeat-preview");
            CheckWorkshopLayout(main);
            string before = File.ReadAllText(saved);
            using (var locked = new FileStream(Path.Combine(directory, ".write.lock"), FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                try { source.SetArchived(monthly.Id, true); throw new Exception("Expected locked archive rejection"); }
                catch (IOException) { Require(source.GetTasks().Single(t => t.Id == monthly.Id) == monthly && File.ReadAllText(saved) == before, "failed archive leaves memory and disk unchanged"); }
            }
            var stale = Reload();
            ClickNative(All(main).OfType<Button>().Single(b => b.Text == "Archive"));
            Application.DoEvents();
            Require(VisibleJobs().Length == 5 && VisibleJobs().All(t => t.Id != monthly.Id), "archive removes one job from the normal list");
            Require(All(main).OfType<Label>().Any(l => l.AccessibleName == "Task feedback" && l.Text.StartsWith("Task archived.")), "successful archive explains where to restore the job");
            Require(Reload().GetTasks().Single(t => t.Id == monthly.Id) == monthly with { IsArchived = true, NextOccurrenceDate = null }, "archive survives reload without changing task identity or schedule");
            Require(System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(saved))!["Schema"]!.GetValue<int>() == 3, "archive upgrades schema 2 without losing schedule metadata");
            before = File.ReadAllText(saved);
            source.SetArchived(monthly.Id, true);
            Require(File.ReadAllText(saved) == before, "repeated archive does not rewrite storage");
            try { source.Complete(monthly.Id); throw new Exception("Expected archived completion rejection"); }
            catch (ArgumentException) { Require(source.GetTasks().Count == 6, "archived recurring job cannot create a successor"); }
            try { source.Save(monthly.Id, new("Changed", monthly.Notes, monthly.DueDate, monthly.TaskType, monthly.Repeat)); throw new Exception("Expected archived edit rejection"); }
            catch (ArgumentException) { Require(File.ReadAllText(saved) == before, "archived edits require restoration first"); }
            try { stale.SetArchived(monthly.Id, true); throw new Exception("Expected stale archive rejection"); }
            catch (IOException) { Require(!stale.GetTasks().Single(t => t.Id == monthly.Id).IsArchived, "stale archive cannot overwrite a newer save"); }
            All(main).OfType<Button>().Single(b => b.Text == "&Home").PerformClick();
            Application.DoEvents();
            Require(All(main).OfType<Label>().Any(l => l.Text == "4  On the bench"), "dashboard excludes archived jobs from active totals");
            Require(VisibleJobs().All(t => !t.IsArchived), "dashboard upcoming jobs exclude archived entries");
            var teamList = (System.Collections.IEnumerable)Call(Field(main, "currentAccount")!, "GetTaskList")!;
            Require(teamList.Cast<object>().Count() == 5, "legacy team screens also exclude archived jobs");
            All(main).OfType<Button>().Single(b => b.Text == "&Tasks").PerformClick();
            Filter("Archived");
            Open(monthly.Id);
            Require(!All(main).OfType<Button>().Any(b => b.Text is "Edit task" or "Mark complete"), "archived sheet offers restoration instead of edit or complete");
            Capture(main, "archived-job");
            using (var locked = new FileStream(Path.Combine(directory, ".write.lock"), FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                try { source.SetArchived(monthly.Id, false); throw new Exception("Expected locked restore rejection"); }
                catch (IOException) { Require(source.GetTasks().Single(t => t.Id == monthly.Id).IsArchived && File.ReadAllText(saved) == before, "failed restore retains archive state on disk and in memory"); }
            }
            ClickNative(All(main).OfType<Button>().Single(b => b.Text == "Restore"));
            Application.DoEvents();
            Require(VisibleJobs().Length == 0 && All(main).OfType<Label>().Any(l => l.Text == "Task restored."), "restore preserves the archive filter and confirms success");
            Require(Reload().GetTasks().Single(t => t.Id == monthly.Id) == monthly && source.GetTasks().Count == 6, "restoring recurrence preserves its original date without adding a job");
            Filter("Completed");
            var done = source.GetTasks().Single(t => t.Title == "Done");
            Open(done.Id);
            All(main).OfType<Button>().Single(b => b.Text == "Archive").PerformClick();
            Filter("Archived");
            Open(done.Id);
            All(main).OfType<Button>().Single(b => b.Text == "Restore").PerformClick();
            Application.DoEvents();
            Require(Reload().GetTasks().Single(t => t.Id == done.Id) == done, "restore preserves completed status and completion timestamp");
            Filter("DueSoon");
            All(main).OfType<Button>().Single(b => b.Text == "&Home").PerformClick();
            All(main).OfType<Button>().Single(b => b.Text == "&Tasks").PerformClick();
            Application.DoEvents();
            Require(VisibleJobs().Length == 2, "task filter survives Home and Tasks navigation");
            Filter("All");
            source.Complete(monthly.Id);
            var successor = source.GetTasks().Single(t => t.PreviousOccurrenceId == monthly.Id);
            Require(successor.DueDate == monthly.NextOccurrenceDate, "restored repeating task completes on its previewed date");
            var finished = source.GetTasks().Single(t => t.Id == monthly.Id);
            source.SetArchived(monthly.Id, true);
            Require(Reload().GetTasks().Single(t => t.Id == successor.Id) == successor, "archiving completed history leaves its pending successor unchanged");
            source.SetArchived(monthly.Id, false);
            Require(Reload().GetTasks().Single(t => t.Id == monthly.Id) == finished && source.GetTasks().Count == 7,
                "restoring completed repeat history never creates a second successor");
        }
        finally { Environment.SetEnvironmentVariable("HMM_DATA_DIRECTORY", previousDirectory); }
    }

    // Exercise queued selection events and sheet replacement while the native window is live.
    private static void CheckSheetLifetime(Form main)
    {
        for (int round = 0; round < 4; round++)
        {
            All(main).OfType<Button>().Single(b => b.Text == "&Tasks").PerformClick();
            Application.DoEvents();
            var list = All(main).OfType<ListView>().Single();
            for (int i = 0; i < list.Items.Count; i++)
            {
                list.Focus();
                list.Items[i].Selected = true;
                Application.DoEvents();
                typeof(Control).GetMethod("RecreateHandle", BindingFlags.Instance | BindingFlags.NonPublic)!
                    .Invoke(All(main).Single(c => c.GetType().Name == "WorkshopTasksControl"), null);
                Application.DoEvents();
                main.ClientSize = i % 2 == 0 ? new Size(850, 660) : new Size(1180, 760);
                Application.DoEvents();
                var close = All(main).OfType<Button>().Single(b => b.Text == "Close");
                close.Focus();
                ClickNative(close);
                Application.DoEvents();
                Require(!All(main).Any(c => c.GetType().Name == "TaskDetailsControl"), "closing a selected sheet leaves the list usable");
            }
            list.Items[0].Selected = true;
            All(main).OfType<Button>().Single(b => b.Text == "&Home").PerformClick();
            Application.DoEvents();
        }
        var task = ((ITaskPresentationSource)Field(main, "taskSource")!).GetTasks()[0];
        Call(main, "ShowDetails", task);
        All(main).OfType<Button>().Single(b => b.Text == "&Home").PerformClick();
        Application.DoEvents();
        Require(All(main).Any(c => c.GetType().Name == "WorkshopDashboardControl"), "navigation drains pending selection events safely");
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr window, int message, IntPtr wParam, IntPtr lParam);

    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    private static extern bool PostMessage(IntPtr window, int message, IntPtr wParam, IntPtr lParam);

    // Native mouse messages also exercise the button's window procedure after its click handler returns.
    private static void ClickNative(Control control)
    {
        IntPtr window = control.Handle;
        IntPtr point = new(10 | (10 << 16));
        SendMessage(window, 0x0201, new IntPtr(1), point);
        SendMessage(window, 0x0202, IntPtr.Zero, point);
    }

    private static void CheckWorkshopLayout(Form main)
    {
        var original = main.ClientSize;
        var list = All(main).OfType<ListView>().Single();
        var sheet = All(main).Single(c => c.GetType().Name == "TaskDetailsControl");
        Rectangle listBounds = list.RectangleToScreen(list.ClientRectangle);
        Rectangle sheetBounds = sheet.RectangleToScreen(sheet.ClientRectangle);
        Require(listBounds.Right <= sheetBounds.Left, "wide job sheet stays beside list");
        Require(list.Columns.Cast<ColumnHeader>().Sum(c => c.Width) <= list.ClientSize.Width, "wide task columns fit their viewport");
        main.ClientSize = new Size(900, 660);
        Application.DoEvents();
        listBounds = list.RectangleToScreen(list.ClientRectangle);
        sheetBounds = sheet.RectangleToScreen(sheet.ClientRectangle);
        Capture(main, "compact-job-sheet");
        Require(sheetBounds.Bottom <= listBounds.Top && list.Height >= 140, "compact sheet preserves visible task list");
        var showNotes = All(sheet).OfType<Button>().Single(b => b.Text == "Show notes");
        Require(showNotes.Visible, "compact notes disclosure is available");
        var edit = All(sheet).OfType<Button>().Single(b => b.Text == "Edit task");
        Require(sheetBounds.Contains(edit.RectangleToScreen(edit.ClientRectangle)), "compact edit action stays visible");
        Capture(main, "compact-job-sheet");
        showNotes.PerformClick();
        Application.DoEvents();
        Require(All(sheet).OfType<TextBox>().Single().Visible, "compact notes can be expanded");
        Require(list.Height >= 140, "expanded notes preserve useful list space");
        Require(sheet.RectangleToScreen(sheet.ClientRectangle).Bottom <= list.RectangleToScreen(list.ClientRectangle).Top,
            "expanded compact sheet never overlaps the list header");
        Capture(main, "expanded-compact-job-sheet");
        ((ScrollableControl)sheet).ScrollControlIntoView(edit);
        Application.DoEvents();
        Require(sheet.RectangleToScreen(sheet.ClientRectangle).Contains(edit.RectangleToScreen(edit.ClientRectangle)),
            "expanded compact sheet actions remain reachable by scrolling");
        main.ClientSize = original;
        Application.DoEvents();
        Require(All(sheet).OfType<TextBox>().Single().Visible, "wide sheet restores visible notes");
        var add = All(main).OfType<Button>().Single(b => b.Text == "+ Add task");
        Require(main.RectangleToScreen(main.ClientRectangle).Contains(add.RectangleToScreen(add.ClientRectangle)), "add task action fits the wide header");
    }

    private static void CheckAccountFailures(string directory)
    {
        using var host = new Form { ClientSize = new Size(460, 320), StartPosition = FormStartPosition.Manual,
            Location = new Point(-30000, -30000), ShowInTaskbar = false };
        using var login = (Control)New("LoginControl");
        login.Dock = DockStyle.Fill;
        host.Controls.Add(login);
        host.Show();
        Button(login, "createAccountButton").PerformClick();
        var create = All(login).Single(c => c.GetType().Name == "CreateAccountControl");
        Text(create, "fnameTextBox").Text = "Another";
        Text(create, "lnameTextBox").Text = "Tester";
        Text(create, "usernameTextBox").Text = "WORKSHOP_TEST";
        Text(create, "passwordTextBox").Text = "another test password";
        var save = Button(create, "createAccountButton");
        // Scroll to the actions before exercising them at a narrow/short size.
        var scroll = All(create).OfType<Panel>().Single(p => p.AutoScroll);
        scroll.ScrollControlIntoView(save);
        Application.DoEvents();
        save.PerformClick();
        Require(All(create).OfType<Label>().Any(l => l.Text.Contains("already exists")), "duplicate usernames rejected regardless of case");
        Text(create, "usernameTextBox").Text = "new-account";
        using (var locked = new FileStream(Path.Combine(directory, ".write.lock"), FileMode.Open, FileAccess.ReadWrite, FileShare.None))
        {
            save.PerformClick();
            Require(Text(create, "usernameTextBox").Text == "new-account" && All(create).OfType<Label>().Any(l => l.Text.Contains("could not be saved")),
                "account save failure preserves entries and gives feedback");
        }
        host.Scale(new SizeF(1.5f, 1.5f));
        host.ClientSize = new Size(460, 320);
        scroll.ScrollControlIntoView(save);
        Application.DoEvents();
        Rectangle visible = scroll.RectangleToScreen(scroll.ClientRectangle);
        Capture(host, "small-scaled-account");
        Require(visible.Contains(save.RectangleToScreen(save.ClientRectangle)), "account actions reachable at small size after 150 percent scaling");
        Require(!scroll.HorizontalScroll.Visible, "account layout avoids horizontal clipping");
        Capture(host, "small-scaled-account");
        string damaged = Path.Combine(directory, "damaged.json");
        File.WriteAllText(damaged, "");
        Button(create, "cancelCreateButton").PerformClick();
        Require(!All(login).Any(c => c.GetType().Name == "CreateAccountControl"), "cancel does not reload damaged storage");
        Text(login, "usernameTextBox").Text = "workshop_test";
        Text(login, "passwordTextextBox").Text = "test-only-password with spaces";
        Button(login, "loginButton").PerformClick();
        Require(All(login).OfType<Label>().Any(l => l.Text.Contains("could not be loaded")), "unreadable account data produces login feedback");
        Require(Text(login, "usernameTextBox").Text == "workshop_test", "failed login load retains username");
        File.Delete(damaged);
        host.Close();
    }

    private static void CheckOverdueEdit(string savedFile)
    {
        // Simulate a previously saved date becoming overdue without depending on the wall clock.
        var json = System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(savedFile))!;
        json["Tasks"]![0]!["DueDate"] = DateTime.Today.AddDays(-2);
        File.WriteAllText(savedFile, json.ToJsonString());
        var accounts = (System.Collections.IList)Call(New("AccountFileReading"), "GetAccountList")!;
        var source = (ITaskPresentationSource)New("UI.AccountTaskSource", accounts[0]!);
        var task = source.GetTasks()[0];
        using var editor = (Form)New("UI.TaskEditorForm", task,
            new Action<TaskFormData>(fields => source.Save(task.Id, fields)));
        editor.StartPosition = FormStartPosition.Manual;
        editor.Location = new Point(-30000, -30000);
        editor.ShowInTaskbar = false;
        editor.Show();
        All(editor).OfType<TextBox>().Single(t => t.AccessibleName == "Description or notes").Text = "Overdue notes edited";
        All(editor).OfType<Button>().Single(b => b.Text == "&Save").PerformClick();
        Require(editor.DialogResult == DialogResult.OK && source.GetTasks()[0].DueDate == task.DueDate,
            "overdue task can be edited without changing its date");
        try { source.Save(task.Id, new(task.Title, task.Notes, DateTime.Today.AddDays(-1), task.TaskType)); throw new Exception("Expected past date rejection"); }
        catch (ArgumentException) { Require(source.GetTasks()[0].DueDate == task.DueDate, "different past date rejected during edit"); }
    }

    private static void AddThroughUi(Form main)
    {
        WithDialog(main, "+ Add task", editor =>
        {
            Button save = All(editor).OfType<Button>().Single(b => b.Text == "&Save");
            save.PerformClick(); Require(All(editor).OfType<Label>().Any(l => l.Text == "Please enter a task title."), "blank title feedback");
            All(editor).OfType<TextBox>().Single(t => t.AccessibleName == "Task title").Text = "Replace HVAC filter";
            save.PerformClick(); Require(All(editor).OfType<Label>().Any(l => l.Text.Contains("Please enter a description")), "required notes feedback");
            All(editor).OfType<TextBox>().Single(t => t.AccessibleName == "Description or notes").Text = "Use the correct filter size.";
            save.PerformClick(); Require(All(editor).OfType<Label>().Any(l => l.Text == "Please select a task type."), "required type feedback");
            All(editor).OfType<ComboBox>().Single(c => c.AccessibleName == "Task type").SelectedItem = "Priority";
            All(editor).OfType<DateTimePicker>().Single().Value = DateTime.Today.AddDays(-1);
            save.PerformClick(); Require(All(editor).OfType<Label>().Any(l => l.Text == "Due date cannot be in the past."), "past date feedback");
            All(editor).OfType<DateTimePicker>().Single().Value = DateTime.Today.AddDays(2);
            Capture(editor, "task-editor"); save.PerformClick();
            Require(editor.DialogResult == DialogResult.OK, "dialog accepts valid fields");
        });
    }
    private static void CancelEdit(Form main, TaskViewData original)
    {
        WithDialog(main, "Edit task", editor =>
        {
            Require(All(editor).OfType<ComboBox>().Single(c => c.AccessibleName == "Task type").SelectedItem?.ToString() == original.TaskType, "edit loads team type");
            All(editor).OfType<TextBox>().Single(t => t.AccessibleName == "Task title").Text = "Unsaved change";
            All(editor).OfType<Button>().Single(b => b.Text == "&Cancel").PerformClick();
        });
    }
    private static void WithDialog(Form main, string action, Action<Form> check)
    {
        Exception? failure = null;
        bool found = false;
        using var timer = new System.Windows.Forms.Timer { Interval = 150 };
        timer.Tick += (_, _) =>
        {
            var dialog = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "TaskEditorForm");
            if (dialog is null) return;
            found = true;
            timer.Stop();
            try { check(dialog); } catch (Exception ex) { failure = ex; dialog.Close(); }
            finally { if (!dialog.IsDisposed) dialog.Close(); }
        };
        timer.Start(); All(main).OfType<Button>().Single(b => b.Text == action).PerformClick(); timer.Stop();
        if (failure is not null) throw failure;
        Require(found, "expected editor opened");
    }
    private static void ExpectValidation(ITaskPresentationSource source, TaskFormData fields)
    {
        int before = source.GetTasks().Count;
        try { source.Save(null, fields); throw new Exception("Expected rejection"); }
        catch (ArgumentException) { Require(source.GetTasks().Count == before, "invalid input does not mutate account"); }
    }
    private static object New(string name, params object[] args) => Activator.CreateInstance(App.GetType("HomeMaintenanceApp." + name)!, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, args, null)!;
    private static TextBox Text(Control parent, string name) => All(parent).OfType<TextBox>().Single(t => t.Name == name);
    private static Button Button(Control parent, string name) => All(parent).OfType<Button>().Single(t => t.Name == name);
    private static object? Call(object obj, string method, params object[] args) => obj.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.Invoke(obj, args);
    private static object? Field(object obj, string field) => obj.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(obj);
    private static IEnumerable<Control> All(Control parent) { foreach (Control child in parent.Controls) { yield return child; foreach (var item in All(child)) yield return item; } }
    private static void Capture(Form form, string name) { Application.DoEvents(); using var bitmap = new Bitmap(form.Width, form.Height); form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, bitmap.Size)); bitmap.Save(Path.Combine(AppContext.BaseDirectory, name + ".png")); }
    private static void CaptureControl(Control control, string name) { Application.DoEvents(); using var bitmap = new Bitmap(control.Width, control.Height); control.DrawToBitmap(bitmap, new Rectangle(Point.Empty, bitmap.Size)); bitmap.Save(Path.Combine(AppContext.BaseDirectory, name + ".png")); }
    private static void Require(bool value, string label) { if (!value) throw new Exception("FAIL: " + label); checks++; Console.WriteLine("PASS: " + label); }
}
