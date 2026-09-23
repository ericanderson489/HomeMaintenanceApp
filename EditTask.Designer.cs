namespace HomeMaintenanceApp
{
    partial class EditTask
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
            doneTaskButton = new Button();
            closeButton = new Button();
            taskTypeBox = new TextBox();
            taskTitleLabel = new Label();
            dueDateLabel = new Label();
            dateTimePicker1 = new DateTimePicker();
            descriptionLabel = new Label();
            descriptionBox = new TextBox();
            taskTypeLabel = new Label();
            CheckIfValid = new Button();
            EditTaskPanel = new Panel();
            TaskNameDropdown = new ComboBox();
            EditTaskPanel.SuspendLayout();
            SuspendLayout();
            // 
            // doneTaskButton
            // 
            doneTaskButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            doneTaskButton.AutoSize = true;
            doneTaskButton.BackColor = SystemColors.ActiveCaptionText;
            doneTaskButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            doneTaskButton.ForeColor = SystemColors.ButtonHighlight;
            doneTaskButton.Location = new Point(717, 506);
            doneTaskButton.Margin = new Padding(4);
            doneTaskButton.Name = "doneTaskButton";
            doneTaskButton.Size = new Size(118, 52);
            doneTaskButton.TabIndex = 7;
            doneTaskButton.Text = "Done";
            doneTaskButton.UseVisualStyleBackColor = false;
            doneTaskButton.Click += doneTaskButton_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.AutoSize = true;
            closeButton.BackColor = SystemColors.ActiveCaptionText;
            closeButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            closeButton.ForeColor = SystemColors.ButtonHighlight;
            closeButton.Location = new Point(0, 506);
            closeButton.Margin = new Padding(4);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(118, 52);
            closeButton.TabIndex = 8;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += closeButton_Click;
            // 
            // taskTypeBox
            // 
            taskTypeBox.Location = new Point(4, 430);
            taskTypeBox.Name = "taskTypeBox";
            taskTypeBox.Size = new Size(312, 31);
            taskTypeBox.TabIndex = 10;
            // 
            // taskTitleLabel
            // 
            taskTitleLabel.AutoSize = true;
            taskTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTitleLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTitleLabel.Location = new Point(0, 18);
            taskTitleLabel.Margin = new Padding(4, 0, 4, 0);
            taskTitleLabel.Name = "taskTitleLabel";
            taskTitleLabel.Size = new Size(111, 32);
            taskTitleLabel.TabIndex = 11;
            taskTitleLabel.Text = "Task Title";
            // 
            // dueDateLabel
            // 
            dueDateLabel.AutoSize = true;
            dueDateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dueDateLabel.ForeColor = SystemColors.ButtonHighlight;
            dueDateLabel.Location = new Point(0, 117);
            dueDateLabel.Margin = new Padding(4, 0, 4, 0);
            dueDateLabel.Name = "dueDateLabel";
            dueDateLabel.Size = new Size(115, 32);
            dueDateLabel.TabIndex = 13;
            dueDateLabel.Text = "Due Date";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.CalendarForeColor = SystemColors.ButtonHighlight;
            dateTimePicker1.CalendarMonthBackground = SystemColors.InactiveCaptionText;
            dateTimePicker1.CalendarTitleBackColor = SystemColors.ActiveCaptionText;
            dateTimePicker1.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.CalendarTrailingForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.Location = new Point(4, 153);
            dateTimePicker1.Margin = new Padding(4);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(312, 31);
            dateTimePicker1.TabIndex = 14;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descriptionLabel.ForeColor = SystemColors.ButtonHighlight;
            descriptionLabel.Location = new Point(4, 214);
            descriptionLabel.Margin = new Padding(4, 0, 4, 0);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(135, 32);
            descriptionLabel.TabIndex = 15;
            descriptionLabel.Text = "Description";
            // 
            // descriptionBox
            // 
            descriptionBox.BackColor = SystemColors.ActiveCaptionText;
            descriptionBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descriptionBox.ForeColor = SystemColors.ControlLightLight;
            descriptionBox.Location = new Point(4, 250);
            descriptionBox.Margin = new Padding(4);
            descriptionBox.Multiline = true;
            descriptionBox.Name = "descriptionBox";
            descriptionBox.Size = new Size(518, 134);
            descriptionBox.TabIndex = 16;
            // 
            // taskTypeLabel
            // 
            taskTypeLabel.AutoSize = true;
            taskTypeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTypeLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTypeLabel.Location = new Point(4, 388);
            taskTypeLabel.Margin = new Padding(4, 0, 4, 0);
            taskTypeLabel.Name = "taskTypeLabel";
            taskTypeLabel.Size = new Size(116, 32);
            taskTypeLabel.TabIndex = 17;
            taskTypeLabel.Text = "Task Type";
            taskTypeLabel.Click += taskTypeLabel_Click;
            // 
            // CheckIfValid
            // 
            CheckIfValid.BackColor = SystemColors.ActiveCaptionText;
            CheckIfValid.ForeColor = SystemColors.ButtonHighlight;
            CheckIfValid.Location = new Point(192, 54);
            CheckIfValid.Name = "CheckIfValid";
            CheckIfValid.Size = new Size(116, 39);
            CheckIfValid.TabIndex = 18;
            CheckIfValid.Text = "Check";
            CheckIfValid.UseVisualStyleBackColor = false;
            CheckIfValid.Click += CheckIfValid_Click;
            // 
            // EditTaskPanel
            // 
            EditTaskPanel.Controls.Add(TaskNameDropdown);
            EditTaskPanel.Controls.Add(doneTaskButton);
            EditTaskPanel.Controls.Add(closeButton);
            EditTaskPanel.Controls.Add(taskTypeBox);
            EditTaskPanel.Controls.Add(taskTitleLabel);
            EditTaskPanel.Controls.Add(dueDateLabel);
            EditTaskPanel.Controls.Add(dateTimePicker1);
            EditTaskPanel.Controls.Add(descriptionLabel);
            EditTaskPanel.Controls.Add(descriptionBox);
            EditTaskPanel.Controls.Add(taskTypeLabel);
            EditTaskPanel.Controls.Add(CheckIfValid);
            EditTaskPanel.Location = new Point(0, 0);
            EditTaskPanel.Name = "EditTaskPanel";
            EditTaskPanel.Size = new Size(839, 562);
            EditTaskPanel.TabIndex = 19;
            // 
            // TaskNameDropdown
            // 
            TaskNameDropdown.FormattingEnabled = true;
            TaskNameDropdown.Location = new Point(4, 58);
            TaskNameDropdown.Name = "TaskNameDropdown";
            TaskNameDropdown.Size = new Size(182, 33);
            TaskNameDropdown.TabIndex = 19;
            // 
            // EditTask
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(EditTaskPanel);
            Name = "EditTask";
            Size = new Size(839, 562);
            EditTaskPanel.ResumeLayout(false);
            EditTaskPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button doneTaskButton;
        private Button closeButton;
        private TextBox taskTypeBox;
        private Label taskTitleLabel;
        private Label dueDateLabel;
        private DateTimePicker dateTimePicker1;
        private Label descriptionLabel;
        private TextBox descriptionBox;
        private Label taskTypeLabel;
        private Button CheckIfValid;
        private Panel EditTaskPanel;
        private ComboBox TaskNameDropdown;
    }
}
