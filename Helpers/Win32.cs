﻿using System;
using System.Runtime.InteropServices;

namespace VirtualSpace.Helpers
{
    /// <summary>
    ///     Contains information about the placement of a window on the screen.
    /// </summary>
    [Serializable]
    [StructLayout( LayoutKind.Sequential )]
    public struct WINDOWPLACEMENT
    {
        /// <summary>
        ///     The length of the structure, in bytes. Before calling the GetWindowPlacement or SetWindowPlacement functions, set this member to sizeof(WINDOWPLACEMENT).
        ///     <para>
        ///         GetWindowPlacement and SetWindowPlacement fail if this member is not set correctly.
        ///     </para>
        /// </summary>
        public int Length;

        /// <summary>
        ///     Specifies flags that control the position of the minimized window and the method by which the window is restored.
        /// </summary>
        public int Flags;

        /// <summary>
        ///     The current show state of the window.
        /// </summary>
        public ShowState ShowCmd;

        /// <summary>
        ///     The coordinates of the window's upper-left corner when the window is minimized.
        /// </summary>
        public POINT MinPosition;

        /// <summary>
        ///     The coordinates of the window's upper-left corner when the window is maximized.
        /// </summary>
        public POINT MaxPosition;

        /// <summary>
        ///     The window's coordinates when the window is in the restored position.
        /// </summary>
        public RECT NormalPosition;

        /// <summary>
        ///     Gets the default (empty) value.
        /// </summary>
        public static WINDOWPLACEMENT Default
        {
            get
            {
                var result = new WINDOWPLACEMENT();
                result.Length = Marshal.SizeOf( result );
                return result;
            }
        }
    }

    public enum ShowState : int
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
        SW_FORCEMINIMIZE   = 11
    }

    [StructLayout( LayoutKind.Sequential )]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT( int left, int top, int right, int bottom )
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    [StructLayout( LayoutKind.Sequential )]
    public struct SIZE
    {
        public int cx;
        public int cy;
    }

    [StructLayout( LayoutKind.Sequential )]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT( int x, int y )
        {
            X = x;
            Y = y;
        }
    }

    public enum GetWindowLongFields
    {
        GWL_USERDATA   = -21, // 0xFFFFFFEB
        GWL_EXSTYLE    = -20, // 0xFFFFFFEC
        GWL_STYLE      = -16, // 0xFFFFFFF0
        GWL_ID         = -12, // 0xFFFFFFF4
        GWL_HWNDPARENT = -8, // 0xFFFFFFF8
        GWL_HINSTANCE  = -6, // 0xFFFFFFFA
        GWL_WNDPROC    = -4 // 0xFFFFFFFC
    }

    [StructLayout( LayoutKind.Sequential )]
    public struct INPUT
    {
        public uint                    Type;
        public MOUSEKEYBDHARDWAREINPUT Data;
    }

    [StructLayout( LayoutKind.Explicit )]
    public struct MOUSEKEYBDHARDWAREINPUT
    {
        [FieldOffset( 0 )] public HARDWAREINPUT Hardware;
        [FieldOffset( 0 )] public KEYBDINPUT    Keyboard;
        [FieldOffset( 0 )] public MOUSEINPUT    Mouse;
    }

    [StructLayout( LayoutKind.Sequential )]
    public struct HARDWAREINPUT
    {
        public uint   Msg;
        public ushort ParamL;
        public ushort ParamH;
    }

    [StructLayout( LayoutKind.Sequential )]
    public struct KEYBDINPUT
    {
        public ushort Vk;
        public ushort Scan;
        public uint   Flags;
        public uint   Time;
        public IntPtr ExtraInfo;
    }

    [StructLayout( LayoutKind.Sequential )]
    public struct MOUSEINPUT
    {
        public int    X;
        public int    Y;
        public uint   MouseData;
        public uint   Flags;
        public uint   Time;
        public IntPtr ExtraInfo;
    }
}