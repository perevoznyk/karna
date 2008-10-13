//===============================================================================
// Copyright © Serhiy Perevoznyk.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Karna.Windows.UI
{
    public partial class GradientPanel : Panel
    {
        private GradientFill gradient;
        public GradientPanel()
        {
            InitializeComponent();
            gradient = new GradientFill();
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            gradient.InvalidatePattern();
            gradient.Changed += HandleChange;
        }

        protected void HandleChange(Object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (gradient.Visible)
            {
                pe.Graphics.DrawImage(gradient.Pattern, 0, 0, this.Width, this.Height);
            }
            else
            {
                base.OnPaint(pe);
            }
        }

        [Browsable(true)]
        public GradientFill Gradient
        {
            get
            {
                return gradient;
            }
        }
    }
}
