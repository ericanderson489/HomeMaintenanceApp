// Program: Shared workshop emblem, ruler, and login illustration.
// Author: Murdock MacAskill
// Date: 09/29/2026
using System.Drawing.Drawing2D;

namespace HomeMaintenanceApp.UI;

internal sealed class WorkshopSign : Control
{
    internal WorkshopSign()
    {
        DoubleBuffered = true;
        AccessibleName = "Breachless Bungalow — Home Maintenance Workshop";
        AccessibleRole = AccessibleRole.StaticText;
        TabStop = false;
        BackColor = WorkshopStyle.Navy;
    }

    /// <summary>Draws the shared framed emblem at sidebar and login sizes.</summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (Width < 20 || Height < 20) return;
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        float scale = Math.Min(Width / 320f, Height / 205f);
        var state = g.Save();
        g.TranslateTransform((Width - 320 * scale) / 2, (Height - 205 * scale) / 2);
        g.ScaleTransform(scale, scale);
        g.TranslateTransform(160, 102);
        g.RotateTransform(-2);
        g.TranslateTransform(-160, -102);
        using var ink = new SolidBrush(WorkshopStyle.Ink);
        using var steel = new SolidBrush(WorkshopStyle.Steel);
        using var gold = new SolidBrush(WorkshopStyle.Gold);
        using var red = new SolidBrush(WorkshopStyle.Red);
        using var paper = new SolidBrush(WorkshopStyle.Paper);
        using var border = new Pen(WorkshopStyle.Steel, 3);
        using var inner = new Pen(Color.FromArgb(56, 86, 99), 2);
        g.FillRectangle(ink, 10, 15, 300, 175);
        g.DrawRectangle(border, 10, 15, 300, 175);
        g.DrawRectangle(inner, 16, 21, 288, 163);
        foreach (int x in new[] { 22, 298 }) foreach (int y in new[] { 27, 178 }) g.FillEllipse(steel, x - 2, y - 2, 4, 4);
        using var first = new Font("Bahnschrift SemiCondensed", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        using var last = new Font("Bahnschrift SemiCondensed", 40, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Pixel);
        using var caption = new Font("Bahnschrift SemiCondensed", 12, FontStyle.Bold, GraphicsUnit.Pixel);
        using var centered = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("BREACHLESS", first, paper, new RectangleF(20, 36, 280, 32), centered);
        g.DrawString("BUNGALOW", last, gold, new RectangleF(15, 68, 290, 48), centered);
        g.FillRectangle(steel, 35, 127, 250, 3);
        g.FillRectangle(red, 259, 124, 27, 8);
        g.DrawString("HOME MAINTENANCE\nWORKSHOP", caption, paper, new RectangleF(20, 140, 280, 35), centered);
        g.Restore(state);
    }
}

internal sealed class WorkshopRuler : Control
{
    internal WorkshopRuler() { DoubleBuffered = true; TabStop = false; }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen = new Pen(WorkshopStyle.Muted);
        e.Graphics.DrawLine(pen, 0, 1, Width, 1);
        int step = Math.Max(6, (int)(8 * DeviceDpi / 96f));
        for (int x = 0; x < Width; x += step) e.Graphics.DrawLine(pen, x, 1, x, x % (step * 4) == 0 ? Height - 1 : Height / 2);
    }
}

internal sealed class WorkshopIllustration : PictureBox
{
    /// <summary>Loads the bundled illustration without relying on a developer's file path.</summary>
    internal WorkshopIllustration()
    {
        using var stream = typeof(WorkshopIllustration).Assembly.GetManifestResourceStream("HomeMaintenanceApp.WorkshopIllustration.png")
            ?? throw new InvalidOperationException("The workshop illustration is missing.");
        using var source = Image.FromStream(stream);
        Image = new Bitmap(source);
        SizeMode = PictureBoxSizeMode.Zoom;
        BackColor = WorkshopStyle.Background;
        AccessibleName = "Wooden workbench, pegboard tools, and navy tool cabinet";
        TabStop = false;
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing) { var image = Image; Image = null; image?.Dispose(); }
        base.Dispose(disposing);
    }
}
