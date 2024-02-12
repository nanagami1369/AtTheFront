using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AtTheFront.Util;

public class HotKeyManager
{
    /// <summary>
    /// HotKeyと紐づくID
    /// 固定値で適当に決める
    /// </summary>
    private readonly int _hotKeyId = 0xBFEE;
    private const int WM_HOTKEY = 0x0312;
    private Action _execCommand;

    public void Register(Form form, KeyModifier keyModifier, Keys key, Action execCommand)
    {
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
}
