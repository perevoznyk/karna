//===============================================================================
// Copyright © Serhiy Perevoznyk.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

// This code is based on Delphi Unit created by Kambiz R. Khojasteh

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Karna.Windows.UI.Attributes;


namespace Karna.Windows.UI
{

    public enum GradientStyle
    {
        RadialCentral, RadialTop, RadialBottom, RadialLeft,
        RadialRight, RadialTopLeft, RadialTopRight, RadialBottomLeft,
        RadialBottomRight, LinearHorizontal, LinearVertical,
        ReflectedHorizontal, ReflectedVertical, DiagonalLinearForward,
        DiagonalLinearBackward, DiagonalReflectedForward,
        DiagonalReflectedBackward, ArrowLelf, ArrowRight, ArrowUp,
        ArrowDown, Diamond, Butterfly
    };


    [TypeConverter(typeof(ExpandableObjectConverter))]
    [KarnaPurpose("Gradient")]
    public class GradientFill
    {
        private Color colorBegin = Color.White;
        private Color colorEnd = Color.SkyBlue;
        private GradientStyle gradientStyle;
        private sbyte shift = 0;
        private sbyte rotation = 0;
        private bool reverse = false;
        private Bitmap pattern = new Bitmap(100, 100, PixelFormat.Format32bppArgb);
        private Bitmap tmpbmp;
        private int updateCount = 0;
        private int height = 100;
        private int width = 100;
        private bool visible = true;
        private RGBTripple[] colors = new RGBTripple[256];

        private void SetColorBegin(Color color)
        {
            if (colorBegin != color)
            {
                colorBegin = color;
                UpdatePattern();
            }
        }

        private void SetColorEnd(Color color)
        {
            if (colorEnd != color)
            {
                colorEnd = color;
                UpdatePattern();
            }
        }

        private void SetStyle(GradientStyle style)
        {
            if (gradientStyle != style)
            {
                gradientStyle = style;
                UpdatePattern();
            }
        }

        private void SetShift(sbyte shiftValue)
        {
            if (shiftValue < -100)
            {
                shiftValue = -100;
            }
            else
                if (shiftValue > 100)
                {
                    shiftValue = 100;
                }

            if (this.shift != shiftValue)
            {
                shift = shiftValue;
                UpdatePattern();
            }
        }

        private void SetRotation(sbyte rotationValue)
        {
            if (rotationValue < -100)
            {
                rotationValue = -100;
            }
            else
                if (rotationValue > 100)
                {
                    rotationValue = 100;
                }

            if (rotation != rotationValue)
            {
                rotation = rotationValue;
                UpdatePattern();
            }
        }

        private void SetReverse(bool reverseValue)
        {
            if (reverse != reverseValue)
            {
                reverse = reverseValue;
                UpdatePattern();
            }
        }

        private void SetVisible(bool visibleValue)
        {
            if (visible != visibleValue)
            {
                visible = visibleValue;
                UpdatePattern();
            }
        }

        /// <summary>
        /// Gets the Red value.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        private byte GetRValue(Color color)
        {
            return color.R;
        }

        /// <summary>
        /// Gets the Green value.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        private byte GetGValue(Color color)
        {
            return color.G;
        }

        /// <summary>
        /// Gets the Blue value.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        private byte GetBValue(Color color)
        {
            return color.B;
        }



        /// <summary>
        /// Updates the pattern.
        /// </summary>
        protected virtual void UpdatePattern()
        {
            int dRed;
            int dGreen;
            int dBlue;

            Color RGBColor1;
            Color RGBColor2;

            RGBTripple RGB1;
            RGBTripple RGB2;


            int index;

            int M;



            if (Reverse)
            {
                RGBColor1 = ColorEnd;
                RGBColor2 = ColorBegin;
            }
            else
            {
                RGBColor1 = ColorBegin;
                RGBColor2 = ColorEnd;
            }

            RGB1.rgbtRed = GetRValue(RGBColor1);
            RGB1.rgbtGreen = GetGValue(RGBColor1);
            RGB1.rgbtBlue = GetBValue(RGBColor1);

            RGB2.rgbtRed = GetRValue(RGBColor2);
            RGB2.rgbtGreen = GetGValue(RGBColor2);
            RGB2.rgbtBlue = GetBValue(RGBColor2);

            if (Shift > 0)
            {
                RGB1.rgbtRed += (byte)KarnaMath.MulDiv(RGB2.rgbtRed - RGB1.rgbtRed, Shift, 100);
                RGB1.rgbtGreen += (byte)KarnaMath.MulDiv(RGB2.rgbtGreen - RGB1.rgbtGreen, Shift, 100);
                RGB1.rgbtBlue += (byte)KarnaMath.MulDiv(RGB2.rgbtBlue - RGB1.rgbtBlue, Shift, 100);
            }
            else
                if (Shift < 0)
                {
                    RGB2.rgbtRed += (byte)KarnaMath.MulDiv(RGB2.rgbtRed - RGB1.rgbtRed, Shift, 100);
                    RGB2.rgbtGreen += (byte)KarnaMath.MulDiv(RGB2.rgbtGreen - RGB1.rgbtGreen, Shift, 100);
                    RGB2.rgbtBlue += (byte)KarnaMath.MulDiv(RGB2.rgbtBlue - RGB1.rgbtBlue, Shift, 100);
                }


            dRed = RGB2.rgbtRed - RGB1.rgbtRed;
            dGreen = RGB2.rgbtGreen - RGB1.rgbtGreen;
            dBlue = RGB2.rgbtBlue - RGB1.rgbtBlue;

            M = KarnaMath.MulDiv(255, Rotation, 100);

            if (M == 0)
            {
                for (index = 0; index < 256; index++)
                {
                    colors[index].rgbtRed = (byte)(RGB1.rgbtRed + (index * dRed) / 255);
                    colors[index].rgbtGreen = (byte)(RGB1.rgbtGreen + (index * dGreen) / 255);
                    colors[index].rgbtBlue = (byte)(RGB1.rgbtBlue + (index * dBlue) / 255);
                }
            }
            else if (M > 0)
            {
                M = 255 - M;
                for (index = 0; index < M; index++)
                {
                    colors[index].rgbtRed = (byte)(RGB1.rgbtRed + (index * dRed) / M);
                    colors[index].rgbtGreen = (byte)(RGB1.rgbtGreen + (index * dGreen) / M);
                    colors[index].rgbtBlue = (byte)(RGB1.rgbtBlue + (index * dBlue) / M);
                }

                for (index = M; index < 256; index++)
                {
                    colors[index].rgbtRed = (byte)(RGB1.rgbtRed + ((255 - index) * dRed) / (255 - M));
                    colors[index].rgbtGreen = (byte)(RGB1.rgbtGreen + ((255 - index) * dGreen) / (255 - M));
                    colors[index].rgbtBlue = (byte)(RGB1.rgbtBlue + ((255 - index) * dBlue) / (255 - M));
                }

            }
            else if (M < 0)
            {
                M = -M;
                for (index = 0; index < M + 1; index++)
                {
                    colors[index].rgbtRed = (byte)(RGB2.rgbtRed - (index * dRed) / M);
                    colors[index].rgbtGreen = (byte)(RGB2.rgbtGreen - (index * dGreen) / M);
                    colors[index].rgbtBlue = (byte)(RGB2.rgbtBlue - (index * dBlue) / M);
                }

                for (index = (M + 1); index < 256; index++)
                {
                    colors[index].rgbtRed = (byte)(RGB2.rgbtRed - ((255 - index) * dRed) / (255 - M));
                    colors[index].rgbtGreen = (byte)(RGB2.rgbtGreen - ((255 - index) * dGreen) / (255 - M));
                    colors[index].rgbtBlue = (byte)(RGB2.rgbtBlue - ((255 - index) * dBlue) / (255 - M));
                }
            }



            switch (gradientStyle)
            {
                case GradientStyle.RadialCentral:
                    {
                        PatternBuilder.RadialCentral(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.ArrowDown:
                    {
                        PatternBuilder.ArrowDown(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.ArrowLelf:
                    {
                        PatternBuilder.ArrowLeft(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.ArrowRight:
                    {
                        PatternBuilder.ArrowRight(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.ArrowUp:
                    {
                        PatternBuilder.ArrowUp(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.Butterfly:
                    {
                        PatternBuilder.Butterfly(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.DiagonalLinearBackward:
                    {
                        PatternBuilder.DiagonalLinearBackward(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.DiagonalLinearForward:
                    {
                        PatternBuilder.DiagonalLinearForward(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.DiagonalReflectedBackward:
                    {
                        PatternBuilder.DiagonalReflectedBackward(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.DiagonalReflectedForward:
                    {
                        PatternBuilder.DiagonalReflectedForward(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.Diamond:
                    {
                        PatternBuilder.Diamond(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.LinearHorizontal:
                    {
                        PatternBuilder.LinearHorizontal(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.LinearVertical:
                    {
                        PatternBuilder.LinearVertical(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.RadialBottom:
                    {
                        PatternBuilder.RadialBottom(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.RadialBottomLeft:
                    {
                        PatternBuilder.RadialBottomLeft(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.RadialBottomRight:
                    {
                        PatternBuilder.RadialBottomRight(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.RadialLeft:
                    {
                        PatternBuilder.RadialLeft(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.RadialRight:
                    {
                        PatternBuilder.RadialRight(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.RadialTop:
                    {
                        PatternBuilder.RadialTop(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.RadialTopLeft:
                    {
                        PatternBuilder.RadialTopLeft(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.RadialTopRight:
                    {
                        PatternBuilder.RadialTopRight(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.ReflectedHorizontal:
                    {
                        PatternBuilder.ReflectedHorizontal(colors, ref tmpbmp);
                        break;
                    }
                case GradientStyle.ReflectedVertical:
                    {
                        PatternBuilder.ReflectedVertical(colors, ref tmpbmp);
                        break;
                    }
            }


            BitmapPainter.StretchDraw(tmpbmp, ref pattern);

            DoChange();
        }


        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        [DefaultValue(100), Browsable(true)]
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    height = value;
                    pattern.Dispose();
                    GC.SuppressFinalize(this.pattern);
                    pattern = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                }
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        [DefaultValue(100), Browsable(true)]
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width != value)
                {
                    width = value;
                    pattern.Dispose();
                    GC.SuppressFinalize(this.pattern);
                    pattern = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                }
            }
        }

        [Browsable(true)]
        public Color ColorBegin
        {
            get
            {
                return colorBegin;
            }
            set
            {
                SetColorBegin(value);
            }
        }

        [Browsable(true)]
        public Color ColorEnd
        {
            get
            {
                return colorEnd;
            }
            set
            {
                SetColorEnd(value);
            }
        }

        [DefaultValue(false), Browsable(true)]
        public bool Reverse
        {
            get
            {
                return reverse;
            }
            set
            {
                SetReverse(value);
            }
        }

        [DefaultValue(0), Browsable(true)]
        public sbyte Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                SetRotation(value);
            }
        }

        [DefaultValue(0), Browsable(true)]
        public sbyte Shift
        {
            get
            {
                return shift;
            }
            set
            {
                SetShift(value);
            }
        }

        [Browsable(true)]
        public GradientStyle Style
        {
            get
            {
                return gradientStyle;
            }
            set
            {
                SetStyle(value);
            }
        }

        [DefaultValue(true), Browsable(true)]
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                SetVisible(value);
            }
        }

        /// <summary>
        /// Invalidates the pattern.
        /// </summary>
        public void InvalidatePattern()
        {
            UpdatePattern();
        }

        public void BeginUpdate()
        {
            updateCount++;
        }

        public void EndUpdate()
        {
            updateCount--;
            if (updateCount == 0)
            {
                UpdatePattern();
            }
        }

        public event EventHandler Changed;

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

        [Browsable(true)]
        public Bitmap Pattern
        {
            get
            {
                return pattern;
            }
        }


    }
}
