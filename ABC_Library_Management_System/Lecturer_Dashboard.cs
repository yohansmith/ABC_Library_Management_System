using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ABC_Library_Management_System
{
    public partial class Lecturer_Dashboard : Form
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
        public Lecturer_Dashboard()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uc_Dashboard1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            uc_STBooks1.BringToFront();
        }

        private void uc_AccountManagement1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            uc_AccountManagement1.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            MessageBox.Show("You have successfully been logged out");
            this.Close();
            Form1 lgf = new Form1();
            lgf.ShowDialog();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.BackColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uc_L_Member_Details1.BringToFront();
        }

        private void uc_Dashboard1_Load(object sender, EventArgs e)
        {

        }
    }
}
