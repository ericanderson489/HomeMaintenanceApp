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
            editTask = new Button();
            CompleteTask = new Button();
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
            addTaskButton.Location = new Point(691, 0);
            addTaskButton.Margin = new Padding(4);
            addTaskButton.Name = "addTaskButton";
            addTaskButton.Size = new Size(148, 52);
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
            priorityTasksLabel.Location = new Point(4, 144);
            priorityTasksLabel.Margin = new Padding(4, 0, 4, 0);
            priorityTasksLabel.Name = "priorityTasksLabel";
            priorityTasksLabel.Size = new Size(150, 32);
            priorityTasksLabel.TabIndex = 1;
            priorityTasksLabel.Text = "Priority Tasks";
            // 
            // recurringTasksLabel
            // 
            recurringTasksLabel.AutoSize = true;
            recurringTasksLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            recurringTasksLabel.ForeColor = SystemColors.ButtonHighlight;
            recurringTasksLabel.Location = new Point(4, 364);
            recurringTasksLabel.Margin = new Padding(4, 0, 4, 0);
            recurringTasksLabel.Name = "recurringTasksLabel";
            recurringTasksLabel.Size = new Size(176, 32);
            recurringTasksLabel.TabIndex = 2;
            recurringTasksLabel.Text = "Recurring Tasks";
            // 
            // flowPanelPriorityTasks
            // 
            flowPanelPriorityTasks.Location = new Point(4, 182);
            flowPanelPriorityTasks.Margin = new Padding(4);
            flowPanelPriorityTasks.Name = "flowPanelPriorityTasks";
            flowPanelPriorityTasks.Size = new Size(835, 156);
            flowPanelPriorityTasks.TabIndex = 3;
            flowPanelPriorityTasks.Paint += flowPanelPriorityTasks_Paint;
            // 
            // recurringTasksPanel
            // 
            recurringTasksPanel.Location = new Point(4, 406);
            recurringTasksPanel.Margin = new Padding(4);
            recurringTasksPanel.Name = "recurringTasksPanel";
            recurringTasksPanel.Size = new Size(835, 156);
            recurringTasksPanel.TabIndex = 4;
            // 
            // tasksControlPanel
            // 
            tasksControlPanel.Controls.Add(CompleteTask);
            tasksControlPanel.Controls.Add(editTask);
            tasksControlPanel.Controls.Add(flowPanelPriorityTasks);
            tasksControlPanel.Controls.Add(recurringTasksLabel);
            tasksControlPanel.Controls.Add(priorityTasksLabel);
            tasksControlPanel.Controls.Add(addTaskButton);
            tasksControlPanel.Controls.Add(recurringTasksPanel);
            tasksControlPanel.Dock = DockStyle.Fill;
            tasksControlPanel.Location = new Point(0, 0);
            tasksControlPanel.Margin = new Padding(4);
            tasksControlPanel.Name = "tasksControlPanel";
            tasksControlPanel.Size = new Size(839, 562);
            tasksControlPanel.TabIndex = 5;
            // 
            // editTask
            // 
            editTask.Anchor = AnchorStyles.Right;
            editTask.AutoSize = true;
            editTask.BackColor = SystemColors.ActiveCaptionText;
            editTask.FlatStyle = FlatStyle.Popup;
            editTask.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editTask.ForeColor = SystemColors.ButtonHighlight;
            editTask.Location = new Point(691, 60);
            editTask.Margin = new Padding(4);
            editTask.Name = "editTask";
            editTask.Size = new Size(148, 52);
            editTask.TabIndex = 5;
            editTask.Text = "Edit Task";
            editTask.UseVisualStyleBackColor = false;
            editTask.Click += editTask_Click;
            // 
            // CompleteTask
            // 
            CompleteTask.Anchor = AnchorStyles.Right;
            CompleteTask.AutoSize = true;
            CompleteTask.BackColor = SystemColors.ActiveCaptionText;
            CompleteTask.FlatStyle = FlatStyle.Popup;
            CompleteTask.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CompleteTask.ForeColor = SystemColors.ButtonHighlight;
            CompleteTask.Location = new Point(660, 120);
            CompleteTask.Margin = new Padding(4);
            CompleteTask.Name = "CompleteTask";
            CompleteTask.Size = new Size(179, 52);
            CompleteTask.TabIndex = 6;
            CompleteTask.Text = "Complete Task";
            CompleteTask.UseVisualStyleBackColor = false;
            CompleteTask.Click += CompleteTask_Click;
            // 
            // TasksControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(tasksControlPanel);
            Margin = new Padding(4);
            Name = "TasksControl";
            Size = new Size(839, 562);
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
        private Button editTask;
        private Button CompleteTask;
    }
}
