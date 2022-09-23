﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGrid_Cursos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopulateData();
        }
        int Id = 0 ;
        SqlConnection con = new SqlConnection("Data Source=A;Initial Catalog=importarExcelORS;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'importarExcelORSDataSet.Capacitaciones_2022' table. You can move, or remove it, as needed.
            this.capacitaciones_2022TableAdapter.Fill(this.importarExcelORSDataSet.Capacitaciones_2022);

        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            MeExit();
        }

        private void MeExit()
        {
            DialogResult iExit;

            iExit = MessageBox.Show("¿Seguro que desea salir?", "Guardar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(iExit == DialogResult.Yes) 
            {
                Application.Exit();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeExit();
        }

        private void button_Add_New_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox_Area.Text) && !String.IsNullOrWhiteSpace(textBox_area_tematica.Text) && !String.IsNullOrWhiteSpace(textBox_JobCode.Text) && !String.IsNullOrWhiteSpace(textBox_Supervisor.Text))
            {
                
                SqlCommand cmd = new SqlCommand("insert into [dbo].[Capacitaciones_2022]([ZONER NAME], [AREA],[SUPERVISOR],[TRAINING NAME],[AREA TEMATICA] ,[JOBCODE]) values (@Zoner_name,@Area,@Supervisor,@Training_Name,@Area_Tematica,@JobCode) ", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Zoner_name", textBox_ZonerName.Text);
                cmd.Parameters.AddWithValue("@Area", textBox_Area.Text);
                cmd.Parameters.AddWithValue("@Supervisor", textBox_Supervisor.Text);
                cmd.Parameters.AddWithValue("@Training_Name", textBox_training_name.Text);
                cmd.Parameters.AddWithValue("@Area_Tematica", textBox_area_tematica.Text);
                cmd.Parameters.AddWithValue("@JobCode", textBox_JobCode.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Datos insertados de manera exitosa");
                PopulateData();
                clearData();
                
                
            }
            else
            {
                MessageBox.Show("Introduzca los valores de indicados");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView2.Rows.Add(textBox_Area.Text, textBox_area_tematica.Text, textBox_JobCode.Text, textBox_Supervisor.Text, textBox_training_name.Text, textBox_ZonerName.Text);
        }

        private void PopulateData()
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select [ID], [ZONER NAME], [AREA],[AREA TEMATICA] from [dbo].[Capacitaciones_2022]", con);
            adapt.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if(Id!=0)
            {
                SqlCommand cmd = new SqlCommand("delete [dbo].[Capacitaciones_2022] where ID =@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Campo borrado exitosamente");
                PopulateData();
                clearData();
                
            }
            else
            {
                MessageBox.Show("Elija un campo a eliminar");
            }
        }

       

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            
                dataGridView2.CurrentRow.Selected = true;          
            
                      
        }

        public void clearData()
        {
            textBox_ZonerName.Text ="";
            textBox_Area.Text = "";
            textBox_Supervisor.Text = "";
            textBox_training_name.Text = "";
            textBox_area_tematica.Text = "";
            textBox_JobCode.Text = "";
            Id = 0;
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString());

            /*
            textBox_ZonerName.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox_Area.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox_Supervisor.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox_training_name.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox_area_tematica.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox_JobCode.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();*/
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            PopulateData();
        }
    }
}
