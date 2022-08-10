﻿using System;
using System.Windows.Forms;

namespace AtTheFront
{
    public static class StringKeysParser
    {
        private static bool KeysTryParse(string s, out Keys key)
        {
            if (Enum.TryParse(s, out key) && Enum.IsDefined(typeof(Keys), key))
            {
                key &= ~Keys.Modifiers;
                return (key != Keys.None);
            }
            else
            {
                return false;
            }
        }

        private static bool KeysModifireParse(string s, out KeyModifier keyModifier)
        {
            if (s == "Shift")
            {
                keyModifier = KeyModifier.MOD_SHIFT;
                return true;
            }
            else if (s == "Ctrl")
            {
                keyModifier = KeyModifier.MOD_CONTROL;
                return true;
            }
            else if (s == "Alt")
            {
                keyModifier = KeyModifier.MOD_ALT;
                return true;
            }
            else if (s == "Win" || s == "Windows")
            {
                keyModifier = KeyModifier.MOD_WIN;
                return true;
            }
            else
            {
                keyModifier = KeyModifier.NONE;
                return false;
            }
        }

        public static (KeyModifier, Keys) StringToKeys(string KeysString)
        {
            if (KeysString is null)
            {
                throw new NullReferenceException($"{nameof(KeysString)} is null");
            }

            var keyStrings = KeysString.Split('+');
            // 修飾キー以外を取得
            var keyString = keyStrings[keyStrings.Length - 1];
            if (!KeysTryParse(keyString, out var inputKey))
            {
                throw new FormatException("変換出来ない文字列が検出されました。");
            }
            // 修飾キー以外が無ければ終了
            if (keyString.Length == 1)
            {
                return (KeyModifier.NONE, inputKey);
            }
            //修飾キーを判定
            KeyModifier keyModifier = KeyModifier.NONE;
            for (int index = 0; index < keyStrings.Length - 1; index++)
            {
                if (KeysModifireParse(keyStrings[index], out var inputKeyModifier))
                {
                    keyModifier |= inputKeyModifier;
                }
                else
                {
                    throw new FormatException("変換出来ない文字列が検出されました。");
                }
            }
            return (keyModifier, inputKey);
        }
    }
}