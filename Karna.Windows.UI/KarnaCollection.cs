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
using System.Collections;

namespace Karna.Windows.UI
{
    public delegate void ChangeHandler();

    public class KarnaCollection : CollectionBase
    {
        public event ChangeHandler Inserted;
        public event ChangeHandler Removed;
        public event ChangeHandler Changed;

        public KarnaCollection()
            : base()
        {
        }

        protected override void OnInsert(int index, Object value)
        {
            if (Inserted != null)
            {
                Inserted();
            }
        }

        protected override void OnRemove(int index, Object value)
        {
            if (Removed != null)
            {
                Removed();
            }
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            if (Changed != null)
            {
                Changed();
            }
        }



    }
}
