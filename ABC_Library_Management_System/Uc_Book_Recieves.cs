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
    public partial class Uc_Book_Recieves : UserControl
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dtbl;
        DataSet DataSet;
        SqlConnection con = new SqlConnection("Data Source=HD3KAHSODS-74\\SQLEXPRESS01;Initial Catalog=Library_Management_Database;Integrated Security=True");

        public Uc_Book_Recieves()
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
                    textBox1.Text = selectedRow.Cells["Member_ID"].Value.ToString();
                    textBox2.Text = selectedRow.Cells["Book_ID"].Value.ToString();
                    dateTimePicker1.Text = selectedRow.Cells["Return_Date"].Value.ToString();

                    DateTime ReturnDate = dateTimePicker1.Value;
                    DateTime RecieveDate = dateTimePicker2.Value;
                    int dayDifference = (int)(RecieveDate - ReturnDate).Days;
                    int multipliedDifference = dayDifference * 10;
                    if (multipliedDifference > 0)
                    {
                        textBox5.Text = multipliedDifference.ToString();
                    }
                    else
                    {
                        textBox5.Text = ("0");
                    }
                }    
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
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1);

                    string Member_ID = textBox1.Text;
                    string Book_ID = textBox2.Text;
                    string Recieved_Date = dateTimePicker2.Text;
                    string Status = textBox4.Text;
                    string Fine = textBox5.Text;

                    SqlCommand command = new SqlCommand("UPDATE BookTR SET Status = @Status, Recieved_Date = @Recieved_Date, Fine = @Fine WHERE Member_ID = @Member_ID AND Book_ID = @Book_ID", con);
                    command.Parameters.AddWithValue("@Member_ID", Member_ID);
                    command.Parameters.AddWithValue("@Book_ID", Book_ID);
                    command.Parameters.AddWithValue("@Recieved_Date", Recieved_Date);
                    command.Parameters.AddWithValue("@Status", Status);
                    command.Parameters.AddWithValue("@Fine", Fine);

                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Updated successfully.");

                    sda = new SqlDataAdapter(@"SELECT Member_ID, Book_ID, Status, Reserved_Date, Issue_Date, Return_Date ,Recieved_Date, Fine FROM BookTR WHERE Status = 'Issued'", con);
                    dtbl = new DataTable();
                    sda.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox5.Text = "";
                    DateTime today = DateTime.Now;
                    dateTimePicker1.Value = today;
                    dateTimePicker2.Value = today;
                }
                else
                {
                    MessageBox.Show("Please enter Book ID & Member ID");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Uc_Book_Recieves_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime today = DateTime.Now;
                dateTimePicker2.Value = today;
                dateTimePicker1.Enabled = false;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                sda = new SqlDataAdapter(@"SELECT Member_ID, Book_ID, Status, Reserved_Date, Issue_Date, Return_Date ,Recieved_Date, Fine FROM BookTR WHERE Status = 'Issued'", con);
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

       private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DateTime ReturnDate = dateTimePicker1.Value;
            DateTime RecieveDate = dateTimePicker2.Value;
            int dayDifference = (int)(RecieveDate - ReturnDate).Days;
            int multipliedDifference = dayDifference * 10;
            if (multipliedDifference > 0)
            {
                textBox5.Text = multipliedDifference.ToString();
            }
            else
            {
                textBox5.Text = ("0");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text.Length > 0)
                {
                    DataView dvStudents = dtbl.DefaultView;
                    dvStudents.RowFilter = "Member_ID = " + textBox3.Text + "";
                }
                else
                {
                    DataView dvStudents = dtbl.DefaultView;
                    dvStudents.RowFilter = "";
                }
            }
            catch 
            {
                MessageBox.Show("Please enter valid Member ID");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void Uc_Book_Recieves_MouseMove(object sender, MouseEventArgs e)
        {
            sda = new SqlDataAdapter(@"SELECT Member_ID, Book_ID, Status, Reserved_Date, Issue_Date, Return_Date ,Recieved_Date, Fine FROM BookTR WHERE Status = 'Issued'", con);
            dtbl = new DataTable();
            sda.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
