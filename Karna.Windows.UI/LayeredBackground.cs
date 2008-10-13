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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Drawing.Imaging;

namespace Karna.Windows.UI
{
    public class LayeredBackground : System.Windows.Forms.UserControl
    {
        #region COLOR MATRIX
        private static float[][] colorMatrixElements = { 
         new float[] {1,  0,  0,  0,   0},
         new float[] {0,  1,  0,  0,   0},
         new float[] {0,  0,  1,  0,   0},
         new float[] {0,  0,  0,  0.6f, 0},
         new float[] {0,  0,  0,  0,   1}};
        private ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
        #endregion

        private int transparency;
        private Bitmap layerImage;
        private Color layerColor;
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush semiTransBrush;
            TextureBrush imageBrush;
            if (this.layerImage == null)
            {
                byte r;
                byte g;
                byte b;

                r = layerColor.R;
                g = layerColor.G;
                b = layerColor.B;
                semiTransBrush = new SolidBrush(Color.FromArgb(transparency, r,g, b));
                e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
                e.Graphics.FillRectangle(semiTransBrush, 0, 0, Width, Height);
                e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), ClientRectangle);

            }
            else
            {
                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                imageBrush = new TextureBrush(layerImage, new Rectangle(0, 0, layerImage.Width, layerImage.Height));
                imageBrush.WrapMode = WrapMode.Tile;
                e.Graphics.FillRectangle(imageBrush, new Rectangle(0, 0, this.Width, this.Height));
            }
        }

        public LayeredBackground()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            transparency = 255;
            layerColor = System.Drawing.SystemColors.Control;
        }

        [Browsable(true)]
        public Color LayerColor
        {
            get
            {
                return layerColor;
            }
            set
            {
                layerColor = value;
                Invalidate();
            }
        }
        
        [Browsable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public Bitmap LayerImage
        {
            get
            {
                return layerImage;
            }
            set
            {
                layerImage = value;
                Invalidate();
            }
        }
                    

        public int Trancparency
        {
            get
            {
                return transparency;
            }
            set
            {
                if (transparency != value)
                {
                    if (value > 255)
                        transparency = 255;
                    else if (value < 0)
                        transparency = 0;
                    else
                        transparency = value;
                    colorMatrix[3, 3] = transparency / 255;
                    this.Invalidate();
                }
            }
        }
    }
}
