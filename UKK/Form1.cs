using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UKK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                    dataGridView.DataSource = databarangBindingSource;
                    this.databarangTableAdapter.Fill(this.appData.Databarang);
                    //databarangBindingSource.DataSource = this.appData.Databarang;
            }
                else
                {
                    var query = from o in this.appData.Databarang
                                where o.Nama.Contains(txtSearch.Text) || o.Jumlah == txtSearch.Text || o.Harga.Contains(txtSearch.Text)
                                select o;
                    databarangBindingSource.DataSource = query.ToList();
                    //dataGridView.DataSource = query.ToList();
                }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    databarangBindingSource.RemoveCurrent();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtNama.Focus();
                this.appData.Databarang.AddDatabarangRow(this.appData.Databarang.NewDatabarangRow());
                databarangBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                databarangBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;;
            txtNama.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yakin untuk menghapus ?", "Messasge", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            databarangBindingSource.RemoveCurrent();
       
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                databarangBindingSource.EndEdit();
                databarangTableAdapter.Update(this.appData.Databarang);
                panel.Enabled = false;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                databarangBindingSource.ResetBindings(false);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.Databarang' table. You can move, or remove it, as needed.
            this.databarangTableAdapter.Fill(this.appData.Databarang);
            databarangBindingSource.DataSource = this.appData.Databarang;

        }
    }
}
