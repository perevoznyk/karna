//===============================================================================
// Copyright © Serhiy Perevoznyk.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace Karna.Windows.FXShow.Design
{
    internal class FXTransitionDesigner : ControlDesigner
    {
        public FXTransitionDesigner()
        {
            base.AutoResizeHandles = true;
        }

        protected virtual void DrawBorder(Graphics graphics)
        {
            Control component = (Control)base.Component;
            if ((component != null) && component.Visible)
            {
                Pen borderPen = this.BorderPen;
                Rectangle clientRectangle = this.Control.ClientRectangle;
                clientRectangle.Width--;
                clientRectangle.Height--;
                graphics.DrawRectangle(borderPen, clientRectangle);
                borderPen.Dispose();
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            Control component = (Control)base.Component;
            this.DrawBorder(pe.Graphics);
            base.OnPaintAdornments(pe);
        }

        // Properties
        protected Pen BorderPen
        {
            get
            {
                Color color = (this.Control.BackColor.GetBrightness() < 0.5) ? ControlPaint.Light(this.Control.BackColor) : ControlPaint.Dark(this.Control.BackColor);
                Pen pen = new Pen(color);
                pen.DashStyle = DashStyle.Dash;
                return pen;
            }
        }

    }
}
