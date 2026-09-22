namespace HomeMaintenanceApp
{
    partial class GoalsControl
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
            addGoalsButton = new Button();
            SuspendLayout();
            // 
            // addGoalsButton
            // 
            addGoalsButton.AutoSize = true;
            addGoalsButton.BackColor = SystemColors.ActiveCaptionText;
            addGoalsButton.FlatStyle = FlatStyle.Popup;
            addGoalsButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            addGoalsButton.ForeColor = SystemColors.ButtonHighlight;
            addGoalsButton.Location = new Point(490, 2);
            addGoalsButton.Margin = new Padding(3, 2, 3, 2);
            addGoalsButton.Name = "addGoalsButton";
            addGoalsButton.Size = new Size(94, 31);
            addGoalsButton.TabIndex = 0;
            addGoalsButton.Text = "Add Goal";
            addGoalsButton.UseVisualStyleBackColor = false;
            // 
            // GoalsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(addGoalsButton);
            Margin = new Padding(3, 2, 3, 2);
            Name = "GoalsControl";
            Size = new Size(587, 338);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addGoalsButton;
    }
}
