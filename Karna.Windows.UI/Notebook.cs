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
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Karna.Windows.UI
{

    public partial class Notebook : Control
    {
        private Color activeColor = Color.FromArgb(224, 232, 246);
        private Color selectedColor = Color.FromArgb(193, 210, 238);
        private Color lineColor = Color.Black;
        private int itemIndex = 0;
        private int activeItem = 0;
        private int itemHeight = 24;
        private NotebookCollection items = new NotebookCollection();
        private bool imagesVisible = true;
        private bool drawGradient = true;
        private byte gradientValue = 20;

        public event EventHandler ItemChange;

        protected virtual void HandleItemsChange()
        {
            Invalidate();
        }

        public Notebook()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            items.Changed += HandleItemsChange;
            items.Inserted += HandleItemsChange;
            items.Removed += HandleItemsChange;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NotebookCollection Items
        {
            get
            {
                return items;
            }
        }


        [DefaultValue(true)]
        public bool ImagesVisible
        {
            get
            {
                return imagesVisible;
            }
            set
            {
                imagesVisible = value;
                Invalidate();
            }
        }

        public bool DrawGradient
        {
            get
            {
                return drawGradient;
            }
            set
            {
                drawGradient = value;
                Invalidate();
            }
        }

        public byte GradientValue
        {
            get
            {
                return gradientValue;
            }
            set
            {
                gradientValue = value;
                Invalidate();
            }
        }

        public int GetItemAt(int y)
        {
            int result;
            result = y / itemHeight;
            if (result >= items.Count)
                result = -1;
            return result;
        }


        public Color ActiveColor
        {
            get
            {
                return activeColor;
            }
            set
            {
                activeColor = value;
                Invalidate();
            }
        }

        public Color SelectedColor
        {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
                Invalidate();
            }
        }

        public Color LineColor
        {
            get
            {
                return lineColor;
            }
            set
            {
                lineColor = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public int ActiveItem
        {
            get
            {
                return activeItem;
            }
        }

        public int ItemIndex
        {
            get
            {
                return itemIndex;
            }
            set
            {
                if (itemIndex != value)
                {
                    if (itemIndex < items.Count)
                    {
                        itemIndex = value;
                        activeItem = value;
                        Invalidate();
                    }
                }
            }
        }

        public int ItemHeight
        {
            get
            {
                return itemHeight;
            }
            set
            {
                itemHeight = value;
                Invalidate();
            }
        }

        protected Color GetItemColor(int index)
        {
            if (index == itemIndex)
                return selectedColor;
            else
                if (index == activeItem)
                    return activeColor;
                else
                    return BackColor;
        }

        protected virtual void DrawItem(int item, Graphics canvas)
        {
            Rectangle itemRect = new Rectangle(0, item * itemHeight,
                    this.Width, itemHeight);

            Rectangle imageRect = new Rectangle(2, itemRect.Top + 2, itemHeight - 4, itemHeight - 4);

            int lineBottom = (item + 1) * itemHeight - 1;
            if ((item > -1) && (item < items.Count))
            {

                if (drawGradient)
                {
                    LinearGradientBrush backBrush = new LinearGradientBrush(itemRect,
                        KarnaColors.Darker(GetItemColor(item), gradientValue),
                        KarnaColors.Lighter(GetItemColor(item), gradientValue), LinearGradientMode.Horizontal);
                    canvas.FillRectangle(backBrush, itemRect);
                }
                else
                {
                    SolidBrush backBrush = new SolidBrush(GetItemColor(item));
                    canvas.FillRectangle(backBrush, itemRect);
                }

                canvas.DrawLine(new Pen(lineColor), 0, lineBottom, this.Width, lineBottom);

                if (imagesVisible)
                {
                    if (items[item].Image != null)
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias; 
                        canvas.DrawImage(items[item].Image, imageRect);
                    }
                }

                if (imagesVisible)
                {
                    itemRect.Offset(itemHeight, 0);
                }

                canvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;
                canvas.DrawString(items[item].Text, this.Font, new SolidBrush(ForeColor), itemRect, format);

            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            for (int cnt = 0; cnt < items.Count; cnt++)
            {
                DrawItem(cnt, pe.Graphics);
            }
        }

        protected bool MouseInBouds(Point p)
        {
            return ((p.Y <= items.Count * itemHeight - 1));
        }

        protected virtual void OnItemChange(EventArgs e)
        {
            if (ItemChange != null)
                ItemChange(this, e);
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int oldSelection;
            int newSelection;

            newSelection = GetItemAt(e.Y);

            base.OnMouseClick(e);
            if ( newSelection != -1)
            {
                oldSelection = itemIndex;
                itemIndex = newSelection;

                if (oldSelection != itemIndex)
                {
                    OnItemChange(EventArgs.Empty);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int overIndex;

            base.OnMouseMove(e);

            Point mousePoint = new Point(e.X, e.Y);
            if (MouseInBouds(mousePoint))
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }

            overIndex = GetItemAt(e.Y);

            if (overIndex != activeItem)
            {
                if (MouseInBouds(mousePoint))
                {
                    activeItem = overIndex;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            activeItem = itemIndex;
            Invalidate();
            base.OnMouseLeave(e);
        }
    }
}
