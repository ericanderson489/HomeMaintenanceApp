namespace HomeMaintenanceApp
{
    partial class CompleteTaskControl
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
            taskTitleLabel = new Label();
            CheckIfValid = new Button();
            CompleteTaskPanel = new Panel();
            TaskNameDropdown = new ComboBox();
            closeButton = new Button();
            CompleteTaskPanel.SuspendLayout();
            SuspendLayout();
            // 
            // taskTitleLabel
            // 
            taskTitleLabel.AutoSize = true;
            taskTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTitleLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTitleLabel.Location = new Point(4, 26);
            taskTitleLabel.Margin = new Padding(4, 0, 4, 0);
            taskTitleLabel.Name = "taskTitleLabel";
            taskTitleLabel.Size = new Size(111, 32);
            taskTitleLabel.TabIndex = 1;
            taskTitleLabel.Text = "Task Title";
            // 
            // CheckIfValid
            // 
            CheckIfValid.BackColor = SystemColors.ActiveCaptionText;
            CheckIfValid.ForeColor = SystemColors.ButtonHighlight;
            CheckIfValid.Location = new Point(192, 67);
            CheckIfValid.Name = "CheckIfValid";
            CheckIfValid.Size = new Size(116, 39);
            CheckIfValid.TabIndex = 19;
            CheckIfValid.Text = "Complete";
            CheckIfValid.UseVisualStyleBackColor = false;
            CheckIfValid.Click += CheckIfValid_Click;
            // 
            // CompleteTaskPanel
            // 
            CompleteTaskPanel.Controls.Add(TaskNameDropdown);
            CompleteTaskPanel.Controls.Add(closeButton);
            CompleteTaskPanel.Controls.Add(taskTitleLabel);
            CompleteTaskPanel.Controls.Add(CheckIfValid);
            CompleteTaskPanel.Location = new Point(0, 0);
            CompleteTaskPanel.Name = "CompleteTaskPanel";
            CompleteTaskPanel.Size = new Size(839, 562);
            CompleteTaskPanel.TabIndex = 20;
            CompleteTaskPanel.Paint += panel1_Paint;
            // 
            // TaskNameDropdown
            // 
            TaskNameDropdown.FormattingEnabled = true;
            TaskNameDropdown.Location = new Point(4, 71);
            TaskNameDropdown.Name = "TaskNameDropdown";
            TaskNameDropdown.Size = new Size(182, 33);
            TaskNameDropdown.TabIndex = 21;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.AutoSize = true;
            closeButton.BackColor = SystemColors.ActiveCaptionText;
            closeButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            closeButton.ForeColor = SystemColors.ButtonHighlight;
            closeButton.Location = new Point(4, 506);
            closeButton.Margin = new Padding(4);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(118, 52);
            closeButton.TabIndex = 20;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += closeButton_Click;
            // 
            // CompleteTaskControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(CompleteTaskPanel);
            Name = "CompleteTaskControl";
            Size = new Size(839, 562);
            CompleteTaskPanel.ResumeLayout(false);
            CompleteTaskPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label taskTitleLabel;
        private Button CheckIfValid;
        private Panel CompleteTaskPanel;
        private Button closeButton;
        private ComboBox TaskNameDropdown;
    }
}
