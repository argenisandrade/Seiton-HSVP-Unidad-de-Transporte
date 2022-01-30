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
            str = "SELECT num FROM entrada_salida.info_solicitud I natural join ";
            str = str + "entrada_salida.autorizacion A WHERE I.estado = 'APROBADA'";

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
                str1 = str1 + "entrada_salida.autorizacion A on I.num = A.num inner join ";
                str1 = str1 + "entrada_salida.vehiculo V on I.vehiculo = V.cod_inst inner join ";
                str1 = str1 + "entrada_salida.conductor C on I.conductor = C.cedula ";
                str1 = str1 + "where O.num = " + writeNumTBox.Text;


                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = str1;
                cmd.Connection = Form5.cn;
                NpgsqlDataReader load_info = cmd.ExecuteReader();

                resultsDataGrid.Rows.Clear();
                //while (load_info.Read())
                //{
                //    resultsDataGrid.Rows.Add(load_info[0], load_info[1], load_info[2], load_info[3], load_info[4]);
                //}
                //load_info.Close();
                load_info.Read();
                try
                {
                    resultsDataGrid.Rows.Add(load_info[0], load_info[1], load_info[2], load_info[3], load_info[4]);
                    load_info.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("No hay registros con el número ingresado. Revisar que haya una orden de movilización aprobada con dicho número y que tenga su correspondiente autorización de salida.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    load_info.Close();
                }
                load_info.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese al menos un número para generar la búsqueda",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            resultsDataGrid.ClearSelection();

        }

        private void selectVehiCbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void selectConducCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Cargar cédula del conductor en textbox cuando se selecciona un nombre
                NpgsqlCommand cmd_cedCondu = new NpgsqlCommand();
                cmd_cedCondu.CommandText = "select cedula from entrada_salida.conductor where nombre = " + "'" + selectConducCBox.Text + "';";
                cmd_cedCondu.Connection = Form5.cn;
                NpgsqlDataReader cedCondu = cmd_cedCondu.ExecuteReader();
                cedCondu.Read();
                cedConductorTBox.Text = cedCondu[0].ToString();
                cedCondu.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buscarVehiBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string str1;

                str1 = "select O.num, O.fecha, O.solicitante, C.nombre as conductor, V.num ";
                str1 = str1 + "from entrada_salida.orden_mov O natural join ";
                str1 = str1 + "entrada_salida.info_solicitud I inner join ";
                str1 = str1 + "entrada_salida.autorizacion A on I.num = A.num inner join ";
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
            resultsDataGrid.ClearSelection();

        }

        private void buscarFechaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string str1;

                str1 = "select O.num, O.fecha, O.solicitante, C.nombre as conductor, V.num ";
                str1 = str1 + "from entrada_salida.orden_mov O natural join ";
                str1 = str1 + "entrada_salida.info_solicitud I inner join ";
                str1 = str1 + "entrada_salida.autorizacion A on I.num = A.num inner join ";
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
            resultsDataGrid.ClearSelection();
        }

        private void buscarConducBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string str1;

                str1 = "select O.num, O.fecha, O.solicitante, C.nombre as conductor, V.num ";
                str1 = str1 + "from entrada_salida.orden_mov O natural join ";
                str1 = str1 + "entrada_salida.info_solicitud I inner join ";
                str1 = str1 + "entrada_salida.autorizacion A on I.num = A.num inner join ";
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
            resultsDataGrid.ClearSelection();
        }

        private void resultsDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void generarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (resultsDataGrid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No hay datos disponibles o seleccionados para generar informes.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (DataGridViewRow row in resultsDataGrid.SelectedRows)
                    {
                        string value1 = row.Cells[0].Value.ToString();
                        string value2 = row.Cells[1].Value.ToString();
                        //...

                        string str1 = "select * from entrada_salida.orden_mov where num = " + row.Cells[0].Value.ToString();
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.CommandText = str1;
                        cmd.Connection = Form5.cn;
                        NpgsqlDataReader check_info = cmd.ExecuteReader();
                        check_info.Read();

                        if (check_info[11].ToString() == "")
                        {
                            check_info.Close();
                            im_actividades.Items.Clear();
                            string str2 = "select * from entrada_salida.orden_mov O natural join ";
                            str2 = str2 + "entrada_salida.info_solicitud I inner join ";
                            str2 = str2 + "entrada_salida.autorizacion A on I.num = A.num inner join ";
                            str2 = str2 + "entrada_salida.vehiculo V on I.vehiculo = V.cod_inst inner join ";
                            str2 = str2 + "entrada_salida.conductor C on I.conductor = C.cedula ";
                            str2 = str2 + "where O.num = " + row.Cells[0].Value.ToString() + " and I.estado = 'APROBADA'";
                            //MessageBox.Show("No destino",
                            //"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            NpgsqlCommand cmd2 = new NpgsqlCommand();
                            cmd2.CommandText = str2;
                            cmd2.Connection = Form5.cn;
                            NpgsqlDataReader load_info1 = cmd2.ExecuteReader();
                            while (load_info1.Read())
                            {
                                // Datos Hoja de Ruta
                                hr_lugar.Text = "IBARRA";
                                hr_num.Text = load_info1[0].ToString();
                                hr_datePicker.Text = load_info1[1].ToString();
                                hr_solicitante.Text = load_info1[2].ToString();
                                hr_unidad.Text = load_info1[3].ToString();
                                hr_motivo.Text = load_info1[4].ToString();
                                hr_conductor.Text = load_info1[48].ToString();
                                hr_vehiculo.Text = load_info1[40].ToString();
                                hr_numVehi.Text = load_info1[36].ToString();
                                hr_marcaVehi.Text = load_info1[38].ToString();
                                hr_placaVehi.Text = load_info1[39].ToString();
                                hr_observaciones.Text = load_info1[22].ToString();
                                hr_lugarSalida1.Text = "IBARRA";
                                hr_fechaSalida1.Text = load_info1[14].ToString();
                                hr_horaSalida1.Text = load_info1[12].ToString();
                                hr_kmSalida1.Text = load_info1[27].ToString();
                                hr_lugarLlegada1.Text = ""; //Ciudad destino
                                hr_fechaLlegada1.Text = load_info1[17].ToString();
                                hr_horaLlegada1.Text = load_info1[15].ToString();
                                //hr_kmLlegada1.Text = "";
                                hr_lugarSalida2.Text = ""; //Ciudad destino
                                hr_fechaSalida2.Text = load_info1[14].ToString();
                                hr_horaSalida2.Text = load_info1[13].ToString();
                                //hr_kmSalida2.Text = "";
                                hr_lugarLlegada2.Text = "IBARRA";
                                hr_fechaLlegada2.Text = load_info1[17].ToString();
                                hr_horaLlegada2.Text = load_info1[16].ToString();
                                hr_kmLlegada2.Text = load_info1[28].ToString();

                                // Datos Informe de Movilización
                                im_conductor.Text = load_info1[48].ToString();
                                im_numuero.Text = load_info1[0].ToString();
                                im_solicitante.Text = load_info1[2].ToString();
                                im_paramedico.Text = load_info1[21].ToString();
                                im_ciudad.Text = "IBARRA";
                                im_actividades.Items.Add(load_info1[4].ToString());
                                im_actividades.Items.Add(load_info1[10].ToString());
                                im_fechaSalida.Text = load_info1[14].ToString();
                                im_horaSalida.Text = load_info1[12].ToString();
                                im_fechaLlegada.Text = load_info1[17].ToString();
                                im_horaLlegada.Text = load_info1[16].ToString();
                                im_fechaLlegadaMed.Text = load_info1[17].ToString();
                                im_horaLlegadaMed.Text = load_info1[15].ToString();
                                im_fechaSalidaMed.Text = load_info1[14].ToString();
                                im_horaSalidaMed.Text = load_info1[13].ToString();
                                im_observaciones.Text = load_info1[22].ToString();
                                im_kmSalida.Text = load_info1[27].ToString();
                                im_kmLlegada.Text = load_info1[28].ToString();
                            }

                            load_info1.Close();
                            MessageBox.Show("Click en las pestañas correspondientes para ver resultados.",
                                "Informes generados", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            check_info.Close();
                            im_actividades.Items.Clear();
                            string str3 = "select * from entrada_salida.orden_mov O natural join ";
                            str3 = str3 + "entrada_salida.info_solicitud I inner join ";
                            str3 = str3 + "entrada_salida.autorizacion A on I.num = A.num inner join ";
                            str3 = str3 + "entrada_salida.vehiculo V on I.vehiculo = V.cod_inst inner join ";
                            str3 = str3 + "entrada_salida.conductor C on I.conductor = C.cedula inner join ";
                            str3 = str3 + "entrada_salida.destino D on O.destino = D.cod_des ";
                            str3 = str3 + "where O.num = " + row.Cells[0].Value.ToString() + " and I.estado = 'APROBADA'";
                            //MessageBox.Show("Sí destino",
                            //"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            NpgsqlCommand cmd3 = new NpgsqlCommand();
                            cmd3.CommandText = str3;
                            cmd3.Connection = Form5.cn;
                            NpgsqlDataReader load_info2 = cmd3.ExecuteReader();

                            while (load_info2.Read())
                            {
                                // Datos Hoja de Ruta
                                hr_lugar.Text = "IBARRA";
                                hr_num.Text = load_info2[0].ToString();
                                hr_datePicker.Text = load_info2[1].ToString();
                                hr_solicitante.Text = load_info2[2].ToString();
                                hr_unidad.Text = load_info2[3].ToString();
                                hr_motivo.Text = load_info2[4].ToString();
                                hr_conductor.Text = load_info2[48].ToString();
                                hr_vehiculo.Text = load_info2[40].ToString();
                                hr_numVehi.Text = load_info2[36].ToString();
                                hr_marcaVehi.Text = load_info2[38].ToString();
                                hr_placaVehi.Text = load_info2[39].ToString();
                                hr_observaciones.Text = load_info2[22].ToString();
                                hr_lugarSalida1.Text = "IBARRA";
                                hr_fechaSalida1.Text = load_info2[14].ToString();
                                hr_horaSalida1.Text = load_info2[12].ToString();
                                hr_kmSalida1.Text = load_info2[27].ToString();
                                hr_lugarLlegada1.Text = load_info2[56].ToString();
                                hr_fechaLlegada1.Text = load_info2[17].ToString();
                                hr_horaLlegada1.Text = load_info2[15].ToString();
                                //hr_kmLlegada1.Text = "";
                                hr_lugarSalida2.Text = load_info2[56].ToString();
                                hr_fechaSalida2.Text = load_info2[14].ToString();
                                hr_horaSalida2.Text = load_info2[13].ToString();
                                //hr_kmSalida2.Text = "";
                                hr_lugarLlegada2.Text = "IBARRA";
                                hr_fechaLlegada2.Text = load_info2[17].ToString();
                                hr_horaLlegada2.Text = load_info2[16].ToString();
                                hr_kmLlegada2.Text = load_info2[28].ToString();

                                // Datos Informe de Movilización
                                im_conductor.Text = load_info2[48].ToString();
                                im_numuero.Text = load_info2[0].ToString();
                                im_solicitante.Text = load_info2[2].ToString();
                                im_paramedico.Text = load_info2[21].ToString();
                                im_ciudad.Text = load_info2[56].ToString();
                                im_actividades.Items.Add(load_info2[4].ToString());
                                im_actividades.Items.Add(load_info2[10].ToString());
                                im_fechaSalida.Text = load_info2[14].ToString();
                                im_horaSalida.Text = load_info2[12].ToString();
                                im_fechaLlegada.Text = load_info2[17].ToString();
                                im_horaLlegada.Text = load_info2[16].ToString();
                                im_fechaLlegadaMed.Text = load_info2[17].ToString();
                                im_horaLlegadaMed.Text = load_info2[15].ToString();
                                im_fechaSalidaMed.Text = load_info2[14].ToString();
                                im_horaSalidaMed.Text = load_info2[13].ToString();
                                im_observaciones.Text = load_info2[22].ToString();
                                im_kmSalida.Text = load_info2[27].ToString();
                                im_kmLlegada.Text = load_info2[28].ToString();
                            }

                            load_info2.Close();
                            MessageBox.Show("Click en las pestañas correspondientes para ver resultados.",
                                "Informes generados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        check_info.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
