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
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI
{
    [KarnaPurpose("Tools")]
    public static class TextPainter
    {
        public static string DefaultName = "Tahoma";
        public static float DefaultSize = 16.0f;
        public static FontStyle DefaultStyle = FontStyle.Bold;
        public static Color DefaultColor = Color.White;

        public static void DrawString(Graphics canvas, string s, int x, int y, int width, int height)
        {
            using (Font font = new Font(DefaultName, DefaultSize, DefaultStyle, GraphicsUnit.Pixel))
            {
                DrawString(canvas, s, font, x, y, width, height, DefaultColor);
            }
        }

        public static void DrawString(Graphics canvas, string s, Font f, int x, int y, int width, int height)
        {
            DrawString(canvas, s, f, x, y, width, height, Color.White);
        }

        public static void DrawString(Graphics canvas, string s, Font f, int x, int y, int width, int height, Color bodyColor)
        {
            float shadowWidth;
            float shadowX;
            float shadowY;
            SolidBrush whiteBrush;
            GraphicsPath textPath;
            Pen blackPen;
            StringFormat stringFormat;
            int ShadowOffsetXSizePercent = -3;
            int ShadowOffsetYSizePercent = -3;
            int ShadowWidthSizePercent = 3;

            canvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            whiteBrush = new SolidBrush(bodyColor);

            stringFormat = new StringFormat(StringFormatFlags.NoWrap);
            stringFormat.Trimming = StringTrimming.EllipsisWord;
            shadowX = ShadowOffsetXSizePercent / 100 * f.Size;
            shadowY = ShadowOffsetYSizePercent / 100 * f.Size;
            shadowWidth = ShadowWidthSizePercent / 100 * f.Size;

            textPath = new GraphicsPath();
            textPath.AddString(s, f.FontFamily, (int)f.Style, f.Size, new Rectangle(x, y, width, height), stringFormat);

            blackPen = new Pen(Brushes.Black, shadowWidth);
            blackPen.LineJoin = LineJoin.Round;

            canvas.SmoothingMode = SmoothingMode.AntiAlias;
            canvas.DrawPath(blackPen, textPath);
            canvas.TranslateTransform(shadowX, shadowY);
            canvas.FillPath(whiteBrush, textPath);
            canvas.TranslateTransform(-shadowX, -shadowY);

            blackPen.Dispose();
            blackPen = null;

            textPath.Dispose();
            textPath = null;

            stringFormat.Dispose();
            stringFormat = null;

            whiteBrush.Dispose();
            whiteBrush = null;
        }

    }
}
