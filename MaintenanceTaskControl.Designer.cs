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
            taskTypeLabel = new Label();
            closeButton = new Button();
            doneTaskButton = new Button();
            tasktypetextBox = new TextBox();
            maintenanceTaskPanel.SuspendLayout();
            SuspendLayout();
            // 
            // taskTitleLabel
            // 
            taskTitleLabel.AutoSize = true;
            taskTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTitleLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTitleLabel.Location = new Point(0, 9);
            taskTitleLabel.Name = "taskTitleLabel";
            taskTitleLabel.Size = new Size(72, 21);
            taskTitleLabel.TabIndex = 0;
            taskTitleLabel.Text = "Task Title";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descriptionLabel.ForeColor = SystemColors.ButtonHighlight;
            descriptionLabel.Location = new Point(3, 122);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(89, 21);
            descriptionLabel.TabIndex = 1;
            descriptionLabel.Text = "Description";
            descriptionLabel.Click += label2_Click;
            // 
            // dueDateLabel
            // 
            dueDateLabel.AutoSize = true;
            dueDateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dueDateLabel.ForeColor = SystemColors.ButtonHighlight;
            dueDateLabel.Location = new Point(0, 68);
            dueDateLabel.Name = "dueDateLabel";
            dueDateLabel.Size = new Size(74, 21);
            dueDateLabel.TabIndex = 2;
            dueDateLabel.Text = "Due Date";
            // 
            // taskTitleBox
            // 
            taskTitleBox.BackColor = SystemColors.ActiveCaptionText;
            taskTitleBox.Font = new Font("Segoe UI Light", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            taskTitleBox.ForeColor = SystemColors.ControlLightLight;
            taskTitleBox.Location = new Point(3, 32);
            taskTitleBox.Margin = new Padding(3, 2, 3, 2);
            taskTitleBox.Name = "taskTitleBox";
            taskTitleBox.Size = new Size(364, 29);
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
            dateTimePicker1.Location = new Point(3, 92);
            dateTimePicker1.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(219, 23);
            dateTimePicker1.TabIndex = 4;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ActiveCaptionText;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.ForeColor = SystemColors.ControlLightLight;
            textBox1.Location = new Point(3, 145);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(364, 82);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // maintenanceTaskPanel
            // 
            maintenanceTaskPanel.Controls.Add(tasktypetextBox);
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
            maintenanceTaskPanel.Margin = new Padding(3, 2, 3, 2);
            maintenanceTaskPanel.Name = "maintenanceTaskPanel";
            maintenanceTaskPanel.Size = new Size(587, 338);
            maintenanceTaskPanel.TabIndex = 6;
            // 
            // taskTypeLabel
            // 
            taskTypeLabel.AutoSize = true;
            taskTypeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTypeLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTypeLabel.Location = new Point(3, 238);
            taskTypeLabel.Name = "taskTypeLabel";
            taskTypeLabel.Size = new Size(75, 21);
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
            closeButton.Location = new Point(3, 307);
            closeButton.Margin = new Padding(3, 2, 3, 2);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(82, 31);
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
            doneTaskButton.Location = new Point(505, 307);
            doneTaskButton.Margin = new Padding(3, 2, 3, 2);
            doneTaskButton.Name = "doneTaskButton";
            doneTaskButton.Size = new Size(82, 31);
            doneTaskButton.TabIndex = 6;
            doneTaskButton.Text = "Done";
            doneTaskButton.UseVisualStyleBackColor = false;
            doneTaskButton.Click += doneTaskButton_Click;
            // 
            // tasktypetextBox
            // 
            tasktypetextBox.Location = new Point(3, 262);
            tasktypetextBox.Name = "tasktypetextBox";
            tasktypetextBox.Size = new Size(150, 23);
            tasktypetextBox.TabIndex = 9;
            tasktypetextBox.TextChanged += tasktypetextBox_TextChanged;
            // 
            // MaintenanceTaskControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(maintenanceTaskPanel);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MaintenanceTaskControl";
            Size = new Size(587, 338);
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
        private TextBox tasktypetextBox;
    }
}
