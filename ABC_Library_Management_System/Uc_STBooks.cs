using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ABC_Library_Management_System
{
    public partial class Uc_STBooks : UserControl
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dtbl;
        DataSet DataSet;
        SqlConnection con = new SqlConnection("Data Source=HD3KAHSODS-74\\SQLEXPRESS01;Initial Catalog=Library_Management_Database;Integrated Security=True");

        public Uc_STBooks()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex] as DataGridViewRow;
                if (selectedRow != null)
                {
                    if (int.Parse(selectedRow.Cells["Available"].Value.ToString()) > 0)
                    {
                        textBox2.Text = selectedRow.Cells["Book_ID"].Value.ToString();
                    }
                    else
                    {
                        MessageBox.Show("This book is not available at the moment");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Uc_STBooks_Load(object sender, EventArgs e)
        {
            try
            {
                sda = new SqlDataAdapter(@"SELECT Book_ID, Book_Name,  Author, Category, No_Copies, Available FROM v_Book_Details", con);
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;

                dateTimePicker1.Enabled = false;
                textBox1.ReadOnly = true;
                textBox4.ReadOnly = true;

                SqlCommand cmd = new SqlCommand("Select Member_ID, Name, Password, Username, User_Type from Members where Username = @Username", con);
                con.Open();
                cmd.Parameters.AddWithValue("Username", Program.ActiveUser);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    textBox1.Text = da.GetValue(0).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataView stbooks = dtbl.DefaultView;
            stbooks.RowFilter = "Book_Name like'%" + textBox6.Text + "%'";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataView dvStudents = dtbl.DefaultView;
            dvStudents.RowFilter = "Category like'%" + comboBox2.Text + "%'";
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            DataView dvStudents = dtbl.DefaultView;
            dvStudents.RowFilter = "Category like'%" + comboBox2.Text + "%'";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != "")
                {
                    con.Open();
                    SqlCommand Command = new SqlCommand("SELECT ISNULL(COUNT(t.Book_ID), 0) AS Book_Count FROM dbo.BookTR AS t WHERE(t.Status = 'Reserved' OR t.Status = 'Issued') AND t.Member_ID = '" + textBox1.Text + "'", con);
                    //con.Open();
                    SqlDataReader dr = Command.ExecuteReader();
                    
                    if (dr.Read()) 
                    {
                        if (int.Parse (dr["Book_Count"].ToString() ) >= 2) 
                        {
                            MessageBox.Show("Only two books can be reserved / borrow at a time.");
                        }
                        else
                        {
                            if (dr.IsClosed == false) { dr.Close(); }
                            SqlCommand cmd = new SqlCommand("INSERT INTO BookTR (Member_ID, Book_ID, Reserved_Date, Status) VALUES (@Member_ID, @Book_ID, @Reserved_Date, @Status)", con);
                            cmd.Parameters.AddWithValue("@Member_ID", textBox1.Text);
                            cmd.Parameters.AddWithValue("@Book_ID", textBox2.Text);
                            cmd.Parameters.AddWithValue("@Reserved_Date", dateTimePicker1.Text);
                            cmd.Parameters.AddWithValue("@Status", textBox4.Text);
                            //con.Open();
                            cmd.ExecuteNonQuery();
                            //con.Close();
                            MessageBox.Show("Reserved Successfully");
                        }
                    }
                    if (dr.IsClosed == false) { dr.Close(); }
                    Command.Dispose();
                    con.Close() ;
                }
                else
                {
                    MessageBox.Show("Please select a book and retry.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            sda = new SqlDataAdapter(@"SELECT Book_ID, Book_Name,  Author, Category, No_Copies, Available FROM v_Book_Details", con);
            dtbl = new DataTable();
            sda.Fill(dtbl);
            dataGridView1.DataSource = dtbl;

        }
    }
}
