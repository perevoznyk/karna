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
using System.Drawing;
using System.Windows.Forms.Design;
using System.ComponentModel;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI
{

    /// <summary>
    /// Base class for all predefined windows embedded into Karna library
    /// </summary>
    [ToolboxItem(false)]
    [KarnaWindow("Base class for embedded semitransparent windows")]
    public class PredefinedWindow : LayeredWindow
    {
        protected Image background;
        protected string resourceName;

        protected virtual void LoadBackgroundImage()
        {
            if (String.IsNullOrEmpty(resourceName))
                return;
            background = new Bitmap(typeof(ToolboxImages), resourceName);
        }

        public Image Background
        {
            get { return background; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Code to cleanup managed resources held by the class.
                if (background != null)
                {
                    background.Dispose();
                    background = null;
                }
            }

            // Code to cleanup unmanaged resources held by the class.

            base.Dispose(disposing);
        }

        protected virtual void SetResourceName()
        {
            resourceName = "";
        }

        public PredefinedWindow() : base()
        {
            SetResourceName();
            LoadBackgroundImage();
            if (background == null)
                return;
            this.Size(background.Size);
            this.Alpha = 230;
            this.UseAlpha = true;
            this.ColorKey = ColorTranslator.ToWin32(Color.FromArgb(0, 255, 255, 255));
            this.UseColorKey = false;
            this.Canvas.DrawImage(background, 0, 0, background.Width, background.Height);
            this.Update();
        }

        public PredefinedWindow(byte alpha)
            : base()
        {
            SetResourceName();
            LoadBackgroundImage();
            if (background == null)
                return;
            this.Size(background.Size);
            this.Alpha = alpha;
            this.UseAlpha = true;
            this.ColorKey = ColorTranslator.ToWin32(Color.FromArgb(0, 255, 255, 255));
            this.UseColorKey = false;
            this.Canvas.DrawImage(background, 0, 0, background.Width, background.Height);
            this.Update();
        }
    }
}
