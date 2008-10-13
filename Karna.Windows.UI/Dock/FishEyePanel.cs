//===============================================================================
// Copyright � Serhiy Perevoznyk.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Karna.Windows.UI.Dock
{
    [ToolboxItem(true)]
    public partial class FishEyeDockPanel : CustomDockPanel
    {
        private Timer returnTimer;
        private DockItem lastItem;
        private int returnSteps = 10;

        public FishEyeDockPanel()
        {
            InitializeComponent();
            returnTimer = new Timer();
            returnTimer.Enabled = false;
            returnTimer.Interval = 30;
            returnTimer.Tick += new EventHandler(TimerHandler);
        }

        public int ReturnSpeed
        {
            get { return returnTimer.Interval; }
            set { returnTimer.Interval = value; }
        }

        [DefaultValue(10)]
        public int ReturnSteps
        {
            get { return returnSteps; }
            set { returnSteps = value; }
        }

        protected void TimerHandler(object sender, EventArgs e)
        {
            if (lastItem == null)
                returnTimer.Enabled = false;
            else
            {
                if (lastItem.Scale > 1.0)
                {
                    ((DockManager)DockManager).DecreaseScale((float)(DefaultDockSettings.MaxScale / returnSteps));
                    Invalidate();
                }
                else
                    returnTimer.Enabled = false;
            }
        }

        public DockOrientation DockOrientation
        {
            get { return ((DockManager)DockManager).DockOrientation; }
            set { ((DockManager)DockManager).DockOrientation = value; }
        }

        protected override CustomDockManager GetDockManager()
        {
            return new DockManager();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            returnTimer.Enabled = false;
            base.OnMouseEnter(e);
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            float maxScale = 0.0f;

            if (DockManager.Items.Count == 0)
            {
                lastItem = null;
            }
            else
            {
                for (int cnt = 0; cnt < DockManager.Items.Count; cnt++)
                {
                    if (DockManager.Items[cnt].Scale > maxScale)
                    {
                        lastItem = DockManager.Items[cnt];
                        maxScale = lastItem.Scale;
                    }
                }
                returnTimer.Enabled = true;
            }
        }
    }
}
