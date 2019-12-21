using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerEx_Assistant
{
    public partial class InputCoordName : Form
    {
        public InputCoordName()
        {
            InitializeComponent();
            
    }
        public delegate void TextEventHandler(string strText);

        public TextEventHandler TextHandler;

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (null != TextHandler)
            {
                TextHandler.Invoke(Namebox.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Namebox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Keys.Enter == (Keys)e.KeyChar)
            {
                if (null != TextHandler)
                {
                    TextHandler.Invoke(Namebox.Text);
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
