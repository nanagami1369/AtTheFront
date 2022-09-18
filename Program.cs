using System;
using System.Windows.Forms;

namespace AtTheFront
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var hotKeyString = args.Length != 0 ? args[0] : null;
            try
            {
                (KeyModifier, Keys) keys;
                if (string.IsNullOrWhiteSpace(hotKeyString))
                {
                    keys = (KeyModifier.MOD_WIN, Keys.Insert);
                }
                else
                {
                    keys = StringKeysParser.StringToKeys(hotKeyString);
                }
                var (keyModifier, key) = keys;
                var app = new NoneForm(keyModifier, key);
                Application.Run();
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
    }
}
