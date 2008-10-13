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

namespace Karna.Windows.FXShow.Design
{
    internal partial class TransitionSelector : Form
    {
        private bool canAnimate = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionSelector"/> class.
        /// </summary>
        public TransitionSelector()
        {
            InitializeComponent();
        }


        private void edtStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!canAnimate)
                return;

            if (edtStyles.SelectedIndex >= 0)
            {
                transition.Progress = 0;
                transition.Style = edtStyles.SelectedIndex + 1;
                for (int cnt = 0; cnt < 101; cnt++)
                {
                    transition.Progress = transition.Progress + 1;
                    Application.DoEvents();
                    if (transition.Progress == 100)
                        return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TransitionSelector_Load(object sender, EventArgs e)
        {
            canAnimate = true;
        }
    }
}