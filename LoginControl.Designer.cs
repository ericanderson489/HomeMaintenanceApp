namespace HomeMaintenanceApp
{
    partial class LoginControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginButton = new Button();
            cancelButton = new Button();
            usernameLabel = new Label();
            passwordLabel = new Label();
            usernameTextBox = new TextBox();
            passwordTextextBox = new TextBox();
            createAccountButton = new Button();
            SuspendLayout();
            // 
            // loginButton
            // 
            loginButton.AutoSize = true;
            loginButton.BackColor = SystemColors.ActiveCaptionText;
            loginButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loginButton.ForeColor = SystemColors.ButtonHighlight;
            loginButton.Location = new Point(268, 412);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(94, 38);
            loginButton.TabIndex = 0;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.AutoSize = true;
            cancelButton.BackColor = SystemColors.ActiveCaptionText;
            cancelButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cancelButton.ForeColor = SystemColors.ButtonHighlight;
            cancelButton.Location = new Point(0, 412);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 38);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Click += cancelButton_Click;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            usernameLabel.ForeColor = SystemColors.ButtonHighlight;
            usernameLabel.Location = new Point(3, 45);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(99, 28);
            usernameLabel.TabIndex = 2;
            usernameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordLabel.ForeColor = SystemColors.ButtonHighlight;
            passwordLabel.Location = new Point(3, 150);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(93, 28);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "Password";
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = SystemColors.ActiveCaptionText;
            usernameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            usernameTextBox.ForeColor = SystemColors.ControlLightLight;
            usernameTextBox.Location = new Point(3, 76);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(356, 34);
            usernameTextBox.TabIndex = 4;
            usernameTextBox.TextChanged += usernameTextBox_TextChanged;
            // 
            // passwordTextextBox
            // 
            passwordTextextBox.BackColor = SystemColors.ActiveCaptionText;
            passwordTextextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordTextextBox.ForeColor = SystemColors.ControlLightLight;
            passwordTextextBox.Location = new Point(3, 181);
            passwordTextextBox.Name = "passwordTextextBox";
            passwordTextextBox.Size = new Size(356, 34);
            passwordTextextBox.TabIndex = 5;
            passwordTextextBox.TextChanged += passwordTextextBox_TextChanged;
            // 
            // createAccountButton
            // 
            createAccountButton.AutoSize = true;
            createAccountButton.BackColor = SystemColors.ActiveCaptionText;
            createAccountButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            createAccountButton.ForeColor = SystemColors.ButtonHighlight;
            createAccountButton.Location = new Point(46, 253);
            createAccountButton.Name = "createAccountButton";
            createAccountButton.Size = new Size(249, 38);
            createAccountButton.TabIndex = 6;
            createAccountButton.Text = "Don't have an account? Create one";
            createAccountButton.UseVisualStyleBackColor = false;
            createAccountButton.Click += createAccountButton_Click;
            // 
            // LoginControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(createAccountButton);
            Controls.Add(passwordTextextBox);
            Controls.Add(usernameTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            Controls.Add(cancelButton);
            Controls.Add(loginButton);
            Name = "LoginControl";
            Size = new Size(376, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button loginButton;
        private Button cancelButton;
        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox usernameTextBox;
        private TextBox passwordTextextBox;
        private Button createAccountButton;
    }
}
