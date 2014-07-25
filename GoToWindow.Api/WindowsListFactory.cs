﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace GoToWindow.Api
{
    /// <remarks>
    /// Thanks to Tommy Carlier for how to get the list of windows: http://blog.tcx.be/2006/05/getting-list-of-all-open-windows.html
    /// </remarks>
    public class WindowsListFactory
    {
        delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("USER32.DLL")]
        static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public static WindowsList Load()
        {
            IntPtr lShellWindow = GetShellWindow();
            var windows = new List<IWindow>();

            EnumWindows((IntPtr hWnd, int lParam) =>
            {
                if (hWnd == lShellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int lLength = GetWindowTextLength(hWnd);
                if (lLength == 0) return true;

                StringBuilder builder = new StringBuilder(lLength);
                GetWindowText(hWnd, builder, lLength + 1);

                uint processId;
                GetWindowThreadProcessId(hWnd, out processId);

                windows.Add(new Window
                {
                    HWnd = hWnd,
                    Title = builder.ToString(),
                    ProcessName = Process.GetProcessById((int)processId).ProcessName
                });

                return true;
            }, 0);

            return new WindowsList(windows);
        }
    }
}
