using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace AtTheFront
{
    public class NoneForm : Form
    {
        private void Close_Click(object sender, EventArgs e)
        {
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
        public NoneForm()
        {
            ShowInTaskbar = false;
            SetComponents();
        }

    }
}
