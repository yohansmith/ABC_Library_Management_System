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

namespace ABC_Library_Management_System
{
    public partial class Uc_P_Member_Details : UserControl
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dtbl;
        DataSet DataSet;
        SqlConnection con = new SqlConnection("Data Source=HD3KAHSODS-74\\SQLEXPRESS01;Initial Catalog=Library_Management_Database;Integrated Security=True");

        public Uc_P_Member_Details()
        {
            InitializeComponent();
        }

        private void Uc_P_Member_Details_Load(object sender, EventArgs e)
        {
            try
            {
                sda = new SqlDataAdapter(@"SELECT Member_ID, Name,  Username, Password, User_Type FROM Members WHERE User_Type = 'Lecturer' OR User_Type = 'Student' ORDER BY Member_ID ASC", con);
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                sda = new SqlDataAdapter(@"SELECT Member_ID, Name,  Username, Password, User_Type FROM Members WHERE User_Type = 'Lecturer' OR User_Type = 'Student' ORDER BY Member_ID ASC", con);
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                comboBox1.Text = "";
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Members (Member_ID, Name, Username, Password, User_Type) VALUES (@Member_ID, @Name, @Username, @Password, @User_Type)", con);
                    cmd.Parameters.AddWithValue("@Member_ID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Username", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox5.Text);
                    cmd.Parameters.AddWithValue("@User_type", comboBox1.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Members WHERE Username = @Username", con);
                command.Parameters.AddWithValue("@Username", textBox4.Text);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                comboBox1.Text = "";
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
                string Member_ID = textBox1.Text;
                string Name = textBox2.Text;
                string Username = textBox4.Text;
                string Password = textBox5.Text;
                string User_Type = comboBox1.Text;

                SqlCommand command = new SqlCommand("UPDATE Members SET Member_ID =@Member_ID, Name = @Name, Password = @Password, User_Type = @User_Type WHERE Username = @Username", con);
                command.Parameters.AddWithValue("@Member_ID", Member_ID);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@User_Type", User_Type);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();

                sda = new SqlDataAdapter(@"SELECT Member_ID, Name,  Username, Password, User_Type FROM Members WHERE User_Type = 'Lecturer' OR User_Type = 'Student' ORDER BY Member_ID ASC", con);
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                comboBox1.Text = "";

                MessageBox.Show("Updated successfully.");
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataView members = dtbl.DefaultView;
            members.RowFilter = "Username like'%" + textBox6.Text + "%'";
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex] as DataGridViewRow;
                if (selectedRow != null)
                {
                    textBox1.Text = selectedRow.Cells["Member_ID"].Value.ToString();
                    textBox2.Text = selectedRow.Cells["Name"].Value.ToString();
                    textBox4.Text = selectedRow.Cells["Username"].Value.ToString();
                    textBox5.Text = selectedRow.Cells["Password"].Value.ToString();
                    comboBox1.Text = selectedRow.Cells["User_Type"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
