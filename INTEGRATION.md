# Workshop UI integration

Open **HomeMaintenanceApp.sln** in Visual Studio with the .NET desktop development
workload and .NET 9 SDK, then run HomeMaintenanceApp with F5. Extract the ZIP first.
Create an account, sign in, and use Home or Tasks to add, edit, and complete work.

Base: master-test commit 9a7a28b9f83f0eb1e30857409768b00609101373.
Integration branch: ui/workshop-theme. Review this branch before merging.

## Changes in this update

- Account details and tasks now persist together as validated JSON. Spaces in names
  and passwords round-trip correctly. Task identifiers remain stable after restart.
- Passwords use PBKDF2-HMAC-SHA256 with 600,000 iterations and a random salt per
  account. Login verifies the hash; no plaintext password is stored.
- Saves write a temporary file and replace the account file only after a successful
  write. A revision check rejects stale sessions instead of overwriting newer work.
- Failed account/task saves retain entered fields and leave the active account
  unchanged. Damaged account files produce feedback instead of crashing login.
- Canceling account creation returns to sign-in without reading storage.
- Shared validation serves the workshop and original team forms. Existing overdue
  dates can remain unchanged during editing; newly chosen past dates are rejected.
- The original edit screen now selects by task identity, so duplicate titles work.
- Account forms adapt to narrow windows and scroll vertically. The task editor also
  scrolls when its window is shortened.
- Integration checks cover actual account creation and task editing through the UI,
  save failures, malformed files, persistence, duplicate titles, and smaller layouts.
  Failures print details and return exit code 1. A watchdog bounds stalled UI tests.

## Data location and existing accounts

Normal account data is stored under
`%LOCALAPPDATA%/HomeMaintenanceManager/Accounts`, independently of the extracted
project folder. Each account file contains its task list and password hash. The
`HMM_DATA_DIRECTORY` environment variable overrides the location for isolated tests.

**Legacy SavedAccounts.txt accounts are not imported. Create a new account in this
version.** The old whitespace-separated format cannot reliably reconstruct fields
containing spaces. Existing legacy files are left untouched. Tasks from the earlier
version existed only in memory and cannot be recovered after that process closed.

Usernames are trimmed and compared without case sensitivity. Passwords retain
spaces and case. If another running session saves the same account, a stale session
must be reopened before further saves. Copy any unsaved form entries before closing.

## Connection to the team code

The workshop uses the team's Account and Tasks model through AccountTaskSource;
there is no second live task collection. Writes now go through Account.AddTask,
Account.UpdateTask, or Account.CompleteTask, which validate and persist before
changing the objects used by either UI. New team features should call those account
operations instead of directly changing task fields.

Modified team files:

- Account.cs, AccountFileReading.cs, Tasks.cs: durable task identity, account
  operations, password verification, and storage integration.
- LoginControl.cs and CreateAccountControl.cs: inline feedback, failure handling,
  and workshop layout integration using the existing controls and events.
- MaintenanceTaskControl.cs and EditTask.cs: shared validation/persistence;
  selection of duplicate titles by object rather than name.
- Form1.cs: workshop shell, account adapter, and navigation.
- HomeMaintenanceApp.csproj and .gitignore: separate tests and excluded local data.

New integration files are in Storage/, UI/, Tests/, TaskRules.cs and *.Workshop.cs.
Designer files, the separate MaintenanceTask model, and future-feature screens
remain in the solution. The workshop's runtime layout is built in code.

GetAccountPassword and GiveFileString were removed. Use VerifyPassword for login.
GetTaskList now returns a read-only list. The old SaveAccount/FullRewrite routines
were replaced by account operations that persist each change. These API changes
should be considered when merging the team's next branch updates.

Priority and Recurring remain the team's existing type labels. The Repeat field
now stores scheduling independently, so priority tasks can also repeat. Reminders,
calendar functionality, and a notebook theme are future work.

## Recurring tasks (SCRUM-8)

In Add task or Edit task, select Weekly, Monthly, Every 3 months, or Yearly from
Repeat. New tasks labeled Recurring require an interval. Choosing an interval
does not remove Priority from a priority task. The schedule appears in the job
sheet and task-list text. To stop future repeats, edit the open occurrence and
choose Does not repeat; the old task type remains a classification only.

Mark complete saves the completed occurrence and exactly one next occurrence
in the same atomic account write. The next job has a new ID, the same title,
notes, type, and repeat interval, and a link to the completed job. The finished
job retains its due date and a UTC completion timestamp (displayed in local time).
Completed jobs stay in the Tasks list. Repeated completion calls are no-ops;
stale sessions cannot add a second successor.

Dates follow the original scheduled date, not the date the button was clicked.
January 31 monthly becomes February's last day, then March 31. A yearly February
29 schedule returns to February 29 in the next leap year. Late completion creates
one next scheduled job even if it is overdue; it does not silently skip missed
work. Editing notes or type preserves the schedule's anchor. Explicitly changing
the due date or interval on an open occurrence establishes a new anchor.
Completed dates and intervals cannot be edited; change the next open job instead.

Storage schema 2 introduced repeat interval, anchor, occurrence number, predecessor ID,
and completion timestamp. The current schema 3 also stores archive state. Schema 1
and 2 JSON accounts load with their jobs unarchived and upgrade on their next
successful save. No interval is invented for old Recurring labels. Older app
versions reject schema 3 instead of silently dropping new data. Unknown
intervals and inconsistent metadata are rejected without overwriting the file.
If a repeat exceeds the supported calendar range, completion fails without
changing history or creating a next job; change the open job's date or stop its
repeat before completing it.

For a sprint demonstration: create a monthly job due October 1, complete it,
verify the completed October 1 job and open November 1 job, then restart and
verify both remain. Completing the October job again must not add another job.

## Filters, archive, and save feedback

The Tasks screen's Show jobs selector offers All jobs (the default), All active,
Overdue, Due soon, Completed, and Archived. All jobs excludes archived entries.
Due soon includes today through six days ahead; Overdue includes unfinished jobs
before today. Completed jobs never appear as overdue. Filters survive navigation
between Home and Tasks and reset on sign-out. Empty filters show a clear message.

An unfinished repeating job's sheet previews its next occurrence using the same
anchored calendar calculation as completion. Previewing does not save or create
a job. If the schedule exceeds the supported date range, the sheet explains that
the schedule must be edited before completion. Nonrepeating, completed, and
archived jobs do not show a pending-completion preview.

Archive hides one occurrence without deleting it, marking it complete, or creating
a successor. Open the Archived filter, select the job, and choose Restore to return
it with the same ID, date, status, and schedule. Archived jobs must be restored
before editing or completing. Restoring a past-due job leaves it overdue; restoring
completed history leaves it completed. Archiving a completed occurrence does not
archive its already-created successor. To stop future repetitions, edit the pending
occurrence's repeat schedule or archive that pending occurrence.

The dashboard excludes archived jobs from counts and upcoming work. Original team
screens using Account.GetTaskList also exclude archives; the workshop adapter uses
GetAllTasks to include them in the dedicated archive view. SetArchived performs one
atomic save and rejects stale sessions before changing live state. Repeating the
same archive/restore action is a no-op. Saved, completed, archived, and restored
actions show confirmation on the Tasks screen only after persistence succeeds.
Failed saves retain the current state and provide an error instead.

In compact layouts the job sheet scrolls within its allotted space, keeping the
task-list header visible even with expanded notes. Double-click and Enter navigation
from the dashboard are deferred until the native list event finishes, preventing
access to a list that was disposed during its own mouse event.

## Verification

The application builds with zero warnings and errors. The current checks cover
account creation through its button,
incorrect/correct login, required fields, add/edit/cancel/complete, persisted field
and ID equality, stale-session rejection, damaged JSON, failed-save retention,
account isolation, and narrow/scaled account layouts. Recurrence checks additionally
cover all four intervals, month ends, leap years, restarting, UI edits and cancel,
stopping repeats, failed atomic saves, duplicate/stale completion, old JSON accounts,
invalid intervals, and calendar limits. Additional checks cover filter boundaries,
archive/restore persistence and failed writes, schema 2 migration, completed repeat
history, next-date previews, confirmation messages, compact scrolling, and native
dashboard double-click/Enter activation. Rendered screens were checked.

Run Tests/Run-IntegrationChecks.ps1 from PowerShell on Windows. It creates a unique
TestResults output folder and records checks.log and errors.log. It never opens the
normal account-data folder. The runner has a 120-second timeout; the executable has
its own 90-second watchdog. UI interactions operate on the application's controls.

The executable's --verify-failure-reporting option intentionally produces a labeled
failure and exit code 1; that path was also verified. Normal successful runs return 0.
Real multi-monitor DPI transitions and a full keyboard/accessibility pass remain
manual acceptance checks; the scaled-layout check is not a substitute for those.

The source ZIP includes the solution, source files, and tests. It excludes account
files, build outputs, test results, and Git history. Extract it into a new folder so
you can compare it with the team's existing checkout before merging.

