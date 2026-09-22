namespace HomeMaintenanceApp
{
    partial class CreateAccountControl
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
            createAccountLabel = new Label();
            fnameTextBox = new TextBox();
            lnameTextBox = new TextBox();
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            createAccountButton = new Button();
            cancelCreateButton = new Button();
            createAccountPanel = new Panel();
            createAccountPanel.SuspendLayout();
            SuspendLayout();
            // 
            // createAccountLabel
            // 
            createAccountLabel.AutoSize = true;
            createAccountLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            createAccountLabel.ForeColor = SystemColors.ControlLightLight;
            createAccountLabel.Location = new Point(66, 19);
            createAccountLabel.Name = "createAccountLabel";
            createAccountLabel.Size = new Size(257, 37);
            createAccountLabel.TabIndex = 0;
            createAccountLabel.Text = "Create Your Account";
            // 
            // fnameTextBox
            // 
            fnameTextBox.BackColor = SystemColors.ActiveCaptionText;
            fnameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fnameTextBox.ForeColor = SystemColors.ControlLight;
            fnameTextBox.Location = new Point(66, 119);
            fnameTextBox.Margin = new Padding(3, 4, 3, 4);
            fnameTextBox.Name = "fnameTextBox";
            fnameTextBox.Size = new Size(231, 34);
            fnameTextBox.TabIndex = 1;
            fnameTextBox.Text = "First Name";
            fnameTextBox.TextChanged += fnameTextBox_TextChanged;
            // 
            // lnameTextBox
            // 
            lnameTextBox.BackColor = SystemColors.ActiveCaptionText;
            lnameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnameTextBox.ForeColor = SystemColors.ControlLight;
            lnameTextBox.Location = new Point(66, 179);
            lnameTextBox.Margin = new Padding(3, 4, 3, 4);
            lnameTextBox.Name = "lnameTextBox";
            lnameTextBox.Size = new Size(231, 34);
            lnameTextBox.TabIndex = 2;
            lnameTextBox.Text = "Last Name";
            lnameTextBox.TextChanged += lnameTextBox_TextChanged;
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = SystemColors.ActiveCaptionText;
            usernameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            usernameTextBox.ForeColor = SystemColors.ControlLight;
            usernameTextBox.Location = new Point(66, 249);
            usernameTextBox.Margin = new Padding(3, 4, 3, 4);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(231, 34);
            usernameTextBox.TabIndex = 3;
            usernameTextBox.Text = "Username";
            usernameTextBox.TextChanged += usernameTextBox_TextChanged;
            // 
            // passwordTextBox
            // 
            passwordTextBox.BackColor = SystemColors.ActiveCaptionText;
            passwordTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordTextBox.ForeColor = SystemColors.ControlLight;
            passwordTextBox.Location = new Point(66, 315);
            passwordTextBox.Margin = new Padding(3, 4, 3, 4);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(231, 34);
            passwordTextBox.TabIndex = 4;
            passwordTextBox.Text = "Password";
            passwordTextBox.TextChanged += passwordTextBox_TextChanged;
            // 
            // createAccountButton
            // 
            createAccountButton.AutoSize = true;
            createAccountButton.BackColor = SystemColors.ActiveCaptionText;
            createAccountButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            createAccountButton.ForeColor = SystemColors.ControlLightLight;
            createAccountButton.Location = new Point(225, 416);
            createAccountButton.Margin = new Padding(3, 4, 3, 4);
            createAccountButton.Name = "createAccountButton";
            createAccountButton.Size = new Size(177, 51);
            createAccountButton.TabIndex = 5;
            createAccountButton.Text = "Create Account";
            createAccountButton.UseVisualStyleBackColor = false;
            createAccountButton.Click += createAccountButton_Click;
            // 
            // cancelCreateButton
            // 
            cancelCreateButton.AutoSize = true;
            cancelCreateButton.BackColor = SystemColors.ActiveCaptionText;
            cancelCreateButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cancelCreateButton.ForeColor = SystemColors.ControlLightLight;
            cancelCreateButton.Location = new Point(0, 416);
            cancelCreateButton.Margin = new Padding(3, 4, 3, 4);
            cancelCreateButton.Name = "cancelCreateButton";
            cancelCreateButton.Size = new Size(143, 51);
            cancelCreateButton.TabIndex = 6;
            cancelCreateButton.Text = "Cancel";
            cancelCreateButton.UseVisualStyleBackColor = false;
            cancelCreateButton.Click += cancelCreateButton_Click;
            // 
            // createAccountPanel
            // 
            createAccountPanel.Controls.Add(cancelCreateButton);
            createAccountPanel.Controls.Add(createAccountButton);
            createAccountPanel.Controls.Add(passwordTextBox);
            createAccountPanel.Controls.Add(usernameTextBox);
            createAccountPanel.Controls.Add(lnameTextBox);
            createAccountPanel.Controls.Add(fnameTextBox);
            createAccountPanel.Controls.Add(createAccountLabel);
            createAccountPanel.Dock = DockStyle.Fill;
            createAccountPanel.Location = new Point(0, 0);
            createAccountPanel.Margin = new Padding(3, 4, 3, 4);
            createAccountPanel.Name = "createAccountPanel";
            createAccountPanel.Size = new Size(403, 471);
            createAccountPanel.TabIndex = 7;
            // 
            // CreateAccountControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(createAccountPanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "CreateAccountControl";
            Size = new Size(403, 471);
            Load += CreateAccountControl_Load;
            createAccountPanel.ResumeLayout(false);
            createAccountPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label createAccountLabel;
        private TextBox fnameTextBox;
        private TextBox lnameTextBox;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button createAccountButton;
        private Button cancelCreateButton;
        private Panel createAccountPanel;
    }
}
