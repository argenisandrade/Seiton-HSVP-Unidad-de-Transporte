using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;

namespace Seiton
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ordenDeMovilizaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.MdiParent = this;
            frm.Show();
        }

        private void autorizaciónDeSalidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.MdiParent = this;
            frm.Show();
        }

        private void informeDeMovilizaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
