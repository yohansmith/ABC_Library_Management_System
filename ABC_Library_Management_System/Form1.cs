using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace ABC_Library_Management_System
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
             int nLeftRect,
             int nTopRect,
             int nRightRect,
             int nBottomRect,
             int nWidthEllipse,
             int nHeightEllipse
             );
        public Form1()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private Dashboard dashF = null;
        public Form1(Dashboard SourceFrom)
        {
            dashF = SourceFrom as Dashboard;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text =="") 
            {
                MessageBox.Show("Enter the username");
            }
            else if(textBox2.Text == "")
            {
                MessageBox.Show("Enter the Password");
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=HD3KAHSODS-74\\SQLEXPRESS01;Initial Catalog=Library_Management_Database;Integrated Security=True");
                    //SqlConnection con = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=" + Environment.CurrentDirectory + "\\DATA\\LMDB.mdf");
                    SqlCommand cmd = new SqlCommand("select * from Members where Username = @Username and Password = @Password", conn);
                    cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                    SqlDataAdapter da = new SqlDataAdapter( cmd );
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        //MessageBox.Show("Login Successfull");
                        Program.ActiveUser = dt.Rows[0]["Username"].ToString();
                        Program.UserType = dt.Rows[0]["User_Type"].ToString();
                        this.Hide();
                        switch (Program.UserType)
                        {
                            case "Librarian":
                                Dashboard lbdb = new Dashboard();
                                lbdb.Show();
                                break;

                            case "Staff":
                                Dashboard sdb = new Dashboard();
                                sdb.Show();
                                break;

                            case "Student":
                                StudentDashboard stdb = new StudentDashboard();
                                stdb.Show();
                                break;

                            case "Professor":
                                ProfessorDashboard pdb = new ProfessorDashboard();
                                pdb.Show();
                                break;

                            case "Lecturer":
                                Lecturer_Dashboard ldb = new Lecturer_Dashboard();
                                ldb.Show();
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username or Password incorrect");
                    }


                }
                catch(Exception ex) 
                {
                    MessageBox.Show("" + ex);
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) 
            {
                e.Handled = true;
                textBox2.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textBox1.Focus();
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textBox1.Focus();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
                
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                button1_Click(sender, e);
            }
           
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                  button1_Click(sender, e);
            }
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            label4.BackColor = Color.Red;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.BackColor= Color.White;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
