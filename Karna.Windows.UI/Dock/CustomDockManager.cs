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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.CompilerServices;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI.Dock
{
    [KarnaPurpose("Dock")]
    public class CustomDockManager
    {
        private DockSettings settings;
        private int width;
        private int height;
        private bool useAlpha = true;
        public EventHandler AlphaChanged;
        protected List<DockItem> items;
        private DockPainter painter;
        [AccessedThroughProperty("ImageList")]
        private ImageList imageList;


        public CustomDockManager()
        {
            items = new List<DockItem>();
            painter = GetDockPainter();
        }

        protected virtual DockPainter GetDockPainter()
        {
            return new DockPainter();
        }

        public List<DockItem> Items
        {
            get
            {
                return items;
            }
        }

        public DockSettings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
                UpdateDockInfo();
            }
        }

        public virtual void UpdateDockInfo()
        {
        }

        public ImageList ImageList
        {
            get { return imageList; }
            set
            {
                imageList = value;
                foreach (DockItem item in items)
                {
                    item.ImageList = value;
                }
            }
        }

        /// <summary>
        /// This method allows derived classes to handle the event without attaching a delegate
        /// </summary>
        /// <param name="CurX"></param>
        /// <param name="CurY"></param>
        public virtual void DoMouseMove(int CurX, int CurY)
        {
        }

        /// <summary>
        /// This method allows derived classes to handle the event without attaching a delegate
        /// </summary>
        public virtual void DoMouseLeave()
        {
        }

        /// <summary>
        /// Raises the <see cref="E:AlphaChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnAlphaChanged(EventArgs e)
        {
            if (AlphaChanged != null)
            {
                AlphaChanged(this, e);
            }
        }

        /// <summary>
        /// This method allows derived classes to handle the event without attaching a delegate
        /// </summary>
        protected virtual void DoAlphaChange()
        {
            OnAlphaChanged(EventArgs.Empty);
        }

        /// <summary>
        /// This method allows derived classes to handle the event without attaching a delegate
        /// </summary>
        public virtual void DoSizeChanged()
        {
        }

        /// <summary>
        /// Loads icon for the dock item from the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public virtual Bitmap IconFromFileName(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
            {
                Bitmap result = (Bitmap)Bitmap.FromFile(fileName);
                return result;
            }
            else
                return null;
        }

        public virtual Bitmap IconFromImageList(int index)
        {
            if (imageList != null)
            {
                Bitmap result = (Bitmap)imageList.Images[index];
                return result;
            }
            else
                return null;
        }

        [DefaultValue(true)]
        public bool UseAlpha
        {
            get { return useAlpha; }
            set
            {
                if (useAlpha != value)
                {
                    useAlpha = value;
                    DoAlphaChange();
                }
            }
        }

        /// <summary>
        /// Gets the left margin of the icon.
        /// </summary>
        /// <returns></returns>
        protected int GetLeftMargin()
        {
            if (settings == null)
                return DefaultDockSettings.LeftMargin;
            else
                return settings.LeftMargin;
        }

        /// <summary>
        /// Gets the top margin of the icon.
        /// </summary>
        /// <returns></returns>
        protected int GetTopMargin()
        {
            if (settings == null)
                return DefaultDockSettings.TopMargin;
            else
                return settings.TopMargin;
        }

        /// <summary>
        /// Gets the size of the icon.
        /// </summary>
        /// <returns></returns>
        protected int GetIconSize()
        {
            if (settings == null)
                return DefaultDockSettings.IconSize;
            else
                return settings.IconSize;
        }

        /// <summary>
        /// Gets the max zoom.
        /// </summary>
        /// <returns></returns>
        protected int GetMaxZoom()
        {
            if (settings == null)
                return DefaultDockSettings.MaxZoom;
            else
                return settings.MaxZoom;
        }

        /// <summary>
        /// Gets the icons spacing.
        /// </summary>
        /// <returns></returns>
        protected int GetIconsSpacing()
        {
            if (settings == null)
                return DefaultDockSettings.IconsSpacing;
            else
                return settings.IconsSpacing;
        }

        /// <summary>
        /// Gets the reflection depth.
        /// </summary>
        /// <returns></returns>
        protected int GetReflectionDepth()
        {
            if (settings == null)
                return DefaultDockSettings.ReflectionDepth;
            else
                return settings.ReflectionDepth;
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Detects if the point is located within the area of the icon
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool PointInItem(DockItem item, int x, int y)
        {
            Rectangle rect = new Rectangle(item.X, item.Y, (int)(item.Width * item.Scale), (int)(item.Height * item.Scale));
            return rect.Contains(x, y);
        }

        /// <summary>
        /// Gets the item at.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public DockItem GetItemAt(int x, int y)
        {
            for (int cnt = 0; cnt < items.Count; cnt++)
            {
                if (PointInItem(items[cnt], x, y))
                {
                    if (cnt < items.Count - 1)
                    {
                        if (PointInItem(items[cnt + 1], x, y))
                            return items[cnt + 1];
                        else
                            return items[cnt];
                    }
                    else
                        return items[cnt];
                }
            }
            return null;
        }

        /// <summary>
        /// Paints the specified canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public virtual void Paint(Graphics canvas)
        {
            IntPtr hdc = canvas.GetHdc();

            try
            {
                foreach (DockItem item in items)
                {
                    painter.Paint(hdc, item);
                }

            }
            finally
            {
                canvas.ReleaseHdc(hdc);
            }
        }

    }

}
