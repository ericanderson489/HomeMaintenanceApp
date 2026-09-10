namespace HomeMaintenanceApp
{
    partial class HistoryControl
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
            completedTasksLabel = new Label();
            completedGoalsLabel = new Label();
            SuspendLayout();
            // 
            // completedTasksLabel
            // 
            completedTasksLabel.AutoSize = true;
            completedTasksLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            completedTasksLabel.ForeColor = SystemColors.ButtonHighlight;
            completedTasksLabel.Location = new Point(3, 100);
            completedTasksLabel.Name = "completedTasksLabel";
            completedTasksLabel.Size = new Size(158, 28);
            completedTasksLabel.TabIndex = 0;
            completedTasksLabel.Text = "Completed Tasks";
            // 
            // completedGoalsLabel
            // 
            completedGoalsLabel.AutoSize = true;
            completedGoalsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            completedGoalsLabel.ForeColor = SystemColors.ButtonHighlight;
            completedGoalsLabel.Location = new Point(3, 252);
            completedGoalsLabel.Name = "completedGoalsLabel";
            completedGoalsLabel.Size = new Size(163, 28);
            completedGoalsLabel.TabIndex = 1;
            completedGoalsLabel.Text = "Completed Goals";
            // 
            // HistoryControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(completedGoalsLabel);
            Controls.Add(completedTasksLabel);
            Name = "HistoryControl";
            Size = new Size(432, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label completedTasksLabel;
        private Label completedGoalsLabel;
    }
}
