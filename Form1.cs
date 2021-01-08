using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_UAS
{
    public partial class Form1 : Form
    {
        Timer time = new Timer();

        public Form1()
        {
            InitializeComponent();
            progressBar1.Hide();
            panel1.Left = 134;
            
        }
        int ples = 1;
        void move(object sender, EventArgs e)
        {
            panel1.Left +=1;

            if (panel1.Left > 134)
            {
               
                ples -= 1;
            }
            if (panel1.Left < 125)
            {
                ples = 1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            time.Tick += new EventHandler(move);
            time.Interval = 1;
            time.Start();


            this.BackColor = Color.Aquamarine;
            timer1.Enabled = true;
                   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(8);
            if (progressBar1.Value == 100)
            {
                Form2 p = new Form2();
                p.Show();
                this.Hide();
                timer1.Enabled = false;


            }

        }
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
