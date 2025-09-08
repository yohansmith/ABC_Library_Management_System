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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ABC_Library_Management_System
{
    public partial class Uc_Book_Issues : UserControl
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dtbl;
        DataSet DataSet;
        SqlConnection con = new SqlConnection("Data Source=HD3KAHSODS-74\\SQLEXPRESS01;Initial Catalog=Library_Management_Database;Integrated Security=True");

        public Uc_Book_Issues()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Uc_Book_Issues_Load(object sender, EventArgs e)
        {
            try
            {
                textBox4.ReadOnly = true;
                dateTimePicker2.Enabled = false;
                DateTime today = DateTime.Now;
                dateTimePicker1.Value = today;
                dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1);
                sda = new SqlDataAdapter(@"SELECT Member_ID, Book_ID, Status, Reserved_Date, Issue_Date, Return_Date FROM BookTR WHERE Status = 'Reserved'", con);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1);

                    string Member_ID = textBox1.Text;
                    string Book_ID = textBox2.Text;
                    string Issue_Date = dateTimePicker1.Text;
                    string Return_Date = dateTimePicker2.Text;
                    string Status = textBox4.Text;

                    SqlCommand command = new SqlCommand("UPDATE BookTR SET Status = @Status, Issue_Date = @Issue_Date, Return_Date = @Return_Date WHERE Member_ID = @Member_ID AND Book_Id = @Book_ID", con);
                    command.Parameters.AddWithValue("@Member_ID", Member_ID);
                    command.Parameters.AddWithValue("@Book_ID", Book_ID);
                    command.Parameters.AddWithValue("@Issue_Date", Issue_Date);
                    command.Parameters.AddWithValue("@Return_Date", Return_Date);
                    command.Parameters.AddWithValue("@Status", Status);

                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();

                    sda = new SqlDataAdapter(@"SELECT Member_ID, Book_ID, Status, Reserved_Date, Issue_Date, Return_Date FROM BookTR WHERE Status = 'Reserved'", con);
                    dtbl = new DataTable();
                    sda.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;

                    textBox1.Text = "";
                    textBox2.Text = "";
                    dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1);
                    DateTime today = DateTime.Now;
                    dateTimePicker1.Value = today;

                    MessageBox.Show("Updated successfully.");
                }
                else
                {
                    MessageBox.Show("Please enter Book ID & Member ID");
                }
            }
            catch(Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
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
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex] as DataGridViewRow;
                if (selectedRow != null)
                {
                    textBox1.Text = selectedRow.Cells["Member_ID"].Value.ToString();
                    textBox2.Text = selectedRow.Cells["Book_ID"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != 8)
            {
              e.Handled = true;
            }
        }

        private void Uc_Book_Issues_MouseMove(object sender, MouseEventArgs e)
        {
            sda = new SqlDataAdapter(@"SELECT Member_ID, Book_ID, Status, Reserved_Date, Issue_Date, Return_Date FROM BookTR WHERE Status = 'Reserved'", con);
            dtbl = new DataTable();
            sda.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
        }
    }
}
