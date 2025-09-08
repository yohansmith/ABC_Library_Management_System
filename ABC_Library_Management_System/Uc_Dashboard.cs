using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ABC_Library_Management_System
{
    public partial class Uc_Dashboard : UserControl
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dtbl;
        DataSet DataSet;
        SqlConnection con = new SqlConnection("Data Source=HD3KAHSODS-74\\SQLEXPRESS01;Initial Catalog=Library_Management_Database;Integrated Security=True");

        public Uc_Dashboard()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Uc_Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Members", con);
                var count1 = cmd.ExecuteScalar();
                label1.Text = count1.ToString();
                con.Close();
                con.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Books", con);
                var count2 = command.ExecuteScalar();
                label3.Text = count2.ToString();
                con.Close();
                con.Open();
                SqlCommand comman = new SqlCommand("SELECT SUM(No_Copies) FROM Books", con);
                var count3 = comman.ExecuteScalar();
                label6.Text = count3.ToString();
                con.Close();

                sda = new SqlDataAdapter("SELECT Book_ID, Book_Name, Author, Category, NO_Copies FROM Books ORDER BY Book_ID DESC", con);
                con.Open();
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
                con.Close();

                sda = new SqlDataAdapter(@"SELECT TOP 5 Book_ID, Book_Name FROM v_Trending_Books ORDER BY Book_Count DESC", con);
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView2.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
