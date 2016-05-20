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
            /// Send when the contents of the clipboard have changed
            /// </summary>
            public const int WM_CLIPBOARDUPDATE = 0x031D;

            /// <summary>
            /// To find message-only windows, specify HWND_MESSAGE in the hwndParent parameter of the FindWindowEx function
            /// </summary>
            public static IntPtr HWND_MESSAGE = new IntPtr(-3);
        }

        private HwndSource hwndSource = new HwndSource(0, 0, 0, 0, 0, null, NativeMethods.HWND_MESSAGE);

        public ClipboardMonitor()
        {
            hwndSource.AddHook(WndProc);
            NativeMethods.AddClipboardFormatListener(hwndSource.Handle);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if(msg == NativeMethods.WM_CLIPBOARDUPDATE)
            {
                ClipboardContentChanged?.Invoke(this, new ClipboardChangedEventArgs(NativeMethods.GetClipboardSequenceNumber()));
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Occurs when the clipboard content changes
        /// </summary>
        public event EventHandler<ClipboardChangedEventArgs> ClipboardContentChanged;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    NativeMethods.RemoveClipboardFormatListener(hwndSource.Handle);
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
