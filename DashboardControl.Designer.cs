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
            SuspendLayout();
            // 
            // tasksLabel
            // 
            tasksLabel.AutoSize = true;
            tasksLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tasksLabel.ForeColor = SystemColors.ButtonHighlight;
            tasksLabel.Location = new Point(27, 21);
            tasksLabel.Name = "tasksLabel";
            tasksLabel.Size = new Size(96, 28);
            tasksLabel.TabIndex = 0;
            tasksLabel.Text = "Tasks Due";
            tasksLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // upcomingLabel
            // 
            upcomingLabel.AutoSize = true;
            upcomingLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            upcomingLabel.ForeColor = SystemColors.ButtonHighlight;
            upcomingLabel.Location = new Point(27, 142);
            upcomingLabel.Name = "upcomingLabel";
            upcomingLabel.Size = new Size(153, 28);
            upcomingLabel.TabIndex = 1;
            upcomingLabel.Text = "Upcoming Tasks";
            upcomingLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // goalsLabel
            // 
            goalsLabel.AutoSize = true;
            goalsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            goalsLabel.ForeColor = SystemColors.ButtonHighlight;
            goalsLabel.Location = new Point(27, 274);
            goalsLabel.Name = "goalsLabel";
            goalsLabel.Size = new Size(61, 28);
            goalsLabel.TabIndex = 2;
            goalsLabel.Text = "Goals";
            goalsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // completedLabel
            // 
            completedLabel.AutoSize = true;
            completedLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            completedLabel.ForeColor = SystemColors.ButtonHighlight;
            completedLabel.Location = new Point(237, 21);
            completedLabel.Name = "completedLabel";
            completedLabel.Size = new Size(158, 28);
            completedLabel.TabIndex = 3;
            completedLabel.Text = "Tasks Completed";
            completedLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(completedLabel);
            Controls.Add(goalsLabel);
            Controls.Add(upcomingLabel);
            Controls.Add(tasksLabel);
            Name = "DashboardControl";
            Size = new Size(432, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tasksLabel;
        private Label upcomingLabel;
        private Label goalsLabel;
        private Label completedLabel;
    }
}
