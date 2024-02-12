using AtTheFront.Util;
using System;
using System.Text;
using System.Windows.Forms;

namespace AtTheFront;

public partial class HotKeyDialog : Form
{
    public HotKeyDialog()
    {
        InitializeComponent();
    }

    private bool _isClosedProperly = false;
    private bool _isHoldWinKey = false;

    public KeyModifier Modifier { get; private set; } = KeyModifier.NONE;
    public Keys Key { get; private set; } = Keys.None;
    public string FormatHotKeyText { get; private set; } = "";
    private void InputHotKeyBox_KeyDown(object sender, KeyEventArgs e)
    {
        var keyCode = e.KeyCode & ~Keys.Modifiers;
        // windowキーの押し判定
        if (keyCode is Keys.LWin || keyCode is Keys.RWin)
        {
            _isHoldWinKey = true;
        }
        // 修飾キーの押し状態を記録
        Modifier = KeyModifier.NONE;
        Modifier |= _isHoldWinKey ? KeyModifier.MOD_WIN : KeyModifier.NONE;
        Modifier |= e.Control ? KeyModifier.MOD_CONTROL : KeyModifier.NONE;
        Modifier |= e.Shift ? KeyModifier.MOD_SHIFT : KeyModifier.NONE;
        Modifier |= e.Alt ? KeyModifier.MOD_ALT : KeyModifier.NONE;
        // キーの押し状態を記録(修飾キーはキーとして記録しない)
        if (keyCode == Keys.ShiftKey ||
            keyCode == Keys.ControlKey ||
            keyCode == Keys.LWin ||
            keyCode == Keys.RWin ||
            keyCode == Keys.Menu)
        {
            Key = Keys.None;
        }
        else
        {
            Key = keyCode;
        }

        var hotKeyText = new StringBuilder();
        hotKeyText
            .Append(_isHoldWinKey ? "Win+" : "")
            .Append(e.Control ? "Ctrl+" : "")
            .Append(e.Shift ? "Shift+" : "")
            .Append(e.Alt ? "Alt+" : "")
            .Append(Key != Keys.None ? Key.ToString() : "");
        InputHotKeyBox.Text = hotKeyText.ToString();
        FormatHotKeyText = hotKeyText.ToString();
    }

    private void InputHotKeyBox_KeyUp(object sender, KeyEventArgs e)
    {
        var key = e.KeyCode & ~Keys.Modifiers;
        // windowキーの押し判定
        if (key is Keys.LWin || key is Keys.RWin)
        {
            _isHoldWinKey = false;
        }
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
        if (Key == Keys.None)
        {
            if (Modifier == KeyModifier.NONE)
            {
                MessageBox.Show("キーが選択されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                MessageBox.Show("修飾キーしか選択されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return;
        }
        _isClosedProperly = true;
        DialogResult = DialogResult.OK;
        Close();
        _isClosedProperly = false;
    }

    private void HotKeyDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!_isClosedProperly && e.CloseReason == CloseReason.UserClosing)
        {
            if (MessageBox.Show("キーの登録をやめますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            Modifier = KeyModifier.NONE;
            Key = Keys.None;
            DialogResult = DialogResult.Cancel;
        }
    }
}