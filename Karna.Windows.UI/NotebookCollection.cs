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

namespace Karna.Windows.UI
{
    public class NotebookCollection : KarnaCollection
    {
        public NotebookCollection()
            : base()
        {
        }

        public NotebookTopic Add(NotebookTopic value)
        {
            List.Add(value);
            return value;
        }

        public void AddRange(NotebookTopic[] values)
        {
            foreach (NotebookTopic topic in values)
                Add(topic);
        }

        public void Remove(NotebookTopic value)
        {
            List.Remove(value);
        }

        public void Insert(int index, NotebookTopic value)
        {
            List.Insert(index, value);
        }

        public bool Contains(NotebookTopic value)
        {
            return List.Contains(value);
        }

        public NotebookTopic this[int index]
        {
            get { return (List[index] as NotebookTopic); }
        }

        public int IndexOf(NotebookTopic value)
        {
            return List.IndexOf(value);
        }

    }
}
