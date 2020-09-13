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
    public partial class Form_Setting : Form
    {
        public Form_Setting()
        {
            InitializeComponent();
        }

        private void FillComboBox()
        {
            
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;
            var converter = new KeysConverter();
            textBox1.Text = converter.ConvertToString(e.KeyData);
        }
    }
}
