using System;
using System.Windows.Forms;
namespace AtTheFront
{
    public class NoneForm : Form
    {
        private readonly HotKeyManager _hotkeyManager = new HotKeyManager();
        private void Close_Click(object sender, EventArgs e)
        {
            _hotkeyManager.UnRegister(this);
            Application.Exit();
        }

        private static void ToFront()
        {
            try
            {
                var handle = WindowManager.GetForegroundWindow();
                var isAtTheFront = WindowManager.IsAtTheFrontForWindow(handle);
                if (isAtTheFront)
                {
                    WindowManager.UnSetAtTheFront(handle);
                }
                else
                {
                    WindowManager.SetAtTheFront(handle);
                }
            }
            catch (WindowManagerException e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public NoneForm(KeyModifier keyModifier, Keys key)
        {
            ShowInTaskbar = false;
            var icon = new NotifyIcon();
            icon.Icon = Properties.Resources.app;
            icon.Visible = true;
            icon.Text = nameof(AtTheFront);
            var menu = new ContextMenuStrip();
            // 終了メニュー
            var exitMenuItem = new ToolStripMenuItem();
            exitMenuItem.Text = "&終了";
            exitMenuItem.Click += new EventHandler(Close_Click);
            menu.Items.Add(exitMenuItem);
            icon.ContextMenuStrip = menu;

            _hotkeyManager.Register(this, keyModifier, key, ToFront);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            _hotkeyManager.WndProcExecMethod(ref m);
        }

    }
}
