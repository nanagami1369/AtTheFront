using System;
using System.Reflection;
using System.Windows.Forms;
using AtTheFront.Util;

namespace AtTheFront
{
    static class Program
    {
        private const string _registerStartUpCommand = "startUpRegister";
        private const string _unRegisterStartUpCommand = "startUpUnRegister";
        private static HotKeyDialog _dialog = new HotKeyDialog();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var option = args.Length != 0 ? args[0] : "";

            switch (option)
            {
                // StartUpへの登録処理
                case _registerStartUpCommand:
                    RegisterStartUp();
                    break;
                case _unRegisterStartUpCommand:
                    UnRegisterStartUp();
                    break;
                default:
                    if (string.IsNullOrEmpty(option))
                    {
                        var result = _dialog.ShowDialog();
                        if (result == DialogResult.Cancel)
                        {
                            MessageBox.Show("キーボードショートカットが登録されませんでした");
                            return;
                        }
                        var app = new NoneForm(_dialog.Modifier, _dialog.Key);
                    }
                    else
                    {
                        try
                        {
                            var (modifier, key) = StringKeysParser.StringToKeys(option);
                            var app = new NoneForm(modifier, key);
                        }
                        catch (FormatException e)
                        {
                            MessageBox.Show(e.Message);
                            return;
                        }
                    }
                    Application.Run();
                    break;
            }
        }

        private static void RegisterStartUp()
        {
            try
            {
                var result = _dialog.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    MessageBox.Show("キーボードショートカットが登録されませんでした");
                    return;
                }
                StartUpManager.RegisterStartUp($"\"{Assembly.GetEntryAssembly().Location}\" \"{_dialog.FormatHotKeyText}\"");
                MessageBox.Show("スタートアップに登録されました");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "スタートアップへの登録時にエラーが発生しました");
            }
        }

        private static void UnRegisterStartUp() {
            try
            {
                StartUpManager.UnRegisterStartUp();
                MessageBox.Show("スタートアップから解除されました");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "スタートアップ解除時にエラーが発生しました");
            }
        }
    }
}
