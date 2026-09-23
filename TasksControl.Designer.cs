namespace HomeMaintenanceApp
{
    partial class TasksControl
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
            addTaskButton = new Button();
            priorityTasksLabel = new Label();
            recurringTasksLabel = new Label();
            flowPanelPriorityTasks = new FlowLayoutPanel();
            recurringTasksPanel = new FlowLayoutPanel();
            tasksControlPanel = new Panel();
            editTaskButton = new Button();
            completeTaskButton = new Button();
            tasksControlPanel.SuspendLayout();
            SuspendLayout();
            // 
            // addTaskButton
            // 
            addTaskButton.Anchor = AnchorStyles.Right;
            addTaskButton.AutoSize = true;
            addTaskButton.BackColor = SystemColors.ActiveCaptionText;
            addTaskButton.FlatStyle = FlatStyle.Popup;
            addTaskButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            addTaskButton.ForeColor = SystemColors.ButtonHighlight;
            addTaskButton.Location = new Point(500, 0);
            addTaskButton.Margin = new Padding(3, 2, 3, 2);
            addTaskButton.Name = "addTaskButton";
            addTaskButton.Size = new Size(88, 31);
            addTaskButton.TabIndex = 0;
            addTaskButton.Text = "Add Task";
            addTaskButton.UseVisualStyleBackColor = false;
            addTaskButton.Click += addTaskButton_Click;
            // 
            // priorityTasksLabel
            // 
            priorityTasksLabel.AutoSize = true;
            priorityTasksLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            priorityTasksLabel.ForeColor = SystemColors.ButtonHighlight;
            priorityTasksLabel.Location = new Point(3, 86);
            priorityTasksLabel.Name = "priorityTasksLabel";
            priorityTasksLabel.Size = new Size(101, 21);
            priorityTasksLabel.TabIndex = 1;
            priorityTasksLabel.Text = "Priority Tasks";
            // 
            // recurringTasksLabel
            // 
            recurringTasksLabel.AutoSize = true;
            recurringTasksLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            recurringTasksLabel.ForeColor = SystemColors.ButtonHighlight;
            recurringTasksLabel.Location = new Point(3, 218);
            recurringTasksLabel.Name = "recurringTasksLabel";
            recurringTasksLabel.Size = new Size(118, 21);
            recurringTasksLabel.TabIndex = 2;
            recurringTasksLabel.Text = "Recurring Tasks";
            // 
            // flowPanelPriorityTasks
            // 
            flowPanelPriorityTasks.Location = new Point(3, 110);
            flowPanelPriorityTasks.Margin = new Padding(3, 2, 3, 2);
            flowPanelPriorityTasks.Name = "flowPanelPriorityTasks";
            flowPanelPriorityTasks.Size = new Size(584, 94);
            flowPanelPriorityTasks.TabIndex = 3;
            // 
            // recurringTasksPanel
            // 
            recurringTasksPanel.Location = new Point(3, 244);
            recurringTasksPanel.Margin = new Padding(3, 2, 3, 2);
            recurringTasksPanel.Name = "recurringTasksPanel";
            recurringTasksPanel.Size = new Size(584, 94);
            recurringTasksPanel.TabIndex = 4;
            // 
            // tasksControlPanel
            // 
            tasksControlPanel.Controls.Add(editTaskButton);
            tasksControlPanel.Controls.Add(completeTaskButton);
            tasksControlPanel.Controls.Add(flowPanelPriorityTasks);
            tasksControlPanel.Controls.Add(recurringTasksLabel);
            tasksControlPanel.Controls.Add(priorityTasksLabel);
            tasksControlPanel.Controls.Add(addTaskButton);
            tasksControlPanel.Controls.Add(recurringTasksPanel);
            tasksControlPanel.Dock = DockStyle.Fill;
            tasksControlPanel.Location = new Point(0, 0);
            tasksControlPanel.Margin = new Padding(3, 2, 3, 2);
            tasksControlPanel.Name = "tasksControlPanel";
            tasksControlPanel.Size = new Size(587, 338);
            tasksControlPanel.TabIndex = 5;
            tasksControlPanel.Paint += tasksControlPanel_Paint;
            // 
            // editTaskButton
            // 
            editTaskButton.Anchor = AnchorStyles.Right;
            editTaskButton.AutoSize = true;
            editTaskButton.BackColor = SystemColors.ActiveCaptionText;
            editTaskButton.FlatStyle = FlatStyle.Popup;
            editTaskButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editTaskButton.ForeColor = SystemColors.ButtonHighlight;
            editTaskButton.Location = new Point(499, 35);
            editTaskButton.Margin = new Padding(3, 2, 3, 2);
            editTaskButton.Name = "editTaskButton";
            editTaskButton.Size = new Size(88, 31);
            editTaskButton.TabIndex = 6;
            editTaskButton.Text = "Edit Task";
            editTaskButton.UseVisualStyleBackColor = false;
            editTaskButton.Click += editTaskButton_Click;
            // 
            // completeTaskButton
            // 
            completeTaskButton.Anchor = AnchorStyles.Right;
            completeTaskButton.AutoSize = true;
            completeTaskButton.BackColor = SystemColors.ActiveCaptionText;
            completeTaskButton.FlatStyle = FlatStyle.Popup;
            completeTaskButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            completeTaskButton.ForeColor = SystemColors.ButtonHighlight;
            completeTaskButton.Location = new Point(464, 70);
            completeTaskButton.Margin = new Padding(3, 2, 3, 2);
            completeTaskButton.Name = "completeTaskButton";
            completeTaskButton.Size = new Size(120, 31);
            completeTaskButton.TabIndex = 5;
            completeTaskButton.Text = "Complete Task";
            completeTaskButton.UseVisualStyleBackColor = false;
            completeTaskButton.Click += completeTaskButton_Click;
            // 
            // TasksControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(tasksControlPanel);
            Margin = new Padding(3, 2, 3, 2);
            Name = "TasksControl";
            Size = new Size(587, 338);
            tasksControlPanel.ResumeLayout(false);
            tasksControlPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button addTaskButton;
        private Label priorityTasksLabel;
        private Label recurringTasksLabel;
        private FlowLayoutPanel flowPanelPriorityTasks;
        private FlowLayoutPanel recurringTasksPanel;
        private Panel tasksControlPanel;
        private Button editTaskButton;
        private Button completeTaskButton;
    }
}
