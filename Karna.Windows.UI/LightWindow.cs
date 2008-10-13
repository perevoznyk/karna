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
using System.Runtime.InteropServices;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI
{
    [KarnaWindow("Lightweight wrapper for native Window")]
    public class LightWindow : MarshalByRefObject, IWin32Window, IDisposable
    {
        private static string defaultClassName = "Karna Window";
        private static string internalClassName = "";

        private IntPtr handle;
        private WindowStyles windowStyle = (WindowStyles.WS_POPUP | WindowStyles.WS_VISIBLE);
        private ExtendedWindowStyles windowExStyle = (ExtendedWindowStyles.WS_EX_TOPMOST | ExtendedWindowStyles.WS_EX_TOOLWINDOW | ExtendedWindowStyles.WS_EX_LAYERED);
        private bool isDisposed = false;
        internal static WndProc WndProcDelegate;
        protected WndProc InternalWndProcDelegate;

        protected virtual void DestroyWindow()
        {
            try
            {
                if (handle != IntPtr.Zero)
                    NativeMethods.DestroyWindow(handle);
            }

            finally
            {
                handle = IntPtr.Zero;
            }
        }

        public  void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // Code to dispose the managed resources 
                    // held by the class
                }
            }

            // Code to dispose the unmanaged resources 
            // held by the class
            DestroyWindow();
            isDisposed = true;
        }

        ~LightWindow()
        {
            ForceExit();
            Dispose(false);
        }

        public IntPtr Handle
        {
            get { return handle; }
        }

        static LightWindow()
        {
            WndProcDelegate = new WndProc(NativeMethods.DefWindowProc);
            RegisterWindowClass();
        }

        public LightWindow()
        {
            InternalWndProcDelegate = new WndProc(InternalWndProc);
        }

        private void ReleaseHandle()
        {
            if (this.handle != IntPtr.Zero)
            {
                lock (this)
                {
                    if (this.handle != IntPtr.Zero)
                    {
                        this.handle = IntPtr.Zero;
                        GC.SuppressFinalize(this);
                    }
                }
            }
        }

        internal void ForceExit()
        {
            IntPtr winHandle;
            lock (this)
            {
                winHandle = this.handle;
            }

            if (this.handle != IntPtr.Zero)
            {
                this.ReleaseHandle();
            }

            if (winHandle != IntPtr.Zero)
            {
                NativeMethods.PostMessage(winHandle, (int)WindowMessages.WM_CLOSE, 0, 0);
            }
        }

        protected virtual IntPtr InternalWndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            return this.WndProc(hWnd, msg, wParam, lParam);
        }

        protected static void RegisterWindowClass()
        {
            WNDCLASS wndClass = new WNDCLASS();

            wndClass.lpfnWndProc = WndProcDelegate;
            wndClass.cbClsExtra = 0;
            wndClass.cbWndExtra = 0;
            wndClass.hbrBackground = IntPtr.Zero;
            wndClass.hIcon = IntPtr.Zero;
            wndClass.hInstance = NativeMethods.GetModuleHandle(null);
            wndClass.hCursor = IntPtr.Zero;
            wndClass.lpszClassName = defaultClassName;
            wndClass.lpszMenuName = "";

            bool b = NativeMethods.RegisterClass(ref wndClass);
            if (!b)
            {
                int i = Marshal.GetLastWin32Error();
                if ((i != 2) && (i != 0))
                    internalClassName = "";
                else
                    internalClassName = defaultClassName;

            }
            else
                internalClassName = defaultClassName;

        }

        protected virtual CreateParams CreateParams
        {
            get
            {
                CreateParams cp = new CreateParams();
                if (!String.IsNullOrEmpty(internalClassName))
                    cp.ClassName = internalClassName;
                else
                    cp.ClassName = "";
                cp.ExStyle = (int)windowExStyle;
                cp.Style = (int)windowStyle;
                return cp;
            }
        }

        public virtual void CreateHandle(CreateParams cp)
        {
            if (cp != null)
            {
                handle = NativeMethods.CreateWindow(cp.ExStyle, cp.ClassName, cp.Caption, cp.Style, 0, 0, 10, 10, cp.Parent, IntPtr.Zero, NativeMethods.GetModuleHandle(null), IntPtr.Zero);
                if (handle != IntPtr.Zero)
                {
                    NativeMethods.SetWindowLong(handle, WindowLong.GWL_WNDPROC, InternalWndProcDelegate);
                }
            }

        }

        public void DefWndProc(ref Message m)
        {
            m.Result = NativeMethods.DefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam);
        }

        protected virtual void WndProc(ref Message m)
        {
            DefWndProc(ref m);
        }

        internal virtual IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            IntPtr result = IntPtr.Zero;
            Message m = new Message();
            m.HWnd = hWnd;
            m.Msg = (int)msg;
            m.WParam = wParam;
            m.LParam = lParam;
            m.Result = IntPtr.Zero;

            WndProc(ref m);
            result = m.Result;

            return result;
        }

    }
}
