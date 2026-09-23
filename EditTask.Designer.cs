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
            taskTitleLabel = new Label();
            taskTitleBox = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            SuspendLayout();
            // 
            // taskTitleLabel
            // 
            taskTitleLabel.AutoSize = true;
            taskTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskTitleLabel.ForeColor = SystemColors.ButtonHighlight;
            taskTitleLabel.Location = new Point(3, 0);
            taskTitleLabel.Name = "taskTitleLabel";
            taskTitleLabel.Size = new Size(72, 21);
            taskTitleLabel.TabIndex = 1;
            taskTitleLabel.Text = "Task Title";
            // 
            // taskTitleBox
            // 
            taskTitleBox.BackColor = SystemColors.ActiveCaptionText;
            taskTitleBox.Font = new Font("Segoe UI Light", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            taskTitleBox.ForeColor = SystemColors.ControlLightLight;
            taskTitleBox.Location = new Point(3, 23);
            taskTitleBox.Margin = new Padding(3, 2, 3, 2);
            taskTitleBox.Name = "taskTitleBox";
            taskTitleBox.Size = new Size(364, 29);
            taskTitleBox.TabIndex = 4;
            taskTitleBox.Text = "<Type Task Title Here>";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.CalendarForeColor = SystemColors.ButtonHighlight;
            dateTimePicker1.CalendarMonthBackground = SystemColors.InactiveCaptionText;
            dateTimePicker1.CalendarTitleBackColor = SystemColors.ActiveCaptionText;
            dateTimePicker1.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.CalendarTrailingForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.Location = new Point(3, 103);
            dateTimePicker1.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(219, 23);
            dateTimePicker1.TabIndex = 5;
            // 
            // EditTask
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(dateTimePicker1);
            Controls.Add(taskTitleBox);
            Controls.Add(taskTitleLabel);
            Name = "EditTask";
            Size = new Size(587, 338);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label taskTitleLabel;
        private TextBox taskTitleBox;
        private DateTimePicker dateTimePicker1;
    }
}
