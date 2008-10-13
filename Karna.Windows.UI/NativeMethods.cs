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
using System.Runtime.InteropServices;
using Karna.Windows.UI.Attributes;

namespace Karna.Windows.UI
{

    /// <summary>
    /// Extracting Low and High order words from integer parameter 
    /// </summary>
    [KarnaPurpose("Internal")]
    internal static class ParamConvertor
    {
        /// <summary>
        /// Extracts low order word from integer
        /// Uses for extracting the coordinates from Windows message 
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Low order word from integer</returns>
        internal static int LoWord(IntPtr param)
        {
            return (short)((uint)param & 0x0000FFFFU);
        }

        /// <summary>
        /// Extracts high order word from integer 
        /// Uses for extracting the coordinates from Windows message
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>High order word from integer</returns>
        internal static int HiWord(IntPtr param)
        {
            return (short)(((uint)param & 0xFFFF0000U) >> 16);
        }
    }

    [KarnaPurpose("Internal")]
    public static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UpdateLayeredWindow(IntPtr windowHandle,
                                                      IntPtr ScreenDC,
                                                      ref POINT windowPosition,
                                                      ref SIZE windowSize,
                                                      IntPtr sourceDC,
                                                      ref POINT sourcePosition,
                                                      Int32 colorKey,
                                                      ref BLENDFUNCTION blendFunction,
                                                      int flags);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr windowHandle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int ReleaseDC(IntPtr windowHandle, IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MoveWindow(IntPtr windowHandle,
                                              int x,
                                              int y,
                                              int width,
                                              int height,
                                              bool repaint);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int ShowWindow(IntPtr windowHandle, short cmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetActiveWindow(IntPtr windowHandle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetForegroundWindow(IntPtr windowHandle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ScreenToClient(IntPtr hWnd, ref POINT pt);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool StretchBlt(IntPtr DestDC, int X, int Y, int Width, int Height, IntPtr SrcDC, int XSrc, int YSrc, int SrcWidth, int SrcHeight, uint Rop);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int SetStretchBltMode(IntPtr DC, int StretchMode);

        [DllImport("Msimg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool AlphaBlend(
        IntPtr hdcDest, // handle to destination DC
        int nXOriginDest, // x-coord of upper-left corner
        int nYOriginDest, // y-coord of upper-left corner
        int nWidthDest, // destination width
        int nHeightDest, // destination height
        IntPtr hdcSrc, // handle to source DC
        int nXOriginSrc, // x-coord of upper-left corner
        int nYOriginSrc, // y-coord of upper-left corner
        int nWidthSrc, // source width
        int nHeightSrc, // source height
        BLENDFUNCTION blendFunction // alpha-blending function
        );

        [DllImport("Msimg32.dll")]
        public static extern bool TransparentBlt(IntPtr hdcDest, // handle to destination DC
        int nXOriginDest, // x-coord of destination upper-left corner
        int nYOriginDest, // y-coord of destination upper-left corner
        int nWidthDest, // width of destination rectangle
        int hHeightDest, // height of destination rectangle
        IntPtr hdcSrc, // handle to source DC
        int nXOriginSrc, // x-coord of source upper-left corner
        int nYOriginSrc, // y-coord of source upper-left corner
        int nWidthSrc, // width of source rectangle
        int nHeightSrc, // height of source rectangle
        int crTransparent // color to make transparent
        );

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest,
        int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc,
        uint dwRop);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(IntPtr hObject, int nSize, [In, Out] BITMAP bm);

        [DllImport("user32.dll", EntryPoint = "RegisterClassW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool RegisterClass(ref WNDCLASS wndClass);

        [DllImport("user32.dll", EntryPoint = "DefWindowProcW", CharSet = CharSet.Unicode)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string modName);

        [DllImport("user32.dll", EntryPoint = "CreateWindowExW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr CreateWindow(int dwExStyle, string lpClassName, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CallingConvention = CallingConvention.StdCall)]
        public extern static bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(IntPtr hwnd, int msg, int wparam, int lparam);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CallingConvention = CallingConvention.StdCall)]
        public extern static int SetWindowLong(IntPtr hwnd, WindowLong index, IntPtr value);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CallingConvention = CallingConvention.StdCall)]
        public extern static int SetWindowLong(IntPtr hwnd, WindowLong index, WndProc value);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr GetWindowLong(IntPtr hwnd, WindowLong index);


    }
}
