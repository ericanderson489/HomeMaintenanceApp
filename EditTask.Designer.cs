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
            TaskNameDropdown = new ComboBox();
            EditTaskPanel = new Panel();
            taskTypeComboBox = new ComboBox();
            completedLabel = new Label();
            taskCheckBox = new CheckBox();
            EditTaskPanel.SuspendLayout();
            SuspendLayout();
            // 
            // editTaskTitleLabel
            // 
            editTaskTitleLabel.AutoSize = true;
            editTaskTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editTaskTitleLabel.ForeColor = SystemColors.ButtonHighlight;
            editTaskTitleLabel.Location = new Point(3, 71);
            editTaskTitleLabel.Name = "editTaskTitleLabel";
            editTaskTitleLabel.Size = new Size(94, 28);
            editTaskTitleLabel.TabIndex = 1;
            editTaskTitleLabel.Text = "Task Title:";
            editTaskTitleLabel.Click += editTaskTitleLabel_Click;
            // 
            // editTitleBox
            // 
            editTitleBox.BackColor = SystemColors.ActiveCaptionText;
            editTitleBox.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            editTitleBox.ForeColor = SystemColors.ControlLightLight;
            editTitleBox.Location = new Point(9, 9);
            editTitleBox.Name = "editTitleBox";
            editTitleBox.Size = new Size(103, 35);
            editTitleBox.TabIndex = 4;
            editTitleBox.Text = "Edit Task";
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
            editCalendar.Location = new Point(115, 276);
            editCalendar.Name = "editCalendar";
            editCalendar.Size = new Size(250, 27);
            editCalendar.TabIndex = 5;
            editCalendar.ValueChanged += editCalendar_ValueChanged;
            // 
            // editDueDateLabel
            // 
            editDueDateLabel.AutoSize = true;
            editDueDateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editDueDateLabel.ForeColor = SystemColors.ButtonHighlight;
            editDueDateLabel.Location = new Point(9, 276);
            editDueDateLabel.Name = "editDueDateLabel";
            editDueDateLabel.Size = new Size(97, 28);
            editDueDateLabel.TabIndex = 6;
            editDueDateLabel.Text = "Due Date:";
            editDueDateLabel.Click += editDueDateLabel_Click;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descriptionLabel.ForeColor = SystemColors.ButtonHighlight;
            descriptionLabel.Location = new Point(0, 133);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(116, 28);
            descriptionLabel.TabIndex = 7;
            descriptionLabel.Text = "Description:";
            descriptionLabel.Click += descriptionLabel_Click;
            // 
            // editTextBox
            // 
            editTextBox.BackColor = SystemColors.ActiveCaptionText;
            editTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editTextBox.ForeColor = SystemColors.ControlLightLight;
            editTextBox.Location = new Point(115, 133);
            editTextBox.Multiline = true;
            editTextBox.Name = "editTextBox";
            editTextBox.Size = new Size(415, 59);
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
            closeEditButton.Location = new Point(3, 397);
            closeEditButton.Name = "closeEditButton";
            closeEditButton.Size = new Size(94, 51);
            closeEditButton.TabIndex = 9;
            closeEditButton.Text = "Cancel";
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
            editTaskButton.Location = new Point(505, 397);
            editTaskButton.Name = "editTaskButton";
            editTaskButton.Size = new Size(162, 51);
            editTaskButton.TabIndex = 10;
            editTaskButton.Text = "Save Changes";
            editTaskButton.UseVisualStyleBackColor = false;
            editTaskButton.Click += editTaskButton_Click;
            // 
            // taskTypeLabel
            // 
            taskTypeLabel.AutoSize = true;
            taskTypeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTypeLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTypeLabel.Location = new Point(9, 213);
            taskTypeLabel.Name = "taskTypeLabel";
            taskTypeLabel.Size = new Size(98, 28);
            taskTypeLabel.TabIndex = 11;
            taskTypeLabel.Text = "Task Type:";
            // 
            // TaskNameDropdown
            // 
            TaskNameDropdown.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TaskNameDropdown.FormattingEnabled = true;
            TaskNameDropdown.Location = new Point(115, 71);
            TaskNameDropdown.Margin = new Padding(3, 4, 3, 4);
            TaskNameDropdown.Name = "TaskNameDropdown";
            TaskNameDropdown.Size = new Size(138, 36);
            TaskNameDropdown.TabIndex = 13;
            TaskNameDropdown.SelectedIndexChanged += TaskNameDropdown_SelectedIndexChanged;
            // 
            // EditTaskPanel
            // 
            EditTaskPanel.Controls.Add(taskTypeComboBox);
            EditTaskPanel.Controls.Add(completedLabel);
            EditTaskPanel.Controls.Add(taskCheckBox);
            EditTaskPanel.Controls.Add(TaskNameDropdown);
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
            EditTaskPanel.Margin = new Padding(3, 4, 3, 4);
            EditTaskPanel.Name = "EditTaskPanel";
            EditTaskPanel.Size = new Size(671, 451);
            EditTaskPanel.TabIndex = 14;
            // 
            // taskTypeComboBox
            // 
            taskTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            taskTypeComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTypeComboBox.FormattingEnabled = true;
            taskTypeComboBox.Items.AddRange(new object[] { "Priority", "Recurring" });
            taskTypeComboBox.Location = new Point(115, 213);
            taskTypeComboBox.Margin = new Padding(3, 4, 3, 4);
            taskTypeComboBox.Name = "taskTypeComboBox";
            taskTypeComboBox.Size = new Size(138, 36);
            taskTypeComboBox.TabIndex = 16;
            // 
            // completedLabel
            // 
            completedLabel.AutoSize = true;
            completedLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            completedLabel.ForeColor = SystemColors.ControlLight;
            completedLabel.Location = new Point(9, 331);
            completedLabel.Name = "completedLabel";
            completedLabel.Size = new Size(113, 28);
            completedLabel.TabIndex = 15;
            completedLabel.Text = "Completed:";
            // 
            // taskCheckBox
            // 
            taskCheckBox.AutoSize = true;
            taskCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskCheckBox.ForeColor = SystemColors.ControlLight;
            taskCheckBox.Location = new Point(128, 331);
            taskCheckBox.Margin = new Padding(3, 4, 3, 4);
            taskCheckBox.Name = "taskCheckBox";
            taskCheckBox.Size = new Size(210, 32);
            taskCheckBox.TabIndex = 14;
            taskCheckBox.Text = "Mark Task Complete";
            taskCheckBox.UseVisualStyleBackColor = true;
            taskCheckBox.CheckedChanged += taskCheckBox_CheckedChanged;
            // 
            // EditTask
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(EditTaskPanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "EditTask";
            Size = new Size(671, 451);
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
        private ComboBox TaskNameDropdown;
        private Panel EditTaskPanel;
        private Label completedLabel;
        private CheckBox taskCheckBox;
        private ComboBox taskTypeComboBox;
    }
}
