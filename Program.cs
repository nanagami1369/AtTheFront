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
            var dialog = new HotKeyDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("キーボードショートカットが登録されませんでした");
                return;
            }
            var app = new NoneForm(dialog.Modifier, dialog.Key);
            Application.Run();
        }
    }
}
