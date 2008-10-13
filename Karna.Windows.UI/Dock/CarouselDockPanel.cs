//===============================================================================
// Copyright © Serhiy Perevoznyk.  All rights reserved.
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
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI.Dock
{
    [ToolboxItem(true)]
    [KarnaPurpose("Dock")]
    public partial class CarouselDockPanel : CustomDockPanel
    {
        public CarouselDockPanel()
        {
            InitializeComponent();
        }

        protected override CustomDockManager GetDockManager()
        {
            return new DockRotator();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
                ((DockRotator)DockManager).MoveDirection = MoveDirection.Left;
            else
                ((DockRotator)DockManager).MoveDirection = MoveDirection.Right;

            for (int i = 0; i < 360/DockManager.Items.Count/((DockRotator)DockManager).Speed/2; i++)
            {
                ((DockRotator)DockManager).Rotate();
                Invalidate();
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            bool turn = false;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.PageDown:
                        turn = true;
                        ((DockRotator)DockManager).MoveDirection = MoveDirection.Left;
                        break;

                    case Keys.Up:
                    case Keys.Right:
                    case Keys.PageUp:
                        turn = true;
                        ((DockRotator)DockManager).MoveDirection = MoveDirection.Right;
                        break;
                }
            }

            if (turn)
            {
                ((DockRotator)DockManager).Rotate();
                Invalidate();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

    }
}
