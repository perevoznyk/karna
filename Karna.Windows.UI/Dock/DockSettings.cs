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
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI.Dock
{

    [KarnaPurpose("Dock")]
    public static class DefaultDockSettings
    {
        public static int IconsSpacing = 10;
        public static int MaxZoom = 128;
        public static int IconSize = 48;
        public static int LeftMargin = 0;
        public static int TopMargin = 0;
        public static int ReflectionDepth = 24;
        public static double Multiplier = 60;
        public static double MaxScale = 3;
    }

    [KarnaPurpose("Dock")]
    public class DockSettings
    {
        private int leftMargin;
        private int topMargin;
        private int iconSize;
        private int iconsSpacing;
        private int maxZoom;
        private int reflectionDepth;

        public EventHandler Changed;

        public DockSettings()
        {
            iconsSpacing = DefaultDockSettings.IconsSpacing;
            maxZoom = DefaultDockSettings.MaxZoom;
            iconSize = DefaultDockSettings.IconSize;
            leftMargin = DefaultDockSettings.LeftMargin;
            topMargin = DefaultDockSettings.TopMargin;
            reflectionDepth = DefaultDockSettings.ReflectionDepth;
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

        public int ReflectionDepth
        {
            get
            {
                return reflectionDepth;
            }
            set
            {
                if (reflectionDepth != value)
                {
                    reflectionDepth = value;
                    DoChange();
                }
            }
        }

        public int LeftMargin
        {
            get
            {
                return leftMargin;
            }
            set
            {
                if (leftMargin != value)
                {
                    leftMargin = value;
                    DoChange();
                }
            }
        }

        public int TopMargin
        {
            get
            {
                return topMargin;
            }
            set
            {
                if (topMargin != value)
                {
                    topMargin = value;
                    DoChange();
                }
            }
        }

        public int IconSize
        {
            get
            {
                return iconSize;
            }
            set
            {
                if (iconSize != value)
                {
                    iconSize = value;
                    DoChange();
                }
            }
        }

        public int IconsSpacing
        {
            get
            {
                return iconsSpacing;
            }
            set
            {
                if (iconsSpacing != value)
                {
                    iconsSpacing = value;
                    DoChange();
                }
            }
        }

        public int MaxZoom
        {
            get
            {
                return maxZoom;
            }
            set
            {
                if (maxZoom != value)
                {
                    maxZoom = value;
                    DoChange();
                }
            }
        }
    }
}
