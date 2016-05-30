using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ClipM
{
    public sealed class ClipboardMonitor : IDisposable
    {

        private static class NativeMethods
        {
            /// <summary>
            /// Places the given window in the system-maintained clipboard format listerner list
            /// </summary>
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AddClipboardFormatListener(IntPtr hwnd);

            /// <summary>
            /// Removes the given window from the system-maintained clipboard format listener list
            /// </summary>
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

            /// <summary>
            /// Retrieves the clipboard sequence number for the current window station.
            /// </summary>
            /// <returns></returns>
            [DllImport("user32.dll", SetLastError = true)]
            public static extern uint GetClipboardSequenceNumber();

            /// <summary>
            /// Registers a system-wide hot key
            /// </summary>
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, int vk);

            /// <summary>
            /// Unregisters a system-wide hot key
            /// </summary>

            /// <summary>
            /// Send when the contents of the clipboard have changed
            /// </summary>
            public const int WM_CLIPBOARDUPDATE = 0x031D;
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnregisterHotKey(IntPtr hwnd, int id);

            /// <summary>
            /// Sent when the user presses a hot key
            /// </summary>
            public const int WM_HOTKEY = 0x0312;

            public const int MOD_ALT        = 0x0001;
            public const int MOD_CONTROL    = 0x0002;
            public const int MOD_SHIFT      = 0x0004;
            public const int MOD_WIN        = 0x0008;
            public const int MOD_NOREPEAT   = 0x0400;
            public const int WKEY = 0x57;

            /// <summary>
            /// To find message-only windows, specify HWND_MESSAGE in the hwndParent parameter of the FindWindowEx function
            /// </summary>
            public static IntPtr HWND_MESSAGE = new IntPtr(-3);
        }

        private HwndSource hwndSource = new HwndSource(0, 0, 0, 0, 0, null, NativeMethods.HWND_MESSAGE);

        public static int HOTKEY_ID = 1;

        public ClipboardMonitor()
        {
            hwndSource.AddHook(WndProc);
            NativeMethods.AddClipboardFormatListener(hwndSource.Handle);

            NativeMethods.RegisterHotKey(hwndSource.Handle, HOTKEY_ID, NativeMethods.MOD_WIN, NativeMethods.WKEY);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if(msg == NativeMethods.WM_CLIPBOARDUPDATE)
            {
                ClipboardContentChanged?.Invoke(this, new ClipboardChangedEventArgs(NativeMethods.GetClipboardSequenceNumber()));
            }

            if(msg == NativeMethods.WM_HOTKEY)
            {
                HotKeyPressed?.Invoke(this, EventArgs.Empty);
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Occurs when the clipboard content changes
        /// </summary>
        public event EventHandler<ClipboardChangedEventArgs> ClipboardContentChanged;

        /// <summary>
        /// Occurs when the hot key is pressed
        /// </summary>
        public event EventHandler<EventArgs> HotKeyPressed;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    NativeMethods.RemoveClipboardFormatListener(hwndSource.Handle);
                    NativeMethods.UnregisterHotKey(hwndSource.Handle, HOTKEY_ID);
                    hwndSource.RemoveHook(WndProc);
                    hwndSource.Dispose();
                }

                disposedValue = true;
            }
        }

        // ~ClipboardMonitor() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }

    public class ClipboardChangedEventArgs : EventArgs
    {
        public uint seqNo { get; set; }
        public ClipboardChangedEventArgs(uint seqNo)
        {
            this.seqNo = seqNo;
        }
    }
}
