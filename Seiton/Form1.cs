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
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Llenado de combobox con nacionalidades
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.CommandText = "select GENTILICIO_NAC from entrada_salida.nacionalidad ORDER BY GENTILICIO_NAC ASC;";
            cmd.Connection = Form5.cn;
            NpgsqlDataReader nac = cmd.ExecuteReader();
            while (nac.Read()) {
                om_nac_comboBox.Items.Add(nac[0]); 
            } 
            nac.Close();

            // Llenado de combobox de destinos a trasladar
            //NpgsqlCommand cmd_tras = new NpgsqlCommand();
            cmd.CommandText = "select nombre from entrada_salida.destino order by nombre ASC;";
            //cmd_tras.Connection = Form5.cn;
            NpgsqlDataReader tras = cmd.ExecuteReader();
            while (tras.Read())
            {
                om_traslado_comboBox.Items.Add(tras[0]);
            }
            tras.Close();

            // Llenado de combobox de estado de solicitud
            est_solicitudComboBox.Items.Add("APROBADA");
            est_solicitudComboBox.Items.Add("NEGADA");

            // Llenado de combobox de nombres de conductores
            //NpgsqlCommand cmd_conduct = new NpgsqlCommand();
            cmd.CommandText = "select nombre from entrada_salida.conductor order by nombre asc;";
            //cmd_conduct.Connection = Form5.cn;
            NpgsqlDataReader conduct = cmd.ExecuteReader();
            while (conduct.Read())
            {
                est_conductComboBox.Items.Add(conduct[0]);
            }
            conduct.Close();

            // Llenado de combobox de número de vehiculos
            //NpgsqlCommand cmd_numVehi = new NpgsqlCommand();
            cmd.CommandText = "select num from entrada_salida.vehiculo;";
            //cmd_numVehi.Connection = Form5.cn;
            NpgsqlDataReader numVehi = cmd.ExecuteReader();
            while (numVehi.Read())
            {
                est_vehiNumComboBox.Items.Add(numVehi[0]);
            }
            numVehi.Close();

            // Cargar combobox de cie y generar autocompletador
            om_cie_comboBox.AutoCompleteCustomSource = CargarCIE();
            om_cie_comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            om_cie_comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private AutoCompleteStringCollection CargarCIE()
        {
            // Cargar combobox de Cie10 y generar autocompletador
            AutoCompleteStringCollection datacie10 = new AutoCompleteStringCollection();

            NpgsqlCommand cmd_cie10 = new NpgsqlCommand();
            cmd_cie10.CommandText = "SELECT distinct code from entrada_salida.cie10 where code not like '%-%' order by code asc;";
            cmd_cie10.Connection = Form5.cn;
            NpgsqlDataReader cie10 = cmd_cie10.ExecuteReader();
            while (cie10.Read())
            {
                datacie10.Add(cie10[0].ToString());
                om_cie_comboBox.Items.Add(cie10[0].ToString());
            }
            cie10.Close();
            return datacie10;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void est_vehiNumComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cargar placa de vehiculo en textbox cuando se selecciona un número
            string stringSQL = "select placa::text from entrada_salida.vehiculo where num = " + est_vehiNumComboBox.Text;
            NpgsqlCommand cmd_placaVehi = new NpgsqlCommand();
            cmd_placaVehi.CommandText = stringSQL;
            cmd_placaVehi.Connection = Form5.cn;
            NpgsqlDataReader placaVehi = cmd_placaVehi.ExecuteReader();
            placaVehi.Read();            
            est_placaTextBox.Text = placaVehi[0].ToString();
            placaVehi.Close();

            // Cargar clase de vehiculo en textbox cuando se selecciona un número
            NpgsqlCommand cmd_clasVehi = new NpgsqlCommand();
            cmd_clasVehi.CommandText = "select upper(clase::text) from entrada_salida.vehiculo where num = " + est_vehiNumComboBox.Text;
            cmd_clasVehi.Connection = Form5.cn;
            NpgsqlDataReader clasVehi = cmd_clasVehi.ExecuteReader();
            clasVehi.Read();            
            est_vehiClassTextBox.Text = clasVehi[0].ToString();         
            clasVehi.Close();

            // Cargar codigo institucional de vehiculo en textbox cuando se selecciona un número
            NpgsqlCommand cmd_codVehi = new NpgsqlCommand();
            cmd_codVehi.CommandText = "select cod_inst from entrada_salida.vehiculo where num = " + est_vehiNumComboBox.Text;
            cmd_codVehi.Connection = Form5.cn;
            NpgsqlDataReader codVehi = cmd_codVehi.ExecuteReader();
            codVehi.Read();
            est_codVehiTextBox.Text = codVehi[0].ToString();
            codVehi.Close();
        }

        

        private void est_vehiClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void om_num_tbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            { e.Handled = true; }
        }
        private void om_solicitantetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
            { e.Handled = true; return;  }
        }
        private void om_unidad_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
            { e.Handled = true; return;  }
        }
        private void om_paciente_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
            { e.Handled = true; return;  }
        }
        private void om_edad_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            { e.Handled = true; }
        }
        private void om_ci_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            { e.Handled = true; }
        }
        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
            { e.Handled = true; return; }
        }
        private void est_obsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
            { e.Handled = true; return; }
        }

        private void est_solicitudComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // habilitar controles requeridos solo si la solicitud es aprobada o caso contrario dejarlos vacíos
            if (est_solicitudComboBox.Text == "APROBADA")
            {
                est_vehiNumComboBox.Enabled = true;
                est_conductComboBox.Enabled = true;
                est_ParamTextBox.Enabled = true;
                est_obsTextBox.Enabled = true;
                est_gruardarBtn.Enabled = true;
            }
            else
            {
                est_vehiNumComboBox.Enabled = false;
                est_vehiNumComboBox.Text = "";
                est_codVehiTextBox.Text = "";
                est_conductComboBox.Enabled = false;
                est_conductComboBox.Text = "";
                est_cedConductorTextBox.Text = "";
                est_ParamTextBox.Enabled = false;
                est_ParamTextBox.Text = "";
                est_obsTextBox.Enabled = false;
                est_obsTextBox.Text = "";
                est_vehiClassTextBox.Text = "";
                est_placaTextBox.Text = "";
                est_gruardarBtn.Enabled = true;
            }
        }

        private void om_nac_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void om_traslado_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void est_solicitudComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void est_vehiNumComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void est_gruardarBtn_Click(object sender, EventArgs e)
        {
            String str1;
            String str2;
            str1 = "insert into entrada_salida.orden_mov values ( ";
            str2 = "insert into entrada_salida.info_solicitud values ( ";

            if (om_num_tbox.Text == "" || om_solicitantetextBox.Text == "" || om_unidad_textBox.Text == "" || om_motivo_textBox.Text == "" )
            {
                MessageBox.Show("El número de orden, el solicitante, la unidad administrativa o el motivo no pueden estar vacíos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                str1 = str1 +       om_num_tbox.Text + ", ";
                str1 = str1 + "'" + om_datePicker.Text + "'" + ", ";
                str1 = str1 + "'" + om_solicitantetextBox.Text + "'" + ", ";
                str1 = str1 + "'" + om_unidad_textBox.Text + "'" + ", ";
                str1 = str1 + "'" + om_motivo_textBox.Text + "'" + ", ";

                if (om_ci_textBox.Text.Length == 10 &&
                   (om_paciente_textBox.Text == "" || om_edad_textBox.Text == "" || om_servicioTextBox.Text == "" ||
                    om_cie_comboBox.Text == "" || om_codNacTextBox.Text == "" || om_codDestinoTextBox.Text == "" || 
                    om_diag_textBox.Text == ""))
                {
                    MessageBox.Show("Los datos del paciente trasladado no pueden quedar vacíos si se ingresa su número de cédula. Compruebe que el cie10 es válido.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (om_ci_textBox.Text.Length < 10 || om_ci_textBox.Text == "")
                    {
                        str1 = str1 + "null" + ", ";
                        str1 = str1 + "null" + ", ";
                        str1 = str1 + "null" + ", ";
                        str1 = str1 + "null" + ", ";
                        str1 = str1 + "null" + ", ";
                        str1 = str1 + "null" + ", ";
                        str1 = str1 + "null" + ", ";
                    }
                    else
                    {
                        str1 = str1 + "'" + om_paciente_textBox.Text + "'" + ", ";
                        str1 = str1 + om_edad_textBox.Text + ", ";
                        str1 = str1 + "'" + om_codNacTextBox.Text + "'" + ", ";
                        str1 = str1 + "'" + om_ci_textBox.Text + "'" + ", ";
                        str1 = str1 + "'" + om_cie_comboBox.Text + "'" + ", ";
                        str1 = str1 + "'" + om_servicioTextBox.Text + "'" + ", ";
                        str1 = str1 + om_codDestinoTextBox.Text + ", ";
                    }
                    str1 = str1 + "'" + om_horaSalidaBase.Text + "'" + ", ";
                    str1 = str1 + "'" + om_horaSalidaDestino.Text + "'" + ", ";
                    str1 = str1 + "'" + om_fechaSalida.Text + "'" + ", ";
                    str1 = str1 + "'" + om_horaLlegadaDestino.Text + "'" + ", ";
                    str1 = str1 + "'" + om_horaLlegadaBase.Text + "'" + ", ";
                    str1 = str1 + "'" + om_fechaEntrada.Text + "'" + ");";

                    if (est_solicitudComboBox.Text == "NEGADA")
                    {
                        str2 = str2 + om_num_tbox.Text + ", ";
                        str2 = str2 + "'" + est_solicitudComboBox.Text + "'" + ", ";
                        str2 = str2 + "null" + ", ";
                        str2 = str2 + "null" + ", ";
                        str2 = str2 + "null" + ", ";
                        if (est_obsTextBox.Text == "")
                        {
                            str2 = str2 + "null" + ", ";
                        }
                        else
                        {
                            str2 = str2 + "'" + est_obsTextBox.Text + "'" + ", ";
                        }
                        str2 = str2 + "'" + est_datePicker.Text + "'" + ");";
                        try
                        {
                            NpgsqlCommand cmd1 = new NpgsqlCommand();
                            cmd1.CommandText = str1;
                            cmd1.Connection = Form5.cn;
                            cmd1.ExecuteNonQuery();

                            NpgsqlCommand cmd2 = new NpgsqlCommand();
                            cmd2.CommandText = str2;
                            cmd2.Connection = Form5.cn;
                            cmd2.ExecuteNonQuery();

                            MessageBox.Show("Registro Insertado.",
                                "Registro insertado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ya existe una Orden de Movilización guardada con el número que se intenta insertar. Comprobar que el número ingresado es el correcto.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (est_vehiNumComboBox.Text == "" || est_conductComboBox.Text == "")
                        {
                            MessageBox.Show("Por favor seleccione el número de vehículo asignado y/o a su conductor.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            str2 = str2 + om_num_tbox.Text + ", ";
                            str2 = str2 + "'" + est_solicitudComboBox.Text + "'" + ", ";
                            str2 = str2 + est_codVehiTextBox.Text + ", ";
                            str2 = str2 + "'" + est_cedConductorTextBox.Text + "'" + ", ";

                            if (est_ParamTextBox.Text == ""){str2 = str2 + "null" + ", ";}
                            else {str2 = str2 + "'" + est_ParamTextBox.Text + "'" + ", ";}
                            if (est_obsTextBox.Text == "") {str2 = str2 + "null" + ", ";}
                            else{str2 = str2 + "'" + est_obsTextBox.Text + "'" + ", ";}

                            str2 = str2 + "'" + est_datePicker.Text + "'" + ");";

                            try
                            {
                                NpgsqlCommand cmd1 = new NpgsqlCommand();
                                cmd1.CommandText = str1;
                                cmd1.Connection = Form5.cn;
                                cmd1.ExecuteNonQuery();
                                NpgsqlCommand cmd2 = new NpgsqlCommand();
                                cmd2.CommandText = str2;
                                cmd2.Connection = Form5.cn;
                                cmd2.ExecuteNonQuery();
                                MessageBox.Show("Registro Insertado.",
                                "Registro insertado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ya existe una Orden de Movilización guardada con el número que se intenta insertar. Comprobar que el número ingresado es el correcto.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    
                }
            }
        }

        private void om_cie_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'a' && e.KeyChar <= 'z')
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void om_cie_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand cmd_diag = new NpgsqlCommand();
            cmd_diag.CommandText = "select description from entrada_salida.cie10 where code = " + "'" + om_cie_comboBox.Text + "'";
            cmd_diag.Connection = Form5.cn;
            NpgsqlDataReader diag = cmd_diag.ExecuteReader();
            diag.Read();

            om_diag_textBox.Text = diag[0].ToString();

            diag.Close();
        }

        private void om_traslado_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand cmd_ccodtras = new NpgsqlCommand();
            cmd_ccodtras.CommandText = "select cod_des from entrada_salida.destino where nombre = " + "'" + om_traslado_comboBox.Text + "';";
            cmd_ccodtras.Connection = Form5.cn;
            NpgsqlDataReader ccodtras = cmd_ccodtras.ExecuteReader();
            ccodtras.Read();
            om_codDestinoTextBox.Text = ccodtras[0].ToString();
            ccodtras.Close();
        }

        private void est_conductComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand cmd_cedCondu = new NpgsqlCommand();
            cmd_cedCondu.CommandText = "select cedula from entrada_salida.conductor where nombre = " + "'" + est_conductComboBox.Text + "';";
            cmd_cedCondu.Connection = Form5.cn;
            NpgsqlDataReader cedCondu = cmd_cedCondu.ExecuteReader();
            cedCondu.Read();
            est_cedConductorTextBox.Text = cedCondu[0].ToString();
            cedCondu.Close();
        }

        private void om_nac_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand cmd_codNac = new NpgsqlCommand();
            cmd_codNac.CommandText = "select ISO_NAC from entrada_salida.nacionalidad where GENTILICIO_NAC = " + "'" + om_nac_comboBox.Text + "';";
            cmd_codNac.Connection = Form5.cn;
            NpgsqlDataReader codNac = cmd_codNac.ExecuteReader();
            codNac.Read();
            om_codNacTextBox.Text = codNac[0].ToString();
            codNac.Close();
        }
        private void om_ci_textBox_TextChanged(object sender, EventArgs e)
        {
            if (om_ci_textBox.Text.Length == 10)
            {
                om_paciente_textBox.Enabled = true;
                om_edad_textBox.Enabled = true;
                om_nac_comboBox.Enabled = true;
                om_servicioTextBox.Enabled = true;
                om_cie_comboBox.Enabled = true;
                om_traslado_comboBox.Enabled = true;
            }
            else
            {
                om_paciente_textBox.Enabled = false;
                om_paciente_textBox.Text = "";
                om_edad_textBox.Enabled = false;
                om_edad_textBox.Text = "";
                om_nac_comboBox.Enabled = false;
                om_nac_comboBox.Text = "";
                om_codNacTextBox.Text = "";
                om_servicioTextBox.Enabled = false;
                om_servicioTextBox.Text = "";
                om_cie_comboBox.Enabled = false;
                om_cie_comboBox.Text = "";
                om_traslado_comboBox.Enabled = false;
                om_traslado_comboBox.Text = "";
                om_codDestinoTextBox.Text = "";
            }
        }
    }
}
