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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI.Dock
{
    /// <summary>
    /// Provides data for the Draw event.
    /// </summary>
    [KarnaPurpose("Dock")]
    public class DrawEventArgs : EventArgs, IDisposable
    {
        private Graphics graphics;
        private DockItem item;

        public DrawEventArgs(Graphics graphics, DockItem item)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            this.graphics = graphics;
            this.item = item;
        }

        /// <summary>
        /// Gets the graphics used to paint.
        /// </summary>
        /// <value>
        /// The Graphics object used to paint. The Graphics object provides methods for drawing objects on the display device.
        /// </value>
        public Graphics Graphics
        {
            get { return graphics; }
        }

        /// <summary>
        /// Gets the Dock item.
        /// </summary>
        /// <value>The Dock item.</value>
        public DockItem Item
        {
            get { return item; }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="DrawEventArgs"/> is reclaimed by garbage collection.
        /// </summary>
        ~DrawEventArgs()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if ((disposing && (this.graphics != null)))
            {
                this.graphics.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

    /// <summary>
    /// Represents the method that will handle the Draw event of a Dock.
    /// </summary>
    [Serializable]
    public delegate void DockItemDrawHandler(object sender, DrawEventArgs e);

    public class DockPainter
    {
        public event DockItemDrawHandler Draw;

        protected virtual void DoDraw(Graphics g, DockItem item)
        {
            DrawEventArgs e = new DrawEventArgs(g, item);
            OnDraw(e);
        }

        protected virtual void OnDraw(DrawEventArgs e)
        {
            if (Draw != null)
                Draw(this, e);
        }

        public virtual void Paint(IntPtr hdc, DockItem item)
        {
            if (item != null)
            {
                //custom draw implementation
                if (Draw != null)
                {
                    Graphics g = Graphics.FromHdc(hdc);
                    DoDraw(g, item);
                    return;
                }

                if (item.Scale == 1.0f)
                {
                    if (item.Alpha == 255)
                        BitmapPainter.DrawImageUnscaled((Bitmap)item.Icon, hdc, item.X, item.Y, item.Width, item.Height);
                    else
                        BitmapPainter.DrawImageUnscaled((Bitmap)item.Icon, hdc, item.X, item.Y, item.Width, item.Height, item.Alpha);

                }
                else
                {
                    if (item.Alpha == 255)
                        BitmapPainter.DrawImageScaled((Bitmap)item.Icon, hdc, item.X, item.Y, (int)(item.Width * item.Scale), (int)(item.Height * item.Scale));
                    else
                    {
                        BitmapPainter.DrawImageScaled((Bitmap)item.Icon, hdc, item.X, item.Y, (int)(item.Width * item.Scale), (int)(item.Height * item.Scale), item.Alpha);
                    }
                }
            }
        }

    }
}
