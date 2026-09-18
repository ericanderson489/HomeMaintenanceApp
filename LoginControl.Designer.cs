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
			UsernameTextbox = new TextBox();
			label1 = new Label();
			LoginLabel = new Label();
			PasswordLabel = new Label();
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// UsernameTextbox
			// 
			UsernameTextbox.Location = new Point(161, 166);
			UsernameTextbox.Name = "UsernameTextbox";
			UsernameTextbox.Size = new Size(181, 27);
			UsernameTextbox.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.ForeColor = Color.Snow;
			label1.Location = new Point(163, 139);
			label1.Name = "label1";
			label1.Size = new Size(75, 20);
			label1.TabIndex = 2;
			label1.Text = "Username";
			label1.Click += label1_Click;
			// 
			// LoginLabel
			// 
			LoginLabel.AutoSize = true;
			LoginLabel.Font = new Font("Segoe UI", 12F);
			LoginLabel.ForeColor = Color.GhostWhite;
			LoginLabel.Location = new Point(233, 74);
			LoginLabel.Name = "LoginLabel";
			LoginLabel.Size = new Size(61, 28);
			LoginLabel.TabIndex = 3;
			LoginLabel.Text = "Login";
			// 
			// PasswordLabel
			// 
			PasswordLabel.AutoSize = true;
			PasswordLabel.ForeColor = Color.Snow;
			PasswordLabel.Location = new Point(161, 235);
			PasswordLabel.Name = "PasswordLabel";
			PasswordLabel.Size = new Size(70, 20);
			PasswordLabel.TabIndex = 4;
			PasswordLabel.Text = "Password";
			// 
			// textBox1
			// 
			textBox1.Location = new Point(163, 258);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(181, 27);
			textBox1.TabIndex = 5;
			// 
			// LoginControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ActiveCaptionText;
			Controls.Add(textBox1);
			Controls.Add(PasswordLabel);
			Controls.Add(LoginLabel);
			Controls.Add(label1);
			Controls.Add(UsernameTextbox);
			Name = "LoginControl";
			Size = new Size(671, 450);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label LoginTextBox;
		private TextBox UsernameTextbox;
		private Label label1;
		private Label LoginLabel;
		private Label PasswordLabel;
		private TextBox textBox1;
	}
}
