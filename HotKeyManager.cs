using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AtTheFront
{
    public class HotKeyManager
    {
        /// <summary>
        /// HotKeyと紐づくID
        /// 固定値で適当に決める
        /// </summary>
        private readonly int _hotKeyId = 0xBFEE;
        private const int WM_HOTKEY = 0x0312;
        private Action _execCommand;

        public void Register(Form form, Keys keys, Action execCommand)
        {
            KeyModifier keyModifier = KeyModifier.NONE;
            if (keys.HasFlag(Keys.Control) || keys.HasFlag(Keys.ControlKey) || keys.HasFlag(Keys.LControlKey) || keys.HasFlag(Keys.RControlKey))
            {
                keyModifier |= KeyModifier.MOD_CONTROL;
            }
            if (keys.HasFlag(Keys.Alt))
            {
                keyModifier |= KeyModifier.MOD_ALT;
            }
            if (keys.HasFlag(Keys.Shift) || keys.HasFlag(Keys.ShiftKey) || keys.HasFlag(Keys.LShiftKey) || keys.HasFlag(Keys.RShiftKey))
            {
                keyModifier |= KeyModifier.MOD_SHIFT;
            }
            if (keys.HasFlag(Keys.LWin) || keys.HasFlag(Keys.RWin))
            {
                keyModifier |= KeyModifier.MOD_WIN;
            }
            var key = keys & ~Keys.Modifiers;
            _execCommand = execCommand;
            NativeMethods.RegisterHotKey(form.Handle, _hotKeyId, (int)keyModifier, key.GetHashCode());
        }

        public void UnRegister(Form form)
        {
            NativeMethods.UnregisterHotKey(form.Handle, _hotKeyId);
        }



        public void WndProcExecMethod(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && _execCommand is Action action)
            {
                action();
            }
        }
        private static class NativeMethods
        {
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        }

        [Flags]
        private enum KeyModifier
        {
            NONE = 0,
            MOD_ALT = 1,
            MOD_CONTROL = 2,
            MOD_SHIFT = 4,
            MOD_WIN = 8
        }
    }
}
