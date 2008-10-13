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

namespace Karna.Windows.UI
{
    /// <summary>
    /// Predefined layered window
    /// </summary>
    [KarnaWindow("Window with glass emulation surface")]
    public class GlassWindow : PredefinedWindow
    {
        protected override void SetResourceName()
        {
            resourceName = "Karna.Windows.UI.Images.frame04.png";
        }
    }
}
