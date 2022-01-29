using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;

namespace Seiton
{
    public partial class Form5 : Form
    {
        public static NpgsqlConnection cn = new NpgsqlConnection();
        

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string str = "Server = 127.0.0.1; Port=5432; Database=Vehículos; User Id=postgres; Password=1234;";

            cn.ConnectionString = str;
            cn.Open();
        }

        private void main_insertBtn_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Form1")
                {
                    Isopen = true;
                    f.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Form1 frm = new Form1();
                //frm.MdiParent = this;
                frm.Show();
            }
            
        }

        private void main_informBtn_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Form4")
                {
                    Isopen = true;
                    f.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Form4 frm = new Form4();
                //frm.MdiParent = this;
                frm.Show();
            }
            
        }

        private void main_autBtn_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Form2")
                {
                    Isopen = true;
                    f.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Form2 frm = new Form2();
                //frm.MdiParent = this;
                frm.Show();
            }
        }
    }
}
