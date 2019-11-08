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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            LoadCards();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (new Card().ShowDialog() == DialogResult.OK)
                LoadCards();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            new Marki().ShowDialog();
        }

        private void LoadCards()
        {
            grid.Rows.Clear();

            var sql = @"SELECT cards.id, cards.number, 
                        marka.mark || ' ' || model.model as mrk, cards.engine, 
            (SELECT repairs.date FROM repairs 
                WHERE idcard = cards.id ORDER BY id DESC LIMIT 1) as last,
            (SELECT repairs.km FROM repairs 
                WHERE idcard = cards.id ORDER BY id DESC LIMIT 1) as km 
            FROM cards INNER JOIN marka ON (cards.mark = marka.id) 
            INNER JOIN model ON(cards.model = model.id);";

            SQLiteCommand cmd = new SQLiteCommand(sql, SQL.Connection);
            foreach (DbDataRecord record in cmd.ExecuteReader())
            {
                grid.Rows.Add(new object[] {
                    record["id"], record["number"],
                    record["mrk"], record["engine"],
                    record["last"], record["km"] });
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow == null) return;

            if (new Repair(grid.CurrentRow.Cells[1].Value.ToString(), grid.CurrentRow.Cells[0].Value.ToString()).ShowDialog() == DialogResult.OK)
                LoadCards();
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.CurrentRow == null) return;

            label1.Text = grid.CurrentRow.Cells[1].Value.ToString();

            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT proper, tel, (SELECT repairs.description FROM repairs WHERE idcard=cards.id ORDER BY id DESC LIMIT 1) as last, (SELECT count(*) FROM repairs WHERE idcard=cards.id) as cnt FROM cards WHERE id={0};", grid.CurrentRow.Cells[0].Value), SQL.Connection);
            foreach (DbDataRecord record in cmd.ExecuteReader())
            {
                label4.Text = record["proper"].ToString();
                label6.Text = record["tel"].ToString();
                label5.Text = record["cnt"].ToString();
                textBox2.Text = record["last"].ToString().Length > 0 ? record["last"].ToString() : "Не обслуживался";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow == null) return;

            new History(grid.CurrentRow.Cells[1].Value, grid.CurrentRow.Cells[0].Value).ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }
    }
}
