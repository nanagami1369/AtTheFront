using System;
using System.Windows.Forms;
namespace AtTheFront
{
    public class NoneForm : Form
    {
        private HotKeyManager _hotkeyManager = new HotKeyManager();
        private void Close_Click(object sender, EventArgs e)
        {
            _hotkeyManager.UnRegister(this);
            Application.Exit();
        }

        private void SetComponents()
        {
            NotifyIcon icon = new NotifyIcon();
            icon.Icon = Properties.Resources.app;
            icon.Visible = true;
            icon.Text = nameof(AtTheFront);
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = "&終了";
            menuItem.Click += new EventHandler(Close_Click);
            menu.Items.Add(menuItem);
            icon.ContextMenuStrip = menu;
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


        public NoneForm(string option)
        {
            ShowInTaskbar = false;
            SetComponents();
            try
            {
                (KeyModifier, Keys) keys;
                if (string.IsNullOrWhiteSpace(option))
                {
                    keys = (KeyModifier.MOD_WIN, Keys.Insert);
                }
                else
                {
                    keys = SettingManager.StringToKeys(option);
                }
                var (keyModifier, key) = keys;
                _hotkeyManager.Register(this, keyModifier, key, ToFront);
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(-1);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            _hotkeyManager.WndProcExecMethod(ref m);
        }

    }
}
