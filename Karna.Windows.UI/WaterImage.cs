//===============================================================================
// Copyright © Serhiy Perevoznyk.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Karna.Windows.UI
{

    [Designer(typeof(Karna.Windows.UI.Design.WaterImageDesigner))]
    [ToolboxBitmap(typeof(ToolboxImages), "Karna.Windows.UI.Images.WaterImage.bmp")]
    public partial class WaterImage : Control
    {
        private Bitmap image;
        private Bitmap reflectionImage;
        private int reflection = 150;
        private Color transparentColor = Color.Magenta; 
        private bool useTransparentColor = true;

        public WaterImage()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        public WaterImage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        [DefaultValue(true)]        
        protected void UpdatedImageTransparency()
        {
            if (image != null)
            {
                Color clr; 
                int w = image.Width;
                int h = image.Height;

                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        clr = image.GetPixel(x, y);
                        if (clr.ToArgb() == transparentColor.ToArgb())
                        {
                            image.SetPixel(x, y, Color.FromArgb(0, clr));
                        }
                    }
                }
            }
        }

        public bool UseTransparentColor
        {
            get
            {
                return useTransparentColor;
            }
            set
            {
                useTransparentColor = value;
                UpdateReflectionImage();
                Invalidate();
            }
        }

        protected void UpdateReflectionImage()
        {
            int w;
            int h;
            float opaque;
            int alpha;
            int newAlpha;
            float frac;

            Color clr;
            Bitmap rotatedImage = new Bitmap(image);

            if (reflectionImage != null)
            {
                reflectionImage.Dispose();
            }

            if (image == null)
                return;

            w = image.Width;
            h = image.Height;

            rotatedImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

            reflectionImage = new Bitmap(w, h, PixelFormat.Format32bppArgb);

            for (int y = 0; y < h; y++)
            {
                opaque = ((255.0f / h) * (h - y)) - reflection;
                if (opaque < 0.0)
                    opaque = 0.0f;
                if (opaque > 255.0)
                    opaque = 255.0f;

                for (int x = 0; x < w; x++)
                {
                    clr = rotatedImage.GetPixel(x, y);
                    if (KarnaColors.SameColors(clr,transparentColor))
                    {
                        if (useTransparentColor)
                        {
                            alpha = 0;
                        }
                        else
                        {
                            alpha = clr.A;
                        }
                    }
                    else
                    {
                        alpha = clr.A;
                    }
                    if (alpha == 0)
                        continue;
                    frac = (float) (opaque / 255);
                    newAlpha = (int)(frac * alpha);
                    reflectionImage.SetPixel(x, y, Color.FromArgb(newAlpha, clr));
                }
            }
        }

        public Color TransparentColor
        {
            get
            {
                return transparentColor;
            }
            set
            {
                transparentColor = value;
                UpdateReflectionImage();
                Invalidate();
            }
        }

        public Bitmap Image
        {
            get
            {
                return image;
            }
            set
            {
                Bitmap realImage = null;
                Graphics canvas;
                if (value != null)
                {
                    realImage = value;
                    if (image != null)
                        image.Dispose();

                    ImageAttributes attributes = new ImageAttributes();
                    image = new Bitmap(realImage.Width, realImage.Height, PixelFormat.Format32bppArgb);
                    canvas = Graphics.FromImage(image);
                    canvas.DrawImage(realImage,  new Rectangle(0, 0, realImage.Width, realImage.Height), 0, 0, realImage.Width, realImage.Height, GraphicsUnit.Pixel, attributes);
                }
                else
                    image = null;
                UpdateReflectionImage();
                Invalidate();
            }
        }

        [DefaultValue(150)]
        public int Reflection
        {
            get
            {
                return reflection;
            }
            set
            {
                if (reflection != value)
                {
                    if (value < 0)
                    {
                        reflection = 0;
                    }
                    else
                        if (value > 255)
                        {
                            reflection = 255;
                        }
                        else
                        {
                            reflection = value;
                        }
                    UpdateReflectionImage();
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (reflectionImage != null)
            {
                pe.Graphics.DrawImageUnscaled(reflectionImage, 0, reflectionImage.Height, 0, 0);
                ImageAttributes attributes = new ImageAttributes();
                if (useTransparentColor)
                {
                    attributes.SetColorKey(transparentColor, transparentColor);
                }
                    pe.Graphics.DrawImage(image,  new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
        }

    }
}
