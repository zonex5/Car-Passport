using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarPassport
{
    public partial class AddFom : Form
    {
        public AddFom(string caption)
        {
            InitializeComponent();

            Text = caption;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Ведите значение.");
                return;
            }

            Tag = textBox1.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
