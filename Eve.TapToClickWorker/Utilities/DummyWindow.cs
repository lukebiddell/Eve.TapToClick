using PInvoke;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Eve.TapToClickWorker.Utilities
{
    public class DummyWindow : IDisposable
    {
        private const string windowClassName = "Eve.TapToClick_DummyWindowClass";
        private IntPtr windowClassNameHGlobalUni;
        private short windowClassAtom;
        private IntPtr windowHandle;

        public DummyWindow()
        {
            windowClassNameHGlobalUni = Marshal.StringToHGlobalUni(windowClassName);

            CreateWindow();
        }

        private void CreateWindow()
        {
            unsafe
            {
                User32.WNDCLASSEX dummyWindowClass = new User32.WNDCLASSEX();
                dummyWindowClass.cbSize = Marshal.SizeOf<User32.WNDCLASSEX>();
                dummyWindowClass.hInstance = Process.GetCurrentProcess().Handle;
                dummyWindowClass.lpszClassName_IntPtr = windowClassNameHGlobalUni;
                dummyWindowClass.lpfnWndProc = DummyWindowWndProc;

                windowClassAtom = User32.RegisterClassEx(ref dummyWindowClass);
                if (windowClassAtom == 0 || Marshal.GetLastWin32Error() != 0)
                {

                }

                windowHandle = User32.CreateWindowEx(
                    0,
                    windowClassAtom,
                    "Eve.TapToClick_DummyWindow",
                    0,
                    0, 0, 0, 0,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero);
                if (windowHandle == IntPtr.Zero || Marshal.GetLastWin32Error() != 0)
                {

                }
            }
        }

        private void DestroyWindow()
        {
            if (windowClassNameHGlobalUni != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(windowClassNameHGlobalUni);
                windowClassNameHGlobalUni = IntPtr.Zero;
            }

            if (windowHandle != IntPtr.Zero)
            {
                User32.DestroyWindow(windowHandle);
                windowHandle = IntPtr.Zero;
            }
        }

        public void Dispose()
        {
            DestroyWindow();
        }

        private unsafe IntPtr DummyWindowWndProc(IntPtr hWnd, User32.WindowMessage msg, void* wParam, void* lParam)
        {
            switch (msg)
            {

            }

            return User32.DefWindowProc(hWnd, msg, new IntPtr(wParam), new IntPtr(lParam));
        }
    }
}
