//===============================================================================
// Copyright © Serhiy Perevoznyk.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================


using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Karna.Windows.UI.Dock
{
    public enum ItemsLayout { Default, Centered }
    public enum DockOrientation { Horizontal, Vertical }


    /// <summary>
    /// Information about Dock Item
    /// </summary>
    public interface IDockInformation
    {
    }

    public abstract class DockInformation : IDockInformation
    {

    }

    /// <summary>
    /// Dock Item interface
    /// </summary>
    public interface IDockItem
    {
        Image Icon { get; set; }
        string Hint { get; set;}
        string Caption { get; set;}
        IDockInformation Info { get; }
    }

    /// <summary>
    /// This class is a placeholder for all information concerning the dock item drawing.
    /// The information is quite generic and used by all dock types implemented in Karna library.
    /// </summary>
    public class DockItem : IDockItem, IDisposable
    {
        private string imageName;
        private string hint;
        private int x = 0;
        private int y = 0;
        private int width;
        private int height;
        private int centerX;
        private int centerY;
        private string id;
        private int tag = 0;
        private int order = 0;
        private Single angle = 0;
        private float scale = 1.0f;
        private byte alpha = 0xFF;
        private int reflectionDepth = DefaultDockSettings.ReflectionDepth;
        private IntPtr srcBitmap = IntPtr.Zero;
        private bool disposed = false;
        private DockInformation information;
        private int initialIndex;
        private Image icon;
        private bool keepNativeBitmap = false;
        [AccessedThroughProperty("Caption")]
        private string caption;
        [AccessedThroughProperty("ImageIndex")]
        private int imageIndex = -1;
        [AccessedThroughProperty("ImageList")]
        private ImageList imageList;

        /// <summary>
        /// Disposes the Windows native bitmap
        /// </summary>
        protected virtual void DisposeBitmap()
        {
            if (srcBitmap != IntPtr.Zero)
            {
                try
                {
                    NativeMethods.DeleteObject(srcBitmap);
                }
                finally
                {
                    srcBitmap = IntPtr.Zero;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [DefaultValue(false)]
        public bool KeepNativeBitmap
        {
            get
            {
                return keepNativeBitmap;
            }
            set
            {
                if (keepNativeBitmap != value)
                {
                    keepNativeBitmap = value;
                    DisposeBitmap();
                    if (value && (icon != null))
                    {
                        srcBitmap = BitmapPainter.GetNativeBitmap((Bitmap)icon);
                    }
                }
            }
        }

        public IDockInformation Info
        {
            get { return information; }
        }

        public ImageList ImageList
        {
            get { return imageList; }
            set { imageList = value; }
        }

        public void SetDockInfo(DockInformation info)
        {
            this.information = info;
        }

        [DefaultValue(-1)]
        public int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }

        protected Image GetImageFromBitmap()
        {
            if (srcBitmap == IntPtr.Zero)
                return null;
            else
                return Bitmap.FromHbitmap(srcBitmap);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //clear managed part
                    if (icon != null)
                    {
                        icon.Dispose();
                    }
                }
                //clear unmanaged resourses
                DisposeBitmap();
            }
            disposed = true;
        }


        ~DockItem()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public Image Icon
        {
            get { return icon; }

            set
            {
                if (icon != value)
                {
                    icon = value;
                    if (keepNativeBitmap)
                    {
                        DisposeBitmap();
                        if (icon != null)
                        {
                            srcBitmap = BitmapPainter.GetNativeBitmap((Bitmap)icon);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the native bitmap.
        /// </summary>
        /// <value>The native bitmap.</value>
        public IntPtr NativeBitmap
        {
            get { return BitmapPainter.GetNativeBitmap((Bitmap)icon); }
        }

        /// <summary>
        /// Gets or sets the hint.
        /// </summary>
        /// <value>The hint.</value>
        public string Hint
        {
            get { return hint; }
            set { hint = value; }
        }

        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the image.
        /// </summary>
        /// <value>The X.</value>
        [DefaultValue(0)]
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the image.
        /// </summary>
        /// <value>The Y.</value>
        [DefaultValue(0)]
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Gets or sets the width of the image.
        /// Be aware: The DockItem class does not check the value of the provided image size,
        /// in case if size is smaller than the real size of the image, only part of the image will be drawn.
        /// In case if Reflection is used the total height of the real image will be the height value
        /// plus the height of the reflection.
        /// </summary>
        /// <value>The width of the image.</value>
        [DefaultValue(0)]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Gets or sets the height of the image.
        /// Be aware: The DockItem class does not check the value of the provided image size,
        /// in case if size is smaller than the real size of the image, only part of the image will be drawn.
        /// In case if Reflection is used the total height of the real image will be the height value
        /// plus the height of the reflection.
        /// </summary>
        /// <value>The height.</value>
        [DefaultValue(0)]
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Sets the size of the image.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Sets the center point of the image.
        /// </summary>
        /// <param name="centerX">The center X coordinate.</param>
        /// <param name="centerY">The center Y coordinate.</param>
        public void SetCenter(int centerX, int centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
        }

        public int CenterX
        {
            get { return centerX; }
            set { centerX = value; }
        }

        public int CenterY
        {
            get { return centerY; }
            set { centerY = value; }
        }

        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        [DefaultValue(0)]
        public int Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public Single Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public byte Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public int ReflectionDepth
        {
            get { return reflectionDepth; }
            set { reflectionDepth = value; }
        }

        public int InitialIndex
        {
            get { return initialIndex; }
            set { initialIndex = value; }
        }

    }
    public class ZOrderComparer : IComparer<DockItem>
    {
        public int Compare(DockItem x, DockItem y)
        {
            if ((x == null) && (y == null))
                return 0;


            return x.Order - y.Order;
        }
    }
}
