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
            descriptionBox = new TextBox();
            maintenanceTaskPanel = new Panel();
            taskTypeBox = new TextBox();
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
            taskTitleLabel.Location = new Point(0, 15);
            taskTitleLabel.Margin = new Padding(4, 0, 4, 0);
            taskTitleLabel.Name = "taskTitleLabel";
            taskTitleLabel.Size = new Size(111, 32);
            taskTitleLabel.TabIndex = 0;
            taskTitleLabel.Text = "Task Title";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descriptionLabel.ForeColor = SystemColors.ButtonHighlight;
            descriptionLabel.Location = new Point(4, 202);
            descriptionLabel.Margin = new Padding(4, 0, 4, 0);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(135, 32);
            descriptionLabel.TabIndex = 1;
            descriptionLabel.Text = "Description";
            descriptionLabel.Click += label2_Click;
            // 
            // dueDateLabel
            // 
            dueDateLabel.AutoSize = true;
            dueDateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dueDateLabel.ForeColor = SystemColors.ButtonHighlight;
            dueDateLabel.Location = new Point(0, 114);
            dueDateLabel.Margin = new Padding(4, 0, 4, 0);
            dueDateLabel.Name = "dueDateLabel";
            dueDateLabel.Size = new Size(115, 32);
            dueDateLabel.TabIndex = 2;
            dueDateLabel.Text = "Due Date";
            // 
            // taskTitleBox
            // 
            taskTitleBox.BackColor = SystemColors.ActiveCaptionText;
            taskTitleBox.Font = new Font("Segoe UI Light", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            taskTitleBox.ForeColor = SystemColors.ControlLightLight;
            taskTitleBox.Location = new Point(4, 54);
            taskTitleBox.Margin = new Padding(4);
            taskTitleBox.Name = "taskTitleBox";
            taskTitleBox.Size = new Size(518, 39);
            taskTitleBox.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.CalendarForeColor = SystemColors.ButtonHighlight;
            dateTimePicker1.CalendarMonthBackground = SystemColors.InactiveCaptionText;
            dateTimePicker1.CalendarTitleBackColor = SystemColors.ActiveCaptionText;
            dateTimePicker1.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.CalendarTrailingForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.Location = new Point(4, 152);
            dateTimePicker1.Margin = new Padding(4);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(312, 31);
            dateTimePicker1.TabIndex = 4;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // descriptionBox
            // 
            descriptionBox.BackColor = SystemColors.ActiveCaptionText;
            descriptionBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descriptionBox.ForeColor = SystemColors.ControlLightLight;
            descriptionBox.Location = new Point(4, 241);
            descriptionBox.Margin = new Padding(4);
            descriptionBox.Multiline = true;
            descriptionBox.Name = "descriptionBox";
            descriptionBox.Size = new Size(518, 134);
            descriptionBox.TabIndex = 5;
            descriptionBox.TextChanged += textBox1_TextChanged;
            // 
            // maintenanceTaskPanel
            // 
            maintenanceTaskPanel.Controls.Add(taskTypeBox);
            maintenanceTaskPanel.Controls.Add(taskTypeLabel);
            maintenanceTaskPanel.Controls.Add(closeButton);
            maintenanceTaskPanel.Controls.Add(doneTaskButton);
            maintenanceTaskPanel.Controls.Add(descriptionBox);
            maintenanceTaskPanel.Controls.Add(dateTimePicker1);
            maintenanceTaskPanel.Controls.Add(taskTitleBox);
            maintenanceTaskPanel.Controls.Add(dueDateLabel);
            maintenanceTaskPanel.Controls.Add(descriptionLabel);
            maintenanceTaskPanel.Controls.Add(taskTitleLabel);
            maintenanceTaskPanel.Dock = DockStyle.Fill;
            maintenanceTaskPanel.Location = new Point(0, 0);
            maintenanceTaskPanel.Margin = new Padding(4);
            maintenanceTaskPanel.Name = "maintenanceTaskPanel";
            maintenanceTaskPanel.Size = new Size(839, 562);
            maintenanceTaskPanel.TabIndex = 6;
            // 
            // taskTypeBox
            // 
            taskTypeBox.Location = new Point(4, 431);
            taskTypeBox.Name = "taskTypeBox";
            taskTypeBox.Size = new Size(312, 31);
            taskTypeBox.TabIndex = 9;
            // 
            // taskTypeLabel
            // 
            taskTypeLabel.AutoSize = true;
            taskTypeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTypeLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTypeLabel.Location = new Point(4, 396);
            taskTypeLabel.Margin = new Padding(4, 0, 4, 0);
            taskTypeLabel.Name = "taskTypeLabel";
            taskTypeLabel.Size = new Size(116, 32);
            taskTypeLabel.TabIndex = 8;
            taskTypeLabel.Text = "Task Type";
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.AutoSize = true;
            closeButton.BackColor = SystemColors.ActiveCaptionText;
            closeButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            closeButton.ForeColor = SystemColors.ButtonHighlight;
            closeButton.Location = new Point(4, 510);
            closeButton.Margin = new Padding(4);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(118, 52);
            closeButton.TabIndex = 7;
            closeButton.Text = "Close";
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
            doneTaskButton.Location = new Point(718, 506);
            doneTaskButton.Margin = new Padding(4);
            doneTaskButton.Name = "doneTaskButton";
            doneTaskButton.Size = new Size(118, 52);
            doneTaskButton.TabIndex = 6;
            doneTaskButton.Text = "Done";
            doneTaskButton.UseVisualStyleBackColor = false;
            doneTaskButton.Click += doneTaskButton_Click;
            // 
            // MaintenanceTaskControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(maintenanceTaskPanel);
            Margin = new Padding(4);
            Name = "MaintenanceTaskControl";
            Size = new Size(839, 562);
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
        private TextBox descriptionBox;
        private Panel maintenanceTaskPanel;
        private Button doneTaskButton;
        private Button closeButton;
        private Label taskTypeLabel;
        private TextBox taskTypeBox;
    }
}
