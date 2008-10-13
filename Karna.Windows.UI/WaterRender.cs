//===============================================================================
// Copyright © Serhiy Perevoznyk.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

// The formulas used in this class can be found on website                   
// http://freespace.virgin.net/hugo.elias/graphics/x_water.htm               

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.ComponentModel;


namespace Karna.Windows.UI
{
    /// <summary>
    /// Renders water drops on the given surface
    /// </summary>
    public class WaterRender
    {
        private int[,] buffer1;
        private int[,] buffer2;
        private int width;
        private int height;
        private double damping = 0.8;
        private double lightBreak = 40.0;
        private byte maxAlpha = 160;
        private bool globalTransparency = true;

        private Random randomizer = new Random();

        public WaterRender(int width, int height)
        {
            this.width = width;
            this.height = height;
            buffer1 = new int[width, height];
            buffer2 = new int[width, height];
        }

        [DefaultValue(0.8)]
        public double Dumping
        {
            get
            {
                return damping;
            }
            set
            {
                if (value < 0)
                    damping = 0;
                else
                    if (value > 1.0)
                        damping = 1.0;
                    else
                        damping = value;
            }
        }

        [DefaultValue(160)]
        public byte MaxAlpha
        {
            get
            {
                return maxAlpha;
            }
            set
            {
                maxAlpha = value;
            }
        }

        [DefaultValue(40.0)]
        public double LightBreak
        {
            get
            {
                return lightBreak;
            }
            set
            {
                if (value < 1.0)
                    lightBreak = 1.0;
                else
                    lightBreak = value;
            }
        }

        [DefaultValue(true)]
        public bool GlobalTransparency
        {
            get
            {
                return globalTransparency;
            }

            set
            {
                globalTransparency = value;
            }
        }

        public void Step()
        {
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    buffer2[x, y] =
                        ((buffer1[x - 1, y] +
                        buffer1[x + 1, y] +
                        buffer1[x, y + 1] +
                        buffer1[x, y - 1]) >> 1) -
                        buffer2[x, y];

                    buffer2[x, y] = (int)((double)buffer2[x, y] * damping);
                }
            }

            for (int x = 0; x < width; x++)
            {
                buffer2[x, 0] = 0;
                buffer2[x, height - 1] = 0;
            }

            for (int y = 0; y < height; y++)
            {
                buffer2[0, y] = 0;
                buffer2[width - 1, y] = 0;
            }

            object o = buffer1;
            buffer1 = buffer2;
            buffer2 = (int[,])o;
        }


        public void Display(Bitmap surface, Bitmap texture)
        {
            int numBytes = width * height * 4;
            int scan;
            int scanNew;

            if (surface == null)
                return;

            if (texture == null)
                return;

            BitmapData tmpData = surface.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData bmpData = texture.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte[] tmpBytes = new Byte[numBytes];
            byte[] bmpBytes = new Byte[numBytes];

            Marshal.Copy(tmpData.Scan0, tmpBytes, 0, numBytes);
            Marshal.Copy(bmpData.Scan0, bmpBytes, 0, numBytes);

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int xoffset = buffer1[x - 1, y] - buffer1[x + 1, y];
                    int yoffset = buffer1[x, y - 1] - buffer1[x, y + 1];

                    int shading = (xoffset - yoffset) / 2;

                    xoffset = (int)(xoffset / lightBreak);
                    yoffset = (int)(yoffset / lightBreak);

                    int xnew = x + xoffset;
                    if (xnew < 0) xnew = 0;
                    if (xnew >= width) xnew = width - 1;

                    int ynew = y + yoffset;
                    if (ynew < 0) ynew = 0;
                    if (ynew >= height) ynew = height - 1;


                    if (shading < 0)
                    {
                        shading = 0;
                    }
                    else
                        if (shading > 255)
                        {
                            shading = 255;
                        }


                    byte alpha = (byte)(255 - xoffset);

                    if (alpha > maxAlpha)
                    {
                        alpha = maxAlpha;
                    }

                    if (!globalTransparency)
                    {
                        if ((xoffset == 0) && (yoffset == 0))
                        {
                            alpha = 255;
                        }
                    }
                    
                    scan = 4 * (x + y * width);
                    scanNew = 4 * (xnew + ynew * width);

                    tmpBytes[scan]     = KarnaMath.MaxByte(bmpBytes[scanNew] + shading);
                    tmpBytes[scan + 1] = KarnaMath.MaxByte(bmpBytes[scanNew + 1] + shading);
                    tmpBytes[scan + 2] = KarnaMath.MaxByte(bmpBytes[scanNew + 2] + shading);
                    tmpBytes[scan + 3] = alpha;


                }
            }

            Marshal.Copy(tmpBytes, 0, tmpData.Scan0, numBytes);
            surface.UnlockBits(tmpData);
            texture.UnlockBits(bmpData);

        }


        public void Splash(int cx, int cy, int rippleRadius, int waveHeight)
        {
            int rQuad;

            if ((cx < 0) || (cx > width - 1))
            {
                cx = 1 + rippleRadius + randomizer.Next(0x7FFF) / (width - 2 * rippleRadius - 1);
            }

            if ((cy < 0) || (cy > height - 1))
            {
                cy = 1 + rippleRadius + randomizer.Next(0x7FFF) / (height - 2 * rippleRadius - 1);
            }

            int sy = cy - rippleRadius;
            if (sy < 0)
            {
                sy = 0;
            }

            int ey = cy + rippleRadius;

            if (ey > height)
            {
                ey = height;
            }

            int sx = cx - rippleRadius;

            if (sx < 0)
            {
                sx = 0;
            }

            int ex = cx + rippleRadius;

            if (ex > width)
            {
                ex = width;
            }

            rQuad = rippleRadius * rippleRadius;

            for (int y = sy; y < ey; y++)
            {
                for (int x = sx; x < ex; x++)
                {
                    int distM2 = (x - cx) * (x - cx) + (y - cy) * (y - cy);
                    if (distM2 < rQuad)
                    {
                        if (waveHeight == 0)
                        {
                            buffer1[x, y] += 255 - (int)(512 - distM2 / (rQuad));
                        }
                        else
                        {
                            buffer1[x, y] += waveHeight;
                        }
                    }
                }
            }

        }
    }



}
