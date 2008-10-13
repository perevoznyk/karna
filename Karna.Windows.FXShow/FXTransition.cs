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
using Karna.Windows.UI;
using System.Runtime.CompilerServices;


namespace Karna.Windows.FXShow
{
    /// <summary>
    /// Image slider control with 172 transitional effects
    /// </summary>
    [Designer(typeof(Karna.Windows.FXShow.Design.FXTransitionDesigner))]
    [ToolboxItem(true)]
    public partial class FXTransition : CustomPanel
    {
        private Bitmap surface;
        private Bitmap texture;
        private Bitmap display;

        private bool busy = false;
        [AccessedThroughProperty("Step")]
        private int step = 4;
        [AccessedThroughProperty("Style")]
        private int style = 51;
        [AccessedThroughProperty("Delay")]
        private int delay = 40;
        [AccessedThroughProperty("Progress")]
        private int progress = 0;
        [AccessedThroughProperty("Center")]
        private bool center = false;
        [AccessedThroughProperty("Stretch")]
        private bool stretch = false;
        [AccessedThroughProperty("Proportional")]
        private bool proportional = false;

        Rectangle PicRect;
        Rectangle DisplayRect;

        private void SetCenter(bool value)
        {
            if (center != value)
            {
                center = value;
                UpdateDisplayRect();
                Invalidate();
            }
        }

        private void SetStretch(bool value)
        {
            if (stretch != value)
            {
                stretch = value;
                UpdateDisplayRect();
                Invalidate();
            }
        }

        private void SetProportional(bool value)
        {
            if (proportional != value)
            {
                proportional = value;
                UpdateDisplayRect();
                Invalidate();
            }
        }

        /// <summary>
        /// Determines the amount of delay in milliseconds before showing the next frame of the transition.
        /// </summary>
        [DefaultValue(40)]
        public int Delay
        {
            get { return delay; }
            set { delay = value; }
        }

        /// <summary>
        /// Determines the amount of change in Progress in automatic transition.
        /// </summary>
        [DefaultValue(4)]
        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the image will be centered 
        /// within the control's boundaries.
        /// </summary>
        /// <value><c>true</c> if center; otherwise, <c>false</c>.</value>
        [DefaultValue(false)]
        public bool Center
        {
            get { return center; }
            set { SetCenter(value); }
        }

        /// <summary>
        /// If set to true, when the control's client area is larger than the loaded image, the image it will be 
        /// stretched (or shrinked) to fit within the control's boundaries.
        /// </summary>
        [DefaultValue(false)]
        public bool Stretch
        {
            get { return stretch; }
            set { SetStretch(value); }
        }

        /// <summary>
        /// Indicates whether the image should be changed, without distortion, so that it fits the bounds of the control. 
        /// When Proportional is True, images that are too large to fit in the control are scaled down 
        /// (while maintaining the same aspect ratio) until they fit in the control. 
        /// Images that are too small are displayed normally. 
        /// </summary>
        [DefaultValue(false)]
        public bool Proportional
        {
            get { return proportional; }
            set { SetProportional(value); }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FXTransition"/> class.
        /// </summary>
        public FXTransition()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the display rect.
        /// </summary>
        protected void UpdateDisplayRect()
        {
            int cW;
            int cH;
            int pW;
            int pH;

            cW = Width;
            cH = Height;

            pW = PicRect.Width;
            pH = PicRect.Height;

            if (Proportional && (pW > 0) && (pH > 0) && (Stretch || (pW > cW) || (pH > cH)))
                if ((cW / pW) < (cH / pH))
                {
                    pH = KarnaMath.MulDiv(pH, cW, pW);
                    pW = cW;
                }
                else
                {
                    pW = KarnaMath.MulDiv(pW, cH, pH);
                    pH = cH;
                }
            else if (Stretch)
            {
                pW = cW;
                pH = cH;
            }

            DisplayRect.Width = pW;
            DisplayRect.Height = pH;
            DisplayRect.Location = new Point(0, 0);

            if (center)
            {
                DisplayRect.Offset((cW - pW) / 2, (cH - pH) / 2);
            }

        }

        /// <summary>
        /// Raises the <see cref="E:Paint"/> event.
        /// </summary>
        /// <param name="pe">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);
            if (display != null)
                pe.Graphics.DrawImage(display, DisplayRect);
        }

        /// <summary>
        /// Gets or sets the surface.
        /// </summary>
        /// <value>The surface.</value>
        public Bitmap Surface
        {
            get { return surface; }
            set
            {
                surface = value;
                PicRect.Width = surface.Width;
                PicRect.Height = surface.Height;
                if (display != null)
                {
                    display.Dispose();
                    GC.SuppressFinalize(display);
                }
                display = (Bitmap)surface.Clone();
                UpdateDisplayRect();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        /// <value>The texture.</value>
        public Bitmap Texture
        {
            get { return texture; }
            set
            {
                texture = value;
            }
        }

        /// <summary>
        /// Updates the display.
        /// </summary>
        protected void UpdateDisplay()
        {
            int X;
            int Y;
            FXEffect effect;
            IntPtr D;
            IntPtr P;

            if (surface == null)
                return;

            if (texture == null)
                return;

            if (Right >= Bottom)
            {
                X = KarnaMath.MulDiv(PicRect.Right, progress, 100);
                Y = KarnaMath.MulDiv(X, PicRect.Bottom, PicRect.Right);
            }
            else
            {
                Y = KarnaMath.MulDiv(PicRect.Bottom, progress, 100);
                X = KarnaMath.MulDiv(Y, PicRect.Right, PicRect.Bottom);
            }

            D = surface.GetHbitmap();
            P = texture.GetHbitmap();
            try
            {
                if (progress == 100)
                {
                    Karna.Windows.UI.BitmapPainter.DrawNativeBitmap(D, P, surface.Width, surface.Height);
                }
                else
                {
                    effect = FXKit.SelectEffectNumber(style);
                    if (effect != null)
                    {
                        try
                        {
                            effect(D, P, PicRect.Right, PicRect.Bottom, X, Y, progress);
                        }
                        catch
                        {
                        }
                    }
                }

                display.Dispose();
                GC.SuppressFinalize(display);
                display = Bitmap.FromHbitmap(D);
            }
            finally
            {
                Karna.Windows.UI.NativeMethods.DeleteObject(D);
                Karna.Windows.UI.NativeMethods.DeleteObject(P);
            }
        }


        /// <summary>
        /// Specifies the index of the transition effect that will be used.
        /// </summary>
        [Editor(typeof(Karna.Windows.FXShow.Design.TransitionPropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(51)]
        [Description("Specifies the index of the transition effect that will be used.")]
        public int Style
        {
            get { return style; }
            set
            {
                if (style != value)
                {
                    style = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Determines the current state of the running transition
        /// </summary>
        [Description("Determines the current state of the running transition")]
        [DefaultValue(0)]
        public int Progress
        {
            get { return progress; }
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 100)
                    value = 100;
                if (progress != value)
                {
                    progress = value;
                    UpdateDisplay();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Updates the progress.
        /// </summary>
        /// <param name="progressStep">The progress step.</param>
        /// <param name="ellapsedTime">The ellapsed time.</param>
        /// <returns></returns>
        protected bool UpdateProgress(int progressStep, out int ellapsedTime)
        {
            int startTime;
            bool result = true;

            startTime = Environment.TickCount;
            if (progress < 100 - progressStep)
            {
                Progress = Progress + progressStep;
            }
            else
            {
                Progress = 100;
            }
            if (progress == 100)
                result = false;

            ellapsedTime = Environment.TickCount - startTime;
            return result;
        }


        /// <summary>
        /// Determines whether the control is performing an image transition
        /// </summary>
        [Browsable(false)]
        public bool Busy
        {
            get { return busy; }
        }

        /// <summary>
        /// Animates the transition using selected style.
        /// </summary>
        public void Animate()
        {
            if (busy)
                return;

            busy = true;
            int progressStep = 0;
            int ellapsedTime = 0;
            int actualDelay = 0;
            int resumeTime = 0;

            lock (this)
            {
                try
                {
                    progressStep = step;
                    while (UpdateProgress(progressStep, out ellapsedTime))
                    {
                        actualDelay = delay - ellapsedTime;
                        if (actualDelay >= 0)
                        {
                            resumeTime = Environment.TickCount + actualDelay;
                            do
                            {
                                Application.DoEvents();
                            }
                            while (Environment.TickCount <= resumeTime);
                            progressStep = step;
                        }
                        else
                        {
                            progressStep = KarnaMath.MulDiv(step, delay - actualDelay, delay);
                        }
                    }
                }
                finally
                {
                    progress = 0;
                    busy = false;
                }
            }
        }

    }
}
