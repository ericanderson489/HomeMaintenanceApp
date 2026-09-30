// Program: Apply workshop styling to the team's login fields.
// Author: Murdock MacAskill
// Date: 09/29/2026
// Reuses the existing LoginSuccessful event and authentication.
namespace HomeMaintenanceApp;

internal partial class LoginControl
{
    private void ApplyWorkshopLayout()
    {
        passwordTextextBox.UseSystemPasswordChar = true;
        loginButton.Text = "Sign in";
        createAccountButton.Text = "Create account";
        UI.AccountLayout.Apply(mainPanelLogin, "Welcome to your workshop.",
            new[] { ("Username", usernameTextBox), ("Password", passwordTextextBox) },
            feedback, loginButton, createAccountButton, cancelButton);
    }
}
