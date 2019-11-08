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
    public partial class Marki : Form
    {
        public Marki()
        {
            InitializeComponent();

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

        private void label4_Click(object sender, EventArgs e)
        {
            if (list2.SelectedItem == null) return;

            if (MessageBox.Show("Удалить выбранную модель?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                new SQLiteCommand(string.Format("DELETE FROM model WHERE id={0};", (list2.SelectedItem as Item).id), SQL.Connection).ExecuteNonQuery();
                LoadModels();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (list1.SelectedItem == null) return;

            if (MessageBox.Show("Удалить выбранную марку и все её модели?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                new SQLiteCommand(string.Format("DELETE FROM model WHERE mark_id={0}; DELETE FROM marka WHERE id={0};", (list1.SelectedItem as Item).id), SQL.Connection).ExecuteNonQuery();
                LoadMark();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            AddFom frm = new AddFom("Новая модель");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                new SQLiteCommand(string.Format("INSERT INTO model (model, mark_id) VALUES ('{0}', {1});", frm.Tag, (list1.SelectedItem as Item).id), SQL.Connection).ExecuteNonQuery();
                LoadModels();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            AddFom frm = new AddFom("Новая марка");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                new SQLiteCommand(string.Format("INSERT INTO marka (mark) VALUES ('{0}');", frm.Tag), SQL.Connection).ExecuteNonQuery();
                LoadMark();
            }
        }
    }
}
