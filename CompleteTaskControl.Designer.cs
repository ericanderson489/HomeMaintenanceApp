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
            taskNameBox = new TextBox();
            taskTitleLabel = new Label();
            CheckIfValid = new Button();
            SuspendLayout();
            // 
            // taskNameBox
            // 
            taskNameBox.Location = new Point(3, 71);
            taskNameBox.Name = "taskNameBox";
            taskNameBox.Size = new Size(150, 31);
            taskNameBox.TabIndex = 0;
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
            CheckIfValid.Location = new Point(172, 67);
            CheckIfValid.Name = "CheckIfValid";
            CheckIfValid.Size = new Size(116, 39);
            CheckIfValid.TabIndex = 19;
            CheckIfValid.Text = "Check";
            CheckIfValid.UseVisualStyleBackColor = false;
            CheckIfValid.Click += CheckIfValid_Click;
            // 
            // CompleteTaskControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(CheckIfValid);
            Controls.Add(taskTitleLabel);
            Controls.Add(taskNameBox);
            Name = "CompleteTaskControl";
            Size = new Size(839, 562);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox taskNameBox;
        private Label taskTitleLabel;
        private Button CheckIfValid;
    }
}
