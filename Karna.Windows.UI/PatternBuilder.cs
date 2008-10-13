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

namespace Karna.Windows.UI
{
    /// <summary>
    /// Pattern builder
    /// </summary>
    internal static class PatternBuilder
    {
        public static void RadialCentral(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            int PreCalcX;
            int PreCalcY;
            int[] PreCalcXs = new int[362];

            for (x = 0; x < 181; x++)
            {
                PreCalcX = 180 - x;
                PreCalcXs[x] = PreCalcX * PreCalcX;
            }

            for (x = 181; x < 362; x++)
            {
                PreCalcX = x - 181;
                PreCalcXs[x] = PreCalcX * PreCalcX;
            }

            BitmapData bmpData;
            pattern = new Bitmap(362, 362, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 362; y++)
            {
                PreCalcY = 180 - y;
                PreCalcY = PreCalcY * PreCalcY;
                for (x = 0; x < 362; x++)
                {
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }

            #region Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
            #endregion
        }

        public static void RadialTop(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            int PreCalcX;
            int PreCalcY;
            int[] PreCalcXs = new int[362];

            for (x = 0; x < 181; x++)
            {
                PreCalcX = 180 - x;
                PreCalcXs[x] = PreCalcX * PreCalcX;
            }

            for (x = 181; x < 362; x++)
            {
                PreCalcX = x - 181;
                PreCalcXs[x] = PreCalcX * PreCalcX;
            }

            BitmapData bmpData;
            pattern = new Bitmap(362, 181, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 181; y++)
            {
                PreCalcY = y * y;
                for (x = 0; x < 362; x++)
                {
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }

            #region Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
            #endregion
        }

        public static void RadialBottom(RGBTripple[] colors, ref Bitmap pattern)
        {

            int y;
            int x;
            int PreCalcX;
            int PreCalcY;
            int[] PreCalcXs = new int[362];

            for (x = 0; x < 181; x++)
            {
                PreCalcX = 180 - x;
                PreCalcXs[x] = PreCalcX * PreCalcX;
            }

            for (x = 181; x < 362; x++)
            {
                PreCalcX = x - 181;
                PreCalcXs[x] = PreCalcX * PreCalcX;
            }

            BitmapData bmpData;
            pattern = new Bitmap(362, 181, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 181; y++)
            {
                PreCalcY = 180 - y;
                PreCalcY = PreCalcY * PreCalcY;
                for (x = 0; x < 362; x++)
                {
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }

            #region Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
            #endregion
        }

        public static void RadialLeft(RGBTripple[] colors, ref Bitmap pattern)
        {
            #region Local variables
            int y;
            int x;
            int PreCalcY;
            BitmapData bmpData;
            int patternWidth;
            int patternHeight;
            int[] PreCalcYs = new int[362];
            #endregion

            patternWidth = 181;
            patternHeight = 362;

            for (y = 0; y < 181; y++)
            {
                PreCalcY = 180 - y;
                PreCalcYs[y] = PreCalcY * PreCalcY;
            }

            for (y = 181; y < 362; y++)
            {
                PreCalcY = y - 181;
                PreCalcYs[y] = PreCalcY * PreCalcY;
            }

            #region Prepare raw data
            pattern = new Bitmap(patternWidth, patternHeight, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, patternWidth, patternHeight);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = patternWidth * patternHeight * 4;
            byte[] rgbValues = new byte[numBytes];
            #endregion

            int idx = 0;

            for (y = 0; y < 362; y++)
            {
                for (x = 0; x < 181; x++)
                {
                    rgbValues[idx] = colors[(int)Math.Sqrt(x * x + PreCalcYs[y])].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(x * x + PreCalcYs[y])].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(x * x + PreCalcYs[y])].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }

            # region Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
            #endregion
        }

        public static void RadialRight(RGBTripple[] colors, ref Bitmap pattern)
        {
            #region Local variables
            int y;
            int x;
            int PreCalcY;
            int PreCalcX;
            BitmapData bmpData;
            int patternWidth;
            int patternHeight;
            int[] PreCalcYs = new int[362];
            int[] PreCalcXs = new int[181];
            #endregion

            patternWidth = 181;
            patternHeight = 362;

            for (x = 0; x < 181; x++)
            {
                PreCalcX = 180 - x;
                PreCalcXs[x] = PreCalcX * PreCalcX;
            }


            for (y = 0; y < 181; y++)
            {
                PreCalcY = 180 - y;
                PreCalcYs[y] = PreCalcY * PreCalcY;
            }

            for (y = 181; y < 362; y++)
            {
                PreCalcY = y - 181;
                PreCalcYs[y] = PreCalcY * PreCalcY;
            }

            #region Prepare raw data
            pattern = new Bitmap(patternWidth, patternHeight, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, patternWidth, patternHeight);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = patternWidth * patternHeight * 4;
            byte[] rgbValues = new byte[numBytes];
            #endregion

            int idx = 0;

            for (y = 0; y < 362; y++)
            {
                for (x = 0; x < 181; x++)
                {
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcYs[y])].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcYs[y])].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcYs[y])].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }

            # region Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
            #endregion
        }

        public static void RadialTopLeft(RGBTripple[] colors, ref Bitmap pattern)
        {
            #region Local variables
            int y;
            int x;
            int PreCalcY;
            BitmapData bmpData;
            int patternWidth;
            int patternHeight;
            int[] PreCalcXs = new int[181];
            #endregion

            patternWidth = 181;
            patternHeight = 181;


            #region Prepare raw data
            pattern = new Bitmap(patternWidth, patternHeight, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, patternWidth, patternHeight);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = patternWidth * patternHeight * 4;
            byte[] rgbValues = new byte[numBytes];
            #endregion

            int idx = 0;

            for (x = 0; x < 181; x++)
            {
                PreCalcXs[x] = x * x;
            }

            for (y = 0; y < 181; y++)
            {
                PreCalcY = y * y;

                for (x = 0; x < 181; x++)
                {
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }



            # region Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
            #endregion
        }

        public static void RadialTopRight(RGBTripple[] colors, ref Bitmap pattern)
        {
            #region Local variables
            int y;
            int x;
            int PreCalcY;
            int PreCalcX;
            BitmapData bmpData;
            int patternWidth;
            int patternHeight;
            int[] PreCalcXs = new int[181];
            #endregion

            patternWidth = 181;
            patternHeight = 181;


            #region Prepare raw data
            pattern = new Bitmap(patternWidth, patternHeight, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, patternWidth, patternHeight);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = patternWidth * patternHeight * 4;
            byte[] rgbValues = new byte[numBytes];
            #endregion

            int idx = 0;

            for (x = 0; x < 181; x++)
            {
                PreCalcX = 180 - x;
                PreCalcXs[x] = PreCalcX * PreCalcX;
            }

            for (y = 0; y < 181; y++)
            {
                PreCalcY = y * y;

                for (x = 0; x < 181; x++)
                {
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }



            # region Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
            #endregion

        }

        public static void RadialBottomLeft(RGBTripple[] colors, ref Bitmap pattern)
        {
            #region Local variables
            int y;
            int x;
            int PreCalcY;
            BitmapData bmpData;
            int patternWidth;
            int patternHeight;
            int[] PreCalcXs = new int[181];
            #endregion

            patternWidth = 181;
            patternHeight = 181;


            #region Prepare raw data
            pattern = new Bitmap(patternWidth, patternHeight, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, patternWidth, patternHeight);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = patternWidth * patternHeight * 4;
            byte[] rgbValues = new byte[numBytes];
            #endregion

            int idx = 0;

            for (x = 0; x < 181; x++)
            {
                PreCalcXs[x] = x * x;
            }

            for (y = 0; y < 181; y++)
            {
                PreCalcY = 180 - y;
                PreCalcY = PreCalcY * PreCalcY;

                for (x = 0; x < 181; x++)
                {
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }



            # region Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
            #endregion

        }

        public static void RadialBottomRight(RGBTripple[] colors, ref Bitmap pattern)
        {
            #region Local variables
            int y;
            int x;
            int PreCalcY;
            int PreCalcX;
            BitmapData bmpData;
            int patternWidth;
            int patternHeight;
            int[] PreCalcXs = new int[181];
            #endregion

            patternWidth = 181;
            patternHeight = 181;


            #region Prepare raw data
            pattern = new Bitmap(patternWidth, patternHeight, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, patternWidth, patternHeight);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = patternWidth * patternHeight * 4;
            byte[] rgbValues = new byte[numBytes];
            #endregion

            int idx = 0;

            for (x = 0; x < 181; x++)
            {
                PreCalcX = 180 - x;
                PreCalcXs[x] = PreCalcX * PreCalcX;
            }

            for (y = 0; y < 181; y++)
            {
                PreCalcY = 180 - y;
                PreCalcY = PreCalcY * PreCalcY;

                for (x = 0; x < 181; x++)
                {
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(int)Math.Sqrt(PreCalcXs[x] + PreCalcY)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }



            # region Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
            #endregion

        }

        public static void LinearHorizontal(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            BitmapData bmpData;
            pattern = new Bitmap(1, 256, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 256; y++)
            {
                rgbValues[idx] = colors[y].rgbtBlue;
                idx++;
                rgbValues[idx] = colors[y].rgbtGreen;
                idx++;
                rgbValues[idx] = colors[y].rgbtRed;
                idx++;
                rgbValues[idx] = 255;
                idx++;

            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
        }

        public static void LinearVertical(RGBTripple[] colors, ref Bitmap pattern)
        {
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(256, 1, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (x = 0; x < 256; x++)
            {
                rgbValues[idx] = colors[x].rgbtBlue;
                idx++;
                rgbValues[idx] = colors[x].rgbtGreen;
                idx++;
                rgbValues[idx] = colors[x].rgbtRed;
                idx++;
                rgbValues[idx] = 255;
                idx++;

            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;

        }

        public static void ReflectedHorizontal(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            BitmapData bmpData;
            pattern = new Bitmap(1, 512, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 256; y++)
            {
                rgbValues[idx] = colors[255 - y].rgbtBlue;
                idx++;
                rgbValues[idx] = colors[255 - y].rgbtGreen;
                idx++;
                rgbValues[idx] = colors[255 - y].rgbtRed;
                idx++;
                rgbValues[idx] = 255;
                idx++;

            }

            for (y = 0; y < 256; y++)
            {
                rgbValues[idx] = colors[y].rgbtBlue;
                idx++;
                rgbValues[idx] = colors[y].rgbtGreen;
                idx++;
                rgbValues[idx] = colors[y].rgbtRed;
                idx++;
                rgbValues[idx] = 255;
                idx++;

            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
        }

        public static void ReflectedVertical(RGBTripple[] colors, ref Bitmap pattern)
        {
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(512, 1, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (x = 0; x < 256; x++)
            {
                rgbValues[idx] = colors[255 - x].rgbtBlue;
                idx++;
                rgbValues[idx] = colors[255 - x].rgbtGreen;
                idx++;
                rgbValues[idx] = colors[255 - x].rgbtRed;
                idx++;
                rgbValues[idx] = 255;
                idx++;

            }

            for (x = 0; x < 256; x++)
            {
                rgbValues[idx] = colors[x].rgbtBlue;
                idx++;
                rgbValues[idx] = colors[x].rgbtGreen;
                idx++;
                rgbValues[idx] = colors[x].rgbtRed;
                idx++;
                rgbValues[idx] = 255;
                idx++;

            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
        }

        public static void DiagonalLinearForward(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(128, 129, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 129; y++)
            {
                for (x = 0; x < 128; x++)
                {
                    rgbValues[idx] = colors[127 + (y - x)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[127 + (y - x)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[127 + (y - x)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }

            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
        }

        public static void DiagonalLinearBackward(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(128, 129, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 129; y++)
            {
                for (x = 0; x < 128; x++)
                {
                    rgbValues[idx] = colors[x + y].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[x + y].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[x + y].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }

            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
        }

        public static void DiagonalReflectedForward(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(256, 256, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 256; y++)
            {
                for (x = 0; x < 256; x++)
                {
                    if (x > y)
                    {
                        rgbValues[idx] = colors[x - y].rgbtBlue;
                        idx++;
                        rgbValues[idx] = colors[x - y].rgbtGreen;
                        idx++;
                        rgbValues[idx] = colors[x - y].rgbtRed;
                        idx++;
                        rgbValues[idx] = 255;
                        idx++;
                    }
                    else
                    {
                        rgbValues[idx] = colors[y - x].rgbtBlue;
                        idx++;
                        rgbValues[idx] = colors[y - x].rgbtGreen;
                        idx++;
                        rgbValues[idx] = colors[y - x].rgbtRed;
                        idx++;
                        rgbValues[idx] = 255;
                        idx++;
                    }
                }
            }

            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;

        }

        public static void DiagonalReflectedBackward(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(256, 256, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 256; y++)
            {
                for (x = 0; x < 256; x++)
                {
                    if ((x + y) < 255)
                    {
                        rgbValues[idx] = colors[255 - (x + y)].rgbtBlue;
                        idx++;
                        rgbValues[idx] = colors[255 - (x + y)].rgbtGreen;
                        idx++;
                        rgbValues[idx] = colors[255 - (x + y)].rgbtRed;
                        idx++;
                        rgbValues[idx] = 255;
                        idx++;
                    }
                    else
                    {
                        rgbValues[idx] = colors[(y + x) - 255].rgbtBlue;
                        idx++;
                        rgbValues[idx] = colors[(y + x) - 255].rgbtGreen;
                        idx++;
                        rgbValues[idx] = colors[(y + x) - 255].rgbtRed;
                        idx++;
                        rgbValues[idx] = 255;
                        idx++;
                    }
                }
            }

            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;

        }

        public static void ArrowLeft(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(129, 256, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 128; y++)
            {
                for (x = 0; x < 129; x++)
                {
                    rgbValues[idx] = colors[255 - (x + y)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[255 - (x + y)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[255 - (x + y)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }


            for (y = 128; y < 256; y++)
            {
                for (x = 0; x < 129; x++)
                {
                    rgbValues[idx] = colors[y - x].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[y - x].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[y - x].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
        }

        public static void ArrowRight(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(129, 256, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 128; y++)
            {
                for (x = 0; x < 129; x++)
                {
                    rgbValues[idx] = colors[(x - y) + 127].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(x - y) + 127].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(x - y) + 127].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }


            for (y = 128; y < 256; y++)
            {
                for (x = 0; x < 129; x++)
                {
                    rgbValues[idx] = colors[(x + y) - 128].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(x + y) - 128].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(x + y) - 128].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
        }

        public static void ArrowUp(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(256, 129, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 129; y++)
            {
                for (x = 0; x < 128; x++)
                {
                    rgbValues[idx] = colors[255 - (x + y)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[255 - (x + y)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[255 - (x + y)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

                for (x = 128; x < 256; x++)
                {
                    rgbValues[idx] = colors[x - y].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[x - y].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[x - y].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }


            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
        }

        public static void ArrowDown(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(256, 129, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 129; y++)
            {
                for (x = 0; x < 128; x++)
                {
                    rgbValues[idx] = colors[127 + (y - x)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[127 + (y - x)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[127 + (y - x)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

                for (x = 128; x < 256; x++)
                {
                    rgbValues[idx] = colors[(x + y) - 128].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(x + y) - 128].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(x + y) - 128].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

            }


            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;
        }

        public static void Diamond(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(256, 256, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 128; y++)
            {
                for (x = 0; x < 128; x++)
                {
                    rgbValues[idx] = colors[255 - (x + y)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[255 - (x + y)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[255 - (x + y)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

                for (x = 128; x < 256; x++)
                {
                    rgbValues[idx] = colors[x - y].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[x - y].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[x - y].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }

            for (y = 128; y < 256; y++)
            {
                for (x = 0; x < 128; x++)
                {
                    rgbValues[idx] = colors[y - x].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[y - x].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[y - x].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

                for (x = 128; x < 256; x++)
                {
                    rgbValues[idx] = colors[(x + y) - 255].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(x + y) - 255].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(x + y) - 255].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;

        }

        public static void Butterfly(RGBTripple[] colors, ref Bitmap pattern)
        {
            int y;
            int x;
            BitmapData bmpData;
            pattern = new Bitmap(256, 256, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, pattern.Width, pattern.Height);
            bmpData = pattern.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                pattern.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int numBytes = pattern.Width * pattern.Height * 4;
            byte[] rgbValues = new byte[numBytes];

            int idx = 0;

            for (y = 0; y < 128; y++)
            {
                for (x = 0; x < 128; x++)
                {
                    rgbValues[idx] = colors[(x - y) + 128].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(x - y) + 128].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(x - y) + 128].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

                for (x = 128; x < 256; x++)
                {
                    rgbValues[idx] = colors[383 - (x + y)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[383 - (x + y)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[383 - (x + y)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }

            for (y = 128; y < 256; y++)
            {
                for (x = 0; x < 128; x++)
                {
                    rgbValues[idx] = colors[(x + y) - 128].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[(x + y) - 128].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[(x + y) - 128].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }

                for (x = 128; x < 256; x++)
                {
                    rgbValues[idx] = colors[128 + (y - x)].rgbtBlue;
                    idx++;
                    rgbValues[idx] = colors[128 + (y - x)].rgbtGreen;
                    idx++;
                    rgbValues[idx] = colors[128 + (y - x)].rgbtRed;
                    idx++;
                    rgbValues[idx] = 255;
                    idx++;
                }
            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            pattern.UnlockBits(bmpData);
            bmpData = null;

        }
    }
}
