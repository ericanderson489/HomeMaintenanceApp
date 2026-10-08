# Workshop UI

The workshop UI now starts with account creation and sign-in, and saves task
changes between sessions. See [INTEGRATION.md](INTEGRATION.md) for setup,
storage behavior, validation results, and guidance for future team changes.

Open HomeMaintenanceApp.sln in Visual Studio, set HomeMaintenanceApp as the
startup project, and press F5. The active branch is ui/workshop-theme.

The shared workshop design now covers sign-in, registration, Home, Tasks, and
the task editor. Navy frames, gold actions, condensed headings, and the same
Breachless Bungalow sign are provided by the shared UI helpers. The login
illustration is an embedded resource and needs no separate installation.

Selecting a task keeps the list visible. Wide windows show a job sheet beside
the list; narrower windows use a compact sheet with expandable, scrollable
notes. Editing and completing still call the existing account task adapter.
The dashboard counts and date-based status labels use the signed-in account.

The task editor now supports saved repeat schedules: weekly, monthly, every
three months, and yearly. Completing a repeating job keeps its completed record
and creates one next occurrence. Select Does not repeat when editing an open
job to stop future repeats. The legacy task type is independent of the schedule,
so a Priority task can also repeat. See the recurrence section in INTEGRATION.md.

The Show jobs filter offers All jobs, All active, Overdue, Due soon, Completed,
and Archived. Archive hides a job without deleting it; Restore in the Archived
view preserves its original status and repeat schedule. Job sheets preview the
next repeat date before completion. Successful save, complete, archive, and restore
actions display confirmation. Archived jobs are excluded from dashboard totals.

Separate category/priority levels, reopening completed jobs, and account recovery remain
later feature work; no placeholder controls claim to perform those actions.

Integration checks also cover the two job-sheet layouts, visible actions,
notes expansion, and returning from compact to wide windows. Test accounts
are isolated from normal account storage.
