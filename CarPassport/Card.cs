using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarPassport
{
    public partial class Card : Form
    {
        public Card()
        {
            InitializeComponent();
            comboBox3.SelectedIndex = 0;

            LoadMark();
        }

        private void LoadMark()
        {
            list1.Items.Clear();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM marka ORDER BY mark;", SQL.Connection);
            foreach (DbDataRecord record in cmd.ExecuteReader())
            {
                list1.Items.Add(new Item(record["id"], record["mark"]));
            }

            list1.SelectedIndex = 0;
        }

        private void LoadModels()
        {
            list2.Items.Clear();
            if (list1.SelectedItem == null) return;

            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM model WHERE mark_id={0} ORDER BY model;", (list1.SelectedItem as Item).id), SQL.Connection);
            foreach (DbDataRecord record in cmd.ExecuteReader())
            {
                list2.Items.Add(new Item(record["id"], record["model"]));
            }
        }

        private void list1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadModels();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Не указан номерной знак.");
                return;
            }
            if (list2.SelectedIndex < 0)
            {
                MessageBox.Show("Не указана модель.");
                return;
            }
            if (maskedTextBox1.Text.Length < 1)
            {
                MessageBox.Show("Не указан номерной знак.");
                return;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("INSERT INTO cards (number, mark, model, engine, proper, tel) " +
                "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');", textBox1.Text, (list1.SelectedItem as Item).id, (list2.SelectedItem as Item).id, comboBox3.Text + ", " + maskedTextBox1.Text, textBox2.Text, textBox3.Text), SQL.Connection);
            cmd.ExecuteNonQuery();

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
