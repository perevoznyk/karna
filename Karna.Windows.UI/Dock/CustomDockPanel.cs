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
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI.Dock
{
    [ToolboxItem(false)]
    [KarnaPurpose("Dock")]
    public partial class CustomDockPanel : BufferedPanel
    {
        private CustomDockManager dockManager;
        private DockSettings settings;
        private int updateCount;

        public event EventHandler NewSettings;


        public CustomDockPanel()
        {
            InitializeComponent();
            dockManager = GetDockManager();
            settings = GetDockSettings();
            settings.Changed += new EventHandler(SettingsChanged);
            if (dockManager != null)
            {
                dockManager.Settings = settings;
            }
        }

        protected void SettingsChanged(object sender, EventArgs e)
        {
            Invalidate();
            if (NewSettings != null)
                NewSettings(this, e);
        }

        protected virtual CustomDockManager GetDockManager()
        {
            return null;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (dockManager != null)
            {
                dockManager.Paint(pe.Graphics);
            }
        }

        public CustomDockManager DockManager
        {
            get { return this.dockManager; }
        }

        protected virtual DockSettings GetDockSettings()
        {
            return new DockSettings();
        }

        public DockSettings DockSettings
        {
            get { return settings; }
        }

        public ImageList ImageList
        {
            get { return dockManager.ImageList; }
            set
            {
                if (dockManager != null)
                {
                    dockManager.ImageList = value;
                }
            }
        }


        public virtual Bitmap ProcessImage(Bitmap image)
        {
            Bitmap result;

            if (image == null)
                return null;

            if ((image.Width != settings.IconSize) || (image.Height != settings.IconSize))
                result = BitmapPainter.ResizeBitmap(image, settings.IconSize, settings.IconSize);
            else
                result = (Bitmap)image.Clone();

            if (settings.ReflectionDepth > 0)
                result = (Bitmap)BitmapPainter.CreateReflectionImage(result, settings.ReflectionDepth);

            return result;
        }

        public virtual Bitmap IconFromFileName(string fileName)
        {
            if (dockManager != null)
            {
                Bitmap result = dockManager.IconFromFileName(fileName);
                if (result != null)
                result = ProcessImage(result);
                return result;
            }
            else
                return null;
        }


        public virtual Bitmap IconFromImageList(int index)
        {
            if (dockManager != null)
            {
                Bitmap result = dockManager.IconFromImageList(index);
                if (result != null)
                    result = ProcessImage(result);
                return result;
            }
            else
                return null;

        }

        public virtual void ReloadImages()
        {
            foreach (DockItem item in dockManager.Items)
            {
                if (!string.IsNullOrEmpty(item.ImageName))
                    item.Icon = this.IconFromFileName(item.ImageName);
                else
                    if (item.ImageIndex >= 0)
                        item.Icon = this.IconFromImageList(item.ImageIndex);
                    
            }
        }

        public DockItem AddItem(string caption, string fileName)
        {
            DockItem item;
            if (dockManager != null)
            {
                item = new DockItem();
                item.ImageName = fileName;
                item.Caption = caption;
                item.Icon = this.IconFromFileName(fileName);
                dockManager.Items.Add(item);
                UpdateItems();
                return item;
            }
            else
                return null;
        }

        public DockItem AddItem(string caption, int imageIndex)
        {
            DockItem item;
            if (dockManager != null)
            {
                item = new DockItem();
                item.Caption = caption;
                item.ImageIndex = imageIndex;
                item.ImageList = dockManager.ImageList;
                item.Icon = this.IconFromImageList(imageIndex); 
                dockManager.Items.Add(item);
                UpdateItems();
                return item;
            }
            else
                return null;

        }

        public DockItem AddItem(string caption, Image icon)
        {
            DockItem item;
            if (dockManager != null)
            {
                item = new DockItem();
                item.Caption = caption;
                item.Icon = ProcessImage((Bitmap)icon);
                dockManager.Items.Add(item);
                UpdateItems();
                return item;
            }
            else
                return null;
        }

        public void RemoveItem(int index)
        {
            if (dockManager != null)
            {
                dockManager.Items.RemoveAt(index);
                UpdateItems();
            }
        }

        public void RemoveItem(DockItem item)
        {
            if (dockManager != null)
            {
                dockManager.Items.Remove(item);
                UpdateItems();
            }
        }

        public void Clear()
        {
            if (dockManager != null)
            {
                dockManager.Items.Clear();
                UpdateItems();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (dockManager != null)
            {
                dockManager.DoMouseMove(e.X, e.Y);
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (dockManager != null)
            {
                dockManager.DoMouseLeave();
                Invalidate();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (this.CanFocus)
                this.Focus();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (dockManager != null)
            {
                dockManager.Width = this.Width;
                dockManager.Height = this.Height;
                dockManager.DoSizeChanged();
                Invalidate();
            }
        }

        public void UpdateItems()
        {
            if (updateCount != 0)
                return;

            if (dockManager != null)
            {
                dockManager.DoSizeChanged();
                Invalidate();
            }
        }

        public void BeginUpdate()
        {
            updateCount++;
        }

        public void EndUpdate()
        {
            updateCount--;
            if (updateCount < 0)
                updateCount = 0;
            if (updateCount == 0)
            {
                UpdateItems();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (dockManager != null)
            {
                DockItem item = dockManager.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    //TODO: Add raise event here
                }
            }
        }

    }
}
