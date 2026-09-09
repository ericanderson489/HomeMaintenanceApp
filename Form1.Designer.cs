namespace HomeMaintenanceApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            sidePanel = new Panel();
            mainPanel = new Panel();
            dashBoardButton = new Button();
            tasksButton = new Button();
            calendarButton = new Button();
            historyButton = new Button();
            goalsButton = new Button();
            settingsButton = new Button();
            sidePanel.SuspendLayout();
            SuspendLayout();
            // 
            // sidePanel
            // 
            sidePanel.BackColor = SystemColors.ActiveCaptionText;
            sidePanel.Controls.Add(settingsButton);
            sidePanel.Controls.Add(goalsButton);
            sidePanel.Controls.Add(historyButton);
            sidePanel.Controls.Add(calendarButton);
            sidePanel.Controls.Add(tasksButton);
            sidePanel.Controls.Add(dashBoardButton);
            sidePanel.Dock = DockStyle.Left;
            sidePanel.Location = new Point(0, 0);
            sidePanel.Name = "sidePanel";
            sidePanel.Size = new Size(134, 450);
            sidePanel.TabIndex = 0;
            sidePanel.Paint += sidePanel_Paint;
            // 
            // mainPanel
            // 
            mainPanel.BackColor = SystemColors.ActiveCaptionText;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(134, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(589, 450);
            mainPanel.TabIndex = 1;
            mainPanel.Paint += mainPanel_Paint;
            // 
            // dashBoardButton
            // 
            dashBoardButton.AutoSize = true;
            dashBoardButton.BackColor = SystemColors.ActiveCaptionText;
            dashBoardButton.FlatStyle = FlatStyle.Popup;
            dashBoardButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dashBoardButton.ForeColor = SystemColors.ButtonHighlight;
            dashBoardButton.Location = new Point(12, 87);
            dashBoardButton.Name = "dashBoardButton";
            dashBoardButton.Size = new Size(118, 38);
            dashBoardButton.TabIndex = 0;
            dashBoardButton.Text = "Dashboard";
            dashBoardButton.TextAlign = ContentAlignment.MiddleLeft;
            dashBoardButton.UseVisualStyleBackColor = false;
            dashBoardButton.Click += dashBoardButton_Click;
            // 
            // tasksButton
            // 
            tasksButton.AutoSize = true;
            tasksButton.BackColor = SystemColors.ActiveCaptionText;
            tasksButton.FlatStyle = FlatStyle.Popup;
            tasksButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tasksButton.ForeColor = SystemColors.ButtonHighlight;
            tasksButton.Location = new Point(12, 163);
            tasksButton.Name = "tasksButton";
            tasksButton.Size = new Size(97, 38);
            tasksButton.TabIndex = 1;
            tasksButton.Text = "Tasks";
            tasksButton.TextAlign = ContentAlignment.MiddleLeft;
            tasksButton.UseVisualStyleBackColor = false;
            tasksButton.Click += tasksButton_Click;
            // 
            // calendarButton
            // 
            calendarButton.AutoSize = true;
            calendarButton.BackColor = SystemColors.ActiveCaptionText;
            calendarButton.FlatStyle = FlatStyle.Popup;
            calendarButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            calendarButton.ForeColor = SystemColors.ButtonHighlight;
            calendarButton.Location = new Point(12, 244);
            calendarButton.Name = "calendarButton";
            calendarButton.Size = new Size(99, 38);
            calendarButton.TabIndex = 2;
            calendarButton.Text = "Calendar";
            calendarButton.TextAlign = ContentAlignment.MiddleLeft;
            calendarButton.UseVisualStyleBackColor = false;
            calendarButton.Click += calendarButton_Click;
            // 
            // historyButton
            // 
            historyButton.AutoSize = true;
            historyButton.BackColor = SystemColors.ActiveCaptionText;
            historyButton.FlatStyle = FlatStyle.Popup;
            historyButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            historyButton.ForeColor = SystemColors.ButtonHighlight;
            historyButton.Location = new Point(12, 311);
            historyButton.Name = "historyButton";
            historyButton.Size = new Size(97, 38);
            historyButton.TabIndex = 3;
            historyButton.Text = "History";
            historyButton.TextAlign = ContentAlignment.MiddleLeft;
            historyButton.UseVisualStyleBackColor = false;
            historyButton.Click += historyButton_Click;
            // 
            // goalsButton
            // 
            goalsButton.AutoSize = true;
            goalsButton.BackColor = SystemColors.ActiveCaptionText;
            goalsButton.FlatStyle = FlatStyle.Popup;
            goalsButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            goalsButton.ForeColor = SystemColors.ButtonHighlight;
            goalsButton.Location = new Point(12, 207);
            goalsButton.Name = "goalsButton";
            goalsButton.Size = new Size(97, 38);
            goalsButton.TabIndex = 4;
            goalsButton.Text = "Goals";
            goalsButton.TextAlign = ContentAlignment.MiddleLeft;
            goalsButton.UseVisualStyleBackColor = false;
            goalsButton.Click += goalsButton_Click;
            // 
            // settingsButton
            // 
            settingsButton.AutoSize = true;
            settingsButton.BackColor = SystemColors.ActiveCaptionText;
            settingsButton.Dock = DockStyle.Bottom;
            settingsButton.FlatStyle = FlatStyle.Popup;
            settingsButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            settingsButton.ForeColor = SystemColors.ButtonHighlight;
            settingsButton.Location = new Point(0, 412);
            settingsButton.Name = "settingsButton";
            settingsButton.Size = new Size(134, 38);
            settingsButton.TabIndex = 5;
            settingsButton.Text = "Settings";
            settingsButton.TextAlign = ContentAlignment.MiddleLeft;
            settingsButton.UseVisualStyleBackColor = false;
            settingsButton.Click += settingsButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(723, 450);
            Controls.Add(mainPanel);
            Controls.Add(sidePanel);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Home Maintenance";
            Load += MainForm_Load;
            sidePanel.ResumeLayout(false);
            sidePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel sidePanel;
        private Button dashBoardButton;
        private Panel mainPanel;
        private Button historyButton;
        private Button calendarButton;
        private Button tasksButton;
        private Button settingsButton;
        private Button goalsButton;
    }
}
