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
using System.ComponentModel;

namespace Karna.Windows.UI
{
    public class NotebookTopic
    {
        private string text;
        private Image image;

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        [LocalizableAttribute(true)]
        public virtual Image Image 
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

    }
}
