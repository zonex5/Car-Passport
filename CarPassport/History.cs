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
    public partial class History : Form
    {
        public History(object caption, object id)
        {
            InitializeComponent();

            Text += caption.ToString();

            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM repairs WHERE idcard={0};", id), SQL.Connection);
            foreach (DbDataRecord record in cmd.ExecuteReader())
            {
                grid.Rows.Add(new object[] { record["date"], record["km"], record["description"] });
            }
        }
    }
}
