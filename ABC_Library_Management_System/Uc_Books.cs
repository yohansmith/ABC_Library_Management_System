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
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ABC_Library_Management_System
{
    public partial class Uc_Books : UserControl
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dtbl;
        DataSet DataSet;
        SqlConnection con = new SqlConnection("Data Source=HD3KAHSODS-74\\SQLEXPRESS01;Initial Catalog=Library_Management_Database;Integrated Security=True");

        public Uc_Books()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Uc_Books_Load(object sender, EventArgs e)
        {
            try
            {
                sda = new SqlDataAdapter(@"SELECT Book_ID, Book_Name,  Author, Category, No_Copies FROM Books ORDER BY Book_ID ASC", con);
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                textBox6.Enabled = true;
                sda = new SqlDataAdapter(@"SELECT Book_ID, Book_Name,  Author, Category, No_Copies FROM Books ORDER BY Book_ID ASC", con);
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox6.Text = "";
                textBox3.Text = "";
                comboBox2.Text = "";
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox3.Text != "" && comboBox2.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Books (Book_ID, Book_Name, Author, Category, No_Copies) VALUES (@Book_ID, @Book_Name, @Author, @Category, @No_Copies)", con);
                    cmd.Parameters.AddWithValue("@Book_ID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Book_Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Author", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Category", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@No_Copies", textBox3.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Inserted");
                }
                else
                {
                    MessageBox.Show("All fields are required and must be filled");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("Select Book_ID, Book_Name, Author, Category, No_Copies from Books where Book_Name = @Book_Name", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("Book_Name", textBox6.Text);
                    SqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        textBox1.Text = da.GetValue(0).ToString();
                        textBox2.Text = da.GetValue(1).ToString();
                        textBox4.Text = da.GetValue(2).ToString();
                        textBox3.Text = da.GetValue(4).ToString();
                        comboBox2.Text = da.GetValue(3).ToString();
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Field cannot be empty.");
                }
                if (string.IsNullOrEmpty(textBox4.Text))
                {
                    // Show a message box.
                    MessageBox.Show("Record not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Books WHERE Book_Name = @Book_Name", con);
                command.Parameters.AddWithValue("@Book_Name", textBox2.Text);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox6.Text = "";
                textBox3.Text = "";
                comboBox2.Text = "";
                MessageBox.Show("Row deleted successfully.");
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string Book_ID = textBox1.Text;
                string Book_Name = textBox2.Text;
                string Author = textBox4.Text;
                string Category = comboBox2.Text;
                string Language = textBox3.Text;

                SqlCommand command = new SqlCommand("UPDATE Books SET Book_ID =@Book_ID, Author = @Author, Category = @Category, No_Copies = @No_Copies WHERE Book_Name = @Book_Name", con);
                command.Parameters.AddWithValue("@Book_ID", Book_ID);
                command.Parameters.AddWithValue("@Book_Name", Book_Name);
                command.Parameters.AddWithValue("@Author", Author);
                command.Parameters.AddWithValue("@Category", Category);
                command.Parameters.AddWithValue("@No_Copies", Language);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();

                sda = new SqlDataAdapter(@"SELECT Book_ID, Book_Name,  Author, Category, Np_Copies FROM Books ORDER BY Book_ID ASC", con);
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox6.Text = "";
                textBox3.Text = "";
                comboBox2.Text = "";

                MessageBox.Show("Updated successfully.");
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                button1_Click(sender, e);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text.Length > 0)
                {
                    DataView dvStudents = dtbl.DefaultView;
                    dvStudents.RowFilter = "Book_Name like '%" + textBox6.Text + "%'";
                }
                else
                {
                    DataView dvStudents = dtbl.DefaultView;
                    dvStudents.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                textBox6.Enabled = false;
                sda = new SqlDataAdapter(@"SELECT Book_ID, Book_Name, Author, Category, No_Copies, Available FROM v_Book_Details", con);
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex] as DataGridViewRow;
                if (selectedRow != null)
                {
                    textBox1.Text = selectedRow.Cells["Book_ID"].Value.ToString();
                    textBox2.Text = selectedRow.Cells["Book_Name"].Value.ToString();
                    textBox4.Text = selectedRow.Cells["Author"].Value.ToString();
                    comboBox2.Text = selectedRow.Cells["Category"].Value.ToString();
                    textBox3.Text = selectedRow.Cells["No_Copies"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                textBox6.Enabled = false;
                sda = new SqlDataAdapter(@"SELECT Member_ID, Book_ID, Status, Reserved_Date, Issue_Date, Return_Date ,Recieved_Date, Fine FROM BookTR ORDER BY Book_ID ASC", con);
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
