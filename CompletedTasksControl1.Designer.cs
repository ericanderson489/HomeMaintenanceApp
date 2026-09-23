namespace HomeMaintenanceApp
{
    partial class CompletedTasksControl1
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
            completedTaskTitleLabel = new Label();
            completedTitleTextBox = new TextBox();
            Check = new Button();
            SuspendLayout();
            // 
            // completedTaskTitleLabel
            // 
            completedTaskTitleLabel.AutoSize = true;
            completedTaskTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            completedTaskTitleLabel.ForeColor = SystemColors.ControlLightLight;
            completedTaskTitleLabel.Location = new Point(3, 21);
            completedTaskTitleLabel.Name = "completedTaskTitleLabel";
            completedTaskTitleLabel.Size = new Size(72, 21);
            completedTaskTitleLabel.TabIndex = 0;
            completedTaskTitleLabel.Text = "Task Title";
            // 
            // completedTitleTextBox
            // 
            completedTitleTextBox.Location = new Point(3, 45);
            completedTitleTextBox.Name = "completedTitleTextBox";
            completedTitleTextBox.Size = new Size(100, 23);
            completedTitleTextBox.TabIndex = 1;
            // 
            // Check
            // 
            Check.AutoSize = true;
            Check.BackColor = SystemColors.ActiveCaptionText;
            Check.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Check.ForeColor = SystemColors.ControlLightLight;
            Check.Location = new Point(109, 38);
            Check.Name = "Check";
            Check.Size = new Size(75, 31);
            Check.TabIndex = 2;
            Check.Text = "Check";
            Check.UseVisualStyleBackColor = false;
            Check.Click += Check_Click;
            // 
            // CompletedTasksControl1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(Check);
            Controls.Add(completedTitleTextBox);
            Controls.Add(completedTaskTitleLabel);
            Name = "CompletedTasksControl1";
            Size = new Size(587, 338);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label completedTaskTitleLabel;
        private TextBox completedTitleTextBox;
        private Button Check;
    }
}
