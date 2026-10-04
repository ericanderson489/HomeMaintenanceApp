namespace HomeMaintenanceApp
{
    partial class MaintenanceTaskControl
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
            descriptionLabel = new Label();
            dueDateLabel = new Label();
            taskTitleBox = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            textBox1 = new TextBox();
            maintenanceTaskPanel = new Panel();
            TaskCategoryCombo = new ComboBox();
            CategoryLabel = new Label();
            addTaskTypeComboBox = new ComboBox();
            taskTypeLabel = new Label();
            closeButton = new Button();
            doneTaskButton = new Button();
            maintenanceTaskPanel.SuspendLayout();
            SuspendLayout();
            // 
            // taskTitleLabel
            // 
            taskTitleLabel.AutoSize = true;
            taskTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTitleLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTitleLabel.Location = new Point(9, 58);
            taskTitleLabel.Margin = new Padding(4, 0, 4, 0);
            taskTitleLabel.Name = "taskTitleLabel";
            taskTitleLabel.Size = new Size(116, 32);
            taskTitleLabel.TabIndex = 0;
            taskTitleLabel.Text = "Task Title:";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descriptionLabel.ForeColor = SystemColors.ButtonHighlight;
            descriptionLabel.Location = new Point(4, 212);
            descriptionLabel.Margin = new Padding(4, 0, 4, 0);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(140, 32);
            descriptionLabel.TabIndex = 1;
            descriptionLabel.Text = "Description:";
            // 
            // dueDateLabel
            // 
            dueDateLabel.AutoSize = true;
            dueDateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dueDateLabel.ForeColor = SystemColors.ButtonHighlight;
            dueDateLabel.Location = new Point(5, 132);
            dueDateLabel.Margin = new Padding(4, 0, 4, 0);
            dueDateLabel.Name = "dueDateLabel";
            dueDateLabel.Size = new Size(120, 32);
            dueDateLabel.TabIndex = 2;
            dueDateLabel.Text = "Due Date:";
            dueDateLabel.Click += dueDateLabel_Click;
            // 
            // taskTitleBox
            // 
            taskTitleBox.BackColor = SystemColors.ActiveCaptionText;
            taskTitleBox.Font = new Font("Segoe UI Light", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            taskTitleBox.ForeColor = SystemColors.ControlLightLight;
            taskTitleBox.Location = new Point(144, 55);
            taskTitleBox.Margin = new Padding(4, 3, 4, 3);
            taskTitleBox.Name = "taskTitleBox";
            taskTitleBox.Size = new Size(518, 39);
            taskTitleBox.TabIndex = 3;
            taskTitleBox.Text = "<Type Task Title Here>";
            taskTitleBox.TextChanged += taskTitleBox_TextChanged;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.CalendarForeColor = SystemColors.ButtonHighlight;
            dateTimePicker1.CalendarMonthBackground = SystemColors.InactiveCaptionText;
            dateTimePicker1.CalendarTitleBackColor = SystemColors.ActiveCaptionText;
            dateTimePicker1.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.CalendarTrailingForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.Location = new Point(144, 132);
            dateTimePicker1.Margin = new Padding(4, 3, 4, 3);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(311, 31);
            dateTimePicker1.TabIndex = 4;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ActiveCaptionText;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.ForeColor = SystemColors.ControlLightLight;
            textBox1.Location = new Point(144, 209);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(518, 89);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // maintenanceTaskPanel
            // 
            maintenanceTaskPanel.Controls.Add(TaskCategoryCombo);
            maintenanceTaskPanel.Controls.Add(CategoryLabel);
            maintenanceTaskPanel.Controls.Add(addTaskTypeComboBox);
            maintenanceTaskPanel.Controls.Add(taskTypeLabel);
            maintenanceTaskPanel.Controls.Add(closeButton);
            maintenanceTaskPanel.Controls.Add(doneTaskButton);
            maintenanceTaskPanel.Controls.Add(textBox1);
            maintenanceTaskPanel.Controls.Add(dateTimePicker1);
            maintenanceTaskPanel.Controls.Add(taskTitleBox);
            maintenanceTaskPanel.Controls.Add(dueDateLabel);
            maintenanceTaskPanel.Controls.Add(descriptionLabel);
            maintenanceTaskPanel.Controls.Add(taskTitleLabel);
            maintenanceTaskPanel.Dock = DockStyle.Fill;
            maintenanceTaskPanel.Location = new Point(0, 0);
            maintenanceTaskPanel.Margin = new Padding(4, 3, 4, 3);
            maintenanceTaskPanel.Name = "maintenanceTaskPanel";
            maintenanceTaskPanel.Size = new Size(839, 563);
            maintenanceTaskPanel.TabIndex = 6;
            // 
            // TaskCategoryCombo
            // 
            TaskCategoryCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            TaskCategoryCombo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TaskCategoryCombo.FormattingEnabled = true;
            TaskCategoryCombo.Location = new Point(144, 413);
            TaskCategoryCombo.Margin = new Padding(4, 5, 4, 5);
            TaskCategoryCombo.Name = "TaskCategoryCombo";
            TaskCategoryCombo.Size = new Size(171, 40);
            TaskCategoryCombo.TabIndex = 12;
            // 
            // CategoryLabel
            // 
            CategoryLabel.AutoSize = true;
            CategoryLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CategoryLabel.ForeColor = SystemColors.ButtonHighlight;
            CategoryLabel.Location = new Point(4, 413);
            CategoryLabel.Margin = new Padding(4, 0, 4, 0);
            CategoryLabel.Name = "CategoryLabel";
            CategoryLabel.Size = new Size(115, 32);
            CategoryLabel.TabIndex = 11;
            CategoryLabel.Text = "Category:";
            // 
            // addTaskTypeComboBox
            // 
            addTaskTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            addTaskTypeComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            addTaskTypeComboBox.FormattingEnabled = true;
            addTaskTypeComboBox.Items.AddRange(new object[] { "Priority", "Recurring" });
            addTaskTypeComboBox.Location = new Point(144, 333);
            addTaskTypeComboBox.Margin = new Padding(4, 5, 4, 5);
            addTaskTypeComboBox.Name = "addTaskTypeComboBox";
            addTaskTypeComboBox.Size = new Size(171, 40);
            addTaskTypeComboBox.TabIndex = 10;
            // 
            // taskTypeLabel
            // 
            taskTypeLabel.AutoSize = true;
            taskTypeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTypeLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTypeLabel.Location = new Point(4, 336);
            taskTypeLabel.Margin = new Padding(4, 0, 4, 0);
            taskTypeLabel.Name = "taskTypeLabel";
            taskTypeLabel.Size = new Size(121, 32);
            taskTypeLabel.TabIndex = 8;
            taskTypeLabel.Text = "Task Type:";
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.AutoSize = true;
            closeButton.BackColor = SystemColors.ActiveCaptionText;
            closeButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            closeButton.ForeColor = SystemColors.ButtonHighlight;
            closeButton.Location = new Point(-1, 493);
            closeButton.Margin = new Padding(4, 3, 4, 3);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(136, 70);
            closeButton.TabIndex = 7;
            closeButton.Text = "Cancel";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += closeButton_Click;
            // 
            // doneTaskButton
            // 
            doneTaskButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            doneTaskButton.AutoSize = true;
            doneTaskButton.BackColor = SystemColors.ActiveCaptionText;
            doneTaskButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            doneTaskButton.ForeColor = SystemColors.ButtonHighlight;
            doneTaskButton.Location = new Point(670, 493);
            doneTaskButton.Margin = new Padding(4, 3, 4, 3);
            doneTaskButton.Name = "doneTaskButton";
            doneTaskButton.Size = new Size(169, 70);
            doneTaskButton.TabIndex = 6;
            doneTaskButton.Text = "Add Task";
            doneTaskButton.UseVisualStyleBackColor = false;
            doneTaskButton.Click += doneTaskButton_Click;
            // 
            // MaintenanceTaskControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(maintenanceTaskPanel);
            Margin = new Padding(4, 3, 4, 3);
            Name = "MaintenanceTaskControl";
            Size = new Size(839, 563);
            maintenanceTaskPanel.ResumeLayout(false);
            maintenanceTaskPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label taskTitleLabel;
        private Label descriptionLabel;
        private Label dueDateLabel;
        private TextBox taskTitleBox;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox1;
        private Panel maintenanceTaskPanel;
        private Button doneTaskButton;
        private Button closeButton;
        private Label taskTypeLabel;
        private ComboBox addTaskTypeComboBox;
        private Label CategoryLabel;
        private ComboBox TaskCategoryCombo;
    }
}
