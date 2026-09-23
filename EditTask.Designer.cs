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
            editTaskTitleLabel = new Label();
            editTitleBox = new TextBox();
            editCalendar = new DateTimePicker();
            editDueDateLabel = new Label();
            descriptionLabel = new Label();
            editTextBox = new TextBox();
            closeEditButton = new Button();
            editTaskButton = new Button();
            taskTypeLabel = new Label();
            taskTypeBox = new TextBox();
            TaskNameDropdown = new ComboBox();
            EditTaskPanel = new Panel();
            EditTaskPanel.SuspendLayout();
            SuspendLayout();
            // 
            // editTaskTitleLabel
            // 
            editTaskTitleLabel.AutoSize = true;
            editTaskTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editTaskTitleLabel.ForeColor = SystemColors.ButtonHighlight;
            editTaskTitleLabel.Location = new Point(3, 0);
            editTaskTitleLabel.Name = "editTaskTitleLabel";
            editTaskTitleLabel.Size = new Size(72, 21);
            editTaskTitleLabel.TabIndex = 1;
            editTaskTitleLabel.Text = "Task Title";
            editTaskTitleLabel.Click += editTaskTitleLabel_Click;
            // 
            // editTitleBox
            // 
            editTitleBox.BackColor = SystemColors.ActiveCaptionText;
            editTitleBox.Font = new Font("Segoe UI Light", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            editTitleBox.ForeColor = SystemColors.ControlLightLight;
            editTitleBox.Location = new Point(3, 23);
            editTitleBox.Margin = new Padding(3, 2, 3, 2);
            editTitleBox.Name = "editTitleBox";
            editTitleBox.Size = new Size(364, 29);
            editTitleBox.TabIndex = 4;
            editTitleBox.Text = "<Type Task Title Here>";
            editTitleBox.TextChanged += editTitleBox_TextChanged;
            // 
            // editCalendar
            // 
            editCalendar.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editCalendar.CalendarForeColor = SystemColors.ButtonHighlight;
            editCalendar.CalendarMonthBackground = SystemColors.InactiveCaptionText;
            editCalendar.CalendarTitleBackColor = SystemColors.ActiveCaptionText;
            editCalendar.CalendarTitleForeColor = SystemColors.ControlLightLight;
            editCalendar.CalendarTrailingForeColor = SystemColors.ControlLightLight;
            editCalendar.Location = new Point(3, 86);
            editCalendar.Margin = new Padding(3, 2, 3, 2);
            editCalendar.Name = "editCalendar";
            editCalendar.Size = new Size(219, 23);
            editCalendar.TabIndex = 5;
            editCalendar.ValueChanged += editCalendar_ValueChanged;
            // 
            // editDueDateLabel
            // 
            editDueDateLabel.AutoSize = true;
            editDueDateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editDueDateLabel.ForeColor = SystemColors.ButtonHighlight;
            editDueDateLabel.Location = new Point(3, 63);
            editDueDateLabel.Name = "editDueDateLabel";
            editDueDateLabel.Size = new Size(74, 21);
            editDueDateLabel.TabIndex = 6;
            editDueDateLabel.Text = "Due Date";
            editDueDateLabel.Click += editDueDateLabel_Click;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descriptionLabel.ForeColor = SystemColors.ButtonHighlight;
            descriptionLabel.Location = new Point(3, 111);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(89, 21);
            descriptionLabel.TabIndex = 7;
            descriptionLabel.Text = "Description";
            descriptionLabel.Click += descriptionLabel_Click;
            // 
            // editTextBox
            // 
            editTextBox.BackColor = SystemColors.ActiveCaptionText;
            editTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editTextBox.ForeColor = SystemColors.ControlLightLight;
            editTextBox.Location = new Point(0, 134);
            editTextBox.Margin = new Padding(3, 2, 3, 2);
            editTextBox.Multiline = true;
            editTextBox.Name = "editTextBox";
            editTextBox.Size = new Size(364, 82);
            editTextBox.TabIndex = 8;
            editTextBox.TextChanged += editTextBox_TextChanged;
            // 
            // closeEditButton
            // 
            closeEditButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeEditButton.AutoSize = true;
            closeEditButton.BackColor = SystemColors.ActiveCaptionText;
            closeEditButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            closeEditButton.ForeColor = SystemColors.ButtonHighlight;
            closeEditButton.Location = new Point(3, 305);
            closeEditButton.Margin = new Padding(3, 2, 3, 2);
            closeEditButton.Name = "closeEditButton";
            closeEditButton.Size = new Size(82, 31);
            closeEditButton.TabIndex = 9;
            closeEditButton.Text = "Close";
            closeEditButton.UseVisualStyleBackColor = false;
            closeEditButton.Click += closeEditButton_Click;
            // 
            // editTaskButton
            // 
            editTaskButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            editTaskButton.AutoSize = true;
            editTaskButton.BackColor = SystemColors.ActiveCaptionText;
            editTaskButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editTaskButton.ForeColor = SystemColors.ButtonHighlight;
            editTaskButton.Location = new Point(502, 305);
            editTaskButton.Margin = new Padding(3, 2, 3, 2);
            editTaskButton.Name = "editTaskButton";
            editTaskButton.Size = new Size(82, 31);
            editTaskButton.TabIndex = 10;
            editTaskButton.Text = "Edit";
            editTaskButton.UseVisualStyleBackColor = false;
            editTaskButton.Click += editTaskButton_Click;
            // 
            // taskTypeLabel
            // 
            taskTypeLabel.AutoSize = true;
            taskTypeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTypeLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTypeLabel.Location = new Point(3, 228);
            taskTypeLabel.Name = "taskTypeLabel";
            taskTypeLabel.Size = new Size(75, 21);
            taskTypeLabel.TabIndex = 11;
            taskTypeLabel.Text = "Task Type";
            // 
            // taskTypeBox
            // 
            taskTypeBox.Location = new Point(3, 252);
            taskTypeBox.Name = "taskTypeBox";
            taskTypeBox.Size = new Size(100, 23);
            taskTypeBox.TabIndex = 12;
            // 
            // TaskNameDropdown
            // 
            TaskNameDropdown.FormattingEnabled = true;
            TaskNameDropdown.Location = new Point(231, 251);
            TaskNameDropdown.Name = "TaskNameDropdown";
            TaskNameDropdown.Size = new Size(121, 23);
            TaskNameDropdown.TabIndex = 13;
            // 
            // EditTaskPanel
            // 
            EditTaskPanel.Controls.Add(TaskNameDropdown);
            EditTaskPanel.Controls.Add(taskTypeBox);
            EditTaskPanel.Controls.Add(taskTypeLabel);
            EditTaskPanel.Controls.Add(editTaskButton);
            EditTaskPanel.Controls.Add(closeEditButton);
            EditTaskPanel.Controls.Add(editTextBox);
            EditTaskPanel.Controls.Add(descriptionLabel);
            EditTaskPanel.Controls.Add(editDueDateLabel);
            EditTaskPanel.Controls.Add(editCalendar);
            EditTaskPanel.Controls.Add(editTitleBox);
            EditTaskPanel.Controls.Add(editTaskTitleLabel);
            EditTaskPanel.Dock = DockStyle.Fill;
            EditTaskPanel.Location = new Point(0, 0);
            EditTaskPanel.Name = "EditTaskPanel";
            EditTaskPanel.Size = new Size(587, 338);
            EditTaskPanel.TabIndex = 14;
            // 
            // EditTask
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(EditTaskPanel);
            Name = "EditTask";
            Size = new Size(587, 338);
            EditTaskPanel.ResumeLayout(false);
            EditTaskPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label editTaskTitleLabel;
        private TextBox editTitleBox;
        private DateTimePicker editCalendar;
        private Label editDueDateLabel;
        private Label descriptionLabel;
        private TextBox editTextBox;
        private Button closeEditButton;
        private Button editTaskButton;
        private Label taskTypeLabel;
        private TextBox taskTypeBox;
        private ComboBox TaskNameDropdown;
        private Panel EditTaskPanel;
    }
}
