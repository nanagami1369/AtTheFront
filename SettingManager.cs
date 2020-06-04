using System;
using System.Windows.Forms;

namespace AtTheFront
{
    public static class SettingManager
    {
        private static bool KeysTryParse(string s, out Keys wd)
        {
            return Enum.TryParse(s, out wd) && Enum.IsDefined(typeof(Keys), wd);
        }

        public static Keys StringToKeys(string stringData)
        {
            if (string.IsNullOrWhiteSpace(stringData))
            {
                return Keys.Shift | Keys.Insert;
            }
            var keysString = stringData.Split('+');
            Keys keys;
            if (KeysTryParse(keysString[0], out var furstKey))
            {
                keys = furstKey;
            }
            else
            {
                throw new Exception("変換出来ない文字列が検出されました。");
            }
            for (int index = 1; index < keysString.Length; index++)
            {
                if (KeysTryParse(keysString[index], out var key))
                {
                    keys = keys | key;
                }
                else
                {
                    throw new Exception("変換出来ない文字列が検出されました。");
                }
            }
            return keys;
        }
    }
}
