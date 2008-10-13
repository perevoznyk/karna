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
using System.Diagnostics;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI
{
    [KarnaPurpose("Debug")]
    public static class TraceDebug
    {
        /// <summary>
        /// Logs the debug info to console.
        /// </summary>
        /// <param name="debugMessage">The output text</param>
        [Conditional("DEBUG")]
        public static void Message(string debugMessage)
        {
            Console.WriteLine(Timestamp + " " + debugMessage);
        }

        /// <summary>
        /// Add text line to debugger output window
        /// </summary>
        /// <param name="debugMessage">The output text</param>
        [Conditional("DEBUG")]
        public static void Trace(string debugMessage)
        {
            System.Diagnostics.Trace.WriteLine(Timestamp + " " + debugMessage);
        }

        /// <summary>
        /// Returns the timestamp that can be used during the log reporting
        /// </summary>
        /// <value>The timestamp.</value>
        public static string Timestamp
        {
            get { return DateTime.Now.ToLongTimeString(); }
        }

    }
}
