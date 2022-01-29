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
    public partial class Form4 : MaterialSkin.Controls.MaterialForm
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Llenado de combobox de forma de busqueda
            burcarPorCBox.Items.Add("NÚMERO DE ORDEN");
            burcarPorCBox.Items.Add("VEHÍCULO");
            burcarPorCBox.Items.Add("FECHA");
            burcarPorCBox.Items.Add("CONDUCTOR");
            //burcarPorCBox.Items.Add("DESTINO");

            writeNumTBox.AutoCompleteCustomSource = CargarCods();
            writeNumTBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            writeNumTBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

            resultsDataGrid.ColumnCount = 5;
            resultsDataGrid.Columns[0].HeaderText = "Número de Orden";
            resultsDataGrid.Columns[1].HeaderText = "Fecha";
            resultsDataGrid.Columns[2].HeaderText = "Solicitante";
            resultsDataGrid.Columns[3].HeaderText = "Conductor";
            resultsDataGrid.Columns[4].HeaderText = "Número de Vehículo";
            resultsDataGrid.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd";

            // Llenado de combobox de número de vehiculos
            NpgsqlCommand cmd_numVehi = new NpgsqlCommand();
            cmd_numVehi.CommandText = "select num from entrada_salida.vehiculo;";
            cmd_numVehi.Connection = Form5.cn;
            NpgsqlDataReader numVehi = cmd_numVehi.ExecuteReader();
            while (numVehi.Read())
            {
                selectVehiCbox.Items.Add(numVehi[0]);
            }
            numVehi.Close();

            // Llenado de combobox de nombres de conductores
            NpgsqlCommand cmd_conduct = new NpgsqlCommand();
            cmd_conduct.CommandText = "select nombre from entrada_salida.conductor order by nombre asc;";
            cmd_conduct.Connection = Form5.cn;
            NpgsqlDataReader conduct = cmd_conduct.ExecuteReader();
            while (conduct.Read())
            {
                selectConducCBox.Items.Add(conduct[0]);
            }
            conduct.Close();
        }

        private AutoCompleteStringCollection CargarCods()
        {
            AutoCompleteStringCollection codOrdenMov = new AutoCompleteStringCollection();

            // query para asegurar que no haya una autorizacion ya insertada con ese codigo 
            String str;
            str = "SELECT num FROM entrada_salida.info_solicitud I ";
            str = str + " WHERE estado = 'APROBADA'";

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

        private void burcarPorCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (burcarPorCBox.SelectedIndex == 0)
            {
                writeNumTBox.Enabled = true;
                writeNumTBox.Visible = true;
                numlabel.Visible = true;
                buscarNumBtn.Enabled = true;
                buscarNumBtn.Visible = true;
            }
            else
            {
                writeNumTBox.Enabled = false;
                writeNumTBox.Visible = false;
                numlabel.Visible = false;
                buscarNumBtn.Enabled = false;
                buscarNumBtn.Visible = false;
            }
            if (burcarPorCBox.SelectedIndex == 1)
            {
                selectVehiCbox.Enabled = true;
                selectVehiCbox.Visible = true;
                vehilabel.Visible = true;
                buscarVehiBtn.Enabled = true;
                buscarVehiBtn.Visible = true;

            }
            else
            {
                selectVehiCbox.Enabled = false;
                selectVehiCbox.Visible = false;
                vehilabel.Visible = false;
                buscarVehiBtn.Enabled = false;
                buscarVehiBtn.Visible = false;

            }
            if (burcarPorCBox.SelectedIndex == 2)
            {
                pickFecha.Enabled = true;
                pickFecha.Visible = true;
                fechalabel.Visible = true;
                buscarFechaBtn.Enabled = true;
                buscarFechaBtn.Visible = true;

            }
            else
            {
                pickFecha.Enabled = false;
                pickFecha.Visible = false;
                fechalabel.Visible = false;
                buscarFechaBtn.Enabled = false;
                buscarFechaBtn.Visible = false;
            }
            if (burcarPorCBox.SelectedIndex == 3)
            {
                selectConducCBox.Enabled = true;
                selectConducCBox.Visible = true;
                conductlabel.Visible = true;
                buscarConducBtn.Enabled = true;
                buscarConducBtn.Visible = true;

            }
            else
            {
                selectConducCBox.Enabled = false;
                selectConducCBox.Visible = false;
                conductlabel.Visible = false;
                buscarConducBtn.Enabled = false;
                buscarConducBtn.Visible = false;
            }
            //if (burcarPorCBox.SelectedIndex == 4)
            //{
            //    selectDestCBox.Enabled = true;
            //    selectDestCBox.Visible = true;
            //    destlabel.Visible = true;
            //    buscarDestBtn.Enabled = true;
            //    buscarDestBtn.Visible = true;

            //}
            //else
            //{
            //    selectDestCBox.Enabled = false;
            //    selectDestCBox.Visible = false;
            //    destlabel.Visible = false;
            //    buscarDestBtn.Enabled = false;
            //    buscarDestBtn.Visible = false;
            //}
        }

        private void writeNumTBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void buscarNumBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string str1;

                str1 = "select O.num, O.fecha, O.solicitante, C.nombre as conductor, V.num ";
                str1 = str1 + "from entrada_salida.orden_mov O natural join ";
                str1 = str1 + "entrada_salida.info_solicitud I inner join ";
                str1 = str1 + "entrada_salida.vehiculo V on I.vehiculo = V.cod_inst inner join ";
                str1 = str1 + "entrada_salida.conductor C on I.conductor = C.cedula ";
                str1 = str1 + "where O.num = " + writeNumTBox.Text;

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = str1;
                cmd.Connection = Form5.cn;
                NpgsqlDataReader load_info = cmd.ExecuteReader();

                resultsDataGrid.Rows.Clear();
                while (load_info.Read())
                {
                    resultsDataGrid.Rows.Add(load_info[0], load_info[1], load_info[2], load_info[3], load_info[4]);
                }
                load_info.Close();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese al menos un número para generar la búsqueda",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        private void selectVehiCbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cargar codigo institucional de vehiculo en textbox cuando se selecciona un número
            NpgsqlCommand cmd_codVehi = new NpgsqlCommand();
            cmd_codVehi.CommandText = "select cod_inst from entrada_salida.vehiculo where num = " + selectVehiCbox.Text;
            cmd_codVehi.Connection = Form5.cn;
            NpgsqlDataReader codVehi = cmd_codVehi.ExecuteReader();
            codVehi.Read();
            codVehiTBox.Text = codVehi[0].ToString();
            codVehi.Close();
        }

        private void selectConducCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand cmd_cedCondu = new NpgsqlCommand();
            cmd_cedCondu.CommandText = "select cedula from entrada_salida.conductor where nombre = " + "'" + selectConducCBox.Text + "';";
            cmd_cedCondu.Connection = Form5.cn;
            NpgsqlDataReader cedCondu = cmd_cedCondu.ExecuteReader();
            cedCondu.Read();
            cedConductorTBox.Text = cedCondu[0].ToString();
            cedCondu.Close();
        }

        private void buscarVehiBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string str1;

                str1 = "select O.num, O.fecha, O.solicitante, C.nombre as conductor, V.num ";
                str1 = str1 + "from entrada_salida.orden_mov O natural join ";
                str1 = str1 + "entrada_salida.info_solicitud I inner join ";
                str1 = str1 + "entrada_salida.vehiculo V on I.vehiculo = V.cod_inst inner join ";
                str1 = str1 + "entrada_salida.conductor C on I.conductor = C.cedula ";
                str1 = str1 + "where I.vehiculo = " + codVehiTBox.Text;

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = str1;
                cmd.Connection = Form5.cn;
                NpgsqlDataReader load_info = cmd.ExecuteReader();

                resultsDataGrid.Rows.Clear();
                while (load_info.Read())
                {
                    resultsDataGrid.Rows.Add(load_info[0], load_info[1], load_info[2], load_info[3], load_info[4]);
                }
                load_info.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione un vehículo.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void buscarFechaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string str1;

                str1 = "select O.num, O.fecha, O.solicitante, C.nombre as conductor, V.num ";
                str1 = str1 + "from entrada_salida.orden_mov O natural join ";
                str1 = str1 + "entrada_salida.info_solicitud I inner join ";
                str1 = str1 + "entrada_salida.vehiculo V on I.vehiculo = V.cod_inst inner join ";
                str1 = str1 + "entrada_salida.conductor C on I.conductor = C.cedula ";
                str1 = str1 + "where O.fecha = " + "'" + pickFecha.Text +"'";

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = str1;
                cmd.Connection = Form5.cn;
                NpgsqlDataReader load_info = cmd.ExecuteReader();

                resultsDataGrid.Rows.Clear();
                while (load_info.Read())
                {
                    resultsDataGrid.Rows.Add(load_info[0], load_info[1], load_info[2], load_info[3], load_info[4]);
                }
                load_info.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese una fecha válida.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buscarConducBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string str1;

                str1 = "select O.num, O.fecha, O.solicitante, C.nombre as conductor, V.num ";
                str1 = str1 + "from entrada_salida.orden_mov O natural join ";
                str1 = str1 + "entrada_salida.info_solicitud I inner join ";
                str1 = str1 + "entrada_salida.vehiculo V on I.vehiculo = V.cod_inst inner join ";
                str1 = str1 + "entrada_salida.conductor C on I.conductor = C.cedula ";
                str1 = str1 + "where I.conductor = " + "'" + cedConductorTBox.Text + "'";

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = str1;
                cmd.Connection = Form5.cn;
                NpgsqlDataReader load_info = cmd.ExecuteReader();

                resultsDataGrid.Rows.Clear();
                while (load_info.Read())
                {
                    resultsDataGrid.Rows.Add(load_info[0], load_info[1], load_info[2], load_info[3], load_info[4]);
                }
                load_info.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione un conductor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
