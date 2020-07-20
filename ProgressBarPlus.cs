using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamGuardDesktop
{
    public class ProgressBarPlus : ProgressBar
    {
        public ProgressBarPlus()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        public void setColor(Brush brush)
        {
            this.colorBrush = brush;
        }

        private Brush colorBrush = Brushes.LimeGreen;

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(this.colorBrush, 2, 2, rec.Width, rec.Height);
        }
    }
}
