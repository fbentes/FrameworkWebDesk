using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataObjectLayer.View.Win
{
    public static class MessageBoxInformation
    {
        public static void Show(IWin32Window owner, string text)
        {
            MessageBox.Show(owner, text.Replace("\\n", "\n"), "Atenção !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
