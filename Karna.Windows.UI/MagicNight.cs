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
using System.Drawing.Drawing2D;

namespace Karna.Windows.UI
{

    [ToolboxItem(true)]
    public class MagicNight : BufferedPanel
    {
        private List<MagicDot> Children = new List<MagicDot>();

        private int density = 50;   
        private double colorOffset = 128;

        private MinMaxValue swingRadius = new MinMaxValue(10, 25);
        private MinMaxValue swingSpeed = new MinMaxValue(2, 4);
        private MinMaxValue dotSize = new MinMaxValue(1.5, 3.5);
        private MinMaxValue upSpeed = new MinMaxValue(1, 2.5);

        public MagicNight()
        {
            swingRadius.Changed += new EventHandler(SettingsChanged);
            swingSpeed.Changed += new EventHandler(SettingsChanged);
            dotSize.Changed += new EventHandler(SettingsChanged);
            upSpeed.Changed += new EventHandler(SettingsChanged);

            addMagicDots(true);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecreateDots();
        }

        protected void SettingsChanged(object sender, EventArgs e)
        {
            RecreateDots();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MinMaxValue SwingRadius
        {
            get { return swingRadius; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MinMaxValue SwingSpeed
        {
            get { return swingSpeed; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MinMaxValue DotSize
        {
            get { return dotSize; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MinMaxValue UpSpeed
        {
            get { return upSpeed; }
        }


        protected void RecreateDots()
        {
            Children.Clear();
            addMagicDots(true);
        }

        [DefaultValue(128)]
        public double ColorOffset
        {
            get { return colorOffset; }
            set
            {
                colorOffset = value;
                RecreateDots();
            }
        }

        [DefaultValue(50)]
        public int Density
        {
            get { return density; }
            set
            {
                density = value;
                RecreateDots();
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            for (int cnt = 0; cnt < Children.Count; cnt++)
            {
                Children[cnt].Paint(e.Graphics);
            }
        }

        public void moveMagicDots()
        {
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                MagicDot magicDot = Children[i];
                magicDot.Run(); 

                if (magicDot.Y + dotSize.MaxValue < 0)
                {
                    Children.Remove(magicDot);
                }
            }
            addMagicDots(false);
        }

        public void addMagicDots(bool useRandom)
        {
            int seed = (int)DateTime.Now.Ticks;
            while (Children.Count < density)
            {
                seed += (int)DateTime.Now.Ticks;
                Random r = new Random(seed);

                double size = dotSize.MinValue + (dotSize.MaxValue - dotSize.MinValue) * r.NextDouble();
                byte red = (byte)(colorOffset + ((255 - colorOffset) * r.NextDouble()));
                byte green = (byte)(colorOffset + ((255 - colorOffset) * r.NextDouble()));
                byte blue = (byte)(colorOffset + ((255 - colorOffset) * r.NextDouble()));

                MagicDot magicDot = new MagicDot(red, green, blue, size);
                magicDot.X = this.Width * r.NextDouble();
                magicDot.Y = useRandom ? this.Height * r.NextDouble() : this.Height;
                magicDot.CenterX = magicDot.X;
                magicDot.UpSpeed = upSpeed.MinValue + (upSpeed.MaxValue - upSpeed.MinValue) * r.NextDouble();
                magicDot.SwingRadius = swingRadius.MinValue + (swingRadius.MaxValue - swingRadius.MaxValue) * r.NextDouble();
                magicDot.SwingSpeed = (int)(swingSpeed.MinValue + (swingSpeed.MaxValue - swingSpeed.MinValue) * r.NextDouble());
                magicDot.Counter = (int)(180 * r.NextDouble()); ;
                magicDot.Run();
                Children.Add(magicDot);
            }
            Invalidate();

        }


    }
}
