# Sprint 1 workshop UI

This branch contains Murdock's ui implement, based on the approved
workshop mockup. It extends the team's existing C# / .NET 9 Windows Forms project.
It does not complete the persistence story or add later-sprint features.

## Run in Visual Studio

Open `HomeMaintenanceApp.sln`, allow package restore, select HomeMaintenanceApp
as the startup project, and press F5. Use a Visual Studio installation that
supports the project's .NET 9 Windows Forms target and has the .NET desktop
development workload. The executable is also built in `bin/Debug/net9.0-windows`.

The app starts in **UI Preview** mode. All task edits are temporary and reset
when the process exits. This is visibly labeled in the title and bottom strip.

## Ownership and changed files

- Existing `.Designer.cs` and `.resx` files and all future-screen files are retained.
- The original parameterless screen constructors remain for Designer compatibility.
  The new layout is constructed at runtime: the Designer still shows the earlier
  scaffold. Run the app to inspect the workshop layout.

`MainForm.Workshop.cs` is another part of the existing MainForm class. A `partial`
class lets related code live in separate files while compiling into one class.
This keeps the new styling and navigation coordination separate from Designer code.

## Integration with teammates

`ITaskPresentationSource` in `UI/TaskPresentation.cs` is a proposed UI boundary,
not an established team API. Agree on it with the logic/storage owners before merging.
It supplies task snapshots and accepts Save and Complete requests. Implement an
adapter backed by the team's service, then pass it to `new MainForm(adapter)` at
the entry point. Set `IsPreview` to false only for that real integration.

The UI never opens a task file or database. The current `PreviewTaskSource` is a
small session-only demo adapter. It must not be mistaken for production storage
or submitted as proof that persistence is complete.

Methods are synchronous for this local prototype. A slow or remote service will
need an asynchronous contract and busy-state handling before integration; do not
perform slow I/O on the UI thread. Save/Complete must report failures by throwing,
and must return only after the operation has succeeded. Production logging and
the team's validation rules belong in the service/adapter.

## Scope and provisional decisions

- Home: outstanding/completed counts and up to three outstanding tasks in due-date order.
- Tasks: all tasks, explicit text status, Add Task, and selected-task details.
- Details: title, notes, date, status, Edit, and Mark complete.
- Create/Edit: native input fields, optional multiline notes, date picker, Save/Cancel.
- Title and due date are provisionally required. Past dates are allowed; the date
  picker starts at today for new tasks. Confirm these rules with the team/rubric.
- Completed tasks remain in the list and cannot be reopened through this UI.
- Cancel and closing the editor do not submit its copied values.
- Future navigation is hidden; no theme picker, calendar, deletion, filtering,
  priorities, reminders, or production storage was implemented.
- Colors and control creation are centralized in `WorkshopStyle.cs`; this is
  groundwork for a later notebook theme, not a completed theme system.

## Validation performed

- Built the .NET 9 project: zero errors and zero warnings.
- Ran a separate Windows Forms verification harness against the compiled project:
  navigation, Home/all-task counts, selected details, completion, blank-title
  feedback, successful save callback, failed-save retention, and cancellation passed.
- Rendered and inspected Home, Tasks at normal/minimum size, Details, Create/Edit,
  and save-error states. Adjusted clipped text and layout after inspection.
- Checked the Git diff for whitespace errors.

Keyboard tab order, screen-reader behavior, multiple monitor DPI settings, and
real storage integration still need manual acceptance testing in your team environment.
Native task lists can scroll when the window or content is too small.

## Manual demo checklist

1. Start the app; verify the preview banner and Home counts.
2. Open Tasks, select a row, and use View details (or double-click the row).
3. Add a task. Attempt a blank title, then enter a title, notes, and date and save.
4. Edit that task; verify its completion status is preserved.
5. Change fields and Cancel; verify the original values remain.
6. Mark a task complete; verify its status and Home counts update.
7. Resize the window and navigate with Tab, Alt+H, Alt+T, Enter, and Escape.
8. Close and reopen: the preview resets. That reset is intentional until the
   team supplies real storage.

## Local build note

This session's package client encountered a Windows TLS credential error. For
verification only, the official .NET 9 reference packages were downloaded with
certificate verification into the task's separate `work/` folder and restored
from that local feed. No alternate feed or machine-specific path was committed
to the project. The target framework and existing project file are unchanged.

Changes are local and uncommitted on `ui/workshop-theme`; nothing has been pushed.
