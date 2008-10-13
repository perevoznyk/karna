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

namespace Karna.Windows.UI
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MinMaxValue
    {
        private double minValue;
        private double maxValue;
        public EventHandler Changed;

        public MinMaxValue(double min, double max)
        {
            this.minValue = min;
            this.maxValue = max;
        }

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        protected virtual void DoChange()
        {
            OnChanged(EventArgs.Empty);
        }

        public double MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                DoChange();
            }
        }

        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                DoChange();
            }
        }

        public override string ToString()
        {
            return minValue.ToString() + ", " + maxValue.ToString();
        }
    }

    public class MagicEllipse
    {
        public double Width;
        public double Height;
        public SolidBrush Fill;
        public double X;
        public double Y;
    }

    public class MagicDot
    {
        private int ellipseCount = 5;
        private double opacity = 0.6;
        private double opacityInc = -0.15;

        public double swingRadius;
        private int counter = 0;
        public int swingSpeed = 5;
        public double upSpeed = 1;
        private double centerX;
        private Color color;
        private double size;


        private double x;
        private double y;

        private List<MagicEllipse> Children = new List<MagicEllipse>();

        public double Size
        {
            get { return size; }
            set { size = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public double CenterX
        {
            get { return centerX; }
            set { centerX = value; }
        }

        public double UpSpeed
        {
            get { return upSpeed; }
            set { upSpeed = value; }
        }

        public double SwingRadius
        {
            get { return swingRadius; }
            set { swingRadius = value; }
        }

        public int SwingSpeed
        {
            get { return swingSpeed; }
            set { swingSpeed = value; }
        }

        public int Counter
        {
            get { return counter; }
            set { counter = value; }
        }

        public MagicDot(byte red, byte green, byte blue, double size)
        {
            this.color = Color.FromArgb(red, green, blue);
            this.size = size;
            CreateEllipses();
        }

        public MagicDot(Color color, double size)
        {
            this.color = color;
            this.size = size;
            CreateEllipses();
        }

        protected void RecreateEllipses()
        {
            Children.Clear();
            CreateEllipses();
        }

        protected void CreateEllipses()
        {
            double opac = opacity;
            byte red = color.R;
            byte green = color.G;
            byte blue = color.B;

            for (int i = 0; i < ellipseCount; i++)
            {
                MagicEllipse ellipse = new MagicEllipse();
                ellipse.Width = size;
                ellipse.Height = size;
                if (i == 0)
                {
                    ellipse.Fill = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
                }
                else
                {
                    ellipse.Fill = new SolidBrush(Color.FromArgb((int)(opac * 255), red, green, blue));
                    opac += opacityInc;
                    size += size;
                }

                ellipse.X = -ellipse.Width / 2;
                ellipse.Y = -ellipse.Height / 2;
                this.Children.Add(ellipse);
            }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public void Run()
        {
            double angle = counter / 180.0 * Math.PI;
            Y = Y - upSpeed;
            X = centerX + Math.Cos(angle) * swingRadius;
            counter += swingSpeed;
        }

        public void Paint(Graphics canvas)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                canvas.FillEllipse(Children[i].Fill, (float)(X + Children[i].X), (float)(Y + Children[i].Y), (float)Children[i].Width, (float)Children[i].Height);
            }
        }
    }
}
