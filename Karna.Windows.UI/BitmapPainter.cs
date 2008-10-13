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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI
{
    /// <summary>
    /// Performs different operations with Bitmap 
    /// </summary>
    [KarnaPurpose("Tools")]
    public static class BitmapPainter
    {
        /// <summary>
        /// Resizes the bitmap.
        /// </summary>
        /// <param name="bmp">The BMP.</param>
        /// <param name="newWidth">The new width.</param>
        /// <param name="newHeight">The new height.</param>
        /// <returns></returns>
        public static Bitmap ResizeBitmap(Image bmp, int newWidth, int newHeight)
        {
            if ( (bmp == null) || (newWidth <= 0) || (newHeight <= 0) )
                return null;

            Bitmap result = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb);
            Graphics canvas = Graphics.FromImage(result);
            canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
            canvas.DrawImage(bmp, 0, 0, newWidth, newHeight);
            return result;
        }

        /// <summary>
        /// Resizes the bitmap.
        /// </summary>
        /// <param name="bmp">The BMP.</param>
        /// <param name="newWidth">The new width.</param>
        /// <param name="newHeight">The new height.</param>
        /// <param name="interpolationMode">The interpolation mode.</param>
        /// <returns></returns>
        public static Bitmap ResizeBitmap(Image bmp, int newWidth, int newHeight, InterpolationMode interpolationMode)
        {
            if ((bmp == null) || (newWidth <= 0) || (newHeight <= 0))
                return null;
            Bitmap result = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb);
            Graphics canvas = Graphics.FromImage(result);
            canvas.InterpolationMode = interpolationMode;
            canvas.DrawImage(bmp, 0, 0, newWidth, newHeight);
            return result;
        }

        /// <summary>
        /// Draws the native bitmap.
        /// </summary>
        /// <param name="dst">The DST.</param>
        /// <param name="src">The SRC.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void DrawNativeBitmap(IntPtr dst, IntPtr src, int width, int height)
        {
            const int SRCCOPY = 0xcc0020;

            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr oldDst;
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, src);

            IntPtr DstDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldDst = NativeMethods.SelectObject(DstDC, dst);

            NativeMethods.BitBlt(DstDC, 0, 0, width, height, srcDC, 0, 0, SRCCOPY);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);

            NativeMethods.SelectObject(DstDC, oldDst);
            NativeMethods.DeleteObject(DstDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Performs native GDI image stretching
        /// </summary>
        public static void StretchDraw(Bitmap src, ref Bitmap dst)
        {
            const int SRCCOPY = 0xcc0020;
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr oldDst;
            int srcWidth;
            int dstWidth;
            int srcHeight;
            int dstHeight;

            if (src == null)
                return;

            if (dst == null)
                return;

            srcWidth = src.Width;
            dstWidth = dst.Width;

            srcHeight = src.Height;
            dstHeight = dst.Height;

            IntPtr srcBitmap = src.GetHbitmap();
            IntPtr dstBitmap = dst.GetHbitmap();

            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            IntPtr dstDC = NativeMethods.CreateCompatibleDC(screenDC);

            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);
            oldDst = NativeMethods.SelectObject(dstDC, dstBitmap);

            NativeMethods.SetStretchBltMode(dstDC, (int)StretchBltMode.COLORONCOLOR);
            if (NativeMethods.StretchBlt(dstDC, 0, 0, dstWidth, dstHeight,
                srcDC, 0, 0, srcWidth, srcHeight, SRCCOPY))
            {
                dst.Dispose();
                GC.SuppressFinalize(dst);
                dst = Bitmap.FromHbitmap(dstBitmap);
            }


            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.SelectObject(dstDC, oldDst);

            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(dstBitmap);

            NativeMethods.DeleteObject(srcDC);
            NativeMethods.DeleteObject(dstDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);

        }

        /// <summary>
        /// Converts Bitmap image to 32 bits Bitmap
        /// </summary>
        /// <param name="realImage">The real image.</param>
        /// <returns></returns>
        public static Bitmap ConvertToRealColors(Bitmap realImage)
        {
            Graphics canvas;
            ImageAttributes attributes = new ImageAttributes();
            Bitmap image = new Bitmap(realImage.Width, realImage.Height, PixelFormat.Format32bppArgb);
            canvas = Graphics.FromImage(image);
            canvas.DrawImage(realImage, new Rectangle(0, 0, realImage.Width, realImage.Height), 0, 0, realImage.Width, realImage.Height, GraphicsUnit.Pixel, attributes);
            return image;
        }

        /// <summary>
        /// Creates the reflection image.
        /// </summary>
        /// <param name="realImage">The real image.</param>
        /// <param name="reflectionDepth">The reflection depth.</param>
        /// <returns></returns>
        public static Image CreateReflectionImage(Image realImage, int reflectionDepth)
        {
            int w;
            int h;
            float opaque;
            int alpha;
            int newAlpha;
            float frac;
            Color clr;

            Bitmap result = new Bitmap(realImage.Width, realImage.Height + reflectionDepth, PixelFormat.Format32bppArgb);
            Graphics canvas = Graphics.FromImage(result);
            Bitmap rotatedImage = new Bitmap(realImage);

            w = realImage.Width;
            h = realImage.Height;

            rotatedImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

            Bitmap reflectionImage = new Bitmap(w, h, PixelFormat.Format32bppArgb);

            h = reflectionDepth;

            for (int y = 0; y < h; y++)
            {
                opaque = ((255.0f / h) * (h - y)) - reflectionDepth;
                if (opaque < 0.0)
                    opaque = 0.0f;
                if (opaque > 255.0)
                    opaque = 255.0f;

                for (int x = 0; x < w; x++)
                {
                    clr = rotatedImage.GetPixel(x, y);
                    alpha = clr.A;
                    if (alpha == 0)
                        continue;
                    frac = (float)(opaque / 255);
                    newAlpha = (int)(frac * alpha);
                    reflectionImage.SetPixel(x, y, Color.FromArgb(newAlpha, clr));
                }
            }


            
            canvas.DrawImageUnscaled(reflectionImage, 0, reflectionImage.Height, 0, 0);
            canvas.DrawImage(realImage, new Rectangle(0, 0, realImage.Width, realImage.Height), 0, 0, realImage.Width, realImage.Height, GraphicsUnit.Pixel);
            return result;
        }

        /// <summary>
        /// Changes the alpha.
        /// </summary>
        /// <param name="bmp">The BMP.</param>
        /// <param name="alpha">The alpha.</param>
        public static void ChangeAlpha(Bitmap bmp, byte alpha)
        {
            int numBytes = bmp.Width * bmp.Height * 4;
            BitmapData tmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte[] tmpBytes = new Byte[numBytes];
            Marshal.Copy(tmpData.Scan0, tmpBytes, 0, numBytes);

            int idx = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    idx += 3;
                    tmpBytes[idx] = alpha;
                    idx++;
                }
            }

            Marshal.Copy(tmpBytes, 0, tmpData.Scan0, numBytes);
            bmp.UnlockBits(tmpData);
        }

        /// <summary>
        /// Multiplexes the alpha.
        /// </summary>
        /// <param name="bmp">The BMP.</param>
        /// <param name="alpha">The alpha.</param>
        public static void MultiplexAlpha(Bitmap bmp, double alpha)
        {
            int numBytes = bmp.Width * bmp.Height * 4;
            BitmapData tmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte[] tmpBytes = new Byte[numBytes];
            Marshal.Copy(tmpData.Scan0, tmpBytes, 0, numBytes);

            int idx = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    idx += 3;
                    tmpBytes[idx] = (byte)(alpha * tmpBytes[idx]);
                    idx++;
                }
            }

            Marshal.Copy(tmpBytes, 0, tmpData.Scan0, numBytes);
            bmp.UnlockBits(tmpData);
        }


        /// <summary>
        /// Draws the image unscaled preserving Alpha channel
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="canvas">Drawing surface</param>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public static void DrawImageUnscaled(Bitmap image, Graphics canvas, int x, int y)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, image.Width, image.Height, srcDC,
                0, 0, image.Width, image.Height, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void DrawImageUnscaled(Bitmap image, IntPtr hdc, int x, int y)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, image.Width, image.Height, srcDC,
                0, 0, image.Width, image.Height, BlendFunction);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void DrawImageUnscaled(Bitmap image, IntPtr hdc, int x, int y, int width, int height)
        {
            IntPtr screenDC;
            IntPtr oldSrc;

            if (image == null)
                return;

            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, width, height, BlendFunction);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void DrawImageUnscaled(IntPtr srcBitmap, Graphics canvas, int x, int y)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);
            BITMAP bmp = new BITMAP();
            int imageWidth;
            int imageHeight;

            NativeMethods.GetObject(srcBitmap, Marshal.SizeOf(typeof(BITMAP)), bmp);
            imageWidth = bmp.bmWidth;
            imageHeight = bmp.bmHeight;

            bmp = null;

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, imageWidth, imageHeight, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        public static void DrawImageUnscaled(IntPtr srcBitmap, Graphics canvas, int x, int y, int imageWidth, int imageHeight)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);
            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, imageWidth, imageHeight, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        public static void DrawImageUnscaled(IntPtr srcBitmap, IntPtr hdc, int x, int y, int imageWidth, int imageHeight)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);
            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, imageWidth, imageHeight, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageUnscaled(Bitmap image, Graphics canvas, int x, int y, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, image.Width, image.Height, srcDC,
                0, 0, image.Width, image.Height, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageUnscaled(Bitmap image, IntPtr hdc, int x, int y, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, image.Width, image.Height, srcDC,
                0, 0, image.Width, image.Height, BlendFunction);


            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageUnscaled(Bitmap image, IntPtr hdc, int x, int y, int width, int height, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, width, height, BlendFunction);


            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageUnscaled(IntPtr srcBitmap, Graphics canvas, int x, int y, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BITMAP bmp = new BITMAP();
            int imageWidth;
            int imageHeight;

            NativeMethods.GetObject(srcBitmap, Marshal.SizeOf(typeof(BITMAP)), bmp);
            imageWidth = bmp.bmWidth;
            imageHeight = bmp.bmHeight;

            bmp = null;

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, imageWidth, imageHeight, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }



        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageUnscaled(IntPtr srcBitmap, Graphics canvas, int x, int y, int imageWidth, int imageHeight, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);


            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, imageWidth, imageHeight, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Draws the image unscaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageUnscaled(IntPtr srcBitmap, IntPtr hdc, int x, int y, int imageWidth, int imageHeight, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);


            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, imageWidth, imageHeight, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void DrawImageScaled(Bitmap image, Graphics canvas, int x, int y, int width, int height)
        {
            if (image == null)
                return;
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, image.Width, image.Height, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void DrawImageScaled(Bitmap image, IntPtr hdc, int x, int y, int width, int height)
        {
            if ( (image == null) || (width == 0) || (height == 0))
                return;

            IntPtr screenDC;
            IntPtr oldSrc;

            Bitmap tmpBmp = ResizeBitmap(image, width, height);

            IntPtr srcBitmap = tmpBmp.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, width, height, BlendFunction);


            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
            tmpBmp.Dispose();
            tmpBmp = null;
        }

        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void DrawImageScaled(IntPtr srcBitmap, Graphics canvas, int x, int y, int width, int height)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BITMAP bmp = new BITMAP();
            int imageWidth;
            int imageHeight;

            NativeMethods.GetObject(srcBitmap, Marshal.SizeOf(typeof(BITMAP)), bmp);
            imageWidth = bmp.bmWidth;
            imageHeight = bmp.bmHeight;

            bmp = null;

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void DrawImageScaled(IntPtr srcBitmap, Graphics canvas, int x, int y, int imageWidth, int imageHeight, int width, int height)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void DrawImageScaled(IntPtr srcBitmap, IntPtr hdc, int x, int y, int imageWidth, int imageHeight, int width, int height)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = 0xFF;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageScaled(Bitmap image, Graphics canvas, int x, int y, int width, int height, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, image.Width, image.Height, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageScaled(Bitmap image, IntPtr hdc, int x, int y, int width, int height, byte alpha)
        {
            if ( (image == null) || (width == 0) || (height == 0))
                return;
            IntPtr screenDC;
            IntPtr oldSrc;
            Bitmap tmpBmp = ResizeBitmap(image, width, height);
            IntPtr srcBitmap = tmpBmp.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, width, height, BlendFunction);


            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);

            tmpBmp.Dispose();
            tmpBmp = null;
        }

        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageScaled(IntPtr srcBitmap, Graphics canvas, int x, int y, int width, int height, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BITMAP bmp = new BITMAP();
            int imageWidth;
            int imageHeight;

            NativeMethods.GetObject(srcBitmap, Marshal.SizeOf(typeof(BITMAP)), bmp);
            imageWidth = bmp.bmWidth;
            imageHeight = bmp.bmHeight;

            bmp = null;

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageScaled(IntPtr srcBitmap, Graphics canvas, int x, int y, int imageWidth, int imageHeight, int width, int height, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);


            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Draws the image scaled.
        /// </summary>
        /// <param name="srcBitmap">The SRC bitmap.</param>
        /// <param name="hdc">The HDC.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="alpha">The alpha.</param>
        public static void DrawImageScaled(IntPtr srcBitmap, IntPtr hdc, int x, int y, int imageWidth, int imageHeight, int width, int height, byte alpha)
        {
            IntPtr screenDC;
            IntPtr oldSrc;
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            BLENDFUNCTION BlendFunction;

            BlendFunction.BlendOp = (byte)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = (byte)BlendOperations.AC_SRC_ALPHA;

            NativeMethods.AlphaBlend(hdc, x, y, width, height, srcDC,
                0, 0, imageWidth, imageHeight, BlendFunction);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }

        /// <summary>
        /// Copies the image unscaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void CopyImageUnscaled(Bitmap image, Graphics canvas, int x, int y)
        {
            const int SRCCOPY = 0xcc0020;

            IntPtr screenDC;
            IntPtr oldSrc;
            IntPtr hdc = canvas.GetHdc();
            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            NativeMethods.BitBlt(hdc, x, y, image.Width, image.Height, srcDC, 0, 0, SRCCOPY);

            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Copies the image scaled.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void CopyImageScaled(Bitmap image, Graphics canvas, int x, int y, int width, int height)
        {
            const int SRCCOPY = 0xcc0020;

            IntPtr screenDC;
            IntPtr oldSrc;

            if ((image == null) || (canvas == null) || (width == 0) || (height == 0))
                return;

            IntPtr hdc = canvas.GetHdc();
            IntPtr srcBitmap = image.GetHbitmap(Color.FromArgb(0));
            screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr srcDC = NativeMethods.CreateCompatibleDC(screenDC);
            oldSrc = NativeMethods.SelectObject(srcDC, srcBitmap);

            if ((image.Height == height) && (image.Width == width))
            {
                NativeMethods.BitBlt(hdc, x, y, width, height, srcDC, 0, 0, SRCCOPY);
            }
            else
            {
                NativeMethods.SetStretchBltMode(hdc, (int)StretchBltMode.COLORONCOLOR);
                NativeMethods.StretchBlt(hdc, x, y, width, height, srcDC, 0, 0, image.Width, image.Height, SRCCOPY);
            }

            canvas.ReleaseHdc(hdc);

            NativeMethods.SelectObject(srcDC, oldSrc);
            NativeMethods.DeleteObject(srcBitmap);
            NativeMethods.DeleteObject(srcDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
        }


        /// <summary>
        /// Creates a GDI bitmap object from a GDI+Bitmap. 
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>
        /// A handle to the GDI bitmap object that this method creates. 
        /// </returns>
        public static IntPtr GetNativeBitmap(Bitmap image)
        {
            if (image != null)
                return image.GetHbitmap(Color.FromArgb(0));
            else
                return IntPtr.Zero;
        }
    }
}
