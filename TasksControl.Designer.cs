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
            addTaskButton.Location = new Point(571, 0);
            addTaskButton.Name = "addTaskButton";
            addTaskButton.Size = new Size(100, 38);
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
            priorityTasksLabel.Location = new Point(3, 115);
            priorityTasksLabel.Name = "priorityTasksLabel";
            priorityTasksLabel.Size = new Size(125, 28);
            priorityTasksLabel.TabIndex = 1;
            priorityTasksLabel.Text = "Priority Tasks";
            // 
            // recurringTasksLabel
            // 
            recurringTasksLabel.AutoSize = true;
            recurringTasksLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            recurringTasksLabel.ForeColor = SystemColors.ButtonHighlight;
            recurringTasksLabel.Location = new Point(3, 291);
            recurringTasksLabel.Name = "recurringTasksLabel";
            recurringTasksLabel.Size = new Size(144, 28);
            recurringTasksLabel.TabIndex = 2;
            recurringTasksLabel.Text = "Recurring Tasks";
            // 
            // flowPanelPriorityTasks
            // 
            flowPanelPriorityTasks.Location = new Point(3, 146);
            flowPanelPriorityTasks.Name = "flowPanelPriorityTasks";
            flowPanelPriorityTasks.Size = new Size(668, 125);
            flowPanelPriorityTasks.TabIndex = 3;
            // 
            // recurringTasksPanel
            // 
            recurringTasksPanel.Location = new Point(3, 325);
            recurringTasksPanel.Name = "recurringTasksPanel";
            recurringTasksPanel.Size = new Size(668, 125);
            recurringTasksPanel.TabIndex = 4;
            // 
            // tasksControlPanel
            // 
            tasksControlPanel.Controls.Add(flowPanelPriorityTasks);
            tasksControlPanel.Controls.Add(recurringTasksLabel);
            tasksControlPanel.Controls.Add(priorityTasksLabel);
            tasksControlPanel.Controls.Add(addTaskButton);
            tasksControlPanel.Controls.Add(recurringTasksPanel);
            tasksControlPanel.Dock = DockStyle.Fill;
            tasksControlPanel.Location = new Point(0, 0);
            tasksControlPanel.Name = "tasksControlPanel";
            tasksControlPanel.Size = new Size(671, 450);
            tasksControlPanel.TabIndex = 5;
            // 
            // TasksControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(tasksControlPanel);
            Name = "TasksControl";
            Size = new Size(671, 450);
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
    }
}
