namespace HomeMaintenanceApp
{
    partial class DashboardControl
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
            tasksLabel = new Label();
            upcomingLabel = new Label();
            goalsLabel = new Label();
            completedLabel = new Label();
            greetingLabel = new Label();
            SuspendLayout();
            // 
            // tasksLabel
            // 
            tasksLabel.AutoSize = true;
            tasksLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tasksLabel.ForeColor = SystemColors.ButtonHighlight;
            tasksLabel.Location = new Point(24, 47);
            tasksLabel.Name = "tasksLabel";
            tasksLabel.Size = new Size(78, 21);
            tasksLabel.TabIndex = 0;
            tasksLabel.Text = "Tasks Due";
            tasksLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // upcomingLabel
            // 
            upcomingLabel.AutoSize = true;
            upcomingLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            upcomingLabel.ForeColor = SystemColors.ButtonHighlight;
            upcomingLabel.Location = new Point(24, 134);
            upcomingLabel.Name = "upcomingLabel";
            upcomingLabel.Size = new Size(122, 21);
            upcomingLabel.TabIndex = 1;
            upcomingLabel.Text = "Upcoming Tasks";
            upcomingLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // goalsLabel
            // 
            goalsLabel.AutoSize = true;
            goalsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            goalsLabel.ForeColor = SystemColors.ButtonHighlight;
            goalsLabel.Location = new Point(24, 235);
            goalsLabel.Name = "goalsLabel";
            goalsLabel.Size = new Size(49, 21);
            goalsLabel.TabIndex = 2;
            goalsLabel.Text = "Goals";
            goalsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // completedLabel
            // 
            completedLabel.AutoSize = true;
            completedLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            completedLabel.ForeColor = SystemColors.ButtonHighlight;
            completedLabel.Location = new Point(360, 47);
            completedLabel.Name = "completedLabel";
            completedLabel.Size = new Size(126, 21);
            completedLabel.TabIndex = 3;
            completedLabel.Text = "Tasks Completed";
            completedLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // greetingLabel
            // 
            greetingLabel.AutoSize = true;
            greetingLabel.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            greetingLabel.ForeColor = SystemColors.ButtonHighlight;
            greetingLabel.Location = new Point(196, 0);
            greetingLabel.Name = "greetingLabel";
            greetingLabel.Size = new Size(148, 32);
            greetingLabel.TabIndex = 4;
            greetingLabel.Text = "Hello Person";
            greetingLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(greetingLabel);
            Controls.Add(completedLabel);
            Controls.Add(goalsLabel);
            Controls.Add(upcomingLabel);
            Controls.Add(tasksLabel);
            Margin = new Padding(3, 2, 3, 2);
            Name = "DashboardControl";
            Size = new Size(587, 338);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tasksLabel;
        private Label upcomingLabel;
        private Label goalsLabel;
        private Label completedLabel;
        private Label greetingLabel;
    }
}
