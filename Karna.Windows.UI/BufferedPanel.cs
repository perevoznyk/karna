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
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Karna.Windows.UI
{
    public partial class BufferedPanel : Control, IDisposable
    {
        private Image wallpaper;

        public BufferedPanel()
        {
            InitializeComponent();
            BackColor = Color.Black;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        public BufferedPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            BackColor = Color.Black;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (wallpaper == null)
                base.OnPaint(pe);
            else
            {
                pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                BitmapPainter.CopyImageScaled((Bitmap)wallpaper, pe.Graphics, 0, 0, this.Width, this.Height); 
                base.OnPaint(pe);
            }
        }


        public Image Wallpaper
        {
            get
            {
                return wallpaper;
            }
            set
            {
                if (wallpaper != value)
                {
                    wallpaper = value;
                    Invalidate();
                }
            }
        }
    }
}
