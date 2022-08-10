using System;
using System.Windows.Forms;

namespace AtTheFront
{
    public static class SettingManager
    {
        private static bool KeysTryParse(string s, out Keys wd)
        {
            if (s == "Ctrl")
            {
                wd = Keys.Control;
                return true;
            }
            if (s == "Win" || s == "Windows")
            {
                wd = Keys.LWin;
                return true;
            }
            return Enum.TryParse(s, out wd) && Enum.IsDefined(typeof(Keys), wd);
        }

        public static Keys StringToKeys(string stringData)
        {
            if (stringData is null)
            {
                throw new NullReferenceException($"{nameof(stringData)} is null");
            }

            var keysString = stringData.Split('+');
            Keys keys;
            if (KeysTryParse(keysString[0], out var beginKey))
            {
                keys = beginKey;
            }
            else
            {
                throw new FormatException("変換出来ない文字列が検出されました。");
            }
            for (int index = 1; index < keysString.Length; index++)
            {
                if (KeysTryParse(keysString[index], out var key))
                {
                    keys = keys | key;
                }
                else
                {
                    throw new FormatException("変換出来ない文字列が検出されました。");
                }
            }
            return keys;
        }
    }
}
