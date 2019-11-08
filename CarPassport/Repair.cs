using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarPassport
{
    public partial class Repair : Form
    {
        private string id;

        public Repair(string v, string id)
        {
            InitializeComponent();

            this.id = id;

            textBox1.Text = DateTime.Now.ToLongDateString();
            Text += v;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Необходимо заполнить описание обслуживания", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (textBox3.Text.Length == 0)
            {
                MessageBox.Show("Необходимо указать текущий пробег", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int n;
            if(!Int32.TryParse(textBox3.Text, out n))
            {
                MessageBox.Show("Текущий пробег должен быть числом.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("INSERT INTO repairs (idcard, date, description, km) VALUES ('{0}','{1}','{2}','{3}');", id, DateTime.Now.ToShortDateString(), textBox2.Text, textBox3.Text), SQL.Connection);
            cmd.ExecuteNonQuery();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
