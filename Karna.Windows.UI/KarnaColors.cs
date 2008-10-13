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

namespace Karna.Windows.UI
{
    public static class KarnaColors
    {
        public static Color Darker(Color clr, byte percent)
        {
            int r;
            int g;
            int b;

            r = clr.R;
            g = clr.G;
            b = clr.B;

            r = r - KarnaMath.MulDiv(r, percent, 100);
            g = g - KarnaMath.MulDiv(g, percent, 100);
            b = b - KarnaMath.MulDiv(b, percent, 100);

            return Color.FromArgb(255, r, g, b);
        }
        public static Color Lighter(Color clr, byte percent)
        {
            int r;
            int g;
            int b;

            r = clr.R;
            g = clr.G;
            b = clr.B;

            r = r + KarnaMath.MulDiv(255 - r, percent, 100); //Percent% closer to white
            g = g + KarnaMath.MulDiv(255 - g, percent, 100);
            b = b + KarnaMath.MulDiv(255 - b, percent, 100);

            return Color.FromArgb(255, r, g, b);
        }

        public static bool SameColors(Color color1, Color color2)
        {
            return (color1.ToArgb() == color2.ToArgb());
        }

        public static bool SameColorsNoAlpha(Color color1, Color color2)
        {
            byte r1, g1, b1;
            byte r2, g2, b2;

            r1 = color1.R;
            r2 = color2.R;

            g1 = color1.G;
            g2 = color2.G;

            b1 = color1.B;
            b2 = color2.B;

            if (r1 != r2)
                return false;

            if (g1 != g2)
                return false;

            if (b1 != b2)
                return false;

            return true;
        }
    }
}
