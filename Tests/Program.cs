using System.Reflection;
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
        All(main).OfType<Button>().Single(b => b.Text == "&Tasks").PerformClick();
        Application.DoEvents();
        var list = All(main).OfType<ListView>().Single();
        Require(list.Items.Count == 2 && list.Items[1].SubItems[2].Text == "Completed", "completed items remain visible");
        list.Items[0].Selected = true;
        All(main).OfType<Button>().Single(b => b.Text == "View details").PerformClick();
        Application.DoEvents(); Capture(main, "integrated-details");
        Require(All(main).OfType<Label>().Any(l => l.Text.Contains("Type: Priority")), "type appears in details");
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
        main.Close();
        Console.WriteLine($"PASS: {checks} integration assertions. Screenshots: {AppContext.BaseDirectory}");
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
        WithDialog(main, "+ Add Task", editor =>
        {
            Button save = All(editor).OfType<Button>().Single(b => b.Text == "&Save");
            save.PerformClick(); Require(All(editor).OfType<Label>().Any(l => l.Text == "Please enter a task title."), "blank title feedback");
            All(editor).OfType<TextBox>().Single(t => t.AccessibleName == "Task title").Text = "Replace HVAC filter";
            save.PerformClick(); Require(All(editor).OfType<Label>().Any(l => l.Text.Contains("Please enter a description")), "required notes feedback");
            All(editor).OfType<TextBox>().Single(t => t.AccessibleName == "Description or notes").Text = "Use the correct filter size.";
            save.PerformClick(); Require(All(editor).OfType<Label>().Any(l => l.Text == "Please select a task type."), "required type feedback");
            All(editor).OfType<ComboBox>().Single().SelectedItem = "Priority";
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
            Require(All(editor).OfType<ComboBox>().Single().SelectedItem?.ToString() == original.TaskType, "edit loads team type");
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
    private static void Require(bool value, string label) { if (!value) throw new Exception("FAIL: " + label); checks++; }
}
