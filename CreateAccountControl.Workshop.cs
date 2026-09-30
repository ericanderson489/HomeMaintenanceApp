// Program: Apply workshop styling to the team's create-account fields.
// Author: Murdock MacAskill
// Date: 09/29/2026
// Reuses the existing creation and cancellation handlers.
namespace HomeMaintenanceApp;

internal partial class CreateAccountControl
{
    private void ApplyWorkshopLayout()
    {
        // Designer example text must not be submitted as real account details.
        fnameTextBox.Clear();
        lnameTextBox.Clear();
        usernameTextBox.Clear();
        passwordTextBox.Clear();
        passwordTextBox.UseSystemPasswordChar = true;
        UI.AccountLayout.Apply(createAccountPanel, "Create your account",
            new[] { ("First name", fnameTextBox), ("Last name", lnameTextBox),
                ("Username", usernameTextBox), ("Password", passwordTextBox) },
            feedback, createAccountButton, cancelCreateButton);
    }
}
