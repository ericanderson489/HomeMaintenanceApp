// Program: Workshop layout for the team's existing account controls and event handlers.
// Author: Murdock MacAskill
// Date: 09/29/2026
// Authentication and account-file behavior remain in the team's classes.
namespace HomeMaintenanceApp.UI;

internal static class AccountLayout
{
    /// <summary>Reparents existing inputs/buttons without replacing their event handlers.</summary>
    internal static void Apply(Panel host, string heading, (string Label, TextBox Input)[] fields, Label feedback, params Button[] buttons)
    {
        var oldControls = host.Controls.Cast<Control>().ToArray();
        host.Dock = DockStyle.Fill;
        host.BackColor = WorkshopStyle.Pegboard;
        var outer = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = WorkshopStyle.Pegboard };
        var form = new TableLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 1, BackColor = WorkshopStyle.Paper, Padding = new Padding(24) };
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        form.Controls.Add(WorkshopStyle.Label("HOME MAINTENANCE", 22, true));
        form.Controls.Add(WorkshopStyle.Label(heading, 16, true));
        int index = 0;
        foreach (var field in fields)
        {
            var label = WorkshopStyle.Label(field.Label);
            label.TabIndex = index++;
            form.Controls.Add(label);
            var input = field.Input;
            input.Dock = DockStyle.Top;
            input.Font = new Font("Segoe UI", 11);
            input.BackColor = Color.White;
            input.ForeColor = WorkshopStyle.Navy;
            input.AccessibleName = field.Label;
            input.TabIndex = index++;
            input.Margin = new Padding(0, 0, 0, 9);
            form.Controls.Add(input);
        }
        feedback.ForeColor = Color.DarkRed;
        feedback.AccessibleName = "Account feedback";
        form.Controls.Add(feedback);
        var actions = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0, 12, 0, 0), TabIndex = index };
        for (int i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];
            button.Font = new Font("Segoe UI", 10);
            button.FlatStyle = FlatStyle.Flat;
            button.AutoSize = true;
            button.Padding = new Padding(10, 6, 10, 6);
            button.BackColor = i == 0 ? WorkshopStyle.Gold : WorkshopStyle.Paper;
            button.ForeColor = WorkshopStyle.Navy;
            button.TabIndex = i;
            actions.Controls.Add(button);
        }
        form.Controls.Add(actions);
        outer.Controls.Add(form);
        // Limit width to the viewport; vertical scrolling keeps every field and action reachable.
        void Fit()
        {
            int width = Math.Max(120, Math.Min((int)(580 * host.DeviceDpi / 96f), outer.ClientSize.Width - 48 - SystemInformation.VerticalScrollBarWidth));
            form.MinimumSize = new Size(width, 0);
            form.MaximumSize = new Size(width, 0);
            foreach (var label in form.Controls.OfType<Label>())
                label.MaximumSize = new Size(Math.Max(60, width - form.Padding.Horizontal - 8), 0);
            form.Location = new Point(Math.Max(12, (outer.ClientSize.Width - width - SystemInformation.VerticalScrollBarWidth) / 2), 24 + outer.AutoScrollPosition.Y);
        }
        outer.SizeChanged += (_, _) => Fit();
        foreach (var old in oldControls) if (old.Parent == host) old.Dispose();
        host.Controls.Add(outer);
        Fit();
    }
}
