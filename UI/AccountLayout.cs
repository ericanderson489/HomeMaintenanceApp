// Program: Workshop layout for the team's existing account controls and event handlers.
// Author: Murdock MacAskill
// Date: 09/29/2026
namespace HomeMaintenanceApp.UI;

internal static class AccountLayout
{
    /// <summary>Reparents existing inputs and buttons; sign-in and creation still use their original handlers.</summary>
    internal static void Apply(Panel host, string heading, (string Label, TextBox Input)[] fields, Label feedback, params Button[] buttons)
    {
        var oldControls = host.Controls.Cast<Control>().ToArray();
        host.Dock = DockStyle.Fill;
        host.BackColor = WorkshopStyle.Background;
        var outer = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = WorkshopStyle.Background };
        var welcome = new Panel { BackColor = WorkshopStyle.Navy };
        var sign = new WorkshopSign();
        var illustration = new WorkshopIllustration();
        var slogan = WorkshopStyle.Label("A little upkeep.\nA lot of peace of mind.", 19, true);
        slogan.ForeColor = WorkshopStyle.Paper;
        slogan.TextAlign = ContentAlignment.MiddleCenter;
        welcome.Controls.AddRange(new Control[] { sign, illustration, slogan });
        var form = new TableLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 1, BackColor = WorkshopStyle.Paper, Padding = new Padding(24), CellBorderStyle = TableLayoutPanelCellBorderStyle.None };
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        form.Controls.Add(WorkshopStyle.Label(heading, 24, true));
        form.Controls.Add(new WorkshopRuler { Width = 140, Height = 12, Margin = new Padding(0, 6, 0, 18) });
        int index = 0;
        foreach (var field in fields)
        {
            var label = WorkshopStyle.Label(field.Label, 10);
            label.TabIndex = index++;
            form.Controls.Add(label);
            var input = field.Input;
            input.Dock = DockStyle.Top;
            input.Font = new Font("Segoe UI", 11);
            input.BackColor = WorkshopStyle.Paper;
            input.ForeColor = WorkshopStyle.Ink;
            input.AccessibleName = field.Label;
            input.TabIndex = index++;
            input.Margin = new Padding(0, 0, 0, 12);
            form.Controls.Add(input);
        }
        feedback.AutoSize = true;
        feedback.ForeColor = Color.DarkRed;
        feedback.AccessibleName = "Account feedback";
        form.Controls.Add(feedback);
        var actions = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0, 8, 0, 0), TabIndex = index };
        for (int i = 0; i < buttons.Length; i++)
        {
            WorkshopStyle.StyleButton(buttons[i], i == 0);
            buttons[i].TabIndex = i;
            actions.Controls.Add(buttons[i]);
        }
        form.Controls.Add(actions);
        var content = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 2, RowCount = 2, Margin = Padding.Empty };
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46));
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54));
        content.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        content.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        welcome.Dock = DockStyle.Fill;
        welcome.Margin = Padding.Empty;
        form.Dock = DockStyle.Top;
        content.Controls.Add(welcome, 0, 0);
        content.Controls.Add(form, 1, 0);
        outer.Controls.Add(content);
        bool fitting = false;
        void Fit()
        {
            if (fitting) return;
            fitting = true;
            try
            {
                float scale = host.DeviceDpi / 96f;
                int viewport = Math.Max(180, outer.ClientSize.Width - SystemInformation.VerticalScrollBarWidth);
                bool wide = viewport >= 720 * scale;
                content.ColumnStyles[0].Width = wide ? 46 : 100;
                content.ColumnStyles[1].Width = wide ? 54 : 0;
                content.SetCellPosition(form, wide ? new TableLayoutPanelCellPosition(1, 0) : new TableLayoutPanelCellPosition(0, 1));
                form.Margin = new Padding(16, 18, 16, 18);
                int left = wide ? (int)(viewport * .46f) : viewport;
                int width = Math.Max(150, wide ? viewport - left - 32 : viewport - 32);
                foreach (var label in form.Controls.OfType<Label>())
                    label.MaximumSize = new Size(Math.Max(60, width - form.Padding.Horizontal - 8), 0);
                int bannerHeight = wide ? (int)(480 * scale) : (int)(155 * scale);
                welcome.MinimumSize = new Size(0, bannerHeight);
                sign.SetBounds(15, (int)(18 * scale), left - 30, (int)((wide ? 140 : 115) * scale));
                illustration.Visible = slogan.Visible = wide;
                if (wide)
                {
                    illustration.SetBounds(22, (int)(180 * scale), left - 44, (int)(210 * scale));
                    slogan.SetBounds(16, (int)(410 * scale), left - 32, (int)(65 * scale));
                }
            }
            finally { fitting = false; }
        }
        outer.SizeChanged += (_, _) => Fit();
        form.SizeChanged += (_, _) => Fit();
        foreach (var old in oldControls) if (old.Parent == host) old.Dispose();
        host.Controls.Add(outer);
        Fit();
    }
}
