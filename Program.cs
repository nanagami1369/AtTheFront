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
                // StartUp�ւ̓o�^����
                case _registerStartUpCommand:
                    try
                    {
                        var result = dialog.ShowDialog();
                        if (result == DialogResult.Cancel)
                        {
                            MessageBox.Show("�L�[�{�[�h�V���[�g�J�b�g���o�^����܂���ł���");
                            return;
                        }
                        StartUpManager.RegisterStartUp($"\"{Assembly.GetEntryAssembly().Location}\" \"{dialog.FormatHotKeyText}\"");
                        MessageBox.Show("�X�^�[�g�A�b�v�ɓo�^����܂���");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "�X�^�[�g�A�b�v�ւ̓o�^���ɃG���[���������܂���");
                    }
                    break;
                case _unRegisterStartUpCommand:
                    {
                        try
                        {
                            StartUpManager.UnRegisterStartUp();
                            MessageBox.Show("�X�^�[�g�A�b�v�����������܂���");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "�X�^�[�g�A�b�v�������ɃG���[���������܂���");
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
                                MessageBox.Show("�L�[�{�[�h�V���[�g�J�b�g���o�^����܂���ł���");
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
