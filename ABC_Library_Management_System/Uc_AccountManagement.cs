using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ABC_Library_Management_System
{
    public partial class Uc_AccountManagement : UserControl
    {
        SqlDataAdapter sda;
        string imgLocation = "";
        SqlCommandBuilder scb;
        DataTable dtbl;
        DataSet DataSet;
        SqlConnection con = new SqlConnection("Data Source=HD3KAHSODS-74\\SQLEXPRESS01;Initial Catalog=Library_Management_Database;Integrated Security=True");

        public Uc_AccountManagement()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Uc_AccountManagement_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select Member_ID, Name, Password, Username, User_Type from Members where Username = @Username", con);
                con.Open();
                cmd.Parameters.AddWithValue("Username", Program.ActiveUser);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    textBox1.Text = da.GetValue(0).ToString();
                    textBox2.Text = da.GetValue(1).ToString();
                    textBox3.Text = da.GetValue(3).ToString();
                    textBox4.Text = da.GetValue(2).ToString();
                    textBox5.Text = da.GetValue(4).ToString();
                }
                con.Close();

                textBox1.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox5.ReadOnly = true;
                //
                sda = new SqlDataAdapter("Select Member_ID, Book_ID, Status, Reserved_Date, Issue_Date, Return_Date, Fine from BookTR WHERE Member_ID like '%" + textBox1.Text + "%'", con);
                con.Open();
                dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT ISNULL(SUM(Fine), 0) from BookTR WHERE Member_ID like '%" + textBox1.Text + "%'", con);
                var count1 = command.ExecuteScalar();
                label9.Text = count1.ToString();
                con.Close();
                //
                SqlCommand comand = new SqlCommand("Select Profile_Picture from Members where Username = @Username", con);
                comand.Parameters.AddWithValue("Username", Program.ActiveUser);
                SqlDataAdapter dai = new SqlDataAdapter(comand);
                DataSet dsi = new DataSet();
                dai.Fill(dsi, "ProfilePicture");
                int c = dsi.Tables["ProfilePicture"].Rows.Count;

                if (c > 0)
                {
                    if (dsi.Tables["ProfilePicture"].Rows[c - 1].IsNull(0) == false)
                    {
                        //BLOB is read into Byte array, then used to construct MemoryStream,
                        //then passed to PictureBox.
                        Byte[] byteBLOBData = new Byte[0];
                        byteBLOBData = (Byte[])(dsi.Tables["ProfilePicture"].Rows[c - 1]["Profile_Picture"]);
                        MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                        pictureBox1.Image = Image.FromStream(stmBLOBData);
                    }
                    else
                    {
                        pictureBox1.Image = ABC_Library_Management_System.Properties.Resources.Default_PP;
                    }
                }
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
                byte[] images = null;
                FileStream Stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(Stream);
                images = brs.ReadBytes((int)Stream.Length);


                string Member_ID = textBox1.Text;
                string Name = textBox2.Text;
                string Username = textBox3.Text;
                string Password = textBox4.Text;
                string User_Type = textBox5.Text;

                SqlCommand command = new SqlCommand("UPDATE Members SET Member_ID =@Member_ID, Name = @Name, Password = @Password, User_Type = @User_Type, Profile_Picture = @images WHERE Username = @Username", con);
                command.Parameters.AddWithValue("@Member_ID", Member_ID);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@User_Type", User_Type);
                command.Parameters.AddWithValue("@images", images);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Profile updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                button2_Click(sender, e);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                button2_Click(sender, e);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Uc_AccountManagement_MouseMove(object sender, MouseEventArgs e)
        {
            sda = new SqlDataAdapter("Select Member_ID, Book_ID, Status, Reserved_Date, Issue_Date, Return_Date, Fine from BookTR WHERE Member_ID like '%" + textBox1.Text + "%'", con);
            con.Open();
            dtbl = new DataTable();
            sda.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imgLocation = dialog.FileName.ToString();
                    pictureBox1.ImageLocation = imgLocation;
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
