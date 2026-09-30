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

Priority and Recurring remain the team's existing type labels. Automatic recurring
work, reminders, calendar functionality, and a notebook theme are future work.

## Verification

The application and test project build with zero warnings and errors. The final
run passed 50 integration assertions, including account creation through its button,
incorrect/correct login, required fields, add/edit/cancel/complete, persisted field
and ID equality, stale-session rejection, damaged JSON, failed-save retention,
account isolation, and narrow/scaled account layouts. Rendered screens were checked.

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

