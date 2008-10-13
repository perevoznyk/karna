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
    /// <summary>
    /// Specifies the style of the window being created
    /// </summary>
    [FlagsAttribute]
    public enum WindowStyles : uint
    {
        /// <summary>
        /// Creates an overlapped window. An overlapped window has a title bar and a border 
        /// </summary>
        WS_OVERLAPPED   = 0x00000000,
        /// <summary>
        /// Creates a pop-up window
        /// </summary>
        WS_POPUP        = 0x80000000,
        /// <summary>
        /// Creates a child window. A window with this style cannot have a menu bar. 
        /// This style cannot be used with the WS_POPUP style.
        /// </summary>
        WS_CHILD        = 0x40000000,
        /// <summary>
        /// Creates a window that is initially minimized. 
        /// Same as the WS_ICONIC style.
        /// </summary>
        WS_MINIMIZE     = 0x20000000,
        /// <summary>
        /// Creates a window that is initially visible.
        /// </summary>
        WS_VISIBLE      = 0x10000000,
        /// <summary>
        /// Creates a window that is initially disabled. 
        /// A disabled window cannot receive input from the user
        /// </summary>
        WS_DISABLED     = 0x08000000,
        /// <summary>
        /// Clips child windows relative to each other; that is, when a particular child window 
        /// receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping 
        /// child windows out of the region of the child window to be updated. 
        /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, 
        /// when drawing within the client area of a child window, to draw within the client area 
        /// of a neighboring child window.
        /// </summary>
        WS_CLIPSIBLINGS = 0x04000000,
        /// <summary>
        /// Excludes the area occupied by child windows when drawing occurs within the parent window. 
        /// This style is used when creating the parent window.
        /// </summary>
        WS_CLIPCHILDREN = 0x02000000,
        /// <summary>
        /// Creates a window that is initially maximized.
        /// </summary>
        WS_MAXIMIZE     = 0x01000000,
        /// <summary>
        /// Creates a window that has a title bar (includes the WS_BORDER style).
        /// </summary>
        WS_CAPTION      = 0x00C00000,
        /// <summary>
        /// Creates a window that has a thin-line border.
        /// </summary>
        WS_BORDER       = 0x00800000,
        /// <summary>
        /// Creates a window that has a border of a style typically used with dialog boxes. 
        /// A window with this style cannot have a title bar.
        /// </summary>
        WS_DLGFRAME     = 0x00400000,
        /// <summary>
        /// Creates a window that has a vertical scroll bar.
        /// </summary>
        WS_VSCROLL      = 0x00200000,
        /// <summary>
        /// Creates a window that has a horizontal scroll bar.
        /// </summary>
        WS_HSCROLL      = 0x00100000,
        /// <summary>
        /// Creates a window that has a window menu on its title bar. 
        /// The WS_CAPTION style must also be specified.
        /// </summary>
        WS_SYSMENU      = 0x00080000,
        /// <summary>
        /// Creates a window that has a sizing border. 
        /// Same as the WS_SIZEBOX style.
        /// </summary>
        WS_THICKFRAME   = 0x00040000,
        /// <summary>
        /// Specifies the first control of a group of controls. 
        /// The group consists of this first control and all controls defined after it, 
        /// up to the next control with the WS_GROUP style. The first control in each group 
        /// usually has the WS_TABSTOP style so that the user can move from group to group. 
        /// The user can subsequently change the keyboard focus from one control in the group 
        /// to the next control in the group by using the direction keys.
        /// </summary>
        WS_GROUP        = 0x00020000,
        /// <summary>
        /// Specifies a control that can receive the keyboard focus when the user presses the TAB key. 
        /// Pressing the TAB key changes the keyboard focus to the next control with the 
        /// WS_TABSTOP style. 
        /// </summary>
        WS_TABSTOP      = 0x00010000,
        /// <summary>
        /// Creates a window that has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP 
        /// style. The WS_SYSMENU style must also be specified. 
        /// </summary>
        WS_MINIMIZEBOX  = 0x00020000,
        /// <summary>
        /// Creates a window that has a maximize button. Cannot be combined with the 
        /// WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified. 
        /// </summary>
        WS_MAXIMIZEBOX  = 0x00010000
    }

    /// <summary>
    ///Common window styles
    /// </summary>
    public enum CommonWindowStyles : uint
    {
        /// <summary>
        ///Creates an overlapped window. An overlapped window has a title bar and a border. Same as the WS_OVERLAPPED style. 
        /// </summary>
        WS_TILED = WindowStyles.WS_OVERLAPPED,
        /// <summary>
        ///Creates a window that is initially minimized. Same as the WS_MINIMIZE style.
        /// </summary>
        WS_ICONIC = WindowStyles.WS_MINIMIZE,
        /// <summary>
        ///Creates a window that has a sizing border. Same as the WS_THICKFRAME style.
        /// </summary>
        WS_SIZEBOX = WindowStyles.WS_THICKFRAME,
        /// <summary>
        /// Creates an overlapped window with the WS_OVERLAPPED, WS_CAPTION, WS_SYSMENU, WS_THICKFRAME, WS_MINIMIZEBOX, and WS_MAXIMIZEBOX styles. Same as the WS_TILEDWINDOW style. 
        /// </summary>
        WS_OVERLAPPEDWINDOW = (WindowStyles.WS_OVERLAPPED | WindowStyles.WS_CAPTION | WindowStyles.WS_SYSMENU | WindowStyles.WS_THICKFRAME | WindowStyles.WS_MINIMIZEBOX | WindowStyles.WS_MAXIMIZEBOX),
        /// <summary>
        ///Creates an overlapped window with the WS_OVERLAPPED, WS_CAPTION, WS_SYSMENU, WS_THICKFRAME, WS_MINIMIZEBOX, and WS_MAXIMIZEBOX styles. Same as the WS_OVERLAPPEDWINDOW style.  
        /// </summary>
        WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
        /// <summary>
        ///Creates a pop-up window with WS_BORDER, WS_POPUP, and WS_SYSMENU styles. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.
        /// </summary>
        WS_POPUPWINDOW = (WindowStyles.WS_POPUP  | 
                          WindowStyles.WS_BORDER | 
                          WindowStyles.WS_SYSMENU),
        /// <summary>
        ///Same as the WS_CHILD style.
        /// </summary>
        WS_CHILDWINDOW = (WindowStyles.WS_CHILD)
    }

    /// <summary>
    /// Specifies the extended style of the window
    /// </summary>
    [FlagsAttribute]
    public enum ExtendedWindowStyles : uint
    {
        WS_EX_DLGMODALFRAME   = 0x00000001,
        WS_EX_NOPARENTNOTIFY  = 0x00000004,
        WS_EX_TOPMOST         = 0x00000008,
        WS_EX_ACCEPTFILES     = 0x00000010,
        WS_EX_TRANSPARENT     = 0x00000020,
        WS_EX_MDICHILD        = 0x00000040,
        WS_EX_TOOLWINDOW      = 0x00000080,
        WS_EX_WINDOWEDGE      = 0x00000100,
        WS_EX_CLIENTEDGE      = 0x00000200,
        WS_EX_CONTEXTHELP     = 0x00000400,
        WS_EX_RIGHT           = 0x00001000,
        WS_EX_LEFT            = 0x00000000,
        WS_EX_RTLREADING      = 0x00002000,
        WS_EX_LTRREADING      = 0x00000000,
        WS_EX_LEFTSCROLLBAR   = 0x00004000,
        WS_EX_RIGHTSCROLLBAR  = 0x00000000,
        WS_EX_CONTROLPARENT   = 0x00010000,
        WS_EX_STATICEDGE      = 0x00020000,
        WS_EX_APPWINDOW       = 0x00040000,
        WS_EX_LAYERED         = 0x00080000,
        WS_EX_NOINHERITLAYOUT = 0x00100000, 
        WS_EX_LAYOUTRTL       = 0x00400000, 
        WS_EX_COMPOSITED      = 0x02000000,
        WS_EX_NOACTIVATE      = 0x08000000
    }

    public enum CommonExtendedWindowStyles : uint
    {
        WS_EX_OVERLAPPEDWINDOW = (ExtendedWindowStyles.WS_EX_WINDOWEDGE | 
                                  ExtendedWindowStyles.WS_EX_CLIENTEDGE),
        WS_EX_PALETTEWINDOW    = (ExtendedWindowStyles. WS_EX_WINDOWEDGE | 
                                  ExtendedWindowStyles.WS_EX_TOOLWINDOW |  
                                  ExtendedWindowStyles.WS_EX_TOPMOST)
    }

    [FlagsAttribute]
    public enum LayeredWindowAttributeFlags : uint
    {
        LWA_COLORKEY = 0x00000001,
        LWA_ALPHA = 0x00000002
    }


    [FlagsAttribute]
    public enum LayeredWindowUpdateFlags : uint
    {
        ULW_COLORKEY = 0x00000001,
        ULW_ALPHA = 0x00000002,
        ULW_OPAQUE = 0x00000004
    }

    [FlagsAttribute]
    public enum BlendOperations : byte
    {
        AC_SRC_OVER = 0x00,
        AC_SRC_ALPHA = 0x01
    }

    public enum ShowWindowStyles : short
    {
        SW_HIDE            = 0,
        SW_SHOWNORMAL      = 1,
        SW_NORMAL          = 1,
        SW_SHOWMINIMIZED   = 2,
        SW_SHOWMAXIMIZED   = 3,
        SW_MAXIMIZE        = 3,
        SW_SHOWNOACTIVATE  = 4,
        SW_SHOW            = 5,
        SW_MINIMIZE        = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA          = 8,
        SW_RESTORE         = 9,
        SW_SHOWDEFAULT     = 10,
        SW_FORCEMINIMIZE   = 11,
        SW_MAX             = 11
    }

    public enum WindowMessages : int
    {
        WM_CREATE = 0x0001,
        WM_DESTROY = 0x0002,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,
        WM_QUIT = 0x0012,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_MOUSEMOVE = 0x0200,
        WM_NCMOUSEMOVE = 0x00A0,
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,
        WM_SYSCHAR = 0x0106,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_MOUSEHOVER = 0x02A1,
        WM_MOUSELEAVE = 0x02A3,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,
        WM_NCXBUTTONDOWN = 0x00AB,
        WM_NCXBUTTONUP = 0x00AC,
        WM_NCHITTEST = 0x0084
    }


    public enum StretchBltMode : int
    {
        BLACKONWHITE = 1,
        WHITEONBLACK = 2,
        COLORONCOLOR = 3,
        HALFTONE = 4
    }

    [Flags]
    public enum WindowLong
    {
        GWL_WNDPROC = -4,
        GWL_HINSTANCE = -6,
        GWL_HWNDPARENT = -8,
        GWL_STYLE = -16,
        GWL_EXSTYLE = -20,
        GWL_USERDATA = -21,
        GWL_ID = -12
    }

}



