using Microsoft.Win32;
using System.Reflection;

namespace AtTheFront
{
    public static class StartUpManager
    {
        public const string keyName = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public static void RegisterStartUp(string value)
        {
            using (var key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                key.SetValue(nameof(AtTheFront), value);
            }
        }

        public static void UnRegisterStartUp() {
            using (var key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                key.DeleteValue(nameof(AtTheFront), true);
            }
        }

        public static bool IsStartUp()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(keyName))
            {
                return key.GetValue(nameof(AtTheFront)) != null;
            }
        }
    }
}
