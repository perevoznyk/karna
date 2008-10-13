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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI
{


    /// <summary>
    /// Basic class for all predefined layered windows
    /// </summary>
    [KarnaWindow("Lightweight layered native window")]
    public class LayeredWindow : LightWindow
    {
        #region Private Fields
        private LayeredWindowUpdateFlags windowFlags = (LayeredWindowUpdateFlags.ULW_ALPHA | LayeredWindowUpdateFlags.ULW_COLORKEY);
        private bool clicked;
        private Size size;
        #endregion

        [AccessedThroughProperty("Position")]
        private Point position;
        [AccessedThroughProperty("Buffer")]
        private Bitmap buffer;
        [AccessedThroughProperty("Canvas")]
        private Graphics canvas;
        [AccessedThroughProperty("Alpha")]
        private byte alpha;
        [AccessedThroughProperty("ColorKey")]
        private int colorKey;


        private bool GetUseAlpha()
        {
            return ((windowFlags & LayeredWindowUpdateFlags.ULW_ALPHA) != 0);
        }

        private bool GetUseColorKey()
        {
            return ((windowFlags & LayeredWindowUpdateFlags.ULW_COLORKEY) != 0);
        }

        private void SetUseAlpha(bool value)
        {
            if (value)
            {
                windowFlags = windowFlags | LayeredWindowUpdateFlags.ULW_ALPHA;
            }
            else
            {
                windowFlags = windowFlags & ~LayeredWindowUpdateFlags.ULW_ALPHA;
            }
        }

        private void SetUseColorKey(bool value)
        {
            if (value)
            {
                windowFlags = windowFlags | LayeredWindowUpdateFlags.ULW_COLORKEY;
            }
            else
            {
                windowFlags = windowFlags & ~LayeredWindowUpdateFlags.ULW_COLORKEY;
            }
        }



        public LayeredWindow()
        {

            position.X = 0;
            position.Y = 0;
            size.Height = 0;
            size.Width = 0;

            buffer = new Bitmap(10, 10, PixelFormat.Format32bppPArgb);
            canvas = Graphics.FromImage(buffer);

            CreateParams cp = this.CreateParams;
            this.CreateHandle(cp);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Code to cleanup managed resources held by the class.
                if (buffer != null)
                {
                    buffer.Dispose();
                    buffer = null;
                }
            }

            // Code to cleanup unmanaged resources held by the class.

            base.Dispose(disposing);
        }

        public bool UseAlpha
        {
            get { return GetUseAlpha(); }
            set { SetUseAlpha(value); }
        }

        public bool UseColorKey
        {
            get { return GetUseColorKey(); }
            set { SetUseColorKey(value); }
        }

        public Point Position
        {
            get { return position; }
        }

        public Graphics Canvas
        {
            get { return canvas; }
        }

        public Bitmap Buffer
        {
            get { return buffer; }
        }

        public byte Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public int ColorKey
        {
            get { return colorKey; }
            set { colorKey = value; }
        }

        public void UpdatePosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            NativeMethods.MoveWindow(this.Handle, position.X, position.Y, size.Width, size.Height, false);
        }

        public void Update()
        {
            IntPtr ScreenDC;
            IntPtr SourceDC;
            IntPtr BufferBitmap;
            IntPtr OldBitmapSelectedInCanvasDC;
            POINT SourcePosition;
            BLENDFUNCTION BlendFunction;
            POINT NativePosition;
            SIZE NativeSize;

            ScreenDC = NativeMethods.GetDC(IntPtr.Zero);
            SourcePosition.x = 0;
            SourcePosition.y = 0;
            BlendFunction.BlendOp = (int)BlendOperations.AC_SRC_OVER;
            BlendFunction.BlendFlags = 0;
            BlendFunction.SourceConstantAlpha = alpha;
            BlendFunction.AlphaFormat = 0x1;

            SourceDC = NativeMethods.CreateCompatibleDC(ScreenDC);
            BufferBitmap = Buffer.GetHbitmap(Color.FromArgb(0));
            OldBitmapSelectedInCanvasDC = NativeMethods.SelectObject(SourceDC, BufferBitmap);

            NativePosition.x = position.X;
            NativePosition.y = position.Y;

            NativeSize.cx = size.Width;
            NativeSize.cy = size.Height;

            if (!NativeMethods.UpdateLayeredWindow(
                this.Handle,
                ScreenDC,
                ref NativePosition,
                ref NativeSize,
                SourceDC,
                ref SourcePosition,
                colorKey,
                ref BlendFunction,
                (int)windowFlags))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            NativeMethods.SelectObject(SourceDC, OldBitmapSelectedInCanvasDC);
            NativeMethods.DeleteObject(BufferBitmap);
            NativeMethods.DeleteDC(SourceDC);
            NativeMethods.ReleaseDC(IntPtr.Zero, ScreenDC);

        }

        public void Show()
        {
            NativeMethods.ShowWindow(this.Handle, (short)ShowWindowStyles.SW_SHOW);
        }

        public void Hide()
        {
            NativeMethods.ShowWindow(this.Handle, (short)ShowWindowStyles.SW_HIDE);
        }

        public void Clear()
        {
            canvas.Clear(Color.FromArgb(colorKey));
        }

        public void Clear(int x, int y, int width, int height)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(colorKey)))
            {
                canvas.FillRectangle(brush, x, y, width, height);
            }
        }

        public void Clear(float x, float y, float width, float height)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(colorKey)))
            {
                canvas.FillRectangle(brush, x, y, width, height);
            }
        }


        protected void RecreateBuffer()
        {
            this.buffer.Dispose();
            GC.SuppressFinalize(this.buffer);
            this.buffer = null;

            buffer = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppPArgb);
            canvas = Graphics.FromImage(buffer);
        }

        public void Size(int width, int height)
        {
            if ((size.Width == width) && (size.Height == height))
                return;

            size.Width = width;
            size.Height = height;

            RecreateBuffer();

        }

        public int Width
        {
            get { return this.size.Width; }
            set
            {
                this.size.Width = value;
                RecreateBuffer();
            }
        }

        public int Height
        {
            get { return this.size.Height; }
            set
            {
                this.size.Height = value;
                RecreateBuffer();
            }
        }

        public void Size(Size size)
        {
            Size(size.Width, size.Height);
        }

        public virtual void Activate()
        {
            NativeMethods.SetActiveWindow(this.Handle);
        }

        public virtual void MoveToFront()
        {
            NativeMethods.SetForegroundWindow(this.Handle);
        }

        public static MouseButtons MouseButtons
        {
            get
            {
                MouseButtons result = MouseButtons.None;
                if (NativeMethods.GetKeyState(1) < 0)
                {
                    result |= MouseButtons.Left;
                }
                if (NativeMethods.GetKeyState(2) < 0)
                {
                    result |= MouseButtons.Right;
                }
                if (NativeMethods.GetKeyState(4) < 0)
                {
                    result |= MouseButtons.Middle;
                }
                if (NativeMethods.GetKeyState(5) < 0)
                {
                    result |= MouseButtons.XButton1;
                }
                if (NativeMethods.GetKeyState(6) < 0)
                {
                    result |= MouseButtons.XButton2;
                }
                return result;
            }
        }

        public static Keys ModifierKeys
        {
            get
            {
                Keys result = Keys.None;
                if (NativeMethods.GetKeyState(0x10) < 0)
                {
                    result |= Keys.Shift;
                }
                if (NativeMethods.GetKeyState(0x11) < 0)
                {
                    result |= Keys.Control;
                }
                if (NativeMethods.GetKeyState(0x12) < 0)
                {
                    result |= Keys.Alt;
                }
                return result;
            }
        }

        /// <summary>
        /// Occurs when the mouse pointer is moved over the window
        /// </summary>
        public MouseEventHandler MouseMove;
        /// <summary>
        /// Occurs when the mouse pointer is over the control and a mouse button is pressed. 
        /// </summary>
        public MouseEventHandler MouseDown;
        /// <summary>
        /// Occurs when the mouse pointer is over the control and a mouse button is released.
        /// </summary>
        public MouseEventHandler MouseUp;
        /// <summary>
        /// Occurs when the control is clicked. 
        /// </summary>
        public MouseEventHandler MouseClick;
        public MouseEventHandler MouseDoubleClick;
        public event EventHandler Move;
        public event EventHandler Close;
        public event EventHandler Quit;
        public KeyEventHandler KeyDown;
        public KeyEventHandler KeyUp;
        public KeyPressEventHandler KeyPress;

        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            if (KeyDown != null)
                KeyDown(this, e);
        }

        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            if (KeyUp != null)
                KeyUp(this, e);
        }

        protected virtual void OnKeyPress(KeyPressEventArgs e)
        {
            if (KeyPress != null)
                KeyPress(this, e);
        }

        protected virtual void OnMove(EventArgs e)
        {
            if (Move != null)
                Move(this, e);
        }

        protected virtual void OnClose(EventArgs e)
        {
            if (Close != null)
                Close(this, e);
        }

        protected virtual void OnQuit(EventArgs e)
        {
            if (Quit != null)
                Quit(this, e);
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null)
                MouseMove(this, e);
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null)
                MouseDown(this, e);
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            if (MouseUp != null)
                MouseUp(this, e);
        }

        protected virtual void OnMouseClick(MouseEventArgs e)
        {
            if (MouseClick != null)
                MouseClick(this, e);
        }

        protected virtual void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (MouseDoubleClick != null)
                MouseDoubleClick(this, e);
        }

        private void WmMouseMove(ref Message m)
        {
            POINT Location;
            Location.x = ParamConvertor.LoWord(m.LParam);
            Location.y = ParamConvertor.HiWord(m.LParam);
            NativeMethods.ScreenToClient(this.Handle, ref Location);
            this.OnMouseMove(new MouseEventArgs(MouseButtons, 0, Location.x, Location.y, 0));
        }

        private void WmMouseDown(ref Message m, MouseButtons button, int click)
        {
            POINT Location;
            Location.x = ParamConvertor.LoWord(m.LParam);
            Location.y = ParamConvertor.HiWord(m.LParam);
            NativeMethods.ScreenToClient(this.Handle, ref Location);
            clicked = true;
            this.OnMouseDown(new MouseEventArgs(button, click, Location.x, Location.y, 0));
        }

        private void WmMouseUp(ref Message m, MouseButtons button, int click)
        {
            POINT Location;
            Location.x = ParamConvertor.LoWord(m.LParam);
            Location.y = ParamConvertor.HiWord(m.LParam);
            NativeMethods.ScreenToClient(this.Handle, ref Location);
            if (this.clicked)
                OnMouseClick(new MouseEventArgs(button, click, Location.x, Location.y, 0));
            this.OnMouseUp(new MouseEventArgs(button, click, Location.x, Location.y, 0));
        }

        private void WmMouseDoubleClick(ref Message m, MouseButtons button, int click)
        {
            POINT Location;
            Location.x = ParamConvertor.LoWord(m.LParam);
            Location.y = ParamConvertor.HiWord(m.LParam);
            NativeMethods.ScreenToClient(this.Handle, ref Location);
            this.OnMouseDoubleClick(new MouseEventArgs(button, click, Location.x, Location.y, 0));
        }


        protected override void WndProc(ref Message m)
        {
            const int HTCAPTION = 2;
            switch (m.Msg)
            {
                case (int)WindowMessages.WM_NCHITTEST:
                    {
                        m.Result = (IntPtr)HTCAPTION;
                        return;
                    }
                case (int)WindowMessages.WM_MOUSEMOVE:
                case (int)WindowMessages.WM_NCMOUSEMOVE:
                    {
                        WmMouseMove(ref m);
                        break;
                    }
                case (int)WindowMessages.WM_LBUTTONDOWN:
                case (int)WindowMessages.WM_NCLBUTTONDOWN:
                    {
                        WmMouseDown(ref m, MouseButtons.Left, 1);
                        break;
                    }
                case (int)WindowMessages.WM_RBUTTONDOWN:
                case (int)WindowMessages.WM_NCRBUTTONDOWN:
                    {
                        WmMouseDown(ref m, MouseButtons.Right, 1);
                        break;
                    }
                case (int)WindowMessages.WM_MBUTTONDOWN:
                case (int)WindowMessages.WM_NCMBUTTONDOWN:
                    {
                        WmMouseDown(ref m, MouseButtons.Middle, 1);
                        break;
                    }
                case (int)WindowMessages.WM_LBUTTONUP:
                case (int)WindowMessages.WM_NCLBUTTONUP:
                    {
                        WmMouseUp(ref m, MouseButtons.Left, 1);
                        break;
                    }
                case (int)WindowMessages.WM_RBUTTONUP:
                case (int)WindowMessages.WM_NCRBUTTONUP:
                    {
                        WmMouseUp(ref m, MouseButtons.Right, 1);
                        break;
                    }
                case (int)WindowMessages.WM_MBUTTONUP:
                case (int)WindowMessages.WM_NCMBUTTONUP:
                    {
                        WmMouseUp(ref m, MouseButtons.Middle, 1);
                        break;
                    }
                case (int)WindowMessages.WM_LBUTTONDBLCLK:
                case (int)WindowMessages.WM_NCLBUTTONDBLCLK:
                    {
                        WmMouseDoubleClick(ref m, MouseButtons.Left, 2);
                        break;
                    }
                case (int)WindowMessages.WM_RBUTTONDBLCLK:
                case (int)WindowMessages.WM_NCRBUTTONDBLCLK:
                    {
                        WmMouseDoubleClick(ref m, MouseButtons.Right, 2);
                        break;
                    }
                case (int)WindowMessages.WM_MBUTTONDBLCLK:
                case (int)WindowMessages.WM_NCMBUTTONDBLCLK:
                    {
                        WmMouseDoubleClick(ref m, MouseButtons.Middle, 2);
                        break;
                    }
                case (int)WindowMessages.WM_MOVE:
                    {
                        int xPos = ParamConvertor.LoWord(m.LParam);
                        int yPos = ParamConvertor.HiWord(m.LParam);
                        position.X = xPos;
                        position.Y = yPos;
                        m.Result = IntPtr.Zero;
                        OnMove(EventArgs.Empty);
                        break;
                    }
                case (int)WindowMessages.WM_QUIT:
                    {
                        OnQuit(EventArgs.Empty);
                        break;
                    }
                case (int)WindowMessages.WM_CLOSE:
                    {
                        OnClose(EventArgs.Empty);
                        break;
                    }
                case (int)WindowMessages.WM_CHAR:
                case (int)WindowMessages.WM_SYSCHAR:
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)((ushort)((long)m.WParam)));
                        OnKeyPress(e);
                        break;
                    }
                case (int)WindowMessages.WM_KEYDOWN:
                    {
                        KeyEventArgs e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) | ModifierKeys);
                        OnKeyDown(e);
                        break;
                    }
                case (int)WindowMessages.WM_KEYUP:
                    {
                        KeyEventArgs e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) | ModifierKeys);
                        OnKeyUp(e);
                        break;
                    }

            }

            base.WndProc(ref m);
        }

    }
}
