using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace AtTheFront
{
    public class NoneForm : Form
    {
        private HotKeyManager _hotkeyManager = new HotKeyManager();
        private void Close_Click(object sender, EventArgs e)
        {
            _hotkeyManager.UnRegister(this);
            Application.Exit();
        }

        private void SetComponents()
        {
            NotifyIcon icon = new NotifyIcon();
            icon.Icon = Properties.Resources.app;
            icon.Visible = true;
            icon.Text = nameof(AtTheFront);
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = "&終了";
            menuItem.Click += new EventHandler(Close_Click);
            menu.Items.Add(menuItem);
            icon.ContextMenuStrip = menu;
        }

        private List<IntPtr> CurrentAtTheFrontWindowHandle { get; set; } = new List<IntPtr>();

        void ToFront()
        {
            IntPtr handle = NativeMethods.GetForegroundWindow();
            var isExists = CurrentAtTheFrontWindowHandle.Any(x => x == handle);
            if (isExists)
            {
                NativeMethods.UnSetAtTheFront(handle);
                CurrentAtTheFrontWindowHandle.Remove(handle);
            }
            else
            {
                NativeMethods.SetAtTheFront(handle);
                CurrentAtTheFrontWindowHandle.Add(handle);
            }
        }


        public NoneForm(string option)
        {
            ShowInTaskbar = false;
            SetComponents();
            try
            {
                Keys keys;
                if (string.IsNullOrWhiteSpace(option))
                {
                    keys = Keys.Shift | Keys.Insert;
                }
                else
                {
                    keys = SettingManager.StringToKeys(option);
                }
                _hotkeyManager.Register(this, keys, ToFront);
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(-1);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            _hotkeyManager.WndProcExecMethod(ref m);
        }

        private static class NativeMethods
        {
            static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
            static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
            const int SWP_SHOWWINDOW = 0x0040;
            const uint SWP_NOSIZE = 0x0001;
            const uint SWP_NOMOVE = 0x0002;

            // (x, y), (cx, cy)を無視するようにする.
            const uint TOPMOST_FLAGS = (SWP_NOSIZE | SWP_NOMOVE);

            public static bool SetAtTheFront(IntPtr handle)
                => SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

            public static bool UnSetAtTheFront(IntPtr handle)
                => SetWindowPos(handle, HWND_NOTOPMOST, 0, 0, 0, 0, (SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW));

            // Declare external functions.
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);
        }
    }
}
