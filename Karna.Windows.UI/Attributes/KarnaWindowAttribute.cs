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
using System.ComponentModel;

namespace Karna.Windows.UI.Attributes
{
    /// <summary>
    /// Custom attribute for all Karna windows for providing additional information about window properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    [Description("Custom attribute for all Karna windows for providing additional information about window properties")]
    public class KarnaWindowAttribute : System.Attribute
    {
        private string windowDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="KarnaWindowAttribute"/> class.
        /// </summary>
        /// <param name="windowDescription">The window description.</param>
        public KarnaWindowAttribute(string windowDescription)
        {
            this.windowDescription = windowDescription;
        }

        /// <summary>
        /// Gets the window description.
        /// </summary>
        /// <value>The window description.</value>
        public string WindowDescription
        {
            get { return windowDescription; }
        }

    }
}
