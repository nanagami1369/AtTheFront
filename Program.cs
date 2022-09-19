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
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var option = args.Length != 0 ? args[0] : "";
            var dialog = new HotKeyDialog();

            switch (option)
            {
                // StartUpへの登録処理
                case _registerStartUpCommand:
                    try
                    {
                        var result = dialog.ShowDialog();
                        if (result == DialogResult.Cancel)
                        {
                            MessageBox.Show("キーボードショートカットが登録されませんでした");
                            return;
                        }
                        StartUpManager.RegisterStartUp($"\"{Assembly.GetEntryAssembly().Location}\" \"{dialog.FormatHotKeyText}\"");
                        MessageBox.Show("スタートアップに登録されました");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "スタートアップへの登録時にエラーが発生しました");
                    }
                    break;
                case _unRegisterStartUpCommand:
                    {
                        try
                        {
                            StartUpManager.UnRegisterStartUp();
                            MessageBox.Show("スタートアップから解除されました");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "スタートアップ解除時にエラーが発生しました");
                        }
                        break;
                    }
                default:
                    {
                        if (string.IsNullOrEmpty(option))
                        {
                            var result = dialog.ShowDialog();
                            if (result == DialogResult.Cancel)
                            {
                                MessageBox.Show("キーボードショートカットが登録されませんでした");
                                return;
                            }
                            var app = new NoneForm(dialog.Modifier, dialog.Key);
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
        }
    }
}
