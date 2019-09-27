using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataObjectLayer.View.Win
{
    public static class MessageBoxConfirmation
    {
        public static bool Show(IWin32Window owner, string text)
        {
            return MessageBox.Show(owner, text.Replace("\\n","\n"), "Atenção !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}
