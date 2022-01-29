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
    public partial class Form2 : MaterialSkin.Controls.MaterialForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            aut_num_textBox.AutoCompleteCustomSource = CargarCods();
            aut_num_textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            aut_num_textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;



        }
        private AutoCompleteStringCollection CargarCods()
        {
            AutoCompleteStringCollection codOrdenMov = new AutoCompleteStringCollection();

            // query para asegurar que no haya una autorizacion ya insertada con ese codigo 
            String str;
            str = "SELECT num FROM entrada_salida.info_solicitud I ";
            str = str + " WHERE estado = 'APROBADA' and NOT EXISTS( ";
            str = str + " SELECT FROM   entrada_salida.autorizacion ";
            str = str + " WHERE  num = I.num) order by num asc; ";
            NpgsqlCommand cmd_codOM = new NpgsqlCommand();
            cmd_codOM.CommandText = str;
            cmd_codOM.Connection = Form5.cn;
            NpgsqlDataReader codOM = cmd_codOM.ExecuteReader();
            while (codOM.Read())
            {
                codOrdenMov.Add(codOM[0].ToString());
                //om_cie_comboBox.Items.Add(codOM[0].ToString());
            }
            codOM.Close();

            return codOrdenMov;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void aut_labelMotivo_Click(object sender, EventArgs e)
        {

        }

        private void aut_num_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void aut_kmSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void aut_kmEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void aut_numVehiComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void aut_motivoComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void aut_num_textBox_TextChanged(object sender, EventArgs e)
        {
            aut_loadBtn.Enabled = true;
        }

        private void aut_loadBtn_Click(object sender, EventArgs e)
        {
            string str1;

            str1 = "select * from entrada_salida.orden_mov O natural join ";
            str1 = str1 + "entrada_salida.info_solicitud I inner join ";
            str1 = str1 + "entrada_salida.vehiculo V on I.vehiculo = V.cod_inst inner join ";
            str1 = str1 + "entrada_salida.conductor C on I.conductor = C.cedula ";
            str1 = str1 + "where O.num = " + aut_num_textBox.Text;
            str1 = str1 + "and I.estado = 'APROBADA'";
            str1 = str1 + "and NOT EXISTS( ";
            str1 = str1 + " SELECT FROM   entrada_salida.autorizacion ";
            str1 = str1 + " WHERE  num = I.num) order by I.num asc; "; 

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.CommandText = str1;
            cmd.Connection = Form5.cn;
            NpgsqlDataReader load_info = cmd.ExecuteReader();
            load_info.Read();
            try
            {
                aut_HsalidaTbox.Text = load_info[12].ToString();
                aut_HentradaTbox.Text = load_info[16].ToString();
                aut_codVehiTbox.Text = load_info[23].ToString();
                aut_numVehiTBox.Text = load_info[25].ToString();
                aut_codConducTbox.Text = load_info[36].ToString();
                aut_nomCondTbox.Text = load_info[37].ToString();
                aut_fecha.Text = load_info[1].ToString();
                aut_solicitante.Text = load_info[2].ToString();
                aut_Asunto.Text = load_info[4].ToString();

                aut_kmEntrada.Enabled = true;
                aut_kmSalida.Enabled = true;
                aut_motivoComboBox.Enabled = true;
                aut_guardarBtn.Enabled = true;

            }
            catch (Exception ex)
            {
                aut_HsalidaTbox.Text = "";
                aut_HentradaTbox.Text = "";
                aut_numVehiTBox.Text = "";
                aut_fecha.Text = "";
                aut_solicitante.Text = "";
                aut_Asunto.Text = "";
                aut_codConducTbox.Text = "";
                aut_nomCondTbox.Text = "";
                aut_kmEntrada.Enabled = false;
                aut_kmEntrada.Text = "";
                aut_kmSalida.Enabled = false;
                aut_kmSalida.Text = "";
                aut_motivoComboBox.Enabled = false;
                aut_motivoComboBox.Text = "";
                aut_guardarBtn.Enabled = false;
                MessageBox.Show("No existe una Orden de movilización Aprobada con el número que se intenta buscar o ya hay una Autorización de Salida guardada con dicho número.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                load_info.Close();
                
            }
            load_info.Close();

        }

        private void aut_motivoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aut_motivoComboBox.SelectedIndex == 0 )
            {
                aut_codMotivTbox.Text = "INSTITUC"; 
            }
            else
            {
                aut_codMotivTbox.Text = "MECANIC";
            }
        }

        private void aut_guardarBtn_Click(object sender, EventArgs e)
        {
            if (aut_motivoComboBox.Text == "")
            {
                MessageBox.Show("Elejir el motivo en la celda junto a la palabra 'Para'",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (aut_kmEntrada.Text == "" || aut_kmSalida.Text == "")
            {
                MessageBox.Show("No dejar kilometrajes en blanco", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Int64.Parse(aut_kmEntrada.Text) <= Int64.Parse(aut_kmSalida.Text))
                {
                    MessageBox.Show("El kilometraje de entrada no puede ser menor o igual al Kilometraje de salida",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        String str1;

                        str1 = "insert into entrada_salida.autorizacion values ( ";
                        str1 = str1 +       aut_num_textBox.Text         + ", ";
                        str1 = str1 + "'" + aut_fecha.Text         + "'" + ", ";
                        str1 = str1 + "'" + aut_HsalidaTbox.Text   + "'" + ", ";
                        str1 = str1 + "'" + aut_HentradaTbox.Text  + "'" + ", ";
                        str1 = str1 +       aut_kmSalida.Text            + ", ";
                        str1 = str1 +       aut_kmEntrada.Text           + ", ";
                        str1 = str1 +       aut_codVehiTbox.Text         + ", ";
                        str1 = str1 + "'" + aut_codConducTbox.Text + "'" + ", ";
                        str1 = str1 + "'" + aut_codMotivTbox.Text  + "'" + ", ";
                        str1 = str1 + "'" + aut_solicitante.Text   + "'" + ", ";
                        str1 = str1 + "'" + aut_Asunto.Text        + "'" + ");";


                        NpgsqlCommand cmd1 = new NpgsqlCommand();
                        cmd1.CommandText = str1;
                        cmd1.Connection = Form5.cn;
                        cmd1.ExecuteNonQuery();

                        MessageBox.Show("Registro Insertado");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            
        }
    }
}
