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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI.Dock
{

    [KarnaPurpose("Dock")]
    public class DockManager : CustomDockManager
    {

        [AccessedThroughProperty("ItemsLayout")]
        private ItemsLayout itemsLayout = ItemsLayout.Centered;
        [AccessedThroughProperty("DockOrientation")]
        private DockOrientation dockOrientation = DockOrientation.Horizontal;


        public override void UpdateDockInfo()
        {
            UpdateCoordinates(true);
        }

        [DefaultValue(ItemsLayout.Centered)]
        public ItemsLayout ItemsLayout
        {
            get { return itemsLayout; }
            set { itemsLayout = value; }
        }

        [DefaultValue(DockOrientation.Horizontal)]
        public DockOrientation DockOrientation
        {
            get { return dockOrientation; }
            set { dockOrientation = value; }
        }

        /// <summary>
        /// Updates all items coordinates to initial state based on the dock settings
        /// </summary>
        /// <param name="resetOrder">Reset items order to initial state</param>
        public virtual void UpdateCoordinates(bool resetOrder)
        {
            int startX = GetLeftMargin();
            int startY = GetTopMargin();
            int defSize = GetIconSize();
            int spacing = GetIconsSpacing();
            int reflection = GetReflectionDepth();
            int idx = 0;
            int availWidth = 0;
            int availHeight = 0;
            int total = items.Count;

            if (itemsLayout == ItemsLayout.Centered)
            {
                availWidth = Width / 2;
                availHeight = Height / 2;
            }

            foreach (DockItem item in items)
            {
                item.X = startX;
                item.Y = startY;
                item.ReflectionDepth = reflection;
                item.Width = defSize;
                item.Height = defSize + reflection;
                item.CenterX = startX + (defSize / 2);
                item.CenterY = startY + (defSize / 2); //without counting the reflection size
                item.Scale = 1.0f;
                if (UseAlpha)
                    item.Alpha = 100;
                else
                    item.Alpha = 0xFF;
                if (resetOrder)
                {
                    item.InitialIndex = idx;
                }

                if (DockOrientation == DockOrientation.Horizontal)
                {
                    if (itemsLayout == ItemsLayout.Centered)
                    {
                        item.X = (int)(availWidth + (item.InitialIndex - (total - 1) / 2) * (spacing + defSize) - (item.Width * item.Scale) / 2);
                        item.Y = (int)(availHeight - (item.Height * item.Scale) / 2);
                    }
                    else
                    {
                        item.Y = startY;
                        item.X = startX + item.InitialIndex * (spacing + defSize);
                    }
                }
                else
                {
                    if (itemsLayout == ItemsLayout.Centered)
                    {
                        item.Y = (int)(availHeight + (item.InitialIndex - (total - 1) / 2) * (spacing + defSize) - (item.Height * item.Scale) / 2);
                        item.X = (int)(availWidth - (item.Width * item.Scale) / 2);
                    }
                    else
                    {
                        item.X = startX;
                        item.Y = startY + item.InitialIndex * (spacing + defSize);
                    }
                }

                idx++;
            }

        }

        /// <summary>
        /// Updates all items coordinates to initial state based on the dock settings
        /// </summary>
        public virtual void UpdateCoordinates()
        {
            UpdateCoordinates(true);
        }

        public override void DoSizeChanged()
        {
            UpdateCoordinates(true);
        }

        public void DecreaseScale(float delta)
        {
            int startX = GetLeftMargin();
            int startY = GetTopMargin();
            int maxZoom = GetMaxZoom();
            int defSize = GetIconSize();
            int spacing = GetIconsSpacing();
            double maxScale = GetMaxScale();
            double multiplier = GetMultiplier();
            int total = items.Count;

            int availWidth = 0;
            int availHeight = 0;

            if (itemsLayout == ItemsLayout.Centered)
            {
                availWidth = Width / 2;
                availHeight = Height / 2;
            }

            foreach (DockItem item in items)
            {

                item.Scale -= delta;
                if (item.Scale < 1.0f)
                    item.Scale = 1.0f;

                item.Order = (int)(defSize * item.Scale);

                if (UseAlpha)
                {
                    item.Alpha = (byte)Math.Min(100 * item.Scale, 255);
                }
                else
                {
                    item.Alpha = 255;
                }

                if (DockOrientation == DockOrientation.Horizontal)
                {
                    if (itemsLayout == ItemsLayout.Centered)
                    {
                        item.X = (int)(availWidth + (item.InitialIndex - (total - 1) / 2) * (spacing + defSize) - (item.Width * item.Scale) / 2);
                        item.Y = (int)(availHeight - (item.Height * item.Scale) / 2);
                    }
                    else
                    {
                        item.Y = startY;
                        item.X = startX + item.InitialIndex * (spacing + defSize);
                    }
                }
                else
                {
                    if (itemsLayout == ItemsLayout.Centered)
                    {
                        item.Y = (int)(availHeight + (item.InitialIndex - (total - 1) / 2) * (spacing + defSize) - (item.Height * item.Scale) / 2);
                        item.X = (int)(availWidth - (item.Width * item.Scale) / 2);
                    }
                    else
                    {
                        item.X = startX;
                        item.Y = startY + item.InitialIndex * (spacing + defSize);
                    }
                }
            }

            items.Sort(new ZOrderComparer());
        }


        public override void DoMouseMove(int CurX, int CurY)
        {
            int startX = GetLeftMargin();
            int startY = GetTopMargin();
            int maxZoom = GetMaxZoom();
            int defSize = GetIconSize();
            int spacing = GetIconsSpacing();
            double maxScale = GetMaxScale();
            double multiplier = GetMultiplier();
            double imageScale;
            int total = items.Count;

            int availWidth = 0;
            int availHeight = 0;

            if (itemsLayout == ItemsLayout.Centered)
            {
                availWidth = Width / 2;
                availHeight = Height / 2;
            }

            foreach (DockItem item in items)
            {
                if (DockOrientation == DockOrientation.Horizontal)
                {
                    imageScale = maxScale - Math.Min(maxScale - 1, Math.Abs(CurX - ((double)item.X + (item.Width * item.Scale) / 2)) / multiplier);
                }
                else
                {
                    imageScale = maxScale - Math.Min(maxScale - 1, Math.Abs(CurY - ((double)item.Y + (item.Height * item.Scale) / 2)) / multiplier);
                }

                item.Scale = (float)imageScale;

                item.Order = (int)(defSize * imageScale);

                if (UseAlpha)
                {
                    item.Alpha = (byte)Math.Min(100 * imageScale, 255);
                }
                else
                {
                    item.Alpha = 255;
                }

                if (DockOrientation == DockOrientation.Horizontal)
                {
                    if (itemsLayout == ItemsLayout.Centered)
                    {
                        item.X = (int)(availWidth + (item.InitialIndex - (total - 1) / 2) * (spacing + defSize) - (item.Width * item.Scale) / 2);
                        item.Y = (int)(availHeight - (item.Height * item.Scale) / 2);
                    }
                    else
                    {
                        item.Y = startY;
                        item.X = startX + item.InitialIndex * (spacing + defSize);
                    }
                }
                else
                {
                    if (itemsLayout == ItemsLayout.Centered)
                    {
                        item.Y = (int)(availHeight + (item.InitialIndex - (total - 1) / 2) * (spacing + defSize) - (item.Height * item.Scale) / 2);
                        item.X = (int)(availWidth - (item.Width * item.Scale) / 2);
                    }
                    else
                    {
                        item.X = startX;
                        item.Y = startY + item.InitialIndex * (spacing + defSize);
                    }
                }
            }

            items.Sort(new ZOrderComparer());
        }


        protected double GetMultiplier()
        {
            return DefaultDockSettings.Multiplier;
        }

        protected double GetMaxScale()
        {
            return DefaultDockSettings.MaxScale;
        }
    }
}
